using System.ComponentModel.DataAnnotations;

namespace s6464_KolokwiumPoprawa.Dto;

public class CreateVisitDto
{
    [Range(1, int.MaxValue)]
    public int ClientId { get; set; }

    [Required, StringLength(14)]
    public string MechanicLicenceNumber { get; set; } = string.Empty;

    [Required, MinLength(1)]
    public List<CreateVisitServiceDto> Services { get; set; } = [];
}

public class CreateVisitServiceDto
{
    [Required, StringLength(100)]
    public string ServiceName { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "99999999.99")]
    public decimal ServiceFee { get; set; }
}
