using Salestack.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salestack.Entities.Company
{
    public class SalestackCompany
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string CompanyCode { get; set; } = string.Empty;

        public string Cnpj { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public SalestackDirector? Director { get; set; }
        public Guid? DirectorId { get; set; }
    }
}
