using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repository;

namespace Infrastructure.Repositories
{
    public class ContainerRepository : GenericRepository<Container> , IContainerRepository
    {
        public ContainerRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }
    }
}
