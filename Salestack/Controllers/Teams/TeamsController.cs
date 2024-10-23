using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities;
using Salestack.Entities.Teams;
using Salestack.Entities.Users;

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


    [HttpPatch("{id}/leaderId={leaderId}")]
    public async Task<IActionResult> UpdateTeamAsync(Guid id, SalestackTeam data, Guid leaderId)
    {
        SalestackBaseUser? teamLeader;

        var selectedTeam = await _context.Team.FindAsync(id);

        if (selectedTeam == null) return NotFound(new { Message = $"Team with id {id} not found." });

        if (selectedTeam.DirectorId != null)
        {
            teamLeader = (SalestackDirector?)await _context.Director
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == leaderId);
        }

        teamLeader = (SalestackManager?)await _context.Manager
        .AsNoTracking()
        .FirstOrDefaultAsync(m => m.Id == leaderId);

        if (teamLeader == null) return NotFound(new { Message = $"No leader with id {leaderId} found for the current Team." });

        selectedTeam.Name = data.Name;
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
