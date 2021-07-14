using GroceryStoreAPI.Data;

namespace GroceryStoreAPI.Models
{
    public class Customer : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}