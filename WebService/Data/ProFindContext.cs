using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Changepasswordcode> Changepasswordcodes { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Profession> Professions { get; set; } = null!;
        public virtual DbSet<Professional> Professionals { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Proposal> Proposals { get; set; } = null!;
        public virtual DbSet<Rank> Ranks { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<Threadid> Threadids { get; set; } = null!;

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
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

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

                entity.Property(e => e.IdA1).IsFixedLength();

                entity.Property(e => e.IdC1).IsFixedLength();

                entity.Property(e => e.IdP1).IsFixedLength();

                entity.HasOne(d => d.IdA1Navigation)
                    .WithMany(p => p.Changepasswordcodes)
                    .HasForeignKey(d => d.IdA1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Admin_ChangePasswordCode");

                entity.HasOne(d => d.IdC1Navigation)
                    .WithMany(p => p.Changepasswordcodes)
                    .HasForeignKey(d => d.IdC1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Client_ChangePasswordCode");

                entity.HasOne(d => d.IdP1Navigation)
                    .WithMany(p => p.Changepasswordcodes)
                    .HasForeignKey(d => d.IdP1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Professional_ChangePasswordCode");
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

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.IdN)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdN).IsFixedLength();

                entity.Property(e => e.IdP1).IsFixedLength();

                entity.Property(e => e.IdPj2).IsFixedLength();

                entity.HasOne(d => d.IdP1Navigation)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.IdP1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Notification_Professional");

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

                entity.Property(e => e.PhoneP).IsFixedLength();

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

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.HasKey(e => e.IdR)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.IdT)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdT).IsFixedLength();

                entity.Property(e => e.IdPj1).IsFixedLength();

                entity.HasOne(d => d.IdPj1Navigation)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.IdPj1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Tag_Project");
            });

            modelBuilder.Entity<Threadid>(entity =>
            {
                entity.HasKey(e => e.IdT)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdC).IsFixedLength();

                entity.Property(e => e.IdP).IsFixedLength();

                entity.HasOne(d => d.IdCNavigation)
                    .WithMany(p => p.Threadids)
                    .HasForeignKey(d => d.IdC)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ThreadIds_Client");

                entity.HasOne(d => d.IdPNavigation)
                    .WithMany(p => p.Threadids)
                    .HasForeignKey(d => d.IdP)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ThreadIds_Professional");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
