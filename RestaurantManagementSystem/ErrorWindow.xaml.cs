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
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public string ButtonPressed { get; private set; }

        public ErrorWindow(string? b1, string? b2)
        {
            InitializeComponent();

            if (b1 != null)
            {
                this.Button1.Visibility = Visibility.Visible;
                this.Button1.Content = b1;
            }
            if (b2 != null)
            {
                this.Button2.Visibility = Visibility.Visible;
                this.Button2.Content = b2;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ButtonPressed = button.Content.ToString();
            this.DialogResult = true;
        }
    }
}
