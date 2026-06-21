namespace s6464_KolokwiumPoprawa.Models;

public class Service
{
    public int ServiceId { get; set; }
    public required string Name { get; set; }
    public decimal BaseFee { get; set; }

    public ICollection<VisitService> VisitServices { get; set; } = new List<VisitService>();
}
