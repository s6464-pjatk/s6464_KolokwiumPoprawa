using Microsoft.EntityFrameworkCore;
using s6464_KolokwiumPoprawa.Models;

namespace s6464_KolokwiumPoprawa.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Mechanic> Mechanics => Set<Mechanic>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Visit> Visits => Set<Visit>();
    public DbSet<VisitService> VisitServices => Set<VisitService>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");
            entity.HasKey(e => e.ClientId);
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.FirstName).HasColumnName("first_name").HasMaxLength(100).IsRequired();
            entity.Property(e => e.LastName).HasColumnName("last_name").HasMaxLength(100).IsRequired();
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth").HasColumnType("datetime");
        });

        modelBuilder.Entity<Mechanic>(entity =>
        {
            entity.ToTable("Mechanic");
            entity.HasKey(e => e.MechanicId);
            entity.Property(e => e.MechanicId).HasColumnName("mechanic_id");
            entity.Property(e => e.FirstName).HasColumnName("first_name").HasMaxLength(100).IsRequired();
            entity.Property(e => e.LastName).HasColumnName("last_name").HasMaxLength(100).IsRequired();
            entity.Property(e => e.LicenceNumber).HasColumnName("licence_number").HasMaxLength(14).IsRequired();
            entity.HasIndex(e => e.LicenceNumber).IsUnique();
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");
            entity.HasKey(e => e.ServiceId);
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            entity.Property(e => e.BaseFee).HasColumnName("base_fee").HasColumnType("decimal(10,2)");
            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.ToTable("Visit");
            entity.HasKey(e => e.VisitId);
            entity.Property(e => e.VisitId).HasColumnName("visit_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.MechanicId).HasColumnName("mechanic_id");
            entity.Property(e => e.Date).HasColumnName("date").HasColumnType("datetime");

            entity.HasOne(e => e.Client)
                .WithMany(e => e.Visits)
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Mechanic)
                .WithMany(e => e.Visits)
                .HasForeignKey(e => e.MechanicId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<VisitService>(entity =>
        {
            entity.ToTable("Visit_Service");
            entity.HasKey(e => new { e.VisitId, e.ServiceId });
            entity.Property(e => e.VisitId).HasColumnName("visit_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.ServiceFee).HasColumnName("service_fee").HasColumnType("decimal(10,2)");

            entity.HasOne(e => e.Visit)
                .WithMany(e => e.VisitServices)
                .HasForeignKey(e => e.VisitId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Service)
                .WithMany(e => e.VisitServices)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
