using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities.Users;

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

    [HttpPost]
    public async Task<IActionResult> CreateManagerAsync(SalestackManager data)
    {
        var newManager = new SalestackManager
        {
            Id = Guid.NewGuid(),
            Name = data.Name,
            Email = data.Email,
            PhoneNumber = data.PhoneNumber,
            Occupation = data.Occupation,
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

        if (selectedCompany == null) return NotFound(new {Message = $"Company with id {companyId} not found." });

        var managers = selectedCompany.Managers;

        return Ok(managers);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetManagerByIdAsync(Guid id)
    {
        var selectedManager = await _context.Manager
            .AsNoTracking()
            .Include(m => m.Company)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (selectedManager == null) return NotFound(new { Message = $"Manager with id {id} not found." });

        return Ok(selectedManager);
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateManagerAsync(Guid id, SalestackManager data)
    {
        var selectedManager = await _context.Manager.FindAsync(id);

        if (selectedManager == null)
            return NotFound(new { Message = $"Manager with id {id} not found." });

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
        var selectedManager = await _context.Manager.FindAsync(id);

        if (selectedManager == null)
            return NotFound(new { Message = $"Manager with id {id} not found." });

        _context.Manager.Remove(selectedManager);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
