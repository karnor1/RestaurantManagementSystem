using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Services
{
    internal class TablesControler
    {
        Grid TableButtonGrid { get; set; }

        public delegate void ButtonCallback(object sender, RoutedEventArgs e);

        private ButtonCallback tableButtonClickedDelegate;

        public TablesControler(Grid tablesGrid, ButtonCallback cb)
        {
            tableButtonClickedDelegate = cb;
            TableButtonGrid = tablesGrid;
        }

        private double offset = 0;

        public void CreateTables(List<RestaurantTable> Tables)
        {
            foreach (var table in Tables)
            {
                // Check if a button for this table already exists
                var existingButton = TableButtonGrid.Children
                    .OfType<Button>()
                    .FirstOrDefault(b => b.Tag == table);

                if (existingButton != null)
                {
                    // A button for this table already exists, skip to the next table
                    continue;
                }

                Button tableButton = new Button();
                tableButton.Content = "Table" + table.id;
                tableButton.HorizontalAlignment = HorizontalAlignment.Stretch;
                tableButton.VerticalAlignment = VerticalAlignment.Stretch;
                tableButton.Tag = table;

                tableButton.MinWidth = 10;

                tableButton.MaxWidth = 100;
                tableButton.Height = 20;

                tableButton.Margin = new Thickness(0, offset, 0, 0);

                tableButton.Click += new RoutedEventHandler(tableButtonClickedDelegate);

                TableButtonGrid.Children.Add(tableButton);
                offset += tableButton.Height + 20;
            }
        }

    }
}
