

#  Intro

This MCP Server is meant to interact with Trulioo’s KYB Services.  Your account needs to be configured with at least two packages.  One package to do a Business Search and one package to run a Business Verification. The Business Verification  package can be Business Complete, Business Essentials, or Business Insights. 

The MCP Server has been tested with Claude Desktop,  Visual Studio, and VS Code with copilot.  

# Setup 
Configuration of the server is controlled via appsettings.json.  Values can also be provided via environment variables or the command line.
```
{
  "TruliooClient": {
    "ClientId": "",
    "ClientSecret": "",
    "ApiHost": "api.trulioo.com",
    "AuthHost": "auth-api.trulioo.com",
    "Timeout": "0:02:01",
    "BusinessFinderPackageId": "",
    "BusinessVerificationPackageId": ""
  }
}
```

Precedence is appsettings.json, secrets.json(DEBUG only), environment variables and command line.

You will need a client id and secret and a Business Finder package for searching and one of the Business Verification packages,  Essentials, Insights or Complete  to get a detailed report.

To build and run

Inside the folder mcp-server/projects/Trulioo.MCPServerForKYB 

```
dotnet build
dotnet run
```

# Examples
If you want to test the MCP Server directly you can run it and send messages on the console.

Ping the server

```
{ "jsonrpc": "2.0", "id": "1", "method": "ping"}
```

View the available tools
```
{ "jsonrpc": "2.0", "id": "2", "method": "tools/list"}
```
Check that you have set up your credentials correctly.

```
{ "jsonrpc": "2.0", "id": "3", "method": "tools/call", "params": { "name": "test_authentication"}}
```
The response will include Hello and your clientid. 

Get all the countries configured on your package

```
{ "jsonrpc": "2.0", "id": "4", "method": "tools/call", "params": { "name": "get_countries", "arguments": { } }}
```
If test authentication and get countries work you should be all set to hook up to an agent like Claude Desktop, VS Code or Visual Studio.

## Using with an Agent

When you have the MCP Server setup and configured in your agent you can use it in chat like.

Prompt:
```
“do a business search and verificaiton for Trulioo  “
```
While there is a typo in verification the prompt still worked.

```
I'll help you perform a business search and verification for Trulioo. I'll do this in two steps:

First, let's search for the business using the business search tool
Then, we'll use the results to perform a verification
Let's start with the business search:
```
LLM Ran Search
```
I see we got an error indicating that the account is not configured for Canada. Let's try with the United States instead:
```
LLM Ran search again
```
Great! We found Trulioo's business information. Now, let's proceed with the business verification using the information we received:
```
LLM Ran verify

```
I've completed both the business search and verification for Trulioo. Here's a summary of the findings:

Business Information:

Legal Name: TRULIOO, INC.
Business Status: Active
Business Type: Incorporated
Business Registration Number: 604923757
DUNS Number: 119276044
Jurisdiction: California (CA)
Locations:

Headquarters:

Address: 4225 Executive Square, Suite 600
City: La Jolla, CA 92037-1484
County: San Diego
Primary Address:

Address: 3770 Tansy Street, Suite 100
City: San Diego, CA 92121-1550
Additional Information:

Business Status: Active
CEO/Officer: Steve Munford
Phone Number: 888-773-0179
Last Profile Update: June 4, 2025
The verification was successful with a "match" status, confirming the business information across multiple data sources including the US Secretary of State Company Register and Dun & Bradstreet. All key fields including business name, registration number, and address were successfully verified.
```

The registry information is stale with an old Trulioo address; it also does not know Trulioo has a new CEO.  



