namespace IMDb.Domain.Entities.Abstract;
public abstract class Cast : Entity
{
    public string Name { get; set; } = string.Empty;
    public string UrlImage { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
}