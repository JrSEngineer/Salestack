using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities.Teams;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly SalestackDbContext _context;

    public TeamsController(SalestackDbContext context)
    {
        _context = context;
    }


    [HttpPost]
    public async Task<IActionResult> CreateTeamAsync(SalestackTeam data)
    {

        if (data.DirectorId == null && data.ManagerId == null)
        {
            return BadRequest("Your new team must have at least one director or manager as leader.");
        }

        var newTeam = new SalestackTeam
        {
            Id = Guid.NewGuid(),
            Name = data.Name,
            CompanyId = data.CompanyId,
            DirectorId = data.DirectorId,
            ManagerId = data.ManagerId,
            Sellers = data.Sellers,
        };

        await _context.Team.AddAsync(newTeam);

        await _context.SaveChangesAsync();

        return Created("salestack-api", newTeam);
    }


    [HttpGet("companyId={companyId}")]
    public async Task<IActionResult> GetAllTeamsAsync(Guid companyId)
    {
        var currentCompany = await _context.Company
            .Include(c => c.Teams)
            .FirstOrDefaultAsync(c => c.Id == companyId);

        if (currentCompany == null) return NotFound(new { Message = $"Company with id {companyId} not found." });

        var teams = currentCompany.Teams;

        return Ok(teams);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamByIdAsync(Guid id)
    {
        var selectedTeam = await _context.Team
            .AsNoTracking()
            .Include(t => t.Sellers)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (selectedTeam == null) return NotFound(new { Message = $"Team with id {id} not found." });

        return Ok(selectedTeam);
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateTeamAsync(Guid id, SalestackTeam data)
    {
        var selectedTeam = await _context.Team.FindAsync(id);

        if (selectedTeam == null)
            return NotFound(new { Message = $"Team with id {id} not found." });

        selectedTeam.Name = data.Name;
        selectedTeam.ManagerId = data.ManagerId;
        selectedTeam.Sellers = data.Sellers;

        await _context.SaveChangesAsync();

        return Ok(selectedTeam);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeamAsync(Guid id)
    {
        var selectedTeam = await _context.Team.FindAsync(id);

        if (selectedTeam == null) return NotFound(new { Message = $"Team with id {id} not found." });

        _context.Team.Remove(selectedTeam);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
