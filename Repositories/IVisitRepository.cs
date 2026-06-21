using s6464_KolokwiumPoprawa.Dto;

namespace s6464_KolokwiumPoprawa.Repositories;

public interface IVisitRepository
{
    Task<VisitResponseDto?> GetVisitAsync(int visitId, CancellationToken cancellationToken);
    Task<bool> ClientExistsAsync(int clientId, CancellationToken cancellationToken);
    Task<int?> GetMechanicIdAsync(string licenceNumber, CancellationToken cancellationToken);
    Task<IReadOnlyDictionary<string, int>> GetServiceIdsAsync(
        IEnumerable<string> serviceNames,
        CancellationToken cancellationToken);
    Task<int> CreateVisitAsync(
        int clientId,
        int mechanicId,
        IReadOnlyCollection<(int ServiceId, decimal ServiceFee)> services,
        CancellationToken cancellationToken);
}
