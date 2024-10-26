using Salestack.Entities.Teams;
using Salestack.Entities.Users;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Salestack.Entities.Company
{
    public class SalestackCompany
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "You must provide a valid Name.")]
        public string Name { get; set; } = string.Empty;

        public string CompanyCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must provide a valid CNPJ."),
         Length(minimumLength: 14, maximumLength: 14)]
        public string Cnpj { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must provide a valid PhoneNumber."),
         Length(minimumLength: 8, maximumLength: 20)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide the data of the company director."), DefaultValue(typeof(SalestackDirector))]
        public SalestackDirector Director { get; set; } = null!;

        public Guid? DirectorId { get; set; }

        public List<SalestackManager> Managers { get; set; } = [];

        public List<SalestackSeller> Sellers { get; set; } = [];

        public List<SalestackTeam> Teams { get; set; } = [];
    }
}
