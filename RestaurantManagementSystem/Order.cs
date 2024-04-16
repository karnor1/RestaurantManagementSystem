using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantManagementSystem
{
    public class Order
    {
        public DateTime CreationTime { get; set; }
        public DateTime Closingtime { get; set; }
        public int OrderNumber { get; set; }
        public Table _table { get; set; }
        public double TotalPrice { get; set; }


        public bool OrderClosed { get; set; } = false;

        public Order()
        {

        }


        public Order(Table _table, int _id)
        {
            this._table = _table;   
            CreationTime = DateTime.Now;
            _servedProducts = new List<ServedProducts>();
            OrderNumber = _id;
        }

        public List<ServedProducts> _servedProducts { get; set; }
    }






    public class ServiceErrors()
    {
        internal bool success = false;
        internal string message = "";
    }
    public interface IOrderDatabaseParser<T>
    {
        List<T> GetData();
        public bool SaveData<T>(T data);

    }
    public class OrderDoesNotExist : Exception
    {
        public OrderDoesNotExist()
        {
        }

        public OrderDoesNotExist(string message)
            : base(message)
        {
        }

        public OrderDoesNotExist(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}

