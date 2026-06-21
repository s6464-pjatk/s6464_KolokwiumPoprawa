namespace s6464_KolokwiumPoprawa.Dto;

public class VisitResponseDto
{
    public DateTime Date { get; set; }
    public required VisitClientDto Client { get; set; }
    public required VisitMechanicDto Mechanic { get; set; }
    public required IReadOnlyCollection<VisitServiceResponseDto> VisitServices { get; set; }
}

public class VisitClientDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class VisitMechanicDto
{
    public int MechanicId { get; set; }
    public required string LicenceNumber { get; set; }
}

public class VisitServiceResponseDto
{
    public required string Name { get; set; }
    public decimal ServiceFee { get; set; }
}
