using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using WebService.Models.Generated;

namespace WebService.Data
{
    public partial class ProFindContext : DbContext
    {

        public ProFindContext()
        {
        }

        public ProFindContext(DbContextOptions<ProFindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<Activitycomment> Activitycomments { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Changepasswordcode> Changepasswordcodes { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Profession> Professions { get; set; } = null!;
        public virtual DbSet<Professional> Professionals { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Projectpay> Projectpays { get; set; } = null!;
        public virtual DbSet<Projectpaytemplate> Projectpaytemplates { get; set; } = null!;
        public virtual DbSet<Projectstatus> Projectstatuses { get; set; } = null!;
        public virtual DbSet<Projecttemplate> Projecttemplates { get; set; } = null!;
        public virtual DbSet<Proposal> Proposals { get; set; } = null!;
        public virtual DbSet<Proposalnotification> Proposalnotifications { get; set; } = null!;
        public virtual DbSet<Rank> Ranks { get; set; } = null!;
        public virtual DbSet<Securityansweradmin> Securityansweradmins { get; set; } = null!;
        public virtual DbSet<Securityanswerclient> Securityanswerclients { get; set; } = null!;
        public virtual DbSet<Securityanswerprofessional> Securityanswerprofessionals { get; set; } = null!;
        public virtual DbSet<Securityquestion> Securityquestions { get; set; } = null!;
        public virtual DbSet<Supportticket> Supporttickets { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<Tagtemplate> Tagtemplates { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;database=ProFind;convert zero datetime=True", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.24-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Activity>(entity =>
            {
              

                entity.HasKey(e => e.IdA)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdA).IsFixedLength();

                entity.Property(e => e.IdPj1).IsFixedLength();

                entity.Property(e => e.IdT1).IsFixedLength();

                entity.HasOne(d => d.IdPj1Navigation)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.IdPj1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Activity_Project");

                entity.HasOne(d => d.IdT1Navigation)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.IdT1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Activity_Tag");
            });

            modelBuilder.Entity<Activitycomment>(entity =>
            {
                entity.HasKey(e => e.IdAc)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdAc).IsFixedLength();

                entity.Property(e => e.IdA1).IsFixedLength();

                entity.Property(e => e.IdC5).IsFixedLength();

                entity.Property(e => e.IdP5).IsFixedLength();

                entity.HasOne(d => d.IdA1Navigation)
                    .WithMany(p => p.Activitycomments)
                    .HasForeignKey(d => d.IdA1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ActivityComment_Activity");

                entity.HasOne(d => d.IdC5Navigation)
                    .WithMany(p => p.Activitycomments)
                    .HasForeignKey(d => d.IdC5)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ActivityComment_Client");

                entity.HasOne(d => d.IdP5Navigation)
                    .WithMany(p => p.Activitycomments)
                    .HasForeignKey(d => d.IdP5)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ActivityComment_Professional");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.IdA)
                    .HasName("PRIMARY");

                entity.HasIndex(e => new { e.NameA, e.EmailA, e.TelA }, "NameA")
                    .HasAnnotation("MySql:FullTextIndex", true);

                entity.Property(e => e.IdA).IsFixedLength();

                entity.Property(e => e.PasswordA).IsFixedLength();

                entity.HasOne(d => d.IdR1Navigation)
                    .WithMany(p => p.Admins)
                    .HasForeignKey(d => d.IdR1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Admin_Rank");
            });

            modelBuilder.Entity<Changepasswordcode>(entity =>
            {
                entity.HasKey(e => e.IdCpc)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdCpc).IsFixedLength();

                entity.Property(e => e.CodeCpc).IsFixedLength();

                entity.Property(e => e.IdC1).IsFixedLength();

                entity.HasOne(d => d.IdC1Navigation)
                    .WithMany(p => p.Changepasswordcodes)
                    .HasForeignKey(d => d.IdC1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Client_ChangePasswordCode");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdC)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdC).IsFixedLength();

                entity.Property(e => e.PasswordC).IsFixedLength();
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.IdDp)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.IdM)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdM).IsFixedLength();

                entity.Property(e => e.IdC4).IsFixedLength();

                entity.Property(e => e.IdP4).IsFixedLength();

                entity.HasOne(d => d.IdC4Navigation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.IdC4)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Message_Client");

                entity.HasOne(d => d.IdP4Navigation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.IdP4)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Message_Professional");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.IdN)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdN).IsFixedLength();

                entity.Property(e => e.IdPj2).IsFixedLength();

                entity.HasOne(d => d.IdPj2Navigation)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.IdPj2)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Notification_Project");
            });

            modelBuilder.Entity<Profession>(entity =>
            {
                entity.HasKey(e => e.IdPfs)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<Professional>(entity =>
            {
                entity.HasKey(e => e.IdP)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdP).IsFixedLength();

                entity.Property(e => e.EmailP).IsFixedLength();

                entity.Property(e => e.NameP).IsFixedLength();

                entity.Property(e => e.PasswordP).IsFixedLength();

                entity.HasOne(d => d.IdDp1Navigation)
                    .WithMany(p => p.Professionals)
                    .HasForeignKey(d => d.IdDp1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Professional_Department");

                entity.HasOne(d => d.IdPfs1Navigation)
                    .WithMany(p => p.Professionals)
                    .HasForeignKey(d => d.IdPfs1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Professional_Profession");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.IdPj)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdPj).IsFixedLength();

                entity.Property(e => e.IdC1).IsFixedLength();

                entity.Property(e => e.IdP1).IsFixedLength();

                entity.Property(e => e.IdPs1).IsFixedLength();

                entity.HasOne(d => d.IdC1Navigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.IdC1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Project_Client");

                entity.HasOne(d => d.IdP1Navigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.IdP1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Project_Professional");

                entity.HasOne(d => d.IdPs1Navigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.IdPs1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Project_ProjectStatus");
            });

            modelBuilder.Entity<Projectpay>(entity =>
            {
                entity.HasKey(e => e.IdPpy)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdPpy).IsFixedLength();

                entity.Property(e => e.IdC3).IsFixedLength();

                entity.Property(e => e.IdP3).IsFixedLength();

                entity.Property(e => e.IdPj3).IsFixedLength();

                entity.HasOne(d => d.IdC3Navigation)
                    .WithMany(p => p.Projectpays)
                    .HasForeignKey(d => d.IdC3)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProjectPay_Client");

                entity.HasOne(d => d.IdP3Navigation)
                    .WithMany(p => p.Projectpays)
                    .HasForeignKey(d => d.IdP3)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProjectPay_Professional");

                entity.HasOne(d => d.IdPj3Navigation)
                    .WithMany(p => p.Projectpays)
                    .HasForeignKey(d => d.IdPj3)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProjectPay_Project");
            });

            modelBuilder.Entity<Projectpaytemplate>(entity =>
            {
                entity.HasKey(e => e.IdPpt)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdPpt).IsFixedLength();

                entity.Property(e => e.IdPt1).IsFixedLength();

                entity.HasOne(d => d.IdPt1Navigation)
                    .WithMany(p => p.Projectpaytemplates)
                    .HasForeignKey(d => d.IdPt1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProjectPayTemplate_ProjectTemplate");
            });

            modelBuilder.Entity<Projectstatus>(entity =>
            {
                entity.HasKey(e => e.IdPs)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdPs).IsFixedLength();
            });

            modelBuilder.Entity<Projecttemplate>(entity =>
            {
                entity.HasKey(e => e.IdPt)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdPt).IsFixedLength();

                entity.Property(e => e.IdP1).IsFixedLength();

                entity.HasOne(d => d.IdP1Navigation)
                    .WithMany(p => p.Projecttemplates)
                    .HasForeignKey(d => d.IdP1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProjectTemplate_Professional");
            });

            modelBuilder.Entity<Proposal>(entity =>
            {
                entity.HasKey(e => e.IdPp)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdPp).IsFixedLength();

                entity.Property(e => e.IdC3).IsFixedLength();

                entity.Property(e => e.IdP3).IsFixedLength();

                entity.HasOne(d => d.IdC3Navigation)
                    .WithMany(p => p.Proposals)
                    .HasForeignKey(d => d.IdC3)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Proposal_Client");

                entity.HasOne(d => d.IdP3Navigation)
                    .WithMany(p => p.Proposals)
                    .HasForeignKey(d => d.IdP3)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Proposal_Professional");
            });

            modelBuilder.Entity<Proposalnotification>(entity =>
            {
                entity.HasKey(e => e.IdPn)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdPn).IsFixedLength();

                entity.Property(e => e.IdC4).IsFixedLength();

                entity.Property(e => e.IdP4).IsFixedLength();

                entity.Property(e => e.IdPp1).IsFixedLength();

                entity.HasOne(d => d.IdC4Navigation)
                    .WithMany(p => p.Proposalnotifications)
                    .HasForeignKey(d => d.IdC4)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProposalNotification_Client");

                entity.HasOne(d => d.IdP4Navigation)
                    .WithMany(p => p.Proposalnotifications)
                    .HasForeignKey(d => d.IdP4)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProposalNotification_Professional");

                entity.HasOne(d => d.IdPp1Navigation)
                    .WithMany(p => p.Proposalnotifications)
                    .HasForeignKey(d => d.IdPp1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProposalNotification_Proposal");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.HasKey(e => e.IdR)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<Securityansweradmin>(entity =>
            {
                entity.HasKey(e => e.IdSa)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdSa).IsFixedLength();

                entity.Property(e => e.IdA1).IsFixedLength();

                entity.Property(e => e.IdSq1).IsFixedLength();

                entity.HasOne(d => d.IdA1Navigation)
                    .WithMany(p => p.Securityansweradmins)
                    .HasForeignKey(d => d.IdA1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Admin_SecurityAnswerAdmins");

                entity.HasOne(d => d.IdSq1Navigation)
                    .WithMany(p => p.Securityansweradmins)
                    .HasForeignKey(d => d.IdSq1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SecurityAnswerAdmins_SecurityQuestion");
            });

            modelBuilder.Entity<Securityanswerclient>(entity =>
            {
                entity.HasKey(e => e.IdSa)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdSa).IsFixedLength();

                entity.Property(e => e.IdC1).IsFixedLength();

                entity.Property(e => e.IdSq1).IsFixedLength();

                entity.HasOne(d => d.IdC1Navigation)
                    .WithMany(p => p.Securityanswerclients)
                    .HasForeignKey(d => d.IdC1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Client_SecurityAnswerClients");

                entity.HasOne(d => d.IdSq1Navigation)
                    .WithMany(p => p.Securityanswerclients)
                    .HasForeignKey(d => d.IdSq1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SecurityAnswerClients_SecurityQuestion");
            });

            modelBuilder.Entity<Securityanswerprofessional>(entity =>
            {
                entity.HasKey(e => e.IdSa)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdSa).IsFixedLength();

                entity.Property(e => e.IdP).IsFixedLength();

                entity.Property(e => e.IdSq).IsFixedLength();

                entity.HasOne(d => d.IdPNavigation)
                    .WithMany(p => p.Securityanswerprofessionals)
                    .HasForeignKey(d => d.IdP)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Professional_SecurityAnswerProfessionals");

                entity.HasOne(d => d.IdSqNavigation)
                    .WithMany(p => p.Securityanswerprofessionals)
                    .HasForeignKey(d => d.IdSq)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SecurityAnswerProfessionals_SecurityQuestion");
            });

            modelBuilder.Entity<Securityquestion>(entity =>
            {
                entity.HasKey(e => e.IdSq)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdSq).IsFixedLength();
            });

            modelBuilder.Entity<Supportticket>(entity =>
            {
                entity.HasKey(e => e.IdSt)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdSt).IsFixedLength();

                entity.Property(e => e.IdA2).IsFixedLength();

                entity.Property(e => e.IdAct1).IsFixedLength();

                entity.Property(e => e.IdC5).IsFixedLength();

                entity.Property(e => e.IdP5).IsFixedLength();

                entity.Property(e => e.IdPj4).IsFixedLength();

                entity.Property(e => e.IdPp2).IsFixedLength();

                entity.Property(e => e.IdPpy1).IsFixedLength();

                entity.HasOne(d => d.IdA2Navigation)
                    .WithMany(p => p.Supporttickets)
                    .HasForeignKey(d => d.IdA2)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SupportTicket_Admin");

                entity.HasOne(d => d.IdAct1Navigation)
                    .WithMany(p => p.Supporttickets)
                    .HasForeignKey(d => d.IdAct1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SupportTicket_Activity");

                entity.HasOne(d => d.IdC5Navigation)
                    .WithMany(p => p.Supporttickets)
                    .HasForeignKey(d => d.IdC5)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SupportTicket_Client");

                entity.HasOne(d => d.IdP5Navigation)
                    .WithMany(p => p.Supporttickets)
                    .HasForeignKey(d => d.IdP5)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SupportTicket_Professional");

                entity.HasOne(d => d.IdPj4Navigation)
                    .WithMany(p => p.Supporttickets)
                    .HasForeignKey(d => d.IdPj4)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SupportTicket_Project");

                entity.HasOne(d => d.IdPp2Navigation)
                    .WithMany(p => p.Supporttickets)
                    .HasForeignKey(d => d.IdPp2)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SupportTicket_Proposal");

                entity.HasOne(d => d.IdPpy1Navigation)
                    .WithMany(p => p.Supporttickets)
                    .HasForeignKey(d => d.IdPpy1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SupportTicket_ProjectPay");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.IdT)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdT).IsFixedLength();

                entity.Property(e => e.ColorT).IsFixedLength();

                entity.Property(e => e.IdPj).IsFixedLength();

                entity.HasOne(d => d.IdPjNavigation)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.IdPj)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Tag_Project");
            });

            modelBuilder.Entity<Tagtemplate>(entity =>
            {
                entity.HasKey(e => e.IdTt)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdTt).IsFixedLength();

                entity.Property(e => e.ColorTt).IsFixedLength();

                entity.Property(e => e.IdPt1).IsFixedLength();

                entity.HasOne(d => d.IdPt1Navigation)
                    .WithMany(p => p.Tagtemplates)
                    .HasForeignKey(d => d.IdPt1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TagTemplate_ProjectTemplate");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
