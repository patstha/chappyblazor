using System.Diagnostics;

namespace DataAccess;
public class DapperDataAccess: IDapperDataAccess
{
    //private readonly string _connectionString = "Host=hansken.db.elephantsql.com;Database=xrbmpoui;User Id=xrbmpoui;Password=i38x7v1O3aNteoNxteJNB5thtPfKqqxn;";
    private readonly string _connectionString;
    private readonly ILogger<DapperDataAccess> _logger;

    public DapperDataAccess(string connectionString, ILogger<DapperDataAccess> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public async Task<IEnumerable<MyPerson>> GetPersons()
    {
        using NpgsqlConnection connection = new(_connectionString);
        Stopwatch stopwatch = Stopwatch.StartNew();
        IEnumerable<MyPerson> persons = await connection.QueryAsync<MyPerson>(
            @"select
                person.*,
                createdby.alias as createdbyname, 
                modifiedby.alias as modifiedbyname
            from myperson person
            left join myperson createdby on createdby.id = person.createdby
            left join myperson modifiedby on modifiedby.id = person.modifiedby
            limit 200
            ;");
        stopwatch.Stop();
        foreach (MyPerson person in persons)
        {
            person.Stopwatch = stopwatch;
        }
        _logger.LogDebug("{methodName} returned {result}", nameof(GetPersons), JsonConvert.SerializeObject(persons));
        return persons;
    }

    public async Task<IEnumerable<MyProgram>> GetPrograms()
    {
        using NpgsqlConnection connection = new(_connectionString);
        Stopwatch stopwatch = Stopwatch.StartNew();
        IEnumerable<MyProgram> programs = await connection.QueryAsync<MyProgram>(
            @"select
                program.*, 
                owner.alias as ownername, 
                createdby.alias as createdbyname, 
                modifiedby.alias as modifiedbyname
            from myprogram program
            left join myperson owner on owner.id = program.owner
            left join myperson createdby on createdby.id = program.createdby
            left join myperson modifiedby on modifiedby.id = program.modifiedby
            limit 200
            ;");
        stopwatch.Stop();
        foreach (MyProgram program in programs)
        {
            program.Stopwatch = stopwatch;
        }
        _logger.LogDebug("{methodName} returned {result}", nameof(GetPrograms), JsonConvert.SerializeObject(programs));
        return programs;
    }

    public async Task<IEnumerable<MyProgram>> SearchProgramsByTitle(string query)
    {
        using NpgsqlConnection connection = new(_connectionString);
        Stopwatch stopwatch = Stopwatch.StartNew();
        IEnumerable<MyProgram> programs = await connection.QueryAsync<MyProgram>(
        @"select
            program.*, 
            owner.alias as ownername, 
            createdby.alias as createdbyname, 
            modifiedby.alias as modifiedbyname
        from myprogram program
        left join myperson owner on owner.id = program.owner
        left join myperson createdby on createdby.id = program.createdby
        left join myperson modifiedby on modifiedby.id = program.modifiedby
        where program.title ilike concat('%',@query,'%')
        limit 200
        ;", new { query = @query });
        stopwatch.Stop();
        foreach (MyProgram program in programs)
        {
            program.Stopwatch = stopwatch;
        }
        _logger.LogDebug("{methodName} returned {result}", nameof(GetPrograms), JsonConvert.SerializeObject(programs));
        return programs;
    }
}