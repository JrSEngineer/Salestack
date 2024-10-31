using Salestack.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Salestack.Entities.Users;

public class Authentication
{
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "You must provide a valid Password.")]
    public string Password { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;

    public CompanyOccupation Occupation { get; set; }
    public Guid UserId{ get; set; }
}
