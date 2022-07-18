namespace DataAccess
{
    public interface IDapperDataAccess
    {
        public Task<IEnumerable<MyPerson>> GetPersons();
        public Task<IEnumerable<MyProgram>> GetPrograms();
        public Task<IEnumerable<MyProgram>> SearchProgramsByTitle(string query);
    }
}