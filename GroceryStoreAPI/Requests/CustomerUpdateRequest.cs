using System.ComponentModel.DataAnnotations;

namespace GroceryStoreAPI.Requests
{
    public class CustomerUpdateRequest
    {
        public long Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}