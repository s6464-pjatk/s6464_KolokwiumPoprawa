namespace s6464_KolokwiumPoprawa.Models;

public class Visit
{
    public int VisitId { get; set; }
    public int ClientId { get; set; }
    public int MechanicId { get; set; }
    public DateTime Date { get; set; }

    public Client Client { get; set; } = null!;
    public Mechanic Mechanic { get; set; } = null!;
    public ICollection<VisitService> VisitServices { get; set; } = new List<VisitService>();
}
