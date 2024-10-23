using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities.Users;
using Salestack.Enums;

namespace Salestack.Controllers.Users.Seller;

[Route("api/[controller]")]
[ApiController]
public class SellersController : ControllerBase
{
    private readonly SalestackDbContext _context;

    public SellersController(SalestackDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSellerAsync(SalestackSeller data)
    {
        var newSeller = new SalestackSeller
        {
            Id = Guid.NewGuid(),
            Name = data.Name,
            Email = data.Email,
            PhoneNumber = data.PhoneNumber,
            Occupation = CompanyOccupation.Seller,
            CompanyId = data.CompanyId,
            TeamId = data.TeamId,
        };

        await _context.Seller.AddAsync(newSeller);

        await _context.SaveChangesAsync();

        return Created("procraft-api", newSeller);
    }

    [HttpGet("companyId={companyId}")]
    public async Task<IActionResult> GetCompanySellersAsync(Guid companyId)
    {
        var selectedCompany = await _context.Company
            .Include(c => c.Sellers)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == companyId);

        if (selectedCompany == null) return NotFound(new { Message = $"Company with id {companyId} not found." });

        var companySellers = selectedCompany.Sellers;

        return Ok(companySellers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSellerByIdAsync(Guid id)
    {
        var selectedSeller = await _context.Seller
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (selectedSeller == null) return NotFound(new { Message = $"Seller with id {id} not found." });

        return Ok(selectedSeller);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateSellerAsync(Guid id, SalestackSeller data)
    {
        var selectedSeller = await _context.Seller
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (selectedSeller == null) return NotFound(new { Message = $"Seller with id {id} not found." });

        selectedSeller.Name = data.Name;
        selectedSeller.Email = data.Email;
        selectedSeller.PhoneNumber = data.PhoneNumber;

        await _context.SaveChangesAsync();

        return Ok(selectedSeller);
    }

    [HttpPatch("{id}/teamId={teamId}")]
    public async Task<IActionResult> ChangeSellerTeamsync(Guid id, Guid teamId)
    {
        var selectedSeller = await _context.Seller
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (selectedSeller == null) return NotFound(new { Message = $"Seller with id {id} not found." });

        selectedSeller.TeamId = teamId;

        await _context.SaveChangesAsync();

        return Ok(selectedSeller);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSellerAsync(Guid id)
    {
        var selectedSeller = await _context.Seller
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (selectedSeller == null) return NotFound(new { Message = $"Seller with id {id} not found." });

        _context.Seller.Remove(selectedSeller);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
