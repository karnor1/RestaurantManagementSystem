using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Interface
{
    public interface ITable
    {
        Order activeOrder { get; set; }
        int CurrentOccupiedSeats { get; set; }
        int id { get; set; }
        bool isOccupied { get; set; }
        int TotalSeats { get; set; }
    }
}