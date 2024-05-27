using ApplicationData.Context;
using ApplicationData.Data;
using ApplicationService.DTOs;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
    public class UserManagementService : IUserManagementService
    {
        private readonly MotoShopDBContext _context;

        public UserManagementService(MotoShopDBContext context)
        {
            _context = context;
        }

        public async Task<UserDTO> CreateUser(UserDTO request)
        {
            await _context.Users.AddAsync(new()
            {
                FName = request.FName,
                LName = request.LName,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            });
            await _context.SaveChangesAsync();
            return new();
        }
        public async Task<List<UserDTO>> GetUsers()
        {
            List<UserDTO> response = new List<UserDTO>();

            var users = await _context.Users.ToListAsync();

            foreach (var user in users)
            {
                response.Add(new()
                {
                    Id = user.Id,
                    FName = user.FName,
                    LName = user.LName,
                    Gender = user.Gender,
                    BirthDate = user.BirthDate,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                });
            }

            return response;
        }
        public async Task<UserDTO> GetUser(int id)
        {
            UserDTO response = new UserDTO();

            var user = await _context.Users.FindAsync(id);
            response.Id = user.Id;
            response.FName = user.FName;
            response.LName = user.LName;
            response.Gender = user.Gender;
            response.BirthDate = user.BirthDate;
            response.Email = user.Email;
            response.PhoneNumber = user.PhoneNumber;

            return response;
        }

        public async Task<UserDTO> EditUser(UserDTO user, int id)
        {
            var findUser = await _context.Users.FindAsync(id);
            findUser.FName = user.FName;
            findUser.LName = user.LName;
            findUser.Gender = user.Gender;
            findUser.BirthDate = user.BirthDate;
            findUser.Email = user.Email;
            findUser.PhoneNumber = user.PhoneNumber;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            await _context.Users.Where(m => m.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserDTO>> GetUsersByFName(string search)
        {
            List<UserDTO> response = new List<UserDTO>();

            var users = await _context.Users.ToListAsync();

            foreach (var user in users)
            {
                if (string.Equals(user.FName, search, StringComparison.InvariantCultureIgnoreCase))
                {
                    response.Add(new()
                    {
                        Id = user.Id,
                        FName = user.FName,
                        LName = user.LName,
                        Gender = user.Gender,
                        BirthDate = user.BirthDate,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    });
                }
            }

            return response;
        }
    }
}
