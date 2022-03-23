using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Netcorext.Extensions.Contracts.AspNetCore;

public static class ObjectExtensions
{
    public static IActionResult ToActionResult<T>(this T source) where T : class
    {
        var type = typeof(T);

        var propCode = type.GetProperty("Code", BindingFlags.Instance | BindingFlags.Public);

        if (propCode == null)
            throw new ArgumentNullException("Property 'Code' not found.");

        var code = propCode?.GetValue(source)?.ToString() ?? "400000";

        if (code.Length != 6)
            throw new ArgumentException("Property 'Code' format error.");

        if (!int.TryParse(code, out var httpStatus))
            throw new ArgumentException("Property 'Code' format error.");

        if (httpStatus / 1000 == 204)
            return new NoContentResult();

        return new ObjectResult(source)
               {
                   StatusCode = httpStatus / 1000
               };
    }

    public static IActionResult ToActionResult<T>(this T source, int httpStatus) where T : class
    {
        switch (httpStatus)
        {
            case < 100:
            case > 500:
                throw new ArgumentOutOfRangeException(nameof(httpStatus));
            case 204:
                return new NoContentResult();
            default:
                return new ObjectResult(source)
                       {
                           StatusCode = httpStatus
                       };
        }
    }

    public static IActionResult ToActionResult<T>(this T source, string contentKey) where T : class
    {
        if (string.IsNullOrWhiteSpace(contentKey))
            throw new ArgumentNullException(nameof(contentKey));

        var type = typeof(T);

        var propCode = type.GetProperty("Code", BindingFlags.Instance | BindingFlags.Public);
        var propContent = type.GetProperty(contentKey, BindingFlags.Instance | BindingFlags.Public);

        if (propCode == null)
            throw new ArgumentNullException("Property 'Code' not found.");

        var code = propCode?.GetValue(source)?.ToString() ?? "400000";

        if (code.Length != 6)
            throw new ArgumentException("Property 'Code' format error.");

        if (!int.TryParse(code, out var httpStatus))
            throw new ArgumentException("Property 'Code' format error.");

        var content = propContent?.GetValue(source);

        if (httpStatus / 1000 == 204)
            return new NoContentResult();

        return new ObjectResult(content)
               {
                   StatusCode = httpStatus / 1000
               };
    }

    public static IActionResult ToActionResult<T>(this T source, int httpStatus, string contentKey) where T : class
    {
        if (httpStatus is < 100 or > 500)
            throw new ArgumentOutOfRangeException(nameof(httpStatus));

        if (string.IsNullOrWhiteSpace(contentKey))
            throw new ArgumentNullException(nameof(contentKey));

        var type = typeof(T);

        var propContent = type.GetProperty(contentKey, BindingFlags.Instance | BindingFlags.Public);

        var content = propContent?.GetValue(source);

        if (httpStatus == 204)
            return new NoContentResult();

        return new ObjectResult(content)
               {
                   StatusCode = httpStatus
               };
    }
}