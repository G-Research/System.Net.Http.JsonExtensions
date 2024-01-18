# Status: Archived

As of .NET 5, the functionality provided by this library has been integrated into the .NET framework itself via the System.Net.Http.Json namespace.

This namespace offers built-in support for JSON serialization and deserialization in HTTP operations, making this library obsolete for projects using .NET 5 and later.
For more information on System.Net.Http.Json, please refer to the official .NET documentation: [System.Net.Http.Json](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json?view=net-5.0).

![status: inactive](https://img.shields.io/badge/status-inactive-red.svg)

## System.Net.Http.JsonExtensions

`System.Net.Http.JsonExtensions` is a library to replicate JSON-related extension methods found on `Microsoft.AspNet.WebApi.Client` that have not been implemented in .NET Core.

## Example Usage

```cs
using System.Net.Http;
using System.Net.Http.JsonExtensions;

public class MyClass
{
    public void SendSomeJson<T>(HttpClient httpClient, T myData)
    {
        httpClient.PostAsJsonAsync("http://example.com/", myData);
        //Data will be sent in JSON with "application/json" mime type to http://example.com/.
    }
}
```
