using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities.Users;
using Salestack.Enums;

namespace Salestack.Controllers.Users.Director;

[Route("api/[controller]")]
[ApiController]
public class DirectorsController : ControllerBase
{
    private readonly SalestackDbContext _context;

    public DirectorsController(SalestackDbContext context)
    {
        _context = context;
    }

    [HttpPost("companyCode={companyCode}")]
    public async Task<IActionResult> CreateDirectorAsync(SalestackDirector data, string companyCode)
    {
        var companyForDirectorCreation = await _context.Company
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == data.CompanyId);

        if (companyForDirectorCreation == null)
            return BadRequest(new
            {
                Message = $"No company found with id '{data.CompanyId}'."
            });

        if (companyForDirectorCreation.CompanyCode != companyCode)
            return BadRequest(new
            {
                Message = "Please, provide a valid company code."
            });

        var newDirector = new SalestackDirector
        {
            Id = Guid.NewGuid(),
            Name = data.Name,
            Email = data.Email,
            PhoneNumber = data.PhoneNumber,
            Occupation = CompanyOccupation.Director,
            CompanyId = data.CompanyId
        };

        await _context.Director.AddAsync(newDirector);

        await _context.SaveChangesAsync();

        return Created("salestack-api", newDirector);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDirectorsAsync()
    {
        var directors = await _context.Director.AsNoTracking().ToListAsync();

        return Ok(directors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDirectorByIdAsync(Guid id)
    {
        var selectedDirector = await _context.Director
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);

        if (selectedDirector == null) return NotFound(new { Message = $"Director with id {id} not found." });

        return Ok(selectedDirector);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateDirectorAsync(Guid id, SalestackDirector data)
    {
        var selectedDirector = await _context.Director.FindAsync(id);

        if (selectedDirector == null)
            return NotFound(new { Message = $"Director with id {id} not found." });

        selectedDirector.Name = data.Name;
        selectedDirector.Email = data.Email;
        selectedDirector.PhoneNumber = data.PhoneNumber;

        await _context.SaveChangesAsync();

        return Ok(selectedDirector);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDirectorAsync(Guid id)
    {
        var selectedDirector = await _context.Director.FindAsync(id);

        if (selectedDirector == null)
            return NotFound(new { Message = $"Director with id {id} not found." });

        _context.Director.Remove(selectedDirector);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
