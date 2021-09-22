using GroceryStoreAPI.Data.EFCore;
using GroceryStoreAPI.Data.Models;
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