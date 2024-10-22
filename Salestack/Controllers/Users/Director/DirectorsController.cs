using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities.Users;

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

    [HttpPost]
    public async Task<IActionResult> CreateDirectorAsync(SalestackDirector data)
    {
        var newDirector = new SalestackDirector(
            Guid.NewGuid(),
            data.Name,
            data.Email,
            data.PhoneNumber,
            data.Occupation,
            data.CompanyId
            );

        await _context.Director.AddAsync(newDirector);

        await _context.SaveChangesAsync();

        return Created("salestack-api", newDirector);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDirectorsAsync()
    {
        var directors = await _context.Director.ToListAsync();

        return Ok(directors);
    }
}
