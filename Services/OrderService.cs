using AutoMapper;
using Dto;
using Microsoft.Extensions.Logging;
using Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, ILogger<OrderService> logger)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<List<DtoOrderIdUserIdDateSumOrderItems?>> GetOrdersUser(int id)
        {
            var orders = await _orderRepository.GetOrdersUser(id);
            var orderDtos = _mapper.Map<List<Order>, List<DtoOrderIdUserIdDateSumOrderItems>>(orders);
            return orderDtos;
        }

        public async Task<DtoOrderIdUserIdDateSumOrderItems?> GetOrderById(int id)
        {
            Order order = await _orderRepository.GetOrderById(id);
            return _mapper.Map<Order, DtoOrderIdUserIdDateSumOrderItems>(order);

        }

        public async Task<DtoOrderIdUserIdDateSumOrderItems> AddNewOrder(DtoOrderIdUserIdDateSumOrderItems order)
        {
            var calculatedTotal = order.OrderItems.Sum(item => item.PriceAtPurchase * item.Quantity);
            if (order.TotalPrice != calculatedTotal)
            {
                _logger.LogWarning("Order sum mismatch. UserId: {UserId}, SentTotalPrice: {SentTotalPrice}, CalculatedTotalPrice: {CalculatedTotalPrice}",
                    order.UserId,
                    order.TotalPrice,
                    calculatedTotal);
            }

            var mappedOrder = _mapper.Map<DtoOrderIdUserIdDateSumOrderItems, Order>(order);
            Order savedOrder = await _orderRepository.AddNewOrder(mappedOrder);
            var orderDto = _mapper.Map<Order, DtoOrderIdUserIdDateSumOrderItems>(savedOrder);
            return orderDto;
        }
    }
}

