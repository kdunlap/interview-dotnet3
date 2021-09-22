using System.ComponentModel.DataAnnotations;

namespace GroceryStoreAPI.Requests
{
    public class CustomerCreateRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}