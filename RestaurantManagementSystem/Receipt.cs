using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem
{
    class Receipt : ClientReceipt, IReceipt
    {
        public Receipt(ITable _table) : base(_table)
        {

        }

        public string PrintReceiptRestaurant()
        {
            return "";
        }




    }

    public class ClientReceipt
    {
        public ITable _table;
        public ClientReceipt(ITable _table)
        {
            this._table = _table;
        }

        public string PrintReceiptClient()
        {
            var receipt = new StringBuilder();
            receipt.AppendLine("--------- RECEIPT ----------");
            receipt.AppendLine($"Table ID {_table.id} \n Table person capacity {_table.TotalSeats} \n Actual perons at the table {_table.CurrentOccupiedSeats}");
            foreach (var product in _table.activeOrder._servedProducts)
            {
                receipt.AppendLine($" {product.Name} * {product.Quantity}");

            }
            return receipt.ToString();
        }



    }
    public interface IReceipt
    {
        public string PrintReceiptClient();
        public string PrintReceiptRestaurant();
    }
}

