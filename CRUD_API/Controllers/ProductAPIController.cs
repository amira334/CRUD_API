using CRUD_API.Data;
using CRUD_API.models;
using CRUD_API.models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductDTO> CreateProduct([FromBody] ProductDTO productDTO)
        {
            //
            //check model state 
            //if (ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if(ProductList.productList.FirstOrDefault(u => u.Name.ToLower() == productDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Product already exists!");
                return BadRequest(ModelState);
            }

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

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteProduct")]

        public IActionResult DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = ProductList.productList.FirstOrDefault(u => u.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            ProductList.productList.Remove(product);
            return NoContent();

        }

     
        [HttpPut("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult UpdateProduct(int id, [FromBody]ProductDTO productDTO)
        {
            if(productDTO == null || id != productDTO.ProductId)
            {
                return BadRequest();
            }
            var product = ProductList.productList.FirstOrDefault(u => u.ProductId == id);
            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.Stock = productDTO.Stock;

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialProduct(int id, JsonPatchDocument<ProductDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var product = ProductList.productList.FirstOrDefault(u => u.ProductId == id);
            if (product == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(product, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();

        }

    } 
}
