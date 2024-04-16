
namespace RestaurantManagementSystem
{
    public interface IOrder
    {
        List<Product> _servedProducts { get; set; }
        Table _table { get; set; }
        DateTime Closingtime { get; set; }
        DateTime CreationTime { get; set; }
        bool OrderClosed { get; set; }
        int OrderNumber { get; set; }
        double TotalPrice { get; set; }
    }
}