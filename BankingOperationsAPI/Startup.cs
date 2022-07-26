﻿using OpBancarias.Core.Biz;
using OpBancarias.Data;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OpBancarias.Api.Core.Filters;
using Microsoft.AspNetCore.Hosting;

namespace OpBancarias.Api.Core
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public virtual void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<Application, Application>();
            services.AddScoped<OpBancariasContext, OpBancariasContext>();

            services.AddLogging();
            services.AddScoped<ExceptionFilter>();

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
