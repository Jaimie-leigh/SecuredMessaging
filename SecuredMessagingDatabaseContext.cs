using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace SMWebAPI
{
    public partial class SMDbContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => 
            {
                builder
                .AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information)
                .AddConsole(); });

        public SMDbContext()
        {
        }

        public SecuredMessagingDatabaseContext(DbContextOptions<SecuredMessagingDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<Broker> Broker { get; set; }
        public virtual DbSet<Colleague> Colleague { get; set; }
        public virtual DbSet<ColleagueMessage> ColleagueMessage { get; set; }
        public virtual DbSet<MessageChain> MessageChain { get; set; }
        public virtual DbSet<MessageSubject> MessageSubject { get; set; }
        public virtual DbSet<ToDo> ToDo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLoggerFactory(MyLoggerFactory)
                    .UseSqlServer("Server=tcp:devprojectdatabaseserver.database.windows.net,1433;Initial Catalog=SecuredMessagingDatabase;Persist Security Info=False;User ID=Jfirth;Password=Kizzy1993*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.RollNumber)
                    .HasName("Apk");

                entity.Property(e => e.RollNumber).ValueGeneratedNever();

                entity.Property(e => e.BrokerId).HasColumnName("BrokerID");

                entity.Property(e => e.CustomerFullName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.BrokerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Afk");
            });

            modelBuilder.Entity<Broker>(entity =>
            {
                entity.Property(e => e.BrokerId)
                    .HasColumnName("BrokerID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BrokerForename)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BrokerSurname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Colleague>(entity =>
            {
                entity.Property(e => e.ColleagueId)
                    .HasColumnName("ColleagueID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ColleagueForename)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ColleagueSurname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ColleagueMessage>(entity =>
            {
                entity.HasKey(e => new { e.ColleagueId, e.MessageChainId })
                    .HasName("CMpk");

                entity.ToTable("Colleague_Message");

                entity.Property(e => e.ColleagueId).HasColumnName("ColleagueID");

                entity.Property(e => e.MessageChainId).HasColumnName("MessageChainID");

                entity.HasOne(d => d.Colleague)
                    .WithMany(p => p.ColleagueMessage)
                    .HasForeignKey(d => d.ColleagueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cmfk");

                entity.HasOne(d => d.MessageChain)
                    .WithMany(p => p.ColleagueMessage)
                    .HasForeignKey(d => d.MessageChainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cmfk2");
            });

            modelBuilder.Entity<MessageChain>(entity =>
            {
                entity.ToTable("Message_Chain");

                entity.Property(e => e.MessageChainId)
                    .HasColumnName("MessageChainID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateTime).HasColumnType("smalldatetime");

                entity.Property(e => e.MessageBody)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MessageSubjectId).HasColumnName("MessageSubjectID");

                entity.Property(e => e.SentFromId).HasColumnName("SentFromID");

                entity.HasOne(d => d.MessageSubject)
                    .WithMany(p => p.MessageChain)
                    .HasForeignKey(d => d.MessageSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MCfk");
            });

            modelBuilder.Entity<MessageSubject>(entity =>
            {
                entity.ToTable("Message_Subject");

                entity.Property(e => e.MessageSubjectId)
                    .HasColumnName("MessageSubjectID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BrokerId).HasColumnName("BrokerID");

                entity.Property(e => e.DateTime).HasColumnType("smalldatetime");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.RollNumberNavigation)
                    .WithMany(p => p.MessageSubject)
                    .HasForeignKey(d => d.RollNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MSfk");
            });

            modelBuilder.Entity<ToDo>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
