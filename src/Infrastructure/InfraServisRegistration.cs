using ApplicationCore.Interfaces.Repository;
using ApplicationCore.Interfaces.UnitOfWork;
using Infrastructure.Repositories;
using Infrastructure.UnitOW;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfraServisRegistration
    {
        public static void AddInfraServices(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddScoped(typeof(IAsyncGenericRepository<>), typeof(GenericRepository<>));
            serviceCollection.AddScoped<IContainerRepository, ContainerRepository>();
            serviceCollection.AddScoped<IVehicleRepository, VehicleRepository>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            
        }
    }
}
