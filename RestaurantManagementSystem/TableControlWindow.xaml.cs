using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RestaurantManagementSystem
{
    /// <summary>
    /// Interaction logic for TableControlWindow.xaml
    /// </summary>
    public partial class TableControlWindow : Window
    {
        Table activeTable;
        Order? activeOrder;
        public TableControlWindow(Table activeTable, Order? activeOrder)
        {
            InitializeComponent();
            this.activeTable = activeTable;
            this.activeOrder = activeOrder;
            activeTable.activeOrder = activeOrder;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void UpdateListBox(ListBox _listbox, eProductCategory _category)
        {
            _listbox.Items.Clear();
            List<Product> _product;
            ProductService _service = new ProductService();
            _product = _service.GetProductList<Product>(_category);
            UpdateListBox(productsListBox, _product);

        }


        private void UpdateListBox(ListBox _listbox, List<Product> _product)
        {
            _listbox.Items.Clear();

            foreach (var item in _product)
            {
                _listbox.Items.Add($"{item.Name} {item.Price}");
                _listbox.Items.Add(item);

            }
        }
        public void PrintTableOrders(Table _table)
        {
            if (_table != null)
            {
                ServedProducts_ListBox.Items.Clear();

                if (_table.activeOrder == null)
                {
                    this.ServedProducts_ListBox.Items.Add($"No order from this table");

                }
                else
                {
                    double totalForOrder = 0;
                    foreach (var product in _table.activeOrder._servedProducts)
                    {
                        this.ServedProducts_ListBox.Items.Add($" {product.Name}  : {product.Quantity} Total  {product.Price * product.Quantity} $");
                        totalForOrder += product.Price * product.Quantity;
                    }
                    this.ServedProducts_ListBox.Items.Add($"\n Total Amount Of Order = {totalForOrder}");
                }

            }
        }
        public void PrintTableOrdersFromDB(Table _table)
        {
            if (_table != null)
            {
                ServedProducts_ListBox.Items.Clear();
                OrderManagementService orderManagementService = new OrderManagementService();

                var servedProducts = orderManagementService.GetOrderFromTable(_table);
                if (servedProducts == null)
                {
                    this.ServedProducts_ListBox.Items.Add($"No order from this table");

                }
                else
                {
                    foreach (var product in servedProducts._servedProducts)
                    {
                        this.ServedProducts_ListBox.Items.Add($"Product {product.Name} Quantity : {product.Quantity}");

                    }
                }

            }
        }

        private void ShowProducts_ButtonClicked(object sender, RoutedEventArgs e)
        {
            Button _btn = (Button)sender;
            if (_btn != null)
            {
                if (_btn.Content.Equals("Show Drinks"))
                {
                    UpdateListBox(productsListBox, eProductCategory.Drink);
                }
                else if (_btn.Content.Equals("Show Food"))
                {
                    UpdateListBox(productsListBox, eProductCategory.Food);

                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            PrintTableOrders(activeTable);

            if (activeTable.activeOrder != null)
            {
                this.CloseOrder_Button.IsEnabled = true;
                this.CreateOrder_Button.IsEnabled = false;
            }
            else
            {
                this.CreateOrder_Button.IsEnabled = true; this.CloseOrder_Button.IsEnabled = false;
            }

        }

        private void AddToServedProducts_Button_Click(object sender, RoutedEventArgs e)
        {

            var item = productsListBox.SelectedItem;

            if (item != null)
            {
                if (activeTable.activeOrder != null)
                {
                    if (item is Iproduct product)
                    {
                        OrderManagementService orderManagementService = new OrderManagementService();
                        orderManagementService.AddProductToOrder(product, activeTable.activeOrder.OrderNumber);
                        activeTable.activeOrder = orderManagementService.GetOrderFromTable(activeTable);

                    }
                }
            }
            PrintTableOrders(activeTable);


        }

        private void CreateOrder_Button_Click(object sender, RoutedEventArgs e)
        {
            if (activeTable.activeOrder == null)
            {
                OrderManagementService orderManagementService = new OrderManagementService();
                orderManagementService.CreateOrder(activeTable);
                activeTable.activeOrder = orderManagementService.GetOrderFromTable(activeTable);
                activeTable.isOccupied = true;
                this.CloseOrder_Button.IsEnabled = true;
                this.CreateOrder_Button.IsEnabled = false;
            }
        }


        private void CloseOrder_Button_Click(object sender, RoutedEventArgs e)
        {
            if (activeTable.activeOrder != null)
            {
                OrderManagementService orderManagementService = new OrderManagementService();
                var _receipt = new Receipt(activeTable);
                _receipt.PrintReceiptClient();
                orderManagementService.CloseOrder(activeTable.activeOrder.OrderNumber);

                this.CloseOrder_Button.IsEnabled = false;
                this.CreateOrder_Button.IsEnabled = true;
            }
        }


    }
}
