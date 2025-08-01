using Duende.IdentityModel;
using Duende.IdentityServer.EntityFramework.Entities;
using Entities;
using Entities.Enums;
using IDP.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;

namespace DoctorFit.IDP.Data
{
    public class IDPDbContext : IdentityDbContext<
     ApplicationUser,
     IdentityRole<int>,
     int,
     IdentityUserClaim<int>,
     IdentityUserRole<int>,
     IdentityUserLogin<int>,
     IdentityRoleClaim<int>,
     IdentityUserToken<int>>
    {
        public IDPDbContext(DbContextOptions<IDPDbContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().Property(o => o.Gender).HasConversion(new BoolToStringConverter("Female", "Male"));

            builder.Entity<ApplicationUser>()
              .HasIndex(p => p.NationalID)
              .IsUnique();
            builder.Entity<ApplicationUser>()
                .HasIndex(p => p.PhoneNumber)
                .IsUnique();
            builder.Entity<ApplicationUser>()
                   .Property(o => o.NationalID)
                   .HasMaxLength(10)
                   .IsFixedLength();

            builder.Entity<Master>()
                            .HasOne(m => m.CreatedBy)
                            .WithMany(p => p.CreatorMaster)
                            .HasForeignKey(m => m.CreatedByPersonId)
                            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Subsidiary>()
                .HasOne(s => s.CreatedBy)
                .WithMany(p => p.CreatorSubsidiary)
                .HasForeignKey(s => s.CreatedByPersonId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

          
        }
    }
}
