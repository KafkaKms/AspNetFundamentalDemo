using AspNetFundamentalDemo.DAOs;
using AspNetFundamentalDemo.DTOs;
using AspNetFundamentalDemo.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspNetFundamentalDemo
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
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            var mongoDbSettings = Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
            services.AddSingleton<IMongoClient>(servicesProvider =>
            {
                return new MongoClient(mongoDbSettings.ConnectionString);

            });

            services.AddSingleton<IChampionDao, ChampionModel>();


            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AspNetFundamentalDemo", Version = "v1" });
            });

            services.AddHealthChecks()
                .AddMongoDb(mongoDbSettings.ConnectionString,
                name: "mongodb",
                timeout: TimeSpan.FromSeconds(3),
                tags: new string[] {
                    "ready"
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetFundamentalDemo v1"));

                app.UseHttpsRedirection();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            // Approach 1: Writing a terminal middleware.
            app.Use(next => async context =>
            {
                if (context.Request.Path == "/")
                {
                    await context.Response.WriteAsync("Hello terminal middleware!");
                    return;
                }

                await next(context);
            });

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/hc/ready", new HealthCheckOptions
                {
                    Predicate = check => check.Tags.Contains("ready"),
                    //ResponseWriter = async (context, report) =>
                    //{
                    //    var res = JsonSerializer.Serialize(
                    //        new
                    //        {
                    //            status = report.Status.ToString(),
                    //            check = report.Entries.Select(entry => new
                    //                {
                    //                    name = entry.Key,
                    //                    status = entry.Value.Status.ToString(),
                    //                    exception = entry.Value.Exception.ToString(),
                    //                    duration = entry.Value.Duration.ToString()
                    //                })
                    //        });

                    //    context.Response.ContentType = MediaTypeNames.Application.Json;
                    //    await context.Response.WriteAsync(res);
                    //}
                });

                endpoints.MapHealthChecks("/hc/alive", new HealthCheckOptions
                {
                    Predicate = _ => false
                });

                endpoints.MapGet("/hello/{name:alpha}", async context =>
                {
                    await context.Response.WriteAsync($"Hello {context.Request.RouteValues["name"]}");
                });

                // Approach 2: Using routing.
                endpoints.MapGet("/Movie", async context =>
                {
                    await context.Response.WriteAsync("Hello routing!");
                });
            });

            app.Map("/my-branch", app =>
            {
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("This brach go through my middleware");
                });
            });
        }
    }
}
