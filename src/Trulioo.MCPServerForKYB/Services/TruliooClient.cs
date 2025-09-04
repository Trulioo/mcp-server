using Trulioo.Client.V3;
using Trulioo.Client.V3.Models.Business;
using Trulioo.Client.V3.Models.Errors;
using Trulioo.Client.V3.Models.Verification;

namespace Trulioo.MCPServerForKYB.Services;
public class TruliooClient
{
    private readonly TruliooApiClient _client;
    private readonly TruliooClientOptions _options;
    private const int UndefinedErrorCode = 2000;

    public TruliooClient(TruliooClientOptions options)
    {
        var context = new Context(options.ClientId, options.ClientSecret, options.Timeout);
        context.AuthHost = options.AuthHost;
        context.ApiHost = options.ApiHost;
        _client = new TruliooApiClient(context);
        _options = options;
    }

    public async Task<string> TestAuthenticationAsync()
    {
        return await _client.Connection.TestAuthenticationAsync();
    }

    public async Task<VerifyResult> BusinessVerifyAsync(BusinessVerifyRequest request)
    {
        request.PackageId = _options.BusinessVerificationPackageId;
        request.Timeout = (int)_options.Timeout.TotalSeconds;
        try
        {
            return await _client.TruliooBusiness.BusinessVerifyAsync(request);
        }
        catch (Exception e)
        {
            var response = new VerifyResult
            {
                Errors = new List<ServiceError>
                {
                    new()
                    {
                        Code = UndefinedErrorCode,
                        Message = e.Message
                    }
                }
            };
            return response;
        }
    }

    public async Task<BusinessSearchResponse> BusinessSearchAsync(BusinessSearchRequest request)
    {
        request.PackageId = _options.BusinessFinderPackageId;
        request.Timeout = (int) _options.Timeout.TotalSeconds;
        try
        {
            return await _client.TruliooBusiness.BusinessSearchAsync(request);
        }
        catch (Exception e)
        {
            var response = new BusinessSearchResponse
            {
                Errors = new List<ServiceError>
                {
                    new()
                    {
                        Code = UndefinedErrorCode,
                        Message = e.Message
                    }
                }
            };
            return response;
        }
    }

    public async Task<GetCountriesResponse> GetCountries()
    {
        try
        {
            var result = new GetCountriesResponse
            {
                CountryISOCodes = (await _client.Configuration.GetCountryCodesAsync(_options.BusinessFinderPackageId))
                    .ToList(),
                Errors = []
            };
            return result;
        }
        catch (Exception e)
        {
            return new GetCountriesResponse
            {
                CountryISOCodes = [],
                Errors = [e.Message]
            };
        }
    }
}
