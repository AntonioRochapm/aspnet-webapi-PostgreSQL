using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace webServiceTest3.Middlewares
{
    public sealed class ExceptionMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate next;

        public ExceptionMiddleware (RequestDelegate next)
        {
            this.next = next;
        }
        
        public async Task InvokeAsync (HttpContext context)
        {
            
            try
            {
                await next(context);                
            }catch(Exception e) 
            {                
                await HandleException(e, context);                       
            }            
        }
        
        private static async Task HandleException(Exception exception, HttpContext context)
        {
            context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
            context.Response.StatusCode = (short)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorDetails
            {
                StatusCode = (short)context.Response.StatusCode,
                Message = exception.Message
            }));
        }
    }
}
