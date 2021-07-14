using GroceryStoreAPI.Data.EFCore;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Data.Repositories
{
    public class CustomerRepository : EfCoreRepository<Customer, CustomerContext>
    {
        public CustomerRepository(CustomerContext context) : base(context)
        {

        }
    }
}