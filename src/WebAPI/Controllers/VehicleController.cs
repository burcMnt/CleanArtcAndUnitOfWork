using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repository;
using ApplicationCore.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.DTOs.VehicleDto;


namespace WebAPI.Controllers
{
    [Route("api/v1/Vehicles")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehicleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //You can use the HttpGet request to take all vehicle list
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok( await _unitOfWork.Vehicles.GetAllAsync());
        }

        //You can use this HttpGet request with id  to get just one vehicle object.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            Vehicle vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return await Task.FromResult(Ok(vehicle));
        }

        [Route("VehicleWithContainers")]
        [HttpGet]
        public async Task<IActionResult> GetVehicleWithContainer(int id)
        {
            Vehicle vehicle = await _unitOfWork.Vehicles.GetVehicleWithContainerAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }

        //You can use this HttpPost request to create new vehicle's object.
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleDto vehicleCreateDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            Vehicle vehc = new()
            {
                VehicleName = vehicleCreateDto.VehicleName,
                VehiclePlate = vehicleCreateDto.VehiclePlate
            };
            await _unitOfWork.Vehicles.AddAsync(vehc);
            _unitOfWork.Complete();

            return CreatedAtAction("GetVehicle", new { Id = vehc.Id }, vehicleCreateDto);
        }

        //You can use this HttpPut request to update vehicle's object already have with using unique id.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle([FromRoute] int id, [FromBody] VehicleUpdateDto vehicleUpdateDto)
        {
            Vehicle vehicle = new()
            {
                Id = vehicleUpdateDto.Id,
                VehicleName = vehicleUpdateDto.VehicleName,
                VehiclePlate = vehicleUpdateDto.VehiclePlate
            };

            if (ModelState.IsValid)
            {
                if (id != vehicle.Id)
                {
                    return BadRequest("Id information is not confirmed");
                }

                await _unitOfWork.Vehicles.UpdateAsync(vehicle);
                _unitOfWork.Complete();
                return Ok("Vehicle Updated");
            }
            return BadRequest(ModelState);

        }

        //You can use this HttpDelete request to delete vehicle's object already have with using unique id.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            Vehicle vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            await _unitOfWork.Vehicles.DeleteAsync(vehicle);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}
