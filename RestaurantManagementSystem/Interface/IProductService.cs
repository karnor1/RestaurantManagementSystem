using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Interface
{
    public interface IProductService
    {
        bool AddNewProduct(Product _product);
        List<T> GetProductList<T>(eProductCategory productCategory);
    }
}