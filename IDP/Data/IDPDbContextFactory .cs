using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DoctorFit.IDP.Data
{
    public class IDPDbContextFactory : IDesignTimeDbContextFactory<IDPDbContext>
    {
        public IDPDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IDPDbContext>();

            // Replace with your actual connection string
            optionsBuilder.UseSqlServer("Server=localhost;Database=ERPAppDB;Trusted_Connection=True;TrustServerCertificate=True;integrated security = true");

            return new IDPDbContext(optionsBuilder.Options);
        }
    }
}
