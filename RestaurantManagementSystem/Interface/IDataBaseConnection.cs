namespace RestaurantManagementSystem.Interface
{
    public interface IDataBaseConnection<T>
    {
        List<T> GetData();
        bool SaveData<T>(T data);
        bool SaveReceipt(string data);
    }
}