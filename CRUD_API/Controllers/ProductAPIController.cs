using CRUD_API.Data;
using CRUD_API.models;
using CRUD_API.models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_API.Controllers

{
    [Route("api/ProductAPI")]
    [ApiController]
    public class ProductAPIController: ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            return Ok(ProductList.productList);
        }

        [HttpGet("{id:int}")]
        public ProductDTO GetProducts(int id)
        {
            return ProductList.productList.FirstOrDefault(u => u.ProductId == id);
        }
    } 
}
