using DataAccess.Model;

namespace ChappyBlazor.Data
{
    public class PersonService
    {
        private readonly DapperDataAccess _dataAccess;
        public PersonService(DapperDataAccess access)
        {
            _dataAccess = access;
        }
        public async Task<List<MyPerson>> GetPersons()
        {
            IEnumerable<MyPerson> persons = await _dataAccess.GetPersons();
            return persons.ToList<MyPerson>();
        }
    }
}