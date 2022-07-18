namespace DataAccessTests;
public class DataAccessTests
{
    IConfiguration Configuration { get; set; }

    public DataAccessTests()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<DataAccessTests>();

        Configuration = builder.Build();
    }

    [Fact]
    public async void GetPersons_ShouldReturn()
    {
        // Arrange
        string _connectionString = Configuration["ConnectionStrings:Default"];
        Mock<ILogger<DapperDataAccess>> mock = new();
        ILogger<DapperDataAccess> logger = mock.Object;
        DapperDataAccess access = new(_connectionString, logger);

        //Act
        IEnumerable<MyPerson> result = await access.GetPersons();
        List<MyPerson> expected = result.ToList<MyPerson>();

        // Assert
        expected.Should().NotBeNull();
        expected.Count.Should().Be(4); // we know this because we wrote the script in create database
        expected.Where(x => x.Id == 4).Count().Should().Be(1);
        expected.Where(x => x.Id == 4).First().ExternalId.Should().NotBeEmpty();
        expected.Where(x => x.Id == 4).First().Title.Should().Be("Mr");
        expected.Where(x => x.Id == 4).First().LegalName.Should().Be("Shawn Corey Carter");
        expected.Where(x => x.Id == 4).First().PreferredName.Should().Be("Jay Z");
        expected.Where(x => x.Id == 4).First().Alias.Should().Be("v-shawncarter");
        expected.Where(x => x.Id == 4).First().CreatedBy.Should().Be(1);
        expected.Where(x => x.Id == 4).First().CreatedDate.Should().BeAfter(DateTime.MinValue);
        expected.Where(x => x.Id == 4).First().CreatedDate.Should().BeBefore(DateTime.UtcNow);
        expected.Where(x => x.Id == 4).First().ModifiedBy.Should().Be(1);
        expected.Where(x => x.Id == 4).First().ModifiedDate.Should().BeAfter(DateTime.MinValue);
        expected.Where(x => x.Id == 4).First().ModifiedDate.Should().BeBefore(DateTime.UtcNow);
    }
    [Fact]
    public async void GetPrograms_ShouldReturn()
    {
        // Arrange
        string _connectionString = Configuration["ConnectionStrings:Default"];
        Mock<ILogger<DapperDataAccess>> mock = new();
        ILogger<DapperDataAccess> logger = mock.Object;
        DapperDataAccess access = new(_connectionString, logger);

        // Act
        IEnumerable<MyProgram> programs = await access.GetPrograms();
        List<MyProgram> expected = programs.ToList();

        // Assert
        expected.Should().NotBeNull();
        expected.Count.Should().Be(2); // we know this because we wrote the script in create database
        expected.Where(x => x.Id == 2).Count().Should().Be(1);
        expected.Where(x => x.Id == 2).First().ExternalId.Should().NotBeEmpty();
        expected.Where(x => x.Id == 2).First().Title.Should().Be("my second program");
        expected.Where(x => x.Id == 2).First().Summary.Should().Be("my summary");
        expected.Where(x => x.Id == 2).First().Description.Should().Be("my description");
        expected.Where(x => x.Id == 2).First().CreatedBy.Should().Be(3);
        expected.Where(x => x.Id == 2).First().CreatedDate.Should().BeAfter(DateTime.MinValue);
        expected.Where(x => x.Id == 2).First().CreatedDate.Should().BeBefore(DateTime.UtcNow);
        expected.Where(x => x.Id == 2).First().ModifiedBy.Should().Be(3);
        expected.Where(x => x.Id == 2).First().ModifiedDate.Should().BeAfter(DateTime.MinValue);
        expected.Where(x => x.Id == 2).First().ModifiedDate.Should().BeBefore(DateTime.UtcNow);
    }

    [Theory]
    [InlineData("my")]
    [InlineData("s")]
    [InlineData("se")]
    [InlineData("sec")]
    [InlineData("seco")]
    [InlineData("second")]
    [InlineData("nd")]
    [InlineData("pr")]
    [InlineData("g")]
    [InlineData("my second program")]
    public async void SearchPrograms_ShouldReturn(string query)
    {
        // Arrange
        string _connectionString = Configuration["ConnectionStrings:Default"];
        Mock<ILogger<DapperDataAccess>> mock = new();
        ILogger<DapperDataAccess> logger = mock.Object;
        DapperDataAccess access = new(_connectionString, logger);

        // Act
        IEnumerable<MyProgram> programs = await access.SearchProgramsByTitle(query);
        List<MyProgram> expected = programs.ToList();

        // Assert
        expected.Should().NotBeNull();
        expected.Where(x => x.Id == 2).Count().Should().Be(1);
        expected.Where(x => x.Id == 2).First().ExternalId.Should().NotBeEmpty();
        expected.Where(x => x.Id == 2).First().Title.Should().Be("my second program");
        expected.Where(x => x.Id == 2).First().Summary.Should().Be("my summary");
        expected.Where(x => x.Id == 2).First().Description.Should().Be("my description");
        expected.Where(x => x.Id == 2).First().CreatedBy.Should().Be(3);
        expected.Where(x => x.Id == 2).First().CreatedDate.Should().BeAfter(DateTime.MinValue);
        expected.Where(x => x.Id == 2).First().CreatedDate.Should().BeBefore(DateTime.UtcNow);
        expected.Where(x => x.Id == 2).First().ModifiedBy.Should().Be(3);
        expected.Where(x => x.Id == 2).First().ModifiedDate.Should().BeAfter(DateTime.MinValue);
        expected.Where(x => x.Id == 2).First().ModifiedDate.Should().BeBefore(DateTime.UtcNow);
    }

    [Theory]
    [InlineData("bernie")]
    [InlineData("abraham")]
    [InlineData("toyota")]
    [InlineData("mazda")]
    [InlineData("avocado")]
    [InlineData("toast")]
    [InlineData("banana")]
    [InlineData("pickle")]
    [InlineData("old mcdonalds had a farm")]
    [InlineData("my fifth program")]
    public async void SearchPrograms_ShouldNotReturn(string query)
    {
        // Arrange
        string _connectionString = Configuration["ConnectionStrings:Default"];
        Mock<ILogger<DapperDataAccess>> mock = new();
        ILogger<DapperDataAccess> logger = mock.Object;
        DapperDataAccess access = new(_connectionString, logger);

        // Act
        IEnumerable<MyProgram> programs = await access.SearchProgramsByTitle(query);
        List<MyProgram> expected = programs.ToList();

        // Assert
        expected.Count.Should().Be(0);
    }
}