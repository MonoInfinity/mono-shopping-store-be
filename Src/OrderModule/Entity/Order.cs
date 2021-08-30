using System;
using store.Src.UserModule.Entity;
namespace store.Src.OrderModule.Entity
{

    public enum OrderStatus
    {
        NOT_PAID = 0,
        PAID = 1
    }
    public enum OrderPaymentMethod
    {
        CASH = 1,
        CREDIT = 2,
        E_WALLET = 3,
        COD = 4
    }
    public class Order
    {
        public string orderId { get; set; }
        public double total { get; set; }
        public string createDate { get; set; }
        public OrderStatus status { get; set; }
        public OrderPaymentMethod paymentMethod { get; set; }
        public User customer { get; set; }
        public User casher { get; set; }
        public int isRetail { get; set; }

        public Order()
        {
            this.orderId = "";
            this.total = 0;
            this.createDate = DateTime.Now.ToLongDateString();
            this.status = OrderStatus.NOT_PAID;
            this.paymentMethod = OrderPaymentMethod.CASH;
            this.customer = new User();
            this.casher = new User();
            this.isRetail = 0;
        }

        public override string ToString()
        {
            return "Order: \nOrderId: " + orderId + " \ntotal: " + total + " \ncreateDate: " + createDate + " \nStatus: " + status + " \nPaymentMethod: " +
                            paymentMethod + " \nCustomer: " + customer + " \nCasher: " + casher + " \nIsRetail: " + isRetail;
        }
    }


}