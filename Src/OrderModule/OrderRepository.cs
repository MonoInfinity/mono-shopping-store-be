using System;
using System.Data.SqlClient;
using store.Src.OrderModule.Entity;
using store.Src.OrderModule.Interface;
using store.Src.Utils.Interface;
using System.Data;

namespace store.Src.OrderModule
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDBHelper dBHelper;
        public OrderRepository(IDBHelper dBHelper)
        {
            this.dBHelper = dBHelper;
        }

        public bool saveOrder(Order order)
        {

            SqlConnection connection = this.dBHelper.getDBConnection();
            bool res = false;
            string sql = "INSERT INTO tblOrder (orderId, total, createDate, status, paymentMethod, customerId, casherId, isRetail) " +
            " VALUES(@orderId, @total, @createDate, @status, @paymentMethod, @customerId, @casherId, @isRetail)";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@orderId", order.orderId);
                command.Parameters.AddWithValue("@total", order.total);
                command.Parameters.AddWithValue("@createDate", order.createDate);
                command.Parameters.AddWithValue("@status", order.status);
                command.Parameters.AddWithValue("@paymentMethod", order.paymentMethod);
                command.Parameters.AddWithValue("@customerId", order.customer.userId);
                command.Parameters.AddWithValue("@casherId", order.casher.userId);
                command.Parameters.AddWithValue("@isRetail", order.isRetail);


                res = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return res;

        }

        public int getQuantityByProductId(string productId)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            int itemQuantity = 0;
            string sql = "SELECT quantity FROM tblProduct " +
            " WHERE productId = @productId";
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@productId", productId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    itemQuantity = reader.GetInt32("quantity");
                }
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return itemQuantity;
        }
        public bool saveItem(Item item)
        {

            SqlConnection connection = this.dBHelper.getDBConnection();
            bool res = false;
            string sql = "INSERT INTO tblItem (itemId, quantity, salePrice, createDate, productId, orderID) " +
            " VALUES(@itemId, @quantity, @salePrice, @createDate, @productId, @orderId)";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", item.itemId);
                command.Parameters.AddWithValue("@quantity", item.quantity);
                command.Parameters.AddWithValue("@salePrice", item.salePrice);
                command.Parameters.AddWithValue("@createDate", item.createDate);
                command.Parameters.AddWithValue("@productId", item.product.productId);
                command.Parameters.AddWithValue("@orderId", item.order.orderId);

                res = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return res;

        }

        public string getLastOrderId(string customerId)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            string orderId = "";
            string sql = "SELECT TOP 1 orderId FROM tblOrder WHERE customerId=@customerId ORDER BY count desc";
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@customerId", customerId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    orderId = reader.GetString("orderId");
                }
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return orderId;
        }

        public bool decreaseQuantity(string productId, int quantity)
        {

            SqlConnection connection = this.dBHelper.getDBConnection();
            bool res = false;
            string sql = "UPDATE tblProduct SET quantity=quantity-@quantity WHERE productId=@productId";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@productId", productId);
                res = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return res;

        }
    }
}