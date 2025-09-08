using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RealEstate.Infrastructure.Data
{
    public class RealEstateDbContext:DbContext
    {
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options):base(options) 
        {
        
        }
        public RealEstateDbContext():base()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("re");            
            base.OnModelCreating(modelBuilder);
            var assemly_Domain = Assembly.Load(new AssemblyName("RealEstate.Domain"));
            EntityHelper.LoadEntities(assemly_Domain, modelBuilder, "RealEstate.Domain.Entities");
            var assemly_Infrastructure = Assembly.Load(new AssemblyName("RealEstate.Infrastructure"));
            EntityHelper.LoadConfigs(modelBuilder, assemly_Infrastructure, "RealEstate.Infrastructure.Configurations");
        }
    }
}
