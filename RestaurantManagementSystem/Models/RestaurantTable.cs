using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public override string ToString()
        {
            return $"{id} occupation {isOccupied} \n" +
                $"Total Seats {TotalSeats}, Current persons {CurrentOccupiedSeats}";
        }
    }

    public class TableReservation
    {
        public DateTime ReservedFrom { get; set; }
        public string ReservedTo { get; set; }

    }
}
