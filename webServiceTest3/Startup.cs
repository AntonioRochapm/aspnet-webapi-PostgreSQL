using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Context;
using webServiceTest3.Middlewares;
using webServiceTest3.Models;
using webServiceTest3.Validation;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using webServiceTest3.Auth;

namespace webServiceTest3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var connString = Configuration.GetConnectionString("MyConStr");

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                                                                .SelectMany(v => v.Errors)
                                                                .Select(v => v.ErrorMessage)
                                                                .ToList();
                        return new BadRequestObjectResult(new ErrorDetails
                        {
                            StatusCode = 400,
                            Message = string.Join(", ", errors)
                        });
                    };
                })
                .AddFluentValidation(fv =>
                {
                    fv.DisableDataAnnotationsValidation = true;
                    fv.ImplicitlyValidateChildProperties = true;
                    fv.ImplicitlyValidateRootCollectionElements = true;
                });

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddEntityFrameworkNpgsql().AddDbContext<PatientContext>(opt => opt.UseNpgsql("User ID =admin1;Password=admin1;Server=localhost;Port=5432;Database=patients;"));

            services.AddSingleton(Serilog.Log.Logger);


            services.AddTransient<IValidator<Patient>, PatientValidationRules>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "webServiceTest3", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "webServiceTest3 v1"));
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (httpContext, next) =>
            {
                var userName = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : "Guest";
                LogContext.PushProperty("Username", userName);
                await next.Invoke();
            });

            app.Use(async (HttpContext, next) =>
            {
                var userName = HttpContext.User.Identity.IsAuthenticated ? HttpContext.User.Identity.Name : "Guest";
                LogContext.PushProperty("Username", userName);
                await next.Invoke();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
