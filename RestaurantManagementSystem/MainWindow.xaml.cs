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
        }
        

        List<Table> Tables = new List<Table> 
        { 
            new Table(0, 5, 5),
            new Table(0, 5, 4),
            new Table(0, 5, 3),
            new Table(0, 5, 2),
            new Table(0, 5, 1),
            new Table(0, 5, 0),

        };

        private void CreateTables()
        {

            TablesControler _tablesController = new TablesControler(TablesGrid, ButtonCallback);
            _tablesController.CreateTables(Tables);

        }

        public void ButtonCallback(object sender, RoutedEventArgs e)
        {
            Button TableButton = (Button)sender;
            activeTable = (Table)TableButton.Tag;

            if (activeTable != (Table)TableButton.Tag)
            {
                TableButton.Background = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            }



            Table _selectedtable = (Table)TableButton.Tag;
            SelectedTable_textBox.Text = $" Current people at the table {_selectedtable.CurrentOccupiedSeats}\n Max table capacity {_selectedtable.CurrentOccupiedSeats}\n Table ID {_selectedtable.id}";

            PrintTableOrders((Table)TableButton.Tag);


            TableSelected((Table)TableButton.Tag);

        }

        public void TableSelected(Table _tableSelected)
        {
            if (_tableSelected != null)
            {

            }

        }

        public void PrintTableOrders (Table _table)
        {
            if (_table != null)
            {
                OrderManagementService orderManagementService = new OrderManagementService();

                var servedProducts= orderManagementService.GetOrdersFromTable(_table);
                foreach (var product in servedProducts) 
                {
                    SelectedTableOrders.Text = $"Product {product.Name} Quantity : {product.Quantity}";
                    SelectedTableOrders.Text += "\n";
                }
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
        private void UpdateListBox(ListBox _listbox, eProductCategory _category)
        {
            _listbox.Items.Clear();
            List<Product> _product;
            ProductService _service = new ProductService();
            _product = _service.GetProductList<Product>(_category);
            UpdateListBox(productsListBox,_product);

        }


        private void UpdateListBox (ListBox _listbox, List< Product> _product)
        {
            _listbox.Items.Clear();
            
            foreach (var item in _product) 
            {
                _listbox.Items.Add($"{item.Name} {item.Price}");
            }
        }

        private void ShowProducts_ButtonClicked(object sender, RoutedEventArgs e)
        {
            Button _btn = (Button)sender;
            if (_btn != null )
            {
                if (_btn.Content.Equals("Show Drinks"))
                {
                    UpdateListBox(productsListBox, eProductCategory.Drink);
                } else if (_btn.Content.Equals( "Show Food"))
                {
                    UpdateListBox(productsListBox, eProductCategory.Food);

                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateTables();
        }
    }
}