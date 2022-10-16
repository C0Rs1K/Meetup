using Meetup.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Infrastracture.DataBase
{
    public partial class MeetupDbContext : DbContext
    {
        public MeetupDbContext(DbContextOptions<MeetupDbContext> options) : base(options)
        {
        }

        public virtual DbSet<AddressEntity> Addresses { get; set; }
        public virtual DbSet<MeetupEntity> Meetups { get; set; }
        public virtual DbSet<MeetupSpeakerEntity> MeetupSpeakerEntities { get; set; }
        public virtual DbSet<SpeakerEntity> Speakers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeetupEntity>(entity =>
            {
                entity.HasIndex(e => e.AddressId, "IX_Meetups_AddressId");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Meetups)
                    .HasForeignKey(d => d.AddressId);
            });

            modelBuilder.Entity<MeetupSpeakerEntity>(entity =>
            {
                entity.ToTable("MeetupSpeakerEntity");

                entity.HasIndex(e => e.MeetupId, "IX_MeetupSpeakerEntity_MeetupId");

                entity.HasIndex(e => e.SpeakerId, "IX_MeetupSpeakerEntity_SpeakerId");

                entity.HasOne(d => d.Meetup)
                    .WithMany(p => p.MeetupSpeakerEntities)
                    .HasForeignKey(d => d.MeetupId);

                entity.HasOne(d => d.Speaker)
                    .WithMany(p => p.MeetupSpeakerEntities)
                    .HasForeignKey(d => d.SpeakerId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
