using Microsoft.EntityFrameworkCore;
using s6464_KolokwiumPoprawa.Data;
using s6464_KolokwiumPoprawa.Dto;
using s6464_KolokwiumPoprawa.Models;

namespace s6464_KolokwiumPoprawa.Repositories;

public class VisitRepository(ApplicationDbContext context) : IVisitRepository
{
    public Task<VisitResponseDto?> GetVisitAsync(int visitId, CancellationToken cancellationToken)
    {
        return context.Visits
            .AsNoTracking()
            .Where(visit => visit.VisitId == visitId)
            .Select(visit => new VisitResponseDto
            {
                Date = visit.Date,
                Client = new VisitClientDto
                {
                    FirstName = visit.Client.FirstName,
                    LastName = visit.Client.LastName,
                    DateOfBirth = visit.Client.DateOfBirth
                },
                Mechanic = new VisitMechanicDto
                {
                    MechanicId = visit.Mechanic.MechanicId,
                    LicenceNumber = visit.Mechanic.LicenceNumber
                },
                VisitServices = visit.VisitServices
                    .OrderBy(visitService => visitService.ServiceId)
                    .Select(visitService => new VisitServiceResponseDto
                    {
                        Name = visitService.Service.Name,
                        ServiceFee = visitService.ServiceFee
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<bool> ClientExistsAsync(int clientId, CancellationToken cancellationToken)
    {
        return context.Clients.AnyAsync(client => client.ClientId == clientId, cancellationToken);
    }

    public Task<int?> GetMechanicIdAsync(string licenceNumber, CancellationToken cancellationToken)
    {
        return context.Mechanics
            .Where(mechanic => mechanic.LicenceNumber == licenceNumber)
            .Select(mechanic => (int?)mechanic.MechanicId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyDictionary<string, int>> GetServiceIdsAsync(
        IEnumerable<string> serviceNames,
        CancellationToken cancellationToken)
    {
        var names = serviceNames.Distinct(StringComparer.Ordinal).ToArray();

        return await context.Services
            .Where(service => names.Contains(service.Name))
            .ToDictionaryAsync(service => service.Name, service => service.ServiceId, cancellationToken);
    }

    public async Task<int> CreateVisitAsync(
        int clientId,
        int mechanicId,
        IReadOnlyCollection<(int ServiceId, decimal ServiceFee)> services,
        CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var visit = new Visit
        {
            ClientId = clientId,
            MechanicId = mechanicId,
            Date = DateTime.Now,
            VisitServices = services.Select(service => new VisitService
            {
                ServiceId = service.ServiceId,
                ServiceFee = service.ServiceFee
            }).ToList()
        };

        context.Visits.Add(visit);
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return visit.VisitId;
    }
}
