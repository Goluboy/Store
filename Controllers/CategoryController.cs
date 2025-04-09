using Microsoft.AspNetCore.Mvc;
using Marketplace.Data;
using Marketplace.DTOs;
using Marketplace.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MarketplaceDbContext _context;

        public CategoryController(MarketplaceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoodCategoryDTO>>> GetCategories()
        {
            return await _context.Categories
                .Select(c => new GoodCategoryDTO
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    Description = c.Description,
                    ParentId = c.ParentId
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GoodCategoryDTO>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return new GoodCategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                ParentId = category.ParentId
            };
        }

        [HttpPost]
        public async Task<ActionResult<GoodCategoryDTO>> PostCategory(GoodCategoryDTO categoryDTO)
        {
            var category = new GoodCategory
            {
                Name = categoryDTO.Name,
                Description = categoryDTO.Description,
                ParentId = categoryDTO.ParentId
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            categoryDTO.CategoryId = category.CategoryId;

            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, categoryDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, GoodCategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
            {
                return BadRequest();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryDTO.Name;
            category.Description = categoryDTO.Description;
            category.ParentId = categoryDTO.ParentId;

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}