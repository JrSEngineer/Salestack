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

    [HttpPost("companyId={companyId}/directorId={directorId}")]
    public async Task<IActionResult> CreateManagerAsync(Guid companyId, Guid directorId, SalestackManager data)
    {
        var newManagerCompany = await _context.Company
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .Include(c => c.Director)
            .FirstOrDefaultAsync(c => c.Id == companyId);

        if (newManagerCompany == null)
            return NotFound(new
            {
                Message = $"Company with id {companyId} not found."
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

        Guid managerId = Guid.NewGuid();

        var newManager = new SalestackManager
        {
            Id = Guid.NewGuid(),
            Name = data.Name,
            PhoneNumber = data.PhoneNumber,
            Occupation = CompanyOccupation.Manager,
            VerificationCode = data.VerificationCode,
            CompanyId = companyId,
            Hierarchy = UserHierarchy.Manager,
            Authentication = new Authentication
            {
                Email = data.Authentication.Email,
                Password = data.Authentication.Password,
                Occupation = CompanyOccupation.Manager,
                UserId = managerId
            }
        };

        await _context.Manager.AddAsync(newManager);

        await _context.SaveChangesAsync();

        return Created("salestack-api", newManager);
    }


    [HttpGet("companyId={companyId}")]
    public async Task<IActionResult> GetComapanyManagersAsync(Guid companyId)
    {
        var selectedCompany = await _context.Company
          .IgnoreAutoIncludes()
          .AsNoTracking()
          .FirstOrDefaultAsync(c => c.Id == companyId);

        if (selectedCompany == null)
            return NotFound(new
            {
                Message = $"Company with id {companyId} not found."
            });

        var managers = await _context.Manager
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .Where(s => s.CompanyId == selectedCompany.Id)
            .ToListAsync();

        return Ok(managers);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetManagerByIdAsync(Guid id)
    {
        var selectedManager = await _context.Manager
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .Include(m => m.Teams)
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
        selectedManager.PhoneNumber = data.PhoneNumber;
        selectedManager.VerificationCode = data.VerificationCode;

        await _context.SaveChangesAsync();

        return Ok(selectedManager);
    }


    [HttpDelete("leaderId={leaderId}/{id}")]
    public async Task<IActionResult> DeleteManagerAsync(Guid leaderId, Guid id)
    {
        var selectedManagerForDeleteOperation = await _context.Manager.FindAsync(id);

        if (selectedManagerForDeleteOperation == null)
        {
            return NotFound(new
            {
                Message = $"Manager with id {id} not found."
            });
        }

        var managerCompany = await _context.Company
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .Include(c => c.Director)
            .FirstOrDefaultAsync(c => c.Id == selectedManagerForDeleteOperation.CompanyId);

        bool allowedConditionForManagerDeleteOperation = managerCompany!.Director!.Id == leaderId;

        if (!allowedConditionForManagerDeleteOperation)
            return BadRequest(new
            {
                Message = "Please, provide a valid leader id (directorId)."
            });

        _context.Manager.Remove(selectedManagerForDeleteOperation);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
