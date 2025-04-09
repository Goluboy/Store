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
    public class SellerController : ControllerBase
    {
        private readonly MarketplaceDbContext _context;

        public SellerController(MarketplaceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SellerDTO>>> GetSellers()
        {
            return await _context.Sellers
                .Select(s => new SellerDTO
                {
                    SellerId = s.SellerId,
                    Name = s.Name,
                    Email = s.Email
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SellerDTO>> GetSeller(int id)
        {
            var seller = await _context.Sellers.FindAsync(id);

            if (seller == null)
            {
                return NotFound();
            }

            return new SellerDTO
            {
                SellerId = seller.SellerId,
                Name = seller.Name,
                Email = seller.Email
            };
        }

        [HttpPost]
        public async Task<ActionResult<SellerDTO>> PostSeller(SellerDTO sellerDTO)
        {
            var seller = new Seller
            {
                Name = sellerDTO.Name,
                Email = sellerDTO.Email
            };

            _context.Sellers.Add(seller);
            await _context.SaveChangesAsync();

            sellerDTO.SellerId = seller.SellerId;

            return CreatedAtAction(nameof(GetSeller), new { id = seller.SellerId }, sellerDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeller(int id, SellerDTO sellerDTO)
        {
            if (id != sellerDTO.SellerId)
            {
                return BadRequest();
            }

            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }

            seller.Name = sellerDTO.Name;
            seller.Email = sellerDTO.Email;

            _context.Entry(seller).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeller(int id)
        {
            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }

            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}