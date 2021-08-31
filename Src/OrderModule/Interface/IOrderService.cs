using store.Src.OrderModule.Entity;
using System.Collections.Generic;

namespace store.Src.OrderModule.Interface
{
    public interface IOrderService
    {
        public bool saveOrder(Order order);
        public bool saveItem(Item item);
        public int getQuantityByProductId(string productId);
        public string getLastOrderId(string customerId);
    }
}