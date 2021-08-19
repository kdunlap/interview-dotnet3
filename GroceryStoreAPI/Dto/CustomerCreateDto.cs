using System.ComponentModel.DataAnnotations;

namespace GroceryStoreAPI.Dto
{
    public class CustomerCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}