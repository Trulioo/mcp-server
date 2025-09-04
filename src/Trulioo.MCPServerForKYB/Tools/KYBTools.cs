using ModelContextProtocol.Server;
using System.ComponentModel;
using Trulioo.Client.V3.Models.Business;
using Trulioo.Client.V3.Models.Verification;
using Trulioo.MCPServerForKYB.Services;

namespace Trulioo.MCPServerForKYB.Tools;
[McpServerToolType]
public class KYBTools
{
    private TruliooClient _client;
    public KYBTools(TruliooClient client)
    {
        _client = client;
    }

    [McpServerTool(OpenWorld = true, Idempotent = true, ReadOnly = true, Destructive = false),
     Description("Can be used to verify credentials are set correctly. ")]
    public async Task<string> TestAuthentication()
    {
        return await _client.TestAuthenticationAsync();
    }

    [McpServerTool(OpenWorld = true, Idempotent = true, ReadOnly = true, Destructive = false), 
     Description("Gets a list of all the countries available in ISO2 format.")]
    public async Task<GetCountriesResponse> GetCountries()
    {
        var result = await _client.GetCountries();
        return result;
    }

    [McpServerTool(OpenWorld = true, Destructive = false, Idempotent = false, ReadOnly = false),
     Description(
         """
         Runs a KYB Business verification.  Fields with no value should not be provided.
         """)]
    public async Task<VerifyResult> BusinessVerification(BusinessVerifyRequest request)
    {
        var result = await _client.BusinessVerifyAsync(request);
        return result;
    }

    [McpServerTool(OpenWorld = true, Destructive = false, Idempotent = false, ReadOnly = false),
     Description("""
                 Search for a business and get details that can be used to run a verification. 
                 Fields with no value should not be provided.
                 """)]
    public async Task<BusinessSearchResponse> BusinessSearch(BusinessSearchRequest request)
    {
        var result = await _client.BusinessSearchAsync(request);

        return result;
    }
}
