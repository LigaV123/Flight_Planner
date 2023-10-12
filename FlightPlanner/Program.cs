using FlightPlanner.Core.Interfaces;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Handlers;
using FlightPlanner.Services;
using FlightPlanner.Validations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<FlightPlannerDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("flight-planner")));
            builder.Services.AddTransient<IFlightPlannerDbContext, FlightPlannerDbContext>();
            builder.Services.AddTransient<IDbService, DbService>();
            builder.Services.AddTransient<IEntityService<Flight>, EntityService<Flight>>();
            builder.Services.AddTransient<IEntityService<Airport>, EntityService<Airport>>();
            builder.Services.AddTransient<IDeleteService, DeleteService>();
            builder.Services.AddTransient<IFlightService, FlightService>();
            builder.Services.AddTransient<IAirportService, AirportService>();
            builder.Services.AddTransient<IValidation, AirportValuesValidation>();
            builder.Services.AddTransient<IValidation, FlightsDatesValidation>();
            builder.Services.AddTransient<IValidation, FlightValuesValidation>();
            builder.Services.AddTransient<IValidation, SameAirportValidation>();
            var mapper = AutoMapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            builder.Services.AddSwaggerGen();
            // configure basic authentication 
            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, 
                    BasicAuthenticationHandler>("BasicAuthentication", null);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}