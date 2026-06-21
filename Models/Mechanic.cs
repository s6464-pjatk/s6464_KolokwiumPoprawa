namespace s6464_KolokwiumPoprawa.Models;

public class Mechanic
{
    public int MechanicId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string LicenceNumber { get; set; }

    public ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
