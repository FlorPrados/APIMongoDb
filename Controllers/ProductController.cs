using APIMongoDb.Models;
using APIMongoDb.Repositories;
using APIMongoDb.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIMongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IProductCollection db = new ProductCollection();

        //private readonly IProductCollection db;
        //public ProductController(IProductCollection _db)
        //{
        //    db = _db;
        //}


        [HttpGet]
        public async Task<IActionResult> GetAllProducts() => Ok(await db.GetAllProducts());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) => Ok(await db.GetProductById(id));

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();
            if(product.Name == string.Empty)
            {
                ModelState.AddModelError("Name", "The product shouln`t be emply");
            }
            await db.InsertProduct(product);

            return Created("Created", true);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product, string id)
        {
            if (product == null)
                return BadRequest();
            if (product.Name == string.Empty)
            {
                ModelState.AddModelError("Name", "The product shouln`t be emply");
            }
            product.Id = new MongoDB.Bson.ObjectId(id);

            await db.UpdateProduct(product);

            return Created("Created", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await db.DeleteProduct(id);
            return NoContent();
        }


    }
}
