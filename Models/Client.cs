namespace s6464_KolokwiumPoprawa.Models;

public class Client
{
    public int ClientId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
