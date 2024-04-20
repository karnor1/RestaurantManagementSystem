using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Interface
{
    public interface IOrderManagementService
    {
        Order CalculateOrderTotals(Order closedOrder);
        Order CloseOrder(Order _order);
        Order GetOrderFromTable(RestaurantTable _table);
        List<ServedProducts> GetOrdersFromTable(RestaurantTable _table);
    }
}