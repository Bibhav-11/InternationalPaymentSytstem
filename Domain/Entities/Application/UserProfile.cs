namespace Domain.Entities.Application;

public class UserProfile
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public string? Address { get; set; } = null!;
    public int Country { get; set; }

    public string ApplicationUserId { get; set; } = null!;
}