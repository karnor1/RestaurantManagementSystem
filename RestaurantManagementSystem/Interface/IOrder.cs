using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Interface
{
    public interface IOrder
    {
        List<ServedProducts> _servedProducts { get; set; }
        RestaurantTable _table { get; set; }
        DateTime Closingtime { get; set; }
        DateTime CreationTime { get; set; }
        bool OrderClosed { get; set; }
        int OrderNumber { get; set; }
        double TotalPrice { get; set; }
        public double TotalPriceForRestaurant { get; set; }
        public double TotalProfitForRestaurant { get; set; }
        public double TotalVAT { get; set; }

    }



    public class ClosedOrder : IOrder
    {
        public List<ServedProducts> _servedProducts { get; set; }
        public RestaurantTable _table { get; set; }
        public DateTime Closingtime { get; set; }
        public DateTime CreationTime { get; set; }
        public bool OrderClosed { get; set; }
        public int OrderNumber { get; set; }
        public double TotalPrice { get; set; }
        public double TotalPriceForRestaurant { get; set; }
        public double TotalProfitForRestaurant { get; set; }
        public double TotalVAT { get; set; }

        public ClosedOrder(IOrder order)
        {
            _servedProducts = order._servedProducts;
            _table = order._table;
            Closingtime = order.Closingtime;
            CreationTime = order.CreationTime;
            OrderClosed = order.OrderClosed;
            OrderNumber = order.OrderNumber;
            TotalPrice = order.TotalPrice;
            TotalPriceForRestaurant = 0.0;
            TotalProfitForRestaurant = 0.0;
            TotalVAT = 0.0;
        }
    }


}