using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RestaurantManagementSystem.Services;
using RestaurantManagementSystem.Interface;
namespace RestaurantManagementSystem.Models
{
    class Receipt : ClientReceipt, IReceipt
    {
        //private static readonly string ClientsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReceiptDatabase.json");
        private static readonly string RestaurantFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReceiptDatabase.json");

        //private DataBaseConnection<string> clientReceiptDatabase = new DataBaseConnection<string>(ClientsFilePath);
        private DataBaseConnection<string> RestaurantReceiptDatabase = new DataBaseConnection<string>(RestaurantFilePath);


        public Receipt(Order order) : base(order)
        {

        }

        public string PrintReceiptRestaurant()
        {
            var receipt = new StringBuilder();
            receipt.Append(PrintReceiptClient());
            receipt.AppendLine($"Total price for restaurant {_order.TotalPriceForRestaurant}");
            receipt.AppendLine($"Total profit {_order.TotalProfitForRestaurant}");
            receipt.AppendLine($"Total VAT {_order.TotalVAT}");
            receipt.AppendLine($"Open time {_order.CreationTime}");
            receipt.AppendLine($"Closing time {_order.Closingtime}");
            return receipt.ToString();
        }

        public void SaveToDataBase()
        {
            RestaurantReceiptDatabase.SaveReceipt(PrintReceiptRestaurant());
        }
    }

    public class ClientReceipt
    {
        public Order _order;
        public ClientReceipt(Order order)
        {
            _order = order;
        }

        public string PrintReceiptClient()
        {
            var receipt = new StringBuilder();
            receipt.AppendLine("--------- RECEIPT ----------");
            receipt.AppendLine($"Table ID {_order._table.id} \n Table person capacity {_order._table.TotalSeats} \n Actual perons at the table {_order._table.CurrentOccupiedSeats}");
            foreach (var product in _order._servedProducts)
            {
                receipt.AppendLine($" {product.Name} {product.Quantity}, {product.Price} = {product.Price * product.Quantity}");
            }
            receipt.AppendLine($"Total price {_order.TotalPrice}");

            return receipt.ToString();

        }



    }
    public interface IReceipt
    {
        public string PrintReceiptClient();
        public string PrintReceiptRestaurant();
        public void SaveToDataBase();
    }
}

