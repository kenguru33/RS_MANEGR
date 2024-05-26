using MassTransit;
using Microsoft.EntityFrameworkCore;
using VesselManager.Entities;

namespace VesselManager.Data;

public class VesselDbContext : DbContext
{
    public VesselDbContext(DbContextOptions<VesselDbContext> options) : base(options) {}
    
    public DbSet<Vessel> Vessels { get; set; }
    public DbSet<PublisherSequenceNumber> PublisherSequenceNumbers { get; set; }
    public DbSet<PublisherSequenceNumber> ConsumerSequenceNumbers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}