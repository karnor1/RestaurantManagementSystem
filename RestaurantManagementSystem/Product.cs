namespace RestaurantManagementSystem
{

    public enum eProductCategory
    {
        Drink,
        Food
    }

    public class Product : Iproduct, Iproducts
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public eProductCategory category { get; set; }
        public DateTime DateAdded { get; set; }
        public double PriceForRestaurant { get; set; }

        public override string ToString()
        {
            return $"{Name} price {Price}";
        }
        public int Quantity { get; set; }
    }

    public interface Iproducts : Iproduct
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public eProductCategory category { get; set; }
        public DateTime DateAdded {  get; set; }
        public double PriceForRestaurant { get; set; }

    }

    public interface IreceiptClient
    {
        public string ClientReceipt { get; set; }
    }
    public interface IreceiptRestaurant
    {
        public string RestaurantReceipt { get; set; }
    }

}

