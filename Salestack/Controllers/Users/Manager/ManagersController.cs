using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities.Users;
using Salestack.Enums;

namespace Salestack.Controllers.Users.Manager;

[Route("api/[controller]")]
[ApiController]
public class ManagersController : ControllerBase
{
    private readonly SalestackDbContext _context;

    public ManagersController(SalestackDbContext context)
    {
        _context = context;
    }

    [HttpPost("directorId")]
    public async Task<IActionResult> CreateManagerAsync(SalestackManager data, Guid directorId)
    {
        var newManagerCompany = await _context.Company.FindAsync(data.CompanyId);

        if (newManagerCompany == null)
            return NotFound(new
            {
                Message = $"Company with id {data.CompanyId} not found."
            });

        var directorForManagerCreation = newManagerCompany.Director;

        if (directorForManagerCreation == null)
            return BadRequest(new
            {
                Message = $"The current company has no director."
            });

        if (directorForManagerCreation.Id != directorId)
            return BadRequest(new
            {
                Message = "Please, provide a valid identifier for company director (directorId)."
            });

        var newManager = new SalestackManager
        {
            Id = Guid.NewGuid(),
            Name = data.Name,
            Email = data.Email,
            PhoneNumber = data.PhoneNumber,
            Occupation = CompanyOccupation.Manager,
            VerificationCode = data.VerificationCode,
            CompanyId = data.CompanyId
        };

        await _context.Manager.AddAsync(newManager);

        await _context.SaveChangesAsync();

        return Created("salestack-api", newManager);
    }


    [HttpGet("companyId={companyId}")]
    public async Task<IActionResult> GetComapanyManagersAsync(Guid companyId)
    {
        var selectedCompany = await _context.Company
            .Include(c => c.Managers)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == companyId);

        if (selectedCompany == null)
            return NotFound(new
            {
                Message = $"Company with id {companyId} not found."
            });

        var managers = selectedCompany.Managers;

        return Ok(managers);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetManagerByIdAsync(Guid id)
    {
        var selectedManager = await _context.Manager
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (selectedManager == null)
            return NotFound(new
            {
                Message = $"Manager with id {id} not found."
            });

        return Ok(selectedManager);
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateManagerAsync(Guid id, SalestackManager data)
    {
        var selectedManager = await _context.Manager.FindAsync(id);

        if (selectedManager == null)
            return NotFound(new
            {
                Message = $"Manager with id {id} not found."
            });

        selectedManager.Name = data.Name;
        selectedManager.Email = data.Email;
        selectedManager.PhoneNumber = data.PhoneNumber;
        selectedManager.VerificationCode = data.VerificationCode;

        await _context.SaveChangesAsync();

        return Ok(selectedManager);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteManagerAsync(Guid id)
    {
        var selectedManagerForDeleteOperation = await _context.Manager.FindAsync(id);

        if (selectedManagerForDeleteOperation == null)
        {
            return NotFound(new
            {
                Message = $"Manager with id {id} not found."
            });
        }

        var managerCompany = await _context.Company.FindAsync(selectedManagerForDeleteOperation.CompanyId);

        bool managerPresentInCurrentCompany = managerCompany!.Managers.Exists(m => m.Id == id);

        if (!managerPresentInCurrentCompany)
            return BadRequest(new
            {
                Message = $"Manager with id {id} is not present in company with id {selectedManagerForDeleteOperation.CompanyId}."
            });

        _context.Manager.Remove(selectedManagerForDeleteOperation);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
