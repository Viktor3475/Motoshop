using ApplicationService.DTOs;
using ApplicationService.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorcyclesController : ControllerBase
    {
        private readonly IMotorcycleManagementService _motorcycleService;

        public MotorcyclesController(IMotorcycleManagementService motorcycleService)
        {
            _motorcycleService = motorcycleService;
        }
        [HttpPost]
        public async Task<ActionResult> Post(MotorcycleDTO vehicle)
        {
            return Ok(await _motorcycleService.CreateMotorcycle(vehicle));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotorcycleDTO>>> Get()
        {
            return Ok(await _motorcycleService.GetMotorcycles());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
             return Ok(await _motorcycleService.GetMotorcycle(id));
        }
        [HttpGet("search/{searchByMake}")]
        public async Task<ActionResult> GetByMake(string searchByMake)
        {
            return Ok(await _motorcycleService.GetMotorcyclesByMake(searchByMake));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(MotorcycleDTO vehicle, int id)
        {
            return Ok(await _motorcycleService.EditMotorcycle(vehicle, id));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _motorcycleService.DeleteMotorcycle(id));
        }


    }
}
