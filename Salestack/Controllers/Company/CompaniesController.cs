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
            CompanyCode = data.CompanyCode
        };

        await _context.Company.AddAsync(newCompany);

        await _context.SaveChangesAsync();

        return Created("salestack-api", newCompany);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompaniesAsync()
    {
        var companies = await _context.Company.ToListAsync();

        return Ok(companies);
    }
}
