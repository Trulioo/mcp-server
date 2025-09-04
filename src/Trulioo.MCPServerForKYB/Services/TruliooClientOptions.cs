
namespace Trulioo.MCPServerForKYB.Services;
public class TruliooClientOptions
{
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string ApiHost { get; set; } = "api.trulioo.com";
    public string AuthHost { get; set; } = "auth-api.trulioo.com";
    public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(2);
    public string BusinessFinderPackageId { get; set; }
    public string BusinessVerificationPackageId { get; set; }
}
