namespace Trulioo.MCPServerForKYB.Services;

public class GetCountriesResponse
{
    public required IReadOnlyList<string> CountryISOCodes { get; init; }
    public required IReadOnlyList<string> Errors { get; init; }
}
