using ApplicationCore.Entities;
using ApplicationCore.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.DTOs.ContainerDto;

namespace WebAPI.Controllers
{
    [Route("api/v1/Containers")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContainerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //You can use the HttpGet request to take all Container list
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.Containers.GetAllAsync());
        }

        //You can use this HttpGet request with id  to get just one Container object.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContainer(int id)
        {
            Container container = await _unitOfWork.Containers.GetByIdAsync(id);
            if (container == null)
            {
                return NotFound();
            }
            return await Task.FromResult(Ok(container));
        }

        //You can use this HttpPost request to create new container's object.
        [HttpPost]
        public async Task<IActionResult> CreateContainer([FromBody] CreateContainerDto containerDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            Container container = new()
            {
                ContainerName=containerDto.ContainerName,
                Latitude=containerDto.Latitude,
                Longitude=containerDto.Longitude,
                VehicleId=containerDto.VehicleId,
            };
            await _unitOfWork.Containers.AddAsync(container);
            _unitOfWork.Complete();

            return CreatedAtAction("GetContainer", new { Id = container.Id }, containerDto);
        }

        //You can use this HttpPut request to update container's object already have with using unique id.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContainer([FromRoute] int id, [FromBody] UpdateContainerDto updateContainerDto)
        {

            if (ModelState.IsValid)
            {
                Container container = new()
                {
                    Id = updateContainerDto.Id,
                    ContainerName = updateContainerDto.ContainerName,
                    Latitude = updateContainerDto.Latitude,
                    Longitude = updateContainerDto.Longitude
                };

                if (id != container.Id)
                {
                    return BadRequest("Id information is not confirmed");
                }

                await _unitOfWork.Containers.UpdateAsync(container);
                _unitOfWork.Complete();
                return Ok("Container Updated");
            }
            return BadRequest(ModelState);

        }

        //You can use this HttpDelete request to delete container's object already have with using unique id.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContainer(int id)
        {
            Container container = await _unitOfWork.Containers.GetByIdAsync(id);
            if (container == null)
            {
                return NotFound();
            }

            await _unitOfWork.Containers.DeleteAsync(container);
            _unitOfWork.Complete();
            return NoContent();
        }

        [Route("GetContanerClusters")]
        [HttpGet]
        public async Task<IActionResult> GetClusters(int id, int N)
        {
            return Ok(await _unitOfWork.Containers.GetVehicleWithContainerClusterAsync(id, N));
        }
    }
}
