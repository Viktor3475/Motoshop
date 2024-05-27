using ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
    public interface IUserManagementService
    {
        Task<UserDTO> CreateUser(UserDTO request);
        Task<List<UserDTO>> GetUsers();
        Task<UserDTO> EditUser(UserDTO user, int id);
        Task<bool> DeleteUser(int id);
        Task<UserDTO> GetUser(int id);

        Task<List<UserDTO>> GetUsersByFName(string search);
    }
}
