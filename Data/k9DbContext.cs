
using K9UnitApi.Enums;
using K9UnitApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
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
            .OnDelete(DeleteBehavior.SetNull);

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

        modelBuilder
       .Entity<Dog>()
       .Property(e => e.Specialty)
       .HasConversion(
           v => v.ToString(),
           v => (Specialty)Enum.Parse(typeof(Specialty), v));

        modelBuilder
       .Entity<Dog>()
       .Property(e => e.Status)
       .HasConversion(
           v => v.ToString(),
           v => (Status)Enum.Parse(typeof(Status), v));

        modelBuilder
       .Entity<TrainingSession>()
       .Property(e => e.TrainingType)
       .HasConversion(
           v => v.ToString(),
           v => (TrainingType)Enum.Parse(typeof(TrainingType), v));
    }
}
