using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem
{
    public class Table
    {
        public Table (int buttCountTotal, int buttCountCurrent, int id)
        {
            this.TotalSeats = buttCountTotal;
            this.CurrentOccupiedSeats = buttCountCurrent;
            this.id = id;

        }

        public int id { get; set; }
        public bool isOccupied = false;
        public int TotalSeats = 0;
        public int CurrentOccupiedSeats = 0;
    }

    public class TableReservation
    {
        public DateTime ReservedFrom { get; set; }
        public string ReservedTo { get; set; }

    }
}
