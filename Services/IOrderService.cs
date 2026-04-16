using Dto;

namespace Services
{
    public interface IOrderService
    {
        Task<List<DtoOrderIdUserIdDateSumOrderItems?>> GetOrdersUser(int id);
        Task<DtoOrderIdUserIdDateSumOrderItems> AddNewOrder(DtoOrderIdUserIdDateSumOrderItems order);
        Task<DtoOrderIdUserIdDateSumOrderItems?> GetOrderById(int id);
    }
}
