using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagementSystem.Interface;

namespace RestaurantManagementSystem.Models
{
    public class RestaurantTable
    {
        public RestaurantTable(int buttCountTotal, int buttCountCurrent, int id)
        {
            TotalSeats = buttCountTotal;
            CurrentOccupiedSeats = buttCountCurrent;
            this.id = id;
        }

        public int id { get; set; }
        public bool isOccupied { get; set; } = false;
        public int TotalSeats { get; set; } = 0;
        public int CurrentOccupiedSeats { get; set; } = 0;
        public Order activeOrder { get; set; }


    }

    public class TableReservation
    {
        public DateTime ReservedFrom { get; set; }
        public string ReservedTo { get; set; }

    }
}
