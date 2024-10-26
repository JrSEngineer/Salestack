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


    [HttpPost("companyId={companyId}/leaderId={leaderId}")]
    public async Task<IActionResult> CreateTeamAsync(Guid companyId, Guid leaderId, SalestackTeam data)
    {
        var newTeamCompany = await _context.Company
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .Include(c => c.Director)
            .Include(c => c.Managers)
            .FirstOrDefaultAsync(c => c.Id == companyId);

        if (newTeamCompany == null)
        {
            return NotFound(new
            {
                Message = $"Company with id {companyId} not found."
            });
        }

        bool allowedTeamCreationCondition =
            newTeamCompany.Director.Id == leaderId ||
            newTeamCompany.Managers.Exists(m => m.Id == leaderId);

        if (!allowedTeamCreationCondition)
        {
            return BadRequest(new
            {
                Message = "Your new team must have at least one director or manager as leader."
            });
        }

        var newTeam = new SalestackTeam
        {
            Id = Guid.NewGuid(),
            Name = data.Name,
            CompanyId = companyId,
            DirectorId = newTeamCompany.Director.Id == leaderId ? leaderId : data.DirectorId,
            ManagerId = newTeamCompany.Managers.Exists(m => m.Id == leaderId) ? leaderId : data.ManagerId,
            Sellers = data.Sellers,
        };

        await _context.Team.AddAsync(newTeam);

        await _context.SaveChangesAsync();

        return Created("salestack-api", newTeam);
    }


    [HttpGet("companyId={companyId}")]
    public async Task<IActionResult> GetAllTeamsFromCompanyAsync(Guid companyId)
    {
        var currentCompany = await _context.Company
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .Include(c => c.Teams)
            .FirstOrDefaultAsync(c => c.Id == companyId);

        if (currentCompany == null)
            return NotFound(new
            {
                Message = $"Company with id {companyId} not found."
            });

        var teams = currentCompany.Teams;

        return Ok(teams);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamByIdAsync(Guid id)
    {
        var selectedTeam = await _context.Team
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .Include(t => t.Sellers)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (selectedTeam == null)
            return NotFound(new
            {
                Message = $"Team with id {id} not found."
            });

        return Ok(selectedTeam);
    }


    [HttpPatch("companyId={companyId}/leaderId={leaderId}/{id}")]
    public async Task<IActionResult> UpdateTeamAsync(Guid companyId, Guid leaderId, Guid id, SalestackTeam data)
    {
        var selectedTeamForUpdateOperation = await _context.Team.FindAsync(id);

        if (selectedTeamForUpdateOperation == null)
        {
            return NotFound(new
            {
                Message = $"Team with id {id} not found."
            });
        }

        var teamCompanyForUpdateOperation = await _context.Company
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .Include(c => c.Director)
            .Include(c => c.Managers)
            .FirstOrDefaultAsync(c => c.Id == companyId);

        bool allowedTeamUpdateCondition = teamCompanyForUpdateOperation!.Director.Id == leaderId ||
            teamCompanyForUpdateOperation.Managers.Exists(m => m.Id == leaderId);

        if (!allowedTeamUpdateCondition)
            return NotFound(new
            {
                Message = $"No leader with id {leaderId} found for the current Team."
            });

        selectedTeamForUpdateOperation.Name = data.Name;
        selectedTeamForUpdateOperation.Sellers = data.Sellers;

        await _context.SaveChangesAsync();

        return Ok(teamCompanyForUpdateOperation);
    }


    [HttpDelete("leaderId={leaderId}/{id}")]
    public async Task<IActionResult> DeleteTeamAsync(Guid leaderId, Guid id)
    {
        var selectedTeamForDeleteOperation = await _context.Team.FindAsync(id);

        if (selectedTeamForDeleteOperation == null)
        {
            return NotFound(new
            {
                Message = $"Team with id {id} not found."
            });
        }

        var teamCompanyForDeleteOperation = await _context.Company
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .Include(c => c.Director)
            .Include(c => c.Managers)
            .FirstOrDefaultAsync(c => c.Id == selectedTeamForDeleteOperation.CompanyId);

        bool allowedTeamDeleteCondition = teamCompanyForDeleteOperation!.Director.Id == leaderId ||
            teamCompanyForDeleteOperation.Managers.Exists(m => m.Id == leaderId);

        if (!allowedTeamDeleteCondition)
            return NotFound(new
            {
                Message = $"No leader with id {leaderId} found for the current Team."
            });

        var selectedTeam = await _context.Team.FindAsync(id);

        if (selectedTeam == null)
            return NotFound(new
            {
                Message = $"Team with id {id} not found."
            });

        _context.Team.Remove(selectedTeamForDeleteOperation);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
