using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace CrossCuttingConcern.Globalization;

public class GlobalizationMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var lang = context.Request.Headers.AcceptLanguage;
        if (lang.Count != 0)
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang[0]!);

        await _next(context);
    }
}