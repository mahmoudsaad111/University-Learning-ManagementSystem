
using System.Reflection;
using Application;
using Infrastructure;
using MediatR;
using Contract;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            builder.Services.AddInfrastructureLayerServices();
            builder.Services.AddApplicationLayerServices();
            builder.Services.AddContractLayerServices();
            builder.Services.AddApiLayerServices();
            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // the next line is important to use enum as property in Dto that is parameters in api 
                //without this line the swagger doesnot work and get 500 internal server error
                c.UseInlineDefinitionsForEnums();
            });

            //  builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(
                // the next line is important to use enum as property in Dto that is parameters in api 
                options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
               );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()) ;
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}