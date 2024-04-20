using System.IO;
using System.Windows.Controls;
using RestaurantManagementSystem.Interface;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Services
{
    public class OrderManagementService() : IOrderManagementService
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OrderDataBase.json");
        private DataBaseConnection<Order> connection = new DataBaseConnection<Order>(FilePath);


        internal ServiceErrors AddProductToOrder(Iproduct _product, int _orderNumber)
        {
            Order order;

            ServiceErrors err = new ServiceErrors();

            var _orderList = new List<Order>();

            _orderList = connection.GetData();


            order = _orderList.FirstOrDefault(order => order.OrderNumber == _orderNumber);
            int index = _orderList.FindIndex(order => order.OrderNumber == _orderNumber);


            if (order != null)
            {
                if (order.OrderClosed)
                {
                    err.message = "Order is already closed";
                }
                else
                {
                    if (order._servedProducts != null)
                    {
                        var servedproduct = order._servedProducts.FirstOrDefault(product => product.Name == _product.Name); // product already exists add quantity
                        if (servedproduct == null)
                        {
                            order._servedProducts.Add(new ServedProducts { Name = _product.Name, Price = _product.Price, Quantity = 1 });
                        }
                        else
                        {
                            foreach (var item in order._servedProducts)
                            {
                                if (item.Name == _product.Name)
                                {
                                    item.Quantity++;
                                }

                            }
                        }
                        err.success = true;
                        err.message = "Product added to the order successfuly";

                        if (index != -1)
                        {
                            _orderList[index] = order;
                        }
                        connection.SaveData(_orderList);
                    }

                }
            }
            return err;

        }


        internal ServiceErrors CreateOrder(RestaurantTable _table)
        {
            ServiceErrors err = new ServiceErrors();

            var _orderList = connection.GetData();

            if (_orderList == null)
            {
                _orderList = new List<Order>();
            }
            int largestId;

            if (_orderList.Any())
            {
                largestId = _orderList.Max(order => order.OrderNumber);
            }
            else largestId = 0;

            var _order = new Order(_table, largestId + 1);




            _orderList.Add(_order);
            _table.isOccupied = true;
            connection.SaveData(_orderList);


            return err;
        }

        public List<ServedProducts> GetOrdersFromTable(RestaurantTable _table)
        {
            var _orderList = new List<Order>();
            _orderList = connection.GetData();
            Order order;

            order = _orderList.FirstOrDefault(order => order._table.id == _table.id);
            if (order != null)
            {
                return order._servedProducts;
            }
            else return new List<ServedProducts>();
        }
        public Order GetOrderFromTable(RestaurantTable _table)
        {
            var _orderList = new List<Order>();
            _orderList = connection.GetData();
            Order order;

            order = _orderList.FirstOrDefault(order => order._table.id == _table.id && !order.OrderClosed);

            return order;
        }
        public Order CloseOrder(Order _order)
        {
            Order order = (Order)_order;

            ServiceErrors err = new ServiceErrors();

            var _orderList = new List<Order>();

            _orderList = connection.GetData();


            int index = _orderList.FindIndex(order => order.OrderNumber == order.OrderNumber && !order.OrderClosed);

            order.OrderClosed = true;
            order.Closingtime = DateTime.Now;


            _orderList[index] = order;
            connection.SaveData(_orderList);
            return order;
        }

        public Order CalculateOrderTotals(Order closedOrder)
        {
            Order _closedOrder = closedOrder;

            foreach (var item in _closedOrder._servedProducts)
            {
                _closedOrder.TotalPriceForRestaurant += item.Quantity * item.PriceForRestaurant;
                _closedOrder.TotalPrice += item.Quantity * item.Price;

            }
            _closedOrder.TotalProfitForRestaurant = _closedOrder.TotalPriceForRestaurant - _closedOrder.TotalPrice;
            return _closedOrder;


        }


    }

}

