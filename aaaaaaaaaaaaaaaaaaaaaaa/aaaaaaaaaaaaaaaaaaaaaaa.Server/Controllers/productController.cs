using aaaaaaaaaaaaaaaaaaaaaaa.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aaaaaaaaaaaaaaaaaaaaaaa.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {

        private readonly MyDbContext _dbContext;
        public productController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _dbContext.Products.ToList();
            return Ok(all);
        }


        [HttpGet("GetFisrt")]
        public IActionResult GetFisrt()
        {
            var First = _dbContext.Products.First();
            return Ok(First);
        }


        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(c => c.Id == id);
            if (product == null)
                return NotFound("product not found");
            return Ok(product);
        }

        // Get category by Name
        [HttpGet("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var product = _dbContext.Products.FirstOrDefault(c => c.Name == name);
            if (product == null)
                return NotFound("product not found");
            return Ok(product);
        }


    }
}
