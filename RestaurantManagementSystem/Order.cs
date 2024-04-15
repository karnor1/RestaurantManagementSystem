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
        public DateTime CreationTime;
        public DateTime Closingtime;
        public int OrderNumber;
        public Table _table;
        public double TotalPrice;


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

        public List<ServedProducts> _servedProducts;
    }




    public class ServedProducts : Iproduct
    {
        public string Name { get; set; } = "";
        public double Price { get; set; } = 0;
        public int Quantity { get; set; } = 0;

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

