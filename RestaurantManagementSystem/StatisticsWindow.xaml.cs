using System.Windows;
using RestaurantManagementSystem.Interface;
using RestaurantManagementSystem.Models;


namespace RestaurantManagementSystem
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        IOrderManagementService _orderManagementsService;
        IProductService _productManagementService;
        public StatisticsWindow(IOrderManagementService _orderService, IProductService _productService)
        {
            InitializeComponent();
            _productManagementService = _productService;
            _orderManagementsService = _orderService;
            Statistics_ListBox.Visibility = Visibility.Hidden;
            Statistics_Label.Visibility = Visibility.Hidden;

        }

        private void ShowFinancials_Click(object sender, RoutedEventArgs e)
        {
            Statistics_ListBox.Visibility = Visibility.Hidden;

            Statistics_Label.Visibility = Visibility.Visible;
            DateTime? dateTimeFrom = DateFrom.SelectedDate;
            DateTime? dateTimeTo = DateTo.SelectedDate;

            if (dateTimeFrom == null || dateTimeTo == null)
            {
                MessageBox.Show("Date From-To must be selected");
                return;
            }


            var orderList = _orderManagementsService.GetOrdersFromDB();
            Statistics_Label.Content = "Financials for given period \n";
            int count = 0;

            float totalIncome = 0;
            float totalProfit = 0;
            float totalVAT = 0;


            if (orderList.Count != 0)
            {
                foreach (var order in orderList)
                {
                    if ((order.Closingtime > dateTimeFrom) && order.Closingtime < dateTimeTo)
                    {
                        totalIncome += (float)order.TotalPrice;
                        totalProfit += (float)(order.TotalPrice - order.TotalPriceForRestaurant);
                        totalVAT += (float)order.TotalVAT;
                        count++;
                    }
                }

                Statistics_Label.Content += "Total Income : " + totalIncome.ToString("F2") + " $ \n";
                Statistics_Label.Content += "Total Profit including VAT : " + totalProfit.ToString("F2") + " $ \n";
                Statistics_Label.Content += "Total VAT : " + totalVAT.ToString("F2") + " $ \n";
                Statistics_Label.Content += "Total Profit without VAT : " + (totalProfit - totalVAT).ToString("F2") + " $ \n";


            }
            else Statistics_Label.Content = "No orders";


        }

        private void ShowTableOccupancy_Click_1(object sender, RoutedEventArgs e)
        {
            Statistics_ListBox.Visibility = Visibility.Hidden;


            Statistics_Label.Visibility = Visibility.Visible;

            float averageOccupation = 0;
            DateTime? dateTimeFrom = DateFrom.SelectedDate;
            DateTime? dateTimeTo = DateTo.SelectedDate;

            if (dateTimeFrom == null || dateTimeTo == null)
            {
                MessageBox.Show("Date From-To must be selected");
                return;
            }

            var orderList = _orderManagementsService.GetOrdersFromDB();
            Statistics_Label.Content = "Average table occupation for given period \n";
            int count = 0;
            float averageFreeSeats = 0;

            if (orderList.Count != 0)
            {

                foreach (var order in orderList)
                {
                    if ((order.Closingtime > dateTimeFrom) && order.Closingtime < dateTimeTo)
                    {

                        averageOccupation += (order._table.CurrentOccupiedSeats * 100) / order._table.TotalSeats;
                        averageFreeSeats += order._table.TotalSeats - order._table.CurrentOccupiedSeats;


                        count++;
                    }
                }

                if (averageOccupation == 0)
                {
                    Statistics_Label.Content = "No orders";
                    return;
                }
                Statistics_Label.Content += "Average occupation : "+(averageOccupation / count).ToString("F2") + " % \n";
                Statistics_Label.Content += "Average free seats : "+ (averageFreeSeats / count).ToString("F1") ;

            }
            else Statistics_Label.Content = "No orders";
        }

        private void ShowNewProducts_Click_2(object sender, RoutedEventArgs e)
        {
            Statistics_ListBox.Items.Clear();
            Statistics_ListBox.Visibility = Visibility.Visible;

            DateTime? dateTimeFrom = DateFrom.SelectedDate;
            DateTime? dateTimeTo = DateTo.SelectedDate;

            if (dateTimeFrom == null || dateTimeTo == null)
            {
                MessageBox.Show("Date From-To must be selected");
                return;
            }

            var productList = _productManagementService.GetProductList<Product>(eProductCategory.Drink);
            productList.AddRange(_productManagementService.GetProductList<Product>(eProductCategory.Food));


            if (productList.Count != 0)
            {

                foreach (var product in productList)
                {
                    if ((product.DateAdded > dateTimeFrom) && product.DateAdded < dateTimeTo)
                    {
                        Statistics_ListBox.Items.Add(product + " added "+product.DateAdded.ToShortDateString());
                    }
                }
            }
            else Statistics_ListBox.Items.Add("No orders");
        }

    
    }
}
