using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        private void SaveProduct_button_Click(object sender, RoutedEventArgs e)
        {
            ErrorWindow _errorWindow = new ErrorWindow();

            Iproducts _product = new Product();
            _product.Name = this.ProductName_textBox.Text;
            try
            {
                try
                {
                    _product.Price = double.Parse(this.ProductPrice_textBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                    _product.PriceForRestaurant = double.Parse(this.ProductPriceForRestaurant_TextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (ArgumentNullException)
                {
                    _errorWindow.ErrorLabel.Content = "No price entered";
                    throw new InvalidOperationException();
                }
                catch (FormatException)
                {
                    _errorWindow.ErrorLabel.Content = "Format not valid for price";
                    throw new InvalidOperationException();

                }
                catch (OverflowException)
                {
                    _errorWindow.ErrorLabel.Content = "Price value is too large";
                    throw new InvalidOperationException();

                }

                _product.DateAdded = DateTime.Now;

                if (this.RadioButton_drink.IsChecked == true)
                {
                    _product.category = eProductCategory.Drink;

                }
                else if (this.RadioButton_Food.IsChecked == true)
                {
                    _product.category = eProductCategory.Food;
                }
                else
                {
                    _errorWindow.ErrorLabel.Content = "Product type must be selected";
                    throw new InvalidOperationException();

                }
            } catch (InvalidOperationException)
            {
                _errorWindow.ShowDialog();

            }
            ProductService _service = new ProductService();

            if (!_service.AddNewProduct((Product)_product))
            {
                this.AddProductOutcome_Label.Text = "This product already exists";

            }
            else this.AddProductOutcome_Label.Text = "Product added";

        }
    }
}
