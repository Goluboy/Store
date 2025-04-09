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
    public class GoodsController : ControllerBase
    {
        private readonly MarketplaceDbContext _context;

        public GoodsController(MarketplaceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoodDTO>>> GetGoods()
        {
            return await _context.Goods
                .Select(g => new GoodDTO
                {
                    GoodId = g.GoodId,
                    Name = g.Name,
                    Price = g.Price,
                    CategoryId = g.CategoryId
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GoodDTO>> GetGood(int id)
        {
            var good = await _context.Goods.FindAsync(id);

            if (good == null)
            {
                return NotFound();
            }

            return new GoodDTO
            {
                GoodId = good.GoodId,
                Name = good.Name,
                Price = good.Price,
                CategoryId = good.CategoryId
            };
        }

        [HttpPost]
        public async Task<ActionResult<GoodDTO>> PostGood(GoodDTO goodDTO)
        {
            var category = await _context.Categories.FindAsync(goodDTO.CategoryId);
            if (category == null)
            {
                return BadRequest("Category does not exist.");
            }

            var good = new Good
            {
                Name = goodDTO.Name,
                Price = goodDTO.Price,
                CategoryId = goodDTO.CategoryId
            };

            _context.Goods.Add(good);
            await _context.SaveChangesAsync();

            goodDTO.GoodId = good.GoodId;

            return CreatedAtAction(nameof(GetGood), new { id = good.GoodId }, goodDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGood(int id, GoodDTO goodDTO)
        {
            if (id != goodDTO.GoodId)
            {
                return BadRequest();
            }

            var good = await _context.Goods.FindAsync(id);
            if (good == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(goodDTO.CategoryId);
            if (category == null)
            {
                return BadRequest("Category does not exist.");
            }

            good.Name = goodDTO.Name;
            good.Price = goodDTO.Price;
            good.CategoryId = goodDTO.CategoryId;

            _context.Entry(good).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGood(int id)
        {
            var good = await _context.Goods.FindAsync(id);
            if (good == null)
            {
                return NotFound();
            }

            _context.Goods.Remove(good);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}