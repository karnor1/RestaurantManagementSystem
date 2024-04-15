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

        public override string ToString()
            {

            return $"{Name} price {Price}";
            }

    }

    public interface Iproducts
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public eProductCategory category { get; set; }
    }

}

