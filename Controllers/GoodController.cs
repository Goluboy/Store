using Microsoft.AspNetCore.Mvc;
using Marketplace.Data;
using Marketplace.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Contracts.DTOs;
using Store.Contracts.Requests;

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
        public async Task<ActionResult<IEnumerable<GoodDTO>>> GetGoods(CancellationToken cancellationToken)
        {
            return await _context.Goods
                .Select(g => new GoodDTO
                {
                    GoodId = g.GoodId,
                    Name = g.Name,
                    Price = g.Price,
                    CategoryId = g.CategoryId
                })
                .ToListAsync(cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GoodDTO>> GetGood(Guid id, CancellationToken cancellationToken)
        {
            var good = await _context.Goods.FindAsync(id, cancellationToken);

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
        public async Task<ActionResult<GoodDTO>> PostGood(GoodRequest goodRequest, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(goodRequest.CategoryId, cancellationToken);
            if (category == null)
            {
                return BadRequest("Category does not exist.");
            }

            var good = new Good
            {
                Name = goodRequest.Name,
                Price = goodRequest.Price,
                CategoryId = goodRequest.CategoryId
            };

            _context.Goods.Add(good);
            await _context.SaveChangesAsync();

            goodRequest.GoodId = good.GoodId;

            return CreatedAtAction(nameof(GetGood), new { id = good.GoodId }, goodRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGood(Guid id, GoodRequest goodRequest, CancellationToken cancellationToken)
        {
            if (id != goodRequest.GoodId)
            {
                return BadRequest();
            }

            var good = await _context.Goods.FindAsync(id, cancellationToken);
            if (good == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(goodRequest.CategoryId, cancellationToken);
            if (category == null)
            {
                return BadRequest("Category does not exist.");
            }

            good.Name = goodRequest.Name;
            good.Price = goodRequest.Price;
            good.CategoryId = goodRequest.CategoryId;

            _context.Entry(good).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGood(Guid id, CancellationToken cancellationToken)
        {
            var good = await _context.Goods.FindAsync(id, cancellationToken);
            if (good == null)
            {
                return NotFound();
            }

            _context.Goods.Remove(good);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}