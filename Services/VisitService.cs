using s6464_KolokwiumPoprawa.Dto;
using s6464_KolokwiumPoprawa.Repositories;

namespace s6464_KolokwiumPoprawa.Services;

public class VisitService(IVisitRepository repository) : IVisitService
{
    public Task<VisitResponseDto?> GetVisitAsync(int visitId, CancellationToken cancellationToken)
    {
        return repository.GetVisitAsync(visitId, cancellationToken);
    }

    public async Task<CreateVisitResult> CreateVisitAsync(
        CreateVisitDto request,
        CancellationToken cancellationToken)
    {
        if (request.Services
            .GroupBy(service => service.ServiceName, StringComparer.Ordinal)
            .Any(group => group.Count() > 1))
        {
            return CreateVisitResult.Failure("Each service can be added to a visit only once.");
        }

        if (!await repository.ClientExistsAsync(request.ClientId, cancellationToken))
        {
            return CreateVisitResult.Failure($"Client with id {request.ClientId} does not exist.");
        }

        var mechanicId = await repository.GetMechanicIdAsync(
            request.MechanicLicenceNumber,
            cancellationToken);

        if (!mechanicId.HasValue)
        {
            return CreateVisitResult.Failure(
                $"Mechanic with licence number '{request.MechanicLicenceNumber}' does not exist.");
        }

        var serviceIds = await repository.GetServiceIdsAsync(
            request.Services.Select(service => service.ServiceName),
            cancellationToken);

        var missingServiceNames = request.Services
            .Select(service => service.ServiceName)
            .Where(serviceName => !serviceIds.ContainsKey(serviceName))
            .ToArray();

        if (missingServiceNames.Length > 0)
        {
            return CreateVisitResult.Failure(
                $"Services do not exist: {string.Join(", ", missingServiceNames)}.");
        }

        var services = request.Services
            .Select(service => (serviceIds[service.ServiceName], service.ServiceFee))
            .ToArray();

        var visitId = await repository.CreateVisitAsync(
            request.ClientId,
            mechanicId.Value,
            services,
            cancellationToken);

        return CreateVisitResult.Success(visitId);
    }
}
