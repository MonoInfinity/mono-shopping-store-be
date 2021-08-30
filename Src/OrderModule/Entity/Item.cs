using System;
using store.Src.ProductModule.Entity;
namespace store.Src.OrderModule.Entity
{
    public class Item
    {

        public string itemId { get; set; }
        public int quantity { get; set; }
        public double salePrice { get; set; }
        public string createDate { get; set; }
        public Product product { get; set; }
        public Order order { get; set; }
        public Item()
        {
            this.itemId = "";
            this.quantity = 0;
            this.salePrice = 0;
            this.createDate = DateTime.Now.ToLongDateString();
            this.product = new Product();
            this.order = new Order();
        }

        public override string ToString()
        {
            return "Item: \nItemId: " + itemId + " \nQuantity: " + quantity + " \nSalePrice: " + salePrice + " \ncreateDate: " + createDate + " \nProductId: " +
                            product.productId + "\nOderId" + order.orderId;
        }


    }
}