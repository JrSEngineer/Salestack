using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using Salestack.Entities.Users;
using Salestack.Enums;

namespace Salestack.Controllers.Users.Seller;

[Route("api/[controller]")]
[ApiController]
public class SellersController : ControllerBase
{
    private readonly SalestackDbContext _context;

    public SellersController(SalestackDbContext context)
    {
        _context = context;
    }

    [HttpPost("companyId={companyId}/teamId={teamId}")]
    public async Task<IActionResult> CreateSellerAsync(Guid companyId, Guid teamId, SalestackSeller data)
    {
        var companyForNewSellerCreationOperation = await _context.Company
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .Include(c => c.Teams)
            .FirstOrDefaultAsync(c => c.Id == companyId);

        if (companyForNewSellerCreationOperation == null)
        {
            return NotFound(new
            {
                Message = $"Company with id {companyId} not found."
            });
        }

        var newSellerTeam = companyForNewSellerCreationOperation.Teams.FirstOrDefault(t => t.Id == teamId);

        if (newSellerTeam == null)
        {
            return NotFound(new
            {
                Message = $"There is no team associated with the current company. Company id: {companyId}."
            });
        }

        Guid sellerId = Guid.NewGuid();

        var newSeller = new SalestackSeller
        {
            Id = Guid.NewGuid(),
            Name = data.Name,
            PhoneNumber = data.PhoneNumber,
            Occupation = CompanyOccupation.Seller,
            CompanyId = companyId,
            TeamId = teamId,
            Authentication = new Authentication
            {
                Email = data.Authentication.Email,
                Password = data.Authentication.Password,
                Occupation = CompanyOccupation.Seller,
                UserId = sellerId
            }
        };

        await _context.Seller.AddAsync(newSeller);

        await _context.SaveChangesAsync();

        return Created("procraft-api", newSeller);
    }

    [HttpGet("companyId={companyId}")]
    public async Task<IActionResult> GetCompanySellersAsync(Guid companyId)
    {
        var selectedCompany = await _context.Company
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .Include(c => c.Sellers)
            .FirstOrDefaultAsync(c => c.Id == companyId);

        if (selectedCompany == null)
            return NotFound(new
            {
                Message = $"Company with id {companyId} not found."
            });

        var companySellers = selectedCompany.Sellers;

        return Ok(companySellers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSellerByIdAsync(Guid id)
    {
        var selectedSeller = await _context.Seller
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (selectedSeller == null)
            return NotFound(new
            {
                Message = $"Seller with id {id} not found."
            });

        return Ok(selectedSeller);
    }

    [HttpPatch("id")]
    public async Task<IActionResult> UpdateSellerAsync(Guid id, SalestackSeller data)
    {
        var selectedSellerForUpdateOperation = await _context.Seller
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (selectedSellerForUpdateOperation == null)
            return NotFound(new
            {
                Message = $"Seller with id {id} not found."
            });

        selectedSellerForUpdateOperation.Name = data.Name;
        selectedSellerForUpdateOperation.PhoneNumber = data.PhoneNumber;

        await _context.SaveChangesAsync();

        return Ok(selectedSellerForUpdateOperation);
    }

    [HttpPatch("companyId={companyId}/leaderId={leaderId}/newTeamId={newTeamId}/sellerId={sellerId}")]
    public async Task<IActionResult> ChangeSellerTeamAsync(Guid companyId, Guid leaderId, Guid newTeamId, Guid sellerId)
    {
        var selectedCompanyToSellerTeamChange = await _context.Company
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .Include(c => c.Director)
            .Include(c => c.Managers)
            .Include(c => c.Teams)
            .FirstOrDefaultAsync(c => c.Id == companyId);

        if (selectedCompanyToSellerTeamChange == null)
        {
            return NotFound($"Company with id {companyId} not found.");
        }

        bool allowedConditionToSellerTeamTransfferOperation = 
            selectedCompanyToSellerTeamChange.Teams.Exists(t => t.DirectorId == leaderId) ||
            selectedCompanyToSellerTeamChange.Teams.Exists(t => t.ManagerId == leaderId);

        if (!allowedConditionToSellerTeamTransfferOperation)
        {
            return BadRequest(new
            {
                Message = "Please, provide a valid value as a leader identifier (directorId | managerId)."
            });
        }

        var newSellerTeam = selectedCompanyToSellerTeamChange.Teams.FirstOrDefault(t => t.Id == newTeamId);

        if (newSellerTeam == null)
        {
            return BadRequest(new
            {
                Message = "Please, provide a valid id for the new seller's team."
            });
        }

        bool sellerAlreadyPresentInTheCurrentTeam = newSellerTeam.Sellers.Exists(t => t.Id == sellerId);

        if (sellerAlreadyPresentInTheCurrentTeam)
        {
            return BadRequest(new
            {
                Message = $"Seller with id {sellerId} is already present in team with id {newTeamId}."
            });
        }

        var selectedSellerForTeamChangeOperation = await _context.Seller
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(s => s.Id == sellerId);

        selectedSellerForTeamChangeOperation!.TeamId = newTeamId;

        await _context.SaveChangesAsync();

        return Ok(selectedSellerForTeamChangeOperation);
    }

    [HttpDelete("leaderId={leaderId}/{id}")]
    public async Task<IActionResult> DeleteSellerAsync(Guid leaderId, Guid id)
    {
        var selectedSellerForDeleteOperation = await _context.Seller
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (selectedSellerForDeleteOperation == null)
            return NotFound(new
            {
                Message = $"Seller with id {id} not found."
            });

        var sellerTeam = await _context.Team.FindAsync(selectedSellerForDeleteOperation.TeamId);

        bool allowedConditionToRemoveSeller = sellerTeam!.DirectorId == leaderId ||
            sellerTeam!.ManagerId == id;

        if (!allowedConditionToRemoveSeller)
        {
            return BadRequest(new
            {
                Message = "Please, provide a valid leader id (directorId | managerId)."
            });
        }

        _context.Seller.Remove(selectedSellerForDeleteOperation);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
