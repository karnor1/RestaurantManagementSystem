using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Interface
{
    public interface IOrderManagementService
    {
        Order CalculateOrderTotals(Order closedOrder);
        Order CloseOrder(Order _order);
        Order GetOrderFromTable(RestaurantTable _table);
        List<ServedProducts> GetOrdersFromTable(RestaurantTable _table);
        ServiceErrors AddProductToOrder(Product _product, int _orderNumber);
        ServiceErrors CreateOrder(RestaurantTable _table);

    }
}