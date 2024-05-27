using ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
    public interface IOrderManagementService
    {
        Task<OrderDTO> CreateOrder(OrderDTO request);
        Task<List<OrderDTO>> GetOrders();
        Task<OrderDTO> EditOrder(OrderDTO order, int id);
        Task<bool> DeleteOrder(int id);
        Task<OrderDTO> GetOrder(int id);
        Task<List<OrderDTO>> GetOrdersByOrderName(string search);
    }
}
