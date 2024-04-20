using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Interface
{
    public interface Iproduct
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

    }

}
