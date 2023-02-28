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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<ProductDTO> GetProduct(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }


            var product = ProductList.productList.FirstOrDefault(u => u.ProductId == id);
            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);


        }
    } 
}


