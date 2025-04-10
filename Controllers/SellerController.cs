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
    public class SellerController : ControllerBase
    {
        private readonly MarketplaceDbContext _context;

        public SellerController(MarketplaceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SellerDTO>>> GetSellers(CancellationToken cancellationToken)
        {
            return await _context.Sellers
                .Select(s => new SellerDTO
                {
                    SellerId = s.SellerId,
                    Name = s.Name,
                    Email = s.Email
                })
                .ToListAsync(cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SellerDTO>> GetSeller(Guid id, CancellationToken cancellationToken)
        {
            var seller = await _context.Sellers.FindAsync(id, cancellationToken);

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
        public async Task<ActionResult<SellerDTO>> PostSeller(SellerRequest sellerRequest, CancellationToken cancellationToken)
        {
            var seller = new Seller
            {
                Name = sellerRequest.Name,
                Email = sellerRequest.Email
            };

            await _context.Sellers.AddAsync(seller, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            sellerRequest.SellerId = seller.SellerId;

            return CreatedAtAction(nameof(GetSeller), new { id = seller.SellerId }, sellerRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeller(Guid id, SellerRequest sellerRequest, CancellationToken cancellationToken)
        {
            if (id != sellerRequest.SellerId)
            {
                return BadRequest();
            }

            var seller = await _context.Sellers.FindAsync(id, cancellationToken);
            if (seller == null)
            {
                return NotFound();
            }

            seller.Name = sellerRequest.Name;
            seller.Email = sellerRequest.Email;

            _context.Entry(seller).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeller(int id, CancellationToken cancellationToken)
        {
            var seller = await _context.Sellers.FindAsync(id, cancellationToken);
            if (seller == null)
            {
                return NotFound();
            }

            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}