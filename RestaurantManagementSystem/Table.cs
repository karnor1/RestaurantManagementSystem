using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem
{
    public class Table : ITable
    {
        public Table(int buttCountTotal, int buttCountCurrent, int id)
        {
            this.TotalSeats = buttCountTotal;
            this.CurrentOccupiedSeats = buttCountCurrent;
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
