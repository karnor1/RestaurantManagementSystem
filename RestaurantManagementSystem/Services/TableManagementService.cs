using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Interface;

namespace RestaurantManagementSystem.Services
{

    internal class TableManagementService
    {
        private static readonly string TablesDatabasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TableDatabase.json");

        private IDataBaseConnection <RestaurantTable> TableDatabase = new DataBaseConnection<RestaurantTable>(TablesDatabasePath);
        public TableManagementService() { }

        public void SaveTablesInfo(List<RestaurantTable> _tablesList)
        {
            TableDatabase.SaveData(_tablesList);

        }

        public List<RestaurantTable> LoadTablesInfo()
        {
            return TableDatabase.GetData();
        }

        public void UpdateTableInfo(RestaurantTable _table)
        {
            var _tables = LoadTablesInfo();
            int index = _tables.FindIndex(table => table.id == _table.id);
            _tables[index] = _table;
            SaveTablesInfo(_tables);
        }

        public bool AddTable(RestaurantTable _table)
        {
            var _tables = LoadTablesInfo();
            int index = _tables.FindIndex(table => table.id == _table.id);

            bool ret = false;

            ErrorWindow errorWindow = new ErrorWindow(null, null);
            if (index > -1) // if table with this id exists
            {
                if (_tables[index].activeOrder != null)
                {
                    errorWindow.ErrorLabel.Content = "This table already exists with an active order";

                    ret = false;
                }
                else
                {


                    errorWindow.ErrorLabel.Content = "Table updated, since table with the same Id already exists";
                    _tables[index] = _table;

                }

                ret = true;

            }
            else
            {
                _tables.Add(_table);
                errorWindow.ErrorLabel.Content = " New table added ";

            }
            SaveTablesInfo(_tables);

            errorWindow.Show();

            return ret;
        }

    }
}
