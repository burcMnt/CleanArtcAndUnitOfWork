using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Repository
{
    public interface IContainerRepository :IAsyncGenericRepository<Container>
    {
        Task<List<Container>> GetAllWithVehicle();
        Task<List<Container[]>> GetVehicleWithContainerClusterAsync(int id, int N);
    }
}
