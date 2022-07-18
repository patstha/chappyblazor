namespace DataAccess.Model;
public class MyProgram: MyBaseClass
{
    public int ParentId { get; set; }
    public string? Title { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public int? Owner { get; set; }
    public string OwnerName { get; set; } = "";
}
