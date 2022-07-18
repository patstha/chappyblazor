using DataAccess.Model;

namespace ChappyBlazor.Data
{
    public class DataService
    {
        private readonly DapperDataAccess _dataAccess;
        public DataService(DapperDataAccess access)
        {
            _dataAccess = access;
        }
        public async Task<List<MyPerson>> GetPersons()
        {
            IEnumerable<MyPerson> persons = await _dataAccess.GetPersons();
            return persons.ToList();
        }
        public async Task<List<MyProgram>> GetPrograms()
        {
            IEnumerable<MyProgram> programs = await _dataAccess.GetPrograms();
            return (List<MyProgram>)programs.ToList();
        }

        public async Task<List<MyProgram>> SearchProgramByTitle(string query)
        {
            IEnumerable<MyProgram> programs = await _dataAccess.SearchProgramsByTitle(query);
            return (List<MyProgram>)programs.ToList();
        }
    }
}