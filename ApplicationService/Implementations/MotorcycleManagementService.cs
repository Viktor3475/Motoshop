using ApplicationData.Context;
using ApplicationData.Data;
using ApplicationService.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
    public class MotorcycleManagementService : IMotorcycleManagementService
    {
        private readonly MotoShopDBContext _context;

        public MotorcycleManagementService(MotoShopDBContext context)
        {
            _context = context;
        }

        public async Task<MotorcycleDTO> CreateMotorcycle(MotorcycleDTO request)
        {
            await _context.Motorcycles.AddAsync(new()
            {
                Make = request.Make,
                Model = request.Model,
                ManufactureDate = request.ManufactureDate,
                HP = request.HP,
                IsAvailable = request.IsAvailable
            });
            await _context.SaveChangesAsync();
            return new();
        }
        public async Task<List<MotorcycleDTO>> GetMotorcycles()
        {
            List<MotorcycleDTO> response = new List<MotorcycleDTO>();

            var motorcycles = await _context.Motorcycles.ToListAsync();

            foreach (var motorcycle in motorcycles)
            {
                response.Add(new()
                {
                    Id = motorcycle.Id,
                    Make = motorcycle.Make,
                    Model = motorcycle.Model,
                    ManufactureDate = motorcycle.ManufactureDate,
                    HP = motorcycle.HP,
                    IsAvailable = motorcycle.IsAvailable
                });
            }

            return response;
        }
        public async Task<MotorcycleDTO> GetMotorcycle(int id)
        {
            MotorcycleDTO response = new MotorcycleDTO();

            var motorcycle = await _context.Motorcycles.FindAsync(id);
            response.Id = motorcycle.Id;
            response.Make = motorcycle.Make;
            response.Model = motorcycle.Model;
            response.ManufactureDate = motorcycle.ManufactureDate;
            response.HP = motorcycle.HP;
            response.IsAvailable = motorcycle.IsAvailable;

            return response;
        }

        public async Task<MotorcycleDTO> EditMotorcycle(MotorcycleDTO motorcycle, int id)
        {
            
            var findMotor = await _context.Motorcycles.FindAsync(id);
            findMotor.Make = motorcycle.Make;
            findMotor.Model = motorcycle.Model;
            findMotor.ManufactureDate = motorcycle.ManufactureDate;
            findMotor.HP = motorcycle.HP;
            findMotor.IsAvailable = motorcycle.IsAvailable;
            await _context.SaveChangesAsync();

            return motorcycle;
        }

        public async Task<bool> DeleteMotorcycle(int id)
        {
            await _context.Motorcycles.Where(m => m.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MotorcycleDTO>> GetMotorcyclesByMake(string search)
        {
            List<MotorcycleDTO> response = new List<MotorcycleDTO>();

            var motorcycles = await _context.Motorcycles.ToListAsync();

            foreach (var motorcycle in motorcycles)
            {
                if (string.Equals(motorcycle.Make,search, StringComparison.InvariantCultureIgnoreCase))
                {
                    response.Add(new()
                    {
                        Id = motorcycle.Id,
                        Make = motorcycle.Make,
                        Model = motorcycle.Model,
                        ManufactureDate = motorcycle.ManufactureDate,
                        HP = motorcycle.HP,
                        IsAvailable = motorcycle.IsAvailable
                    });
                }
            }

            return response;
        }
    }
}
