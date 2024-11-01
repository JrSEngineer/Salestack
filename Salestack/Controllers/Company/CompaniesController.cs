using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities.Company;
using Salestack.Entities.Users;
using Salestack.Enums;

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
        var companyId = Guid.NewGuid();
        var directorId = Guid.NewGuid();

        var newCompany = new SalestackCompany
        {
            Id = companyId,
            Name = data.Name,
            Cnpj = data.Cnpj,
            CompanyCode = data.CompanyCode,
            PhoneNumber = data.PhoneNumber,
            DirectorId = directorId,
            Director = new SalestackDirector
            {
                Id = directorId,
                Name = data.Director.Name,
                PhoneNumber = data.Director.PhoneNumber,
                Occupation = CompanyOccupation.Director,
                CompanyId = companyId,
                Authentication = new Authentication
                {
                    Email = data.Director.Authentication.Email,
                    Password = data.Director.Authentication.Password,
                    Occupation = CompanyOccupation.Director,
                    UserId = directorId
                },
            }
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
            .Include(c => c.Director)
            .ToListAsync();

        return Ok(companies);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanyByIdAsync(Guid id)
    {
        var selectedCompany = await _context.Company
            .AsNoTracking()
            .Include(c => c.Managers)
            .Include(c => c.Teams)
            .Include(c => c.Sellers)
            .Include(c => c.Products)
            .Include(c => c.Services)
            .Include(c => c.Customers)
            .Include(c => c.Director)
            .ThenInclude(d => d.Authentication)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (selectedCompany == null)
            return NotFound(new
            {
                Message = $"Company with id {id} not found."
            });

        return Ok(selectedCompany);
    }


    [HttpPatch("{id}/companyCode={companyCode}")]
    public async Task<IActionResult> UpdateCompanyAsync(Guid id, SalestackCompany data, string companyCode)
    {
        var selectedCompanyForUpdateOperation = await _context.Company
            .Include(c => c.Director)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (selectedCompanyForUpdateOperation == null)
            return NotFound(new
            {
                Message = $"Company with id {id} not found."
            });

        if (selectedCompanyForUpdateOperation.CompanyCode != companyCode)
        {
            return NotFound(new
            {
                Message = $"Please, provide a valid company code."
            });
        }

        selectedCompanyForUpdateOperation.Name = data.Name;
        selectedCompanyForUpdateOperation.Cnpj = data.Cnpj;
        selectedCompanyForUpdateOperation.PhoneNumber = data.PhoneNumber;

        await _context.SaveChangesAsync();

        return Ok(selectedCompanyForUpdateOperation);
    }


    [HttpDelete("{id}/companyCode={companyCode}")]
    public async Task<IActionResult> DeleteCompanyAsync(Guid id, string companyCode)
    {
        var selectedCompanyForDeleteOperation = await _context.Company.FindAsync(id);

        if (selectedCompanyForDeleteOperation == null)
            return NotFound(new
            {
                Message = $"Company with id {id} not found."
            });
        if (selectedCompanyForDeleteOperation.CompanyCode != companyCode)
        {
            return NotFound(new
            {
                Message = $"Please, provide a valid company code."
            });
        }

        _context.Company.Remove(selectedCompanyForDeleteOperation);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
