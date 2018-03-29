using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MonoSAR.Modeldump
{
    public partial class monosarsqlContext : DbContext
    {
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Capacity> Capacity { get; set; }
        public virtual DbSet<Certification> Certification { get; set; }
        public virtual DbSet<Cpr> Cpr { get; set; }
        public virtual DbSet<Medical> Medical { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<MemberCertification> MemberCertification { get; set; }
        public virtual DbSet<MemberCpr> MemberCpr { get; set; }
        public virtual DbSet<MemberMedical> MemberMedical { get; set; }
        public virtual DbSet<Office> Office { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<OperationMember> OperationMember { get; set; }
        public virtual DbSet<Training> Training { get; set; }
        public virtual DbSet<TrainingMember> TrainingMember { get; set; }
        public virtual DbSet<TrainingMemberInstructor> TrainingMemberInstructor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=tcp:monosardbserver.database.windows.net,1433;Initial Catalog=monosarsql;Persist Security Info=False;User ID=monodblogin;Password=C8lb6T0nkAC4RuM7GRtO;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<Capacity>(entity =>
            {
                entity.Property(e => e.CapacityId).HasColumnName("CapacityID");

                entity.Property(e => e.CapacityName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Certification>(entity =>
            {
                entity.Property(e => e.CertificationId).HasColumnName("CertificationID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cpr>(entity =>
            {
                entity.ToTable("CPR");

                entity.Property(e => e.Cprid).HasColumnName("CPRID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Medical>(entity =>
            {
                entity.Property(e => e.MedicalId).HasColumnName("MedicalID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Title)
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

                entity.Property(e => e.CapacityId).HasColumnName("CapacityID");

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

                entity.HasOne(d => d.Capacity)
                    .WithMany(p => p.Member)
                    .HasForeignKey(d => d.CapacityId)
                    .HasConstraintName("FK__Member__Capacity__17036CC0");
            });

            modelBuilder.Entity<MemberCertification>(entity =>
            {
                entity.Property(e => e.MemberCertificationId).HasColumnName("MemberCertificationID");

                entity.Property(e => e.CertificationId).HasColumnName("CertificationID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Expiration).HasColumnType("datetime");

                entity.Property(e => e.Issued).HasColumnType("datetime");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Certification)
                    .WithMany(p => p.MemberCertification)
                    .HasForeignKey(d => d.CertificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MemberCer__Certi__282DF8C2");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberCertification)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MemberCer__Membe__2739D489");
            });

            modelBuilder.Entity<MemberCpr>(entity =>
            {
                entity.ToTable("MemberCPR");

                entity.Property(e => e.MemberCprid).HasColumnName("MemberCPRID");

                entity.Property(e => e.Cprid).HasColumnName("CPRID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Expiration).HasColumnType("datetime");

                entity.Property(e => e.Issued).HasColumnType("datetime");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Cpr)
                    .WithMany(p => p.MemberCpr)
                    .HasForeignKey(d => d.Cprid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MemberCPR__CPRID__245D67DE");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberCpr)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MemberCPR__Membe__236943A5");
            });

            modelBuilder.Entity<MemberMedical>(entity =>
            {
                entity.Property(e => e.MemberMedicalId).HasColumnName("MemberMedicalID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Expiration).HasColumnType("datetime");

                entity.Property(e => e.Issued).HasColumnType("datetime");

                entity.Property(e => e.MedicalId).HasColumnName("MedicalID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Medical)
                    .WithMany(p => p.MemberMedical)
                    .HasForeignKey(d => d.MedicalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MemberMed__Medic__208CD6FA");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberMedical)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MemberMed__Membe__1F98B2C1");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.OfficeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.Property(e => e.OperationId).HasColumnName("OperationID");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.OperationEnd).HasColumnType("datetime");

                entity.Property(e => e.OperationNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OperationStart).HasColumnType("datetime");

                entity.Property(e => e.SequenceNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OperationMember>(entity =>
            {
                entity.Property(e => e.OperationMemberId).HasColumnName("OperationMemberID");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OperationId).HasColumnName("OperationID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.OperationMember)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Operation__Membe__41EDCAC5");

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.OperationMember)
                    .HasForeignKey(d => d.OperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Operation__Opera__40F9A68C");
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
                entity.HasIndex(e => e.MemberId)
                    .HasName("idx_trainingmember_memberid");

                entity.HasIndex(e => new { e.Created, e.TrainingDate })
                    .HasName("idx_trainingmember_created_trainingdate");

                entity.HasIndex(e => new { e.MemberId, e.TrainingId })
                    .HasName("idx_trainingmember_memberid_trainingid");

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

            modelBuilder.Entity<TrainingMemberInstructor>(entity =>
            {
                entity.Property(e => e.TrainingMemberInstructorId).HasColumnName("TrainingMemberInstructorID");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IntstructorMemberId).HasColumnName("IntstructorMemberID");

                entity.Property(e => e.TrainingMemberId).HasColumnName("TrainingMemberID");

                entity.HasOne(d => d.IntstructorMember)
                    .WithMany(p => p.TrainingMemberInstructor)
                    .HasForeignKey(d => d.IntstructorMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrainingM__Intst__498EEC8D");

                entity.HasOne(d => d.TrainingMember)
                    .WithMany(p => p.TrainingMemberInstructor)
                    .HasForeignKey(d => d.TrainingMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrainingM__Train__489AC854");
            });
        }
    }
}
