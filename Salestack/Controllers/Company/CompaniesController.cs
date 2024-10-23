using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities.Company;

namespace Salestack.Controllers.Company;

[Route("api/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly SalestackDbContext _context;

    public CompaniesController(SalestackDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompanyAsync(SalestackCompany data)
    {
        var newCompany = new SalestackCompany
        {
            Id = Guid.NewGuid(),
            Name = data.Name,
            Cnpj = data.Cnpj,
            CompanyCode = data.CompanyCode,
            PhoneNumber = data.PhoneNumber
        };

        await _context.Company.AddAsync(newCompany);

        await _context.SaveChangesAsync();

        return Created("salestack-api", newCompany);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompaniesAsync()
    {
        var companies = await _context.Company
            .AsNoTracking()
            .Include(c => c.Director).ToListAsync();

        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanyByIdAsync(Guid id)
    {
        var selectedCompany = await _context.Company
            .AsNoTracking()
            .Include(c => c.Director)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (selectedCompany == null) return NotFound(new { Message = $"Company with id {id} not found." });

        return Ok(selectedCompany);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCompanyAsync(Guid id, SalestackCompany data)
    {
        var selectedCompany = await _context.Company.FindAsync(id);

        if (selectedCompany == null) return NotFound(new { Message = $"Company with id {id} not found." });

        selectedCompany.Name = data.Name;
        selectedCompany.Cnpj = data.Cnpj;
        selectedCompany.CompanyCode = data.CompanyCode;
        selectedCompany.PhoneNumber = data.PhoneNumber;

        await _context.SaveChangesAsync();

        return Ok(selectedCompany);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompanyAsync(Guid id)
    {
        var selectedCompany = await _context.Company.FindAsync(id);

        if (selectedCompany == null) return NotFound(new { Message = $"Company with id {id} not found." });

        _context.Company.Remove(selectedCompany);
        
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
