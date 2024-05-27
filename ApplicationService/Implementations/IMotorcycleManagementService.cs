using ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
    public interface IMotorcycleManagementService
    {
        Task<MotorcycleDTO> CreateMotorcycle(MotorcycleDTO request);
        Task<List<MotorcycleDTO>> GetMotorcycles();
        Task<MotorcycleDTO> EditMotorcycle(MotorcycleDTO motorcycle, int id);
        Task<bool> DeleteMotorcycle(int id);
        Task<MotorcycleDTO> GetMotorcycle(int id);

        Task<List<MotorcycleDTO>> GetMotorcyclesByMake(string search);
    }
}
