using System;
using System.Threading.Tasks;
using System.Web.Http;
using ProductInventoryAPI.Models;
using ProductInventoryAPI.Repositories;
using ProductInventoryAPI.Services;

namespace ProductInventoryAPI.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly ProductService _service;

        public ProductsController()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["NileshDB"].ConnectionString;
            ProductRepository repository = new ProductRepository(connectionString);
            _service = new ProductService(repository);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddProduct([FromBody] Product product)
        {
            try
            {
                await _service.AddProductAsync(product);
                return Ok("Product added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                product.Id = id;
                await _service.UpdateProductAsync(product);
                return Ok("Product updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            try
            {
                await _service.DeleteProductAsync(id);
                return Ok("Product deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetProducts()
        {
            try
            {
                var products = await _service.GetProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
