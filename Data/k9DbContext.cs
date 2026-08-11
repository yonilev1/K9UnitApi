
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using K9UnitApi.Models;
namespace K9UnitApi.Data;

public class k9DbContext : DbContext 
{
    public k9DbContext(DbContextOptions<k9DbContext> options)
        :base(options)
    { }

    public DbSet<Handler> Handlers => Set<Handler>();
    public DbSet<Dog> Dogs => Set<Dog>();
    public DbSet<TrainingSession> TrainingSessions => Set<TrainingSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Dog>()
            .HasOne(d => d.Handler)
            .WithOne(h => h.Dog)
            .HasForeignKey<Dog>(d => d.HandlerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TrainingSession>()
            .HasOne(t => t.Dog)
            .WithMany(d => d.TrainingSessions)
            .HasForeignKey(t => t.DogId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Handler>()
            .HasIndex(h => h.PersonalNumber)
            .IsUnique();

        modelBuilder.Entity<Dog>()
            .HasIndex(d => d.MicrochipId)
            .IsUnique();
    }
}
