using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace SMWebAPI.Models
{
    public class SMDbContext : DbContext
    {
        public SMDbContext(DbContextOptions<SMDbContext> options)
            : base(options)
        {
        }

        public DbSet<Application> Application { get; set; }
        public DbSet<Broker> Broker { get; set; }
        public DbSet<Colleague> Colleague { get; set; }
        public DbSet<Colleague_Message> Colleague_Message { get; set; }
        public DbSet<Message_Chain> Message_Chain { get; set; }
        public DbSet<Message_Subject> Message_Subject { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder
        //            //.UseLoggerFactory(MyLoggerFactory);
        //            //.UseSqlServer("Server=tcp:devprojectdatabaseserver.database.windows.net,1433;Initial Catalog=SecuredMessagingDatabase;Persist Security Info=False;User ID=Jfirth;Password=Kizzy1993*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        //    }
        //}

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

            modelBuilder.Entity<Colleague_Message>(entity =>
            {
                entity.HasKey(e => new { e.ColleagueId, e.MessageChainId })
                    .HasName("CMpk");

                entity.ToTable("Colleague_Message");

                entity.Property(e => e.ColleagueId).HasColumnName("ColleagueID");

                entity.Property(e => e.MessageChainId).HasColumnName("MessageChainID");

                entity.HasOne(d => d.Colleague)
                    .WithMany(p => p.Colleague_Message)
                    .HasForeignKey(d => d.ColleagueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cmfk");

                entity.HasOne(d => d.Message_Chain)
                    .WithMany(p => p.Colleague_Message)
                    .HasForeignKey(d => d.MessageChainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cmfk2");
            });

            modelBuilder.Entity<Message_Chain>(entity =>
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

                entity.HasOne(d => d.Message_Subject)
                    .WithMany(p => p.Message_Chain)
                    .HasForeignKey(d => d.MessageSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MCfk");
            });

            modelBuilder.Entity<Message_Subject>(entity =>
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

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.Message_Subject)
                    .HasForeignKey(d => d.RollNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MSfk");
            });

            //OnModelCreatingPartial(modelBuilder);
        }

       //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

