using Microsoft.AspNetCore.Mvc;
using InventoryTracker.Data;
using InventoryTracker.Models;
using System.Linq;
namespace InventoryTracker.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductController :  ControllerBase
  {
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Products.ToList());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var product = _context.Products.Find(id);
      return product == null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
      _context.Products.Add(product);
      _context.SaveChanges();
      return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Product updated)
    {
      var product = _context.Products.Find(id);
      if (product == null) return NotFound();
      
      product.Name = updated.Name;
      product.Price = updated.Price;
      product.Quantity = updated.Quantity;
      _context.SaveChanges();
      return NoContent(); 
    }       

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var product = _context.Products.Find(id);
      if (product == null) return NotFound();

      _context.Products.Remove(product);
      _context.SaveChanges();
      return NoContent();
    }
  }
}