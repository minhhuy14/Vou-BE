namespace AuthServer.Data.Entities;

public class ApiResourceScope
{
    public int Id { get; set; }
    public string Scope { get; set; }
    public int ApiResourceId { get; set; }
}