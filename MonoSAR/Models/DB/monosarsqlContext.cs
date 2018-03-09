using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace MonoSAR.Models.DB
{
    public partial class monosarsqlContext : DbContext
    {
        public virtual DbSet<Capacity> Capacity { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<MemberOffice> MemberOffice { get; set; }
        public virtual DbSet<Office> Office { get; set; }
        public virtual DbSet<Training> Training { get; set; }
        public virtual DbSet<TrainingMember> TrainingMember { get; set; }

        private String m_sqlConnectioNString;

        public monosarsqlContext(IConfiguration config)
        {
            this.m_sqlConnectioNString = config["sqlconnectionstring"];
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this.m_sqlConnectioNString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Capacity>(entity =>
            {
                entity.Property(e => e.CapacityId).HasColumnName("CapacityID");

                entity.Property(e => e.CapacityName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ham)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Joined).HasColumnType("date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneCell)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneHome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneWork)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zipcode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MemberOffice>(entity =>
            {
                entity.Property(e => e.MemberOfficeId).HasColumnName("MemberOfficeID");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.MemberOffice)
                    .HasForeignKey(d => d.OfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MemberOff__Offic__68487DD7");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.OfficeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Training>(entity =>
            {
                entity.Property(e => e.TrainingId).HasColumnName("TrainingID");

                entity.Property(e => e.TrainingTitle)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TrainingMember>(entity =>
            {
                entity.Property(e => e.TrainingMemberId).HasColumnName("TrainingMemberID");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.TrainingDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TrainingHours)
                    .HasColumnType("decimal(16, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TrainingId).HasColumnName("TrainingID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.TrainingMember)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrainingM__Membe__7A672E12");

                entity.HasOne(d => d.Training)
                    .WithMany(p => p.TrainingMember)
                    .HasForeignKey(d => d.TrainingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrainingM__Train__797309D9");
            });
        }
    }
}
