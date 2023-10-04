namespace DbEntity;

public interface IHeading
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? PageLink { get; set; }
    public string? ImageRef { get; set; }
}