using System.Reflection;
using RealEstate.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace RealEstate.Infrastructure.Data
{
    public class EntityHelper
    {
        public static void LoadEntities(Assembly assemly, ModelBuilder modelBuilder, string nameSpace)
        {
            var entityTypes = assemly.GetTypes()
                .Where(type => type.BaseType != null &&
                               type.BaseType != Type.GetType("System.Enum") &&
                               type.Name != "BaseEntity" &&
                               !type.IsAbstract &&
                               (type.IsSubclassOf(typeof(BaseEntity)) &&
                               type.Namespace != null &&
                               type.Namespace.Contains(nameSpace)))
                               .ToList();

            // entityTypes.ForEach(modelBuilder.RegisterEntityType);          
            entityTypes.ForEach(m => 
            {
                var obj = modelBuilder.Model.FindEntityType(m); 
                if(obj == null)
                {
                    modelBuilder.Model.AddEntityType(m);
                }               
            });

        }
        public static void LoadConfigs(ModelBuilder modelBuilder, Assembly assembly, string nameSpace)
        {
            var configurationTypes = assembly.GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace)
                               && type.Namespace.Contains(nameSpace, StringComparison.OrdinalIgnoreCase)
                               && type.GetInterfaces().Any(i =>
                                   i.IsGenericType &&
                                   i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (var type in configurationTypes)
            {
                // Create an instance of the configuration type
                var configurationInstance = Activator.CreateInstance(type);

                if (configurationInstance != null)
                {
                    // Get the entity type from the IEntityTypeConfiguration<TEntity> interface
                    var entityType = type.GetInterfaces()
                        .First(i => i.IsGenericType &&
                                    i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                        .GetGenericArguments()[0];

                    // Create a generic method for ApplyConfiguration<TEntity>
                    var applyConfigMethod = typeof(ModelBuilder)
                        .GetMethod(nameof(ModelBuilder.ApplyConfiguration))
                        ?.MakeGenericMethod(entityType);

                    // Invoke the ApplyConfiguration<TEntity> method
                    applyConfigMethod?.Invoke(modelBuilder, [configurationInstance]);
                }
            }
        }

    }
}
