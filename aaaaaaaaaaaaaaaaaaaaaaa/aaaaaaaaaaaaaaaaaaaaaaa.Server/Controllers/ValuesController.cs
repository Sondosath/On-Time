using aaaaaaaaaaaaaaaaaaaaaaa.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aaaaaaaaaaaaaaaaaaaaaaa.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        public ValuesController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _dbContext.Categories.ToList();
            return Ok(all);
        }


        [HttpGet("GetFisrt")]
        public IActionResult GetFisrt()
        {
            var First = _dbContext.Categories.First();
            return Ok(First);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
                return NotFound("Category not found");
            return Ok(category);
        }

        // Get category by Name
        [HttpGet("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var category = _dbContext.Categories.FirstOrDefault(c => c.CategoryName == name);
            if (category == null)
                return NotFound("Category not found");
            return Ok(category);
        }
    }
}


