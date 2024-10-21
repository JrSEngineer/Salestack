using Salestack.Entities.Users;

namespace Salestack.Entities.Company
{
    public class SalestackCompany
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CompanyCode { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string phoneNumber {  get; set; } = string.Empty;

        public SalestackDirector? Director { get; set; }
    }
}
