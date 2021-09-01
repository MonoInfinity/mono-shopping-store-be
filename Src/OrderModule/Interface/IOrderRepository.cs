using store.Src.OrderModule.Entity;


namespace store.Src.OrderModule.Interface
{
    public interface IOrderRepository
    {
        public bool saveOrder(Order order);
        public bool saveItem(Item item);
        public int getQuantityByProductId(string productdId);
        public string getLastOrderId(string customerId);
        public bool decreaseQuantity(string productId, int quantity);
    }
}