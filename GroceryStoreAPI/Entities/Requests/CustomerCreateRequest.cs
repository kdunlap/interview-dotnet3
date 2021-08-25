using System.ComponentModel.DataAnnotations;

namespace GroceryStoreAPI.Dto
{
    public class CustomerCreateRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}