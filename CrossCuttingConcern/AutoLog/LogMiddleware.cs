using System.Collections;
using System.Text;
using Microsoft.AspNetCore.Http;
using Security.Token.Models;

namespace CrossCuttingConcern.AutoLog;

public class LogMiddleware
{
    private readonly RequestDelegate _next;

    public LogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);


        string responseBody;
        var responseStream = context.Response.Body;
        responseStream.Seek(0, SeekOrigin.Begin);
        using (var reader = new StreamReader(responseStream, Encoding.UTF8))
            responseBody = await reader.ReadToEndAsync();


        string requestBody;
        var requestStream = context.Response.Body;
        requestStream.Seek(0, SeekOrigin.Begin);
        using (var reader = new StreamReader(requestStream, Encoding.UTF8))
            requestBody = await reader.ReadToEndAsync();


        var x = new Log
        {
            Id = 1,
            UserId = Convert.ToInt32(context.User.Claims.FirstOrDefault(x => x.Type.Equals(nameof(AccessTokenModel.UId).ToLowerInvariant()))),
            Uri = context.Request.Path,
            StatusCode = context.Response.StatusCode,
            RequestBody = requestBody,
            ResponseBody = responseBody,
            IsError = false,
            CreateTime = default
        };
    }
}


public class Program
{
    public static void Main()
    {
        const int MAX_NUMBER_PERMISSIONS = 304;
        var perms = new BitArray(MAX_NUMBER_PERMISSIONS);

        var random = new Random();
        for (var i = 16; i < 166; i++)
        {
            var item = random.Next(MAX_NUMBER_PERMISSIONS);
            perms[item] = true;
        }
        // Convert from bitarray to byte
        var bytes = new byte[(perms.Length - 1) / 8 + 1];
        perms.CopyTo(bytes, 0);
        var str = Convert.ToBase64String(bytes);
        Console.WriteLine($"Permission string: {str}");
		
        // Now convert back from permission string back to BitArray
        var bytes2 = Convert.FromBase64String(str);
        var perms2 = new BitArray(bytes2);
		
        Console.WriteLine(perms2.Length);
        for(var x = 0; x < MAX_NUMBER_PERMISSIONS; x++)
        {
            Console.WriteLine($"Permission {x} originally {perms[x]} now {perms2[x]}");
        }
    }
}