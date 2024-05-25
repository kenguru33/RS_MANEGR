using MassTransit;
using Microsoft.EntityFrameworkCore;
using StationManager.Entities;

namespace StationManager.Data;

public class StationDbContext : DbContext
{
    public StationDbContext(DbContextOptions<StationDbContext> options) : base(options) {}
    
    public DbSet<Station> Stations { get; set; }
    public DbSet<StationSequenceNumber> SequenceNumber { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}