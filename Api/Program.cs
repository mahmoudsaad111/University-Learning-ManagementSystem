
using System.Reflection;
using Application;
using Infrastructure;
using MediatR;
using Contract;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;

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
            builder.Services.AddInfrastructureLayerServices(builder.Configuration);
            builder.Services.AddApplicationLayerServices();
            builder.Services.AddContractLayerServices();
            builder.Services.AddApiLayerServices();
            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // the next line is important to use enum as property in Dto that is parameters in api 
                //without this line the swagger doesnot work and get 500 internal server error
                c.UseInlineDefinitionsForEnums();

                // this for authentication 
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                   {
                        new OpenApiSecurityScheme{ Reference = new OpenApiReference{
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                           }
                        },
                        new string[] {}
                   }
                });
            });

            //  builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(
                // the next line is important to use enum as property in Dto that is parameters in api 
                options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
               );


            builder.Services.AddAuthorization();

            // For the reset password Token 
            builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
                     opt.TokenLifespan = TimeSpan.FromHours(2));

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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}