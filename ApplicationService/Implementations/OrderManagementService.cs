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
    public class OrderManagementService : IOrderManagementService
    {
        private readonly MotoShopDBContext _context;

        public OrderManagementService(MotoShopDBContext context)
        {
            _context = context;
        }
        public async Task<OrderDTO> CreateOrder(OrderDTO request)
        {
            await _context.Orders.AddAsync(new()
            {
                MotorcycleId = request.MotorcycleId,
                Motorcycle = request.Motorcycle,
                UserId = request.UserId,
                User = request.User,
                OrderName = request.OrderName,
                OrderDate = request.OrderDate
            });
            await _context.SaveChangesAsync();
            return new();
        }
        public async Task<List<OrderDTO>> GetOrders()
        {
            List<OrderDTO> response = new List<OrderDTO>();

            var orders = await _context.Orders.Include(m => m.Motorcycle).Include(m => m.User).ToListAsync();

            foreach (var order in orders)
            {
                response.Add(new()
                {
                    Id = order.Id,
                    MotorcycleId = order.MotorcycleId,
                    Motorcycle = order.Motorcycle,
                    UserId = order.UserId,
                    User = order.User,
                    OrderName = order.OrderName,
                    OrderDate = order.OrderDate
                });
            }

            return response;
        }
        public async Task<OrderDTO> GetOrder(int id)
        {
            OrderDTO response = new OrderDTO();

            var order = await _context.Orders.Include(o => o.User).Include(o => o.Motorcycle).FirstOrDefaultAsync(o => o.Id == id);
            response.Id = order.Id;
            response.MotorcycleId = order.MotorcycleId;
            response.UserId = order.UserId;
            response.User = order.User;
            response.Motorcycle = order.Motorcycle;
            response.User.FName = order.User.FName; 
            response.Motorcycle.Make = order.Motorcycle.Make;
            response.OrderName = order.OrderName;
            response.OrderDate = order.OrderDate;

            return response;
        }
        public async Task<OrderDTO> EditOrder(OrderDTO order, int id)
        {
            var findOrder = await _context.Orders.FindAsync(id);
            findOrder.UserId = order.UserId;
            findOrder.MotorcycleId = order.MotorcycleId;
            findOrder.OrderName = order.OrderName;
            findOrder.OrderDate = order.OrderDate;
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            await _context.Orders.Where(m => m.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<OrderDTO>> GetOrdersByOrderName(string search)
        {
            List<OrderDTO> response = new List<OrderDTO>();

            var orders = await _context.Orders.Include(m => m.Motorcycle).Include(m => m.User).ToListAsync();

            foreach (var order in orders)
            {
                if (string.Equals(order.OrderName, search, StringComparison.InvariantCultureIgnoreCase))
                {
                    response.Add(new()
                    {
                        Id = order.Id,
                        MotorcycleId = order.MotorcycleId,
                        Motorcycle = order.Motorcycle,
                        UserId = order.UserId,
                        User = order.User,
                        OrderName = order.OrderName,
                        OrderDate = order.OrderDate
                                           
                    });
                }
            }

            return response;
        }


    }
}
