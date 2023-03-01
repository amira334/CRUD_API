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

        [HttpGet("{id:int}", Name ="GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductDTO> CreateProduct([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest(productDTO);
            }
            if (productDTO.ProductId > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            productDTO.ProductId = ProductList.productList.OrderByDescending(u => u.ProductId).FirstOrDefault().ProductId + 1;
            ProductList.productList.Add(productDTO);

            return CreatedAtRoute("GetProduct", new { id = productDTO.ProductId}, productDTO);

        }
    } 
}

// 


