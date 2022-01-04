using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ContainerRepository : GenericRepository<Container> , IContainerRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ContainerRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Container> GetAllWithVehicle()
        {
            List<Container> cList= dbContext.Containers.Include(x => x.Vehicle).ToList();
            return cList;
        }
    }
}
