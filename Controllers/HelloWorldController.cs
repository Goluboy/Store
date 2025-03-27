using MarketplaceTest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Entities;

namespace MarketplaceTest.Controllers
{
    namespace YourNamespace.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class HelloWorldController : ControllerBase
        {
            private readonly StoreContext _dbContext;

            public HelloWorldController(StoreContext dbContext)
            {
                _dbContext = dbContext;
            }

            [HttpGet]
            public IActionResult Get()
            {

                return Ok();
            }

            [HttpPost]
            public async Task<IActionResult> Post([FromBody] GoodCategory category)
            {
                if (category == null)
                {
                    return BadRequest();
                }

                _dbContext.GoodCategories.Add(category);
                await _dbContext.SaveChangesAsync();

                return Ok(category);
            }
        }
    }
}
