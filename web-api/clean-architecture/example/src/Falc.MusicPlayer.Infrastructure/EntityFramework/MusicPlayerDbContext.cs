using Falc.MusicPlayer.Domain;
using Falc.MusicPlayer.Domain.Events;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Falc.MusicPlayer.Infrastructure.EntityFramework;

public class MusicPlayerDbContext(IMediator mediator, IPublishEndpoint publishEndpoint) : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Marker).Assembly);
        
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties().Where(x => x.IsPrimaryKey()))
            {
                property.ValueGenerated = ValueGenerated.Never;
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .SelectMany(x => x.Entity.DomainEvents).ToList();

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent, cancellationToken);
        }

        var integrationEvents = domainEvents.OfType<IIntegrationEvent<INotification>>();
        foreach (var integrationEvent in integrationEvents)
        {
            var contract = integrationEvent.MapToContract();
            await publishEndpoint.Publish(contract, cancellationToken);
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
}