# System.Net.Http.JsonExtensions

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
