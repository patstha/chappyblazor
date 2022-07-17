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
        IEnumerable<MyPerson> persons = await connection.QueryAsync<MyPerson>(@"select * from myperson");
        _logger.LogDebug("{methodName} returned {result}", nameof(GetPersons), JsonConvert.SerializeObject(persons));
        return persons;
    }

    public async Task<IEnumerable<MyProgram>> GetPrograms()
    {
        using NpgsqlConnection connection = new(_connectionString);
        IEnumerable<MyProgram> programs = await connection.QueryAsync<MyProgram>(@"select * from myprogram");
        _logger.LogDebug("{methodName} returned {result}", nameof(GetPrograms), JsonConvert.SerializeObject(programs));
        return programs;
    }
}