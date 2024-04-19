using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace RestaurantManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Table activeTable;

        public void TableButton_Clicked(object sender, RoutedEventArgs e)
        {
            // Implementation of your method
        }

        public MainWindow()
        {
            InitializeComponent();
            TableManagementService _tableService = new TableManagementService();

            Tables = _tableService.LoadTablesInfo();
        }


        List<Table> Tables = new List<Table>() ;



        private void CreateTables()
        {

            TablesControler _tablesController = new TablesControler(TablesGrid, ButtonCallback);
            _tablesController.CreateTables(Tables);

        }

        public void ButtonCallback(object sender, RoutedEventArgs e)
        {
            Button TableButton = (Button)sender;
            activeTable = (Table)TableButton.Tag;

            CheckTableButtonColor(TablesGrid);

           OrderManagementService orderManagementService = new OrderManagementService();
           var activeOrder = orderManagementService.GetOrderFromTable(activeTable);



            TableControlWindow _controlWindow = new TableControlWindow(activeTable, activeOrder);
            _controlWindow.Show();

            Table _selectedtable = (Table)TableButton.Tag;
            SelectedTable_textBox.Text = $" Current people at the table {_selectedtable.CurrentOccupiedSeats}\n Max table capacity {_selectedtable.CurrentOccupiedSeats}\n Table ID {_selectedtable.id}";



            TableSelected((Table)TableButton.Tag);

        }

        void CheckTableButtonColor (object container)
        {
            if (container != null)
            {
                if (container is Grid TablesGrid)
                {
                    foreach (var item  in TablesGrid.Children)
                    {
                        if (item is Button  TableButton)
                        {
                            if (TableButton.Tag is Table buttonTag)
                            {
                                if (buttonTag.isOccupied)
                                {
                                    TableButton.Foreground = TableButton.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));

                                }
                                else TableButton.Foreground = TableButton.Foreground = new SolidColorBrush(Color.FromRgb(0, 30, 0));

                            }
                        }


                    }
                }
            }
        }

        public void TableSelected(Table _tableSelected)
        {
            if (_tableSelected != null)
            {

            }

        }



        private void GetDatabas_Click(object sender, RoutedEventArgs e)
        {
            OrderManagementService orderManagementService = new OrderManagementService();
            Iproduct alus = new Product();
            alus.Name = " alus";
            alus.Price = 50;

            orderManagementService.CreateOrder(new Table (5,3,0));
            orderManagementService.AddProductToOrder(alus, 5);

            // Create a new button

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var AddProduct = new AddProduct();
            AddProduct.Show();


        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateTables();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var TableManagementWindow = new TableManagementWindow();
            TableManagementWindow.Show();
        }
    }
}