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
using System.IO;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Services;
using RestaurantManagementSystem.Interface;

namespace RestaurantManagementSystem
{
    /// <summary>
    /// Interaction logic for TableControlWindow.xaml
    /// </summary>
    public partial class TableControlWindow : Window
    {
        RestaurantTable activeTable;
        Order? activeOrder;
        TableManagementService _tableService = new TableManagementService();


        IOrderManagementService orderManagementService = new OrderManagementService();

        public TableControlWindow(RestaurantTable activeTable)
        {
            InitializeComponent();
            this.activeTable = activeTable;
            this.activeOrder = activeTable.activeOrder;
 
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
                _listbox.Items.Add(item);

            }
        }

        public void PrintTableOrders(RestaurantTable _table)
        {
            if (_table != null)
            {
                ServedProducts_ListBox.Items.Clear();

                if (_table.activeOrder == null)
                {
                    this.ServedProducts_ListBox.Items.Add($"No order from this table");
                    this.ServedProducts_ListBox.IsEnabled = false; ;

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
                this.PeopleCount_TextBox.Text = activeTable.CurrentOccupiedSeats.ToString();
                this.TableInfo_Label.Content = activeTable.ToString();
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
                    if (item is Product product)
                    {
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

                try
                {
                    activeTable.CurrentOccupiedSeats =(int)UInt16.Parse(this.PeopleCount_TextBox.Text);

                    if (activeTable.TotalSeats < activeTable.CurrentOccupiedSeats)
                    {
                        activeTable.CurrentOccupiedSeats = 0;
                        throw new ArgumentException();
                    }

                }
                catch (ArgumentException ex)
                {
                    this.PeopleCount_TextBox.Background = new SolidColorBrush(Color.FromRgb(200, 0, 0));
                    ErrorWindow errorWindow = new ErrorWindow(null, null);
                    errorWindow.ErrorLabel.Content = $"This table seats capacity is lower \n than current persons at the table \n max seats capacity {activeTable.TotalSeats} ";
                    errorWindow.Show();

                    return;
                }
                catch (Exception ex)
                {
                    this.PeopleCount_TextBox.Background = new SolidColorBrush(Color.FromRgb(200, 0, 0));
                    ErrorWindow errorWindow = new ErrorWindow(null, null);
                    errorWindow.ErrorLabel.Content =  "Please check current people at the table input \n "+ex.Message ;
                    errorWindow.Show();

                    return;
                }

                orderManagementService.CreateOrder(activeTable);
                activeTable.activeOrder = orderManagementService.GetOrderFromTable(activeTable);
                
                this.CloseOrder_Button.IsEnabled = true;
                this.CreateOrder_Button.IsEnabled = false;
                _tableService.UpdateTableInfo(activeTable);
            }
        }


        private void CloseOrder_Button_Click(object sender, RoutedEventArgs e)
        {
            if (activeTable.activeOrder != null)
            {

                var _closedOrder = orderManagementService.CalculateOrderTotals(activeTable.activeOrder);

                _closedOrder = orderManagementService.CloseOrder(_closedOrder);
                activeTable.activeOrder = (Order)_closedOrder;

               var _receipt = new Receipt(_closedOrder);

                ErrorWindow ReceiptAsk = new ErrorWindow("yes", "No");
                ReceiptAsk.ErrorLabel.Content = "Do client need a receipt?";
                bool? result = ReceiptAsk.ShowDialog();

                if (result == true && ReceiptAsk.ButtonPressed.Equals("yes"))
                {
                    ReceiptWindow receiptWindow = new ReceiptWindow();
                    receiptWindow.Receipt_Label.Content = _receipt.PrintReceiptClient();
                    receiptWindow.Show();
                }

                _receipt.SaveToDataBase();
                activeTable.activeOrder = null;
                activeTable.isOccupied = false;
                _tableService.UpdateTableInfo(activeTable);
                ServedProducts_ListBox.Items.Clear();


                
                this.CloseOrder_Button.IsEnabled = false;
                this.CreateOrder_Button.IsEnabled = true;
            }
        }

        private void PeopleCount_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.PeopleCount_TextBox.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

        }
    }
}
