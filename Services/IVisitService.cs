using s6464_KolokwiumPoprawa.Dto;

namespace s6464_KolokwiumPoprawa.Services;

public interface IVisitService
{
    Task<VisitResponseDto?> GetVisitAsync(int visitId, CancellationToken cancellationToken);
    Task<CreateVisitResult> CreateVisitAsync(CreateVisitDto request, CancellationToken cancellationToken);
}

public record CreateVisitResult(int? VisitId, string? ErrorMessage)
{
    public bool IsSuccess => VisitId.HasValue;

    public static CreateVisitResult Success(int visitId) => new(visitId, null);
    public static CreateVisitResult Failure(string errorMessage) => new(null, errorMessage);
}
