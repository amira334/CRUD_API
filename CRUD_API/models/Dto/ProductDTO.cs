using System.ComponentModel.DataAnnotations;

namespace CRUD_API.models.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }
}
