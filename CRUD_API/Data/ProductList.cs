using CRUD_API.models.DTO;

namespace CRUD_API.Data
{
    public static class ProductList
    {
        public static List<ProductDTO> productList = new List<ProductDTO>
        {
            new ProductDTO {ProductId=1, Name= "TestProduct1", Description = "TestDescription1", Price = 1, Stock = 1},
            new ProductDTO {ProductId=2, Name= "TestProduct2", Description = "TestDescription2", Price = 1, Stock = 0}
        };
    }
}
