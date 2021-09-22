namespace GroceryStoreAPI.Data.Models
{
    public class Customer : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}