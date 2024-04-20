using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Models
{

    public class ServedProducts : Iproducts
    {
        public string Name { get; set; } = "";
        public double Price { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public eProductCategory category { get; set; }
        public DateTime DateAdded { get; set; }
        public double PriceForRestaurant { get; set; }
    }
}
