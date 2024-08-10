using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Elasticsearch.Net;
using Nest;
using PermissionsAPI.Data.Repositories;
using PermissionsAPI.Data;
using PermissionsAPI.Services;
using System.Text.Json.Serialization;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {        
        services.AddDbContext<PermissionsDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        services.AddCors(options => 
        {
            var fronturl = Configuration.GetValue<string>("frontend_url");
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins(fronturl).AllowAnyMethod().AllowAnyHeader();
            });
        });

        services.AddMediatR(typeof(Startup));

        services.AddMediatR(typeof(Startup).Assembly, typeof(GetPermissionsHandler).Assembly);

        services.AddAutoMapper(typeof(Startup));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<KafkaProducer>(provider =>
            new KafkaProducer(Configuration["Kafka:BootstrapServers"]));

        services.AddSingleton<IElasticClient>(provider =>
        {
            var settings = new ConnectionSettings(new Uri(Configuration["Elasticsearch:Uri"]))
                .DefaultIndex("permissions");
            return new ElasticClient(settings);
        });

        services.AddSingleton<ElasticSearchService>();

        services.AddControllers();

        services.AddSwaggerGen();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PermissionsAPI v1"));
        }

        app.UseHttpsRedirection();
        app.UseCors();
        app.UseCors("CorsPolicy");
        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}