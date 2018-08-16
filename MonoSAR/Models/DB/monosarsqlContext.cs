using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace MonoSAR.Models.DB
{
    public partial class monosarsqlContext : DbContext
    {
        public virtual DbSet<Callout> Callout { get; set; }
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
        public virtual DbSet<TrainingClass> TrainingClass { get; set; }
        public virtual DbSet<TrainingClassStudent> TrainingClassStudent { get; set; }
        public virtual DbSet<TrainingClassInstructor> TrainingClassInstructor { get; set; }

        private String m_sqlConnectioNString;

        public monosarsqlContext(IConfiguration config)
        {
            this.m_sqlConnectioNString = config["sqlconnectionstring"];
        }

        public monosarsqlContext()
        {
            //only here for scaffolding, do not use
            throw new NotImplementedException("DBContext only used for scaffolding, make use of DI method.");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (string.IsNullOrEmpty(m_sqlConnectioNString))
                {
                    var configurationBuilder = new ConfigurationBuilder();
                    configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("azurekeyvault.json", false, true)
                    .AddJsonFile("appsettings.json", false, true)
                    .AddEnvironmentVariables();
                    
                    var config = configurationBuilder.Build();

                    configurationBuilder.AddAzureKeyVault(
                        $"https://{config["azureKeyVault:vault"]}.vault.azure.net/",
                        config["azureKeyVault:clientId"],
                        config["azureKeyVault:clientSecret"]
                    );

                    config = configurationBuilder.Build();
                    optionsBuilder.UseSqlServer(config.GetConnectionString("sqlconnectionstring"));
                }
                else
                {
                    optionsBuilder.UseSqlServer(this.m_sqlConnectioNString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Callout>(entity =>
            {
                entity.Property(e => e.CalloutId).HasColumnName("CalloutID");

                entity.Property(e => e.CalloutMessage)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Created).HasColumnType("datetime");
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

            modelBuilder.Entity<Training>(entity =>
            {
                entity.Property(e => e.TrainingId).HasColumnName("TrainingID");

                entity.Property(e => e.TrainingTitle)
                    .IsRequired()
                    .HasMaxLength(250)
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

            modelBuilder.Entity<TrainingClass>(entity =>
            {
                entity.Property(e => e.TrainingClassId).HasColumnName("TrainingClassID");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TrainingDate).HasColumnType("datetime");

                entity.Property(e => e.TrainingId).HasColumnName("TrainingID");

                entity.HasOne(d => d.Training)
                    .WithMany(p => p.TrainingClass)
                    .HasForeignKey(d => d.TrainingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrainingC__Train__5224328E");
            });

            modelBuilder.Entity<TrainingClassInstructor>(entity =>
            {
                entity.Property(e => e.TrainingClassInstructorId).HasColumnName("TrainingClassInstructorID");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TrainingClassId).HasColumnName("TrainingClassID");

                entity.Property(e => e.TrainingClassInstructorMemberId).HasColumnName("TrainingClassInstructorMemberID");

                entity.Property(e => e.TrainingClassStudentHours).HasColumnType("decimal(16, 2)");

                entity.HasOne(d => d.TrainingClass)
                    .WithMany(p => p.TrainingClassInstructor)
                    .HasForeignKey(d => d.TrainingClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrainingC__Train__65370702");

                entity.HasOne(d => d.TrainingClassInstructorMember)
                    .WithMany(p => p.TrainingClassInstructor)
                    .HasForeignKey(d => d.TrainingClassInstructorMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrainingC__Train__6442E2C9");
            });

            modelBuilder.Entity<TrainingClassStudent>(entity =>
            {
                entity.Property(e => e.TrainingClassStudentId).HasColumnName("TrainingClassStudentID");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TrainingClassId).HasColumnName("TrainingClassID");

                entity.Property(e => e.TrainingClassStudentHours).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.TrainingClassStudentMemberId).HasColumnName("TrainingClassStudentMemberID");

                entity.HasOne(d => d.TrainingClass)
                    .WithMany(p => p.TrainingClassStudent)
                    .HasForeignKey(d => d.TrainingClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrainingC__Train__607251E5");

                entity.HasOne(d => d.TrainingClassStudentMember)
                    .WithMany(p => p.TrainingClassStudent)
                    .HasForeignKey(d => d.TrainingClassStudentMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TrainingC__Train__5F7E2DAC");
            });

        }
    }
}



