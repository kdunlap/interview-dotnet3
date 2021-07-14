using System.ComponentModel.DataAnnotations;

namespace GroceryStoreAPI.Dto
{
    public class CustomerDto
    {
        public long Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}