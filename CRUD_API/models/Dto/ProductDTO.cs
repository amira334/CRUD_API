using System.ComponentModel.DataAnnotations;

namespace CRUD_API.models.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
