using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace RickAndMorty.API.NET5.Middleware
{
    public class AuthenticationMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
