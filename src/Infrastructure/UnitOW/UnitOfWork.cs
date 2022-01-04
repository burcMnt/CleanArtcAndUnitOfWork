using ApplicationCore.Interfaces.Repository;
using ApplicationCore.Interfaces.UnitOfWork;
using System;

namespace Infrastructure.UnitOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IContainerRepository Containers { get; }
        public IVehicleRepository Vehicles { get; }

        public UnitOfWork(ApplicationDbContext dbContext, IContainerRepository containerRepository, IVehicleRepository vehicleRepository)
        {
            _dbContext = dbContext;
            Containers = containerRepository;
            Vehicles = vehicleRepository;
        }


        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
