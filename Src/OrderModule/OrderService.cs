using store.Src.OrderModule.Entity;
using store.Src.OrderModule.Interface;
using System.Collections.Generic;

namespace store.Src.OrderModule
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;


        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public bool saveOrder(Order order)
        {
            bool res = this.orderRepository.saveOrder(order);
            return res;
        }
        public bool saveItem(Item item)
        {
            bool res = this.orderRepository.saveItem(item);
            return res;
        }

        public int getQuantityByProductId(Item item)
        {
            int quantity = this.orderRepository.getQuantityByProductId(item);
            return quantity;
        }
    }
}