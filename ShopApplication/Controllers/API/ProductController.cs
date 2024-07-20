using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Data;
using ShopApplication.Models;

namespace ShopApplication.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ApplicationDbContext _db) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddProduct(Product product)
        {
            if (!ModelState.IsValid)  return BadRequest();
            _db.products.Add(product);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult EditProduct(Product updatedProduct)
        {
            if (!ModelState.IsValid) return BadRequest();
            Product product = _db.products.FirstOrDefault(x => x.Id == updatedProduct.Id);
            if (product == null) return NotFound();
            product.Name=updatedProduct.Name;
            product.Description=updatedProduct.Description;
            product.Price=updatedProduct.Price;
            product.img=updatedProduct.img;
            product.CategoryID=updatedProduct.CategoryID;
            _db.products.Update(product);
            _db.SaveChanges();
            return Ok();
        }
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProduct(int id)
        {
            if(id<=0) return BadRequest();
            var product = _db.products.FirstOrDefault(y => y.Id == id);
            if (product == null) return NotFound();
            _db.products.Remove(product);
            _db.SaveChanges();
            return Ok();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Product>))]
        public IActionResult GetProduct()
        {
            var product = _db.products.Include(m=>m.Category).ToList();
            return Ok(product); 
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProduct(int id)
        {
            var product = _db.products.Include(m=>m.Category).FirstOrDefault(x => x.Id == id);
            if(product == null) return NotFound();
            if(id<=0) return BadRequest();
            return Ok(product);
        }
    }
}