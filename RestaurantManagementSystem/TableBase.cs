namespace RestaurantManagementSystem
{
    public class TableBase
    {
        public int CurrentOccupiedSeats = 0;
        public bool isOccupied = false;
        public int TotalSeats = 0;
        public Order activeOrder { get; set; }

        public int id { get; set; }
    }
}