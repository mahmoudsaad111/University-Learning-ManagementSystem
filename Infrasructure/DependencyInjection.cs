using LearningPlatformTest.Application.Common.Authentication;
using LearningPlatformTest.infrastructure.Authentication;
using LearningPlatformTest.infrastructure;
using LearningPlatformTest.infrastructure.DatabaseConfig; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using InfraStructure;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces.Presistance;
 
using System.Text.Json.Serialization;
using Infrastructure.Common;
using Application.Common.Interfaces.FileProcessing;
using Infrastructure.FileProcessing;

namespace Infrastructure;



public static class DependencyInjection 
{
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services)
    {
		services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUnitOfwork, UnitOfwork>();
        services.AddScoped<IFileCourseProcessing, FileCourseProcessingService>();
        services.AddScoped<IFileSectionProcessing , FileSectionProcessingService>();
        services.AddScoped<IFileAssignementProcessing, FileAssignementProcessingService>();
        services.AddScoped<IFileAssignementAnswerProcessing, FileAssignementAnswerProcessingService>();
        services.AddScoped<IFileImageProcessing,FileImageProcessingServes>();


        services.AddDbContext<AppDbContext>(cfg => {
            cfg.UseSqlServer(DbSettings.ConnectionStr);               
          //  cfg.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                                           });

		 
			//	services.Configure<DbSettings>(configuration.GetSection(DbSettings.SectionName));

      //&& the next line is important to prevent cycle occur when the data load the navigation properties but i commented it
      //because i use [JsonIgonre] instead because it's returned format is better than the returned format of the commented line
	//		services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
			services.AddIdentity<User, AppRole>().AddEntityFrameworkStores<AppDbContext>();
			//services.AddAuth(configuration); 

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName,jwtSettings);


        services.AddSingleton(Options.Create(jwtSettings)); 
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options => options.TokenValidationParameters=new TokenValidationParameters { 
                 ValidateIssuer=false,
                 ValidateAudience=false, 
                 ValidateLifetime=true, 
                 ValidateIssuerSigningKey=true,
                 //ValidIssuer=jwtSettings.Issuer,
                 //ValidAudience=jwtSettings.Audience,
                 IssuerSigningKey= new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                ClockSkew = TimeSpan.Zero

            }); 

        return services;       
    }
       
}
