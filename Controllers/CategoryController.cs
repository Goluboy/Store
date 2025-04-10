using Microsoft.AspNetCore.Mvc;
using Marketplace.Data;
using Marketplace.Models;
using Microsoft.EntityFrameworkCore;
using Store.Contracts.DTOs;
using Store.Contracts.Requests;

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
        public async Task<ActionResult<IEnumerable<GoodCategoryDTO>>> GetCategories(CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Select(c => new GoodCategoryDTO
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    Description = c.Description,
                    ParentId = c.ParentId
                })
                .ToListAsync(cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GoodCategoryDTO>> GetCategory(int id, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(id, cancellationToken);

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
        public async Task<ActionResult<GoodCategoryDTO>> PostCategory(GoodCategoryRequest categoryRequest, CancellationToken cancellationToken)
        {
            var category = new GoodCategory
            {
                Name = categoryRequest.Name,
                Description = categoryRequest.Description,
                ParentId = categoryRequest.ParentId
            };

            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync();

            categoryRequest.CategoryId = category.CategoryId;

            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, categoryRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, GoodCategoryRequest categoryRequest, CancellationToken cancellationToken)
        {
            if (id != categoryRequest.CategoryId)
            {
                return BadRequest();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryRequest.Name;
            category.Description = categoryRequest.Description;
            category.ParentId = categoryRequest.ParentId;

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}