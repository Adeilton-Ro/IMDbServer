namespace IMDb.Domain.Entities.Abstract;

public abstract class User : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Hash { get; set; }
    public string Salt { get; set; }
    public bool isActive { get; set; }
}
