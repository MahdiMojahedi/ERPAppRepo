using Application.Interfaces;
using Entities;
using Entities.Enums;
using IDP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Persistance
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }


        public DbSet<Subsidiary> Subsidiaries { get; set; }
        public DbSet<Master> Masters { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subsidiary>().ToTable("tblSubsidiary");
            modelBuilder.Entity<Master>().ToTable("tblMaster");
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");

            SubsiDiaryConfig(modelBuilder);


            //-----------------------------------------
            
            //---------------------------------



            MasterConfig(modelBuilder);

        }

        private static void MasterConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Master>()
                            .HasOne(m => m.CreatedBy)
                            .WithMany(p => p.CreatorMaster)
                            .HasForeignKey(m => m.CreatedByPersonId)
                            .OnDelete(DeleteBehavior.Restrict)
                            .IsRequired();

            modelBuilder.Entity<Master>()
                .HasIndex(m => m.Code)
                .IsUnique();


            modelBuilder.Entity<Master>(entity =>
            {
                entity.HasOne(m => m.CreatedBy)
                      .WithMany(o => o.CreatorMaster)
                      .HasForeignKey(m => m.CreatedByPersonId)
                      .HasPrincipalKey(u => u.Id)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired();
            });
        }

        private static void SubsiDiaryConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subsidiary>(entity =>
            {
                entity.Property(e => e.Title)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Code)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(e => e.IsLastLevel)
                      .IsRequired();

                entity.Property(e => e.IsDeleted)
                      .HasDefaultValue(false);

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");



                entity.HasOne(e => e.Master)
                      .WithMany()
                      .HasForeignKey(e => e.MasterId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.Code)
                      .IsUnique(); // Enforce unique code


                entity.HasOne(s => s.Master)
                .WithMany(m => m.Subsidiaries)
                .HasForeignKey(s => s.MasterId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

                entity.HasOne(s => s.ParentSubsidiary)
                .WithMany(s => s.Children)
                .HasForeignKey(s => s.ParentSubsidiaryId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(s => s.Code)
                .IsUnique();

                entity.HasOne(s => s.CreatedBy)
                .WithMany(p => p.CreatorSubsidiary)
                .HasForeignKey(s => s.CreatedByPersonId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            });
        }
    }
}
