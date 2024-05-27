using ApplicationService.DTOs;
using ApplicationService.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IUserManagementService _userService;

        public UsersController(IUserManagementService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult> Post(UserDTO user)
        {
            return Ok(await _userService.CreateUser(user));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            return Ok(await _userService.GetUsers());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _userService.GetUser(id));
        }

        [HttpGet("search/{searchByFName}")]
        public async Task<ActionResult> GetByFName(string searchByFName)
        {
            return Ok(await _userService.GetUsersByFName(searchByFName));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(UserDTO vehicle, int id)
        {
            return Ok(await _userService.EditUser(vehicle, id));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _userService.DeleteUser(id));
        }


    }
}
