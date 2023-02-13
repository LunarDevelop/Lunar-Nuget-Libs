using Microsoft.AspNetCore.Mvc;

namespace LunarExtensions.WebResults;

/// <summary>
/// A cookie response class for sending a cookie as a response
/// </summary>
public class CookieResponse : ObjectResult
{
    public int statusCode = 250;
    public CookieResponse(string key, object value) : base(value)
    {
        value = new Dictionary<string, object>() {
            { key, value }
        };
    }
}