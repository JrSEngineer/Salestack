namespace Salestack.Data.Config;

public class SalestackSettings
{
    public string SalestackDbConnectionString { get; set; } = string.Empty;
    public string MigrationConnectionString { get; set; } = string.Empty;
    public string SecureKey { get; set; } = string.Empty;

}
