using Microsoft.EntityFrameworkCore;
using TlcBackend.Entities;
using TlcBackend.Entities.Enums;

namespace TlcBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets the participants.
        /// </summary>
        public DbSet<Participant> Participants => Set<Participant>();

        /// <summary>
        /// Gets the rewards.
        /// </summary>
        public DbSet<Reward> Rewards => Set<Reward>();

        /// <summary>
        /// Gets the redemptions.
        /// </summary>
        public DbSet<Redemption> Redemptions => Set<Redemption>();


        /// <summary>
        /// Configures the database model.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.ToTable("Participants");

                entity.HasKey(x => x.ParticipantId);

                entity.Property(x => x.CountryCode)
                    .HasColumnType("char(2)");
            });

            modelBuilder.Entity<Reward>(entity =>
            {
                entity.ToTable("Rewards");

                entity.HasKey(x => x.RewardId);
            });

            modelBuilder.Entity<Redemption>(entity =>
            {
                entity.ToTable("Redemptions");

                entity.HasKey(x => x.RedemptionId);

                entity.Property(x => x.Status)
     .HasConversion(
         status => status.ToString(),
         status => Enum.Parse<RedemptionStatus>(status))
     .HasMaxLength(20);

                entity.Property(x => x.FailureReason)
                    .HasMaxLength(500);

                entity.HasOne(x => x.Participant)
                    .WithMany(x => x.Redemptions)
                    .HasForeignKey(x => x.ParticipantId);

                entity.HasOne(x => x.Reward)
                    .WithMany(x => x.Redemptions)
                    .HasForeignKey(x => x.RewardId);
            });
        }
    }
}