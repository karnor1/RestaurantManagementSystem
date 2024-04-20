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
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Services;


namespace RestaurantManagementSystem
{
    /// <summary>
    /// Interaction logic for TableManagementWindow.xaml
    /// </summary>
    public partial class TableManagementWindow : Window
    {
        #region ScaleValue Depdency Property
        public static readonly DependencyProperty ScaleValueProperty = DependencyProperty.Register("ScaleValue", typeof(double), typeof(TableManagementWindow), new UIPropertyMetadata(1.0, new PropertyChangedCallback(OnScaleValueChanged), new CoerceValueCallback(OnCoerceScaleValue)));

        private static object OnCoerceScaleValue(DependencyObject o, object value)
        {
            TableManagementWindow TableManagementWindow = o as TableManagementWindow;
            if (TableManagementWindow != null)
                return TableManagementWindow.OnCoerceScaleValue((double)value);
            else
                return value;
        }

        private static void OnScaleValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            TableManagementWindow TableManagementWindow = o as TableManagementWindow;
            if (TableManagementWindow != null)
                TableManagementWindow.OnScaleValueChanged((double)e.OldValue, (double)e.NewValue);
        }

        protected virtual double OnCoerceScaleValue(double value)
        {
            if (double.IsNaN(value))
                return 1.0d;

            value = Math.Max(0.1, value);
            return value;
        }

        protected virtual void OnScaleValueChanged(double oldValue, double newValue)
        {

        }

        public double ScaleValue
        {
            get
            {
                return (double)GetValue(ScaleValueProperty);
            }
            set
            {
                SetValue(ScaleValueProperty, value);
            }
        }
        #endregion

        private void MainGrid_SizeChanged(object sender, EventArgs e)
        {
            CalculateScale();
        }

        private void CalculateScale()
        {
            double yScale = ActualHeight / 400.0d;
            double xScale = ActualWidth / 500.0d;
            double value = Math.Min(xScale, yScale);
            ScaleValue = (double)OnCoerceScaleValue(MainGrid, value);
        }

        public TableManagementWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TableManagementService _tableManagementService = new TableManagementService();
            var newTable = new RestaurantTable(int.Parse(this.TotalSeats_TextBox.Text),0, int.Parse(this.Table_Number_TextBox.Text));
            _tableManagementService.AddTable(newTable);
        }

        private void TotalPersons_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
