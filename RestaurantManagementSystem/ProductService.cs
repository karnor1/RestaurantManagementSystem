using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace RestaurantManagementSystem
{
    internal class ProductService
    {
        private static readonly string DrinksPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DrinksDatabase.json");
        private static readonly string FoodPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FoodDatabase.json");


        public ProductService()
        {

        }

        public bool AddNewProduct(Product _product)
        {
            DataBaseConnection<Product> connection = new DataBaseConnection<Product>(_product.category == eProductCategory.Drink ? DrinksPath : FoodPath);
            var productList = connection.GetData();
            if (productList != null)
            {
                if (!productList.Any(p => p.Name.Equals(_product.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    productList.Add(_product);
                    connection.SaveData(productList);
                    return true;
                }
            }
            return false;
        }
        public List<T> GetProductList<T>(eProductCategory productCategory)
        {
            DataBaseConnection<T> connection = new DataBaseConnection<T>(productCategory == eProductCategory.Drink ? DrinksPath : FoodPath);
            var productList = connection.GetData();
            return productList;
        }



    }

}

