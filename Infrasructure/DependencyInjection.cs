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
using Application.Common.Interfaces.RealTimeInterfaces;
using Infrastructure.RealTimeServices;
using Infrastructure.MailService;
using Application.Common.Interfaces.MailService;
using infrastructure.Authentication;
using Application.Common.Interfaces.Authentication;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Application.Common.Interfaces.InterfacesForRepository;
using Infrastructure.Repositories;
namespace Infrastructure;



public static class DependencyInjection 
{
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services,
         ConfigurationManager configuration)
    {
		services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUnitOfwork, UnitOfwork>();
        services.AddScoped<IFileCourseProcessing, FileCourseProcessingService>();
        services.AddScoped<IFileSectionProcessing , FileSectionProcessingService>();
        services.AddScoped<IFileAssignementProcessing, FileAssignementProcessingService>();
        services.AddScoped<IFileAssignementAnswerProcessing, FileAssignementAnswerProcessingService>();
        services.AddScoped<IFileImageProcessing,FileImageProcessingServes>();
        services.AddScoped<ICheckDataOfRealTimeRequests, CheckDataOfRealTimeRequests>();

        services.AddScoped<IStudentAnswerInMCQRepository, StudentAnswerInMCQRepository>();
        services.AddScoped<IStudentAnswerInTFQRepository , StudentAnswerInTFQRepository>(); 
        services.AddScoped<IMCQRepository, MCQRepository>();
        services.AddScoped<ITFQRepository,TFQRepository>();

        services.AddDbContext<AppDbContext>(cfg => {
            cfg.UseSqlServer(DbSettings.ConnectionStr);               
          //  cfg.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                                           });


        //	services.Configure<DbSettings>(configuration.GetSection(DbSettings.SectionName));

        //&& the next line is important to prevent cycle occur when the data load the navigation properties but i commented it
        //because i use [JsonIgonre] instead because it's returned format is better than the returned format of the commented line
        //		services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
        services.AddIdentity<User, AppRole>(Options => Options.User.RequireUniqueEmail = true).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

        //services.AddAuth(configuration); 

        // Mapping the Mailsettings configuration to the class MailSettings 
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddTransient<IMailingService, MailingService>();
        //	services.Configure<DbSettings>(configuration.GetSection(DbSettings.SectionName));

        services.AddAuth(configuration);

        // This Line to register signalR service
        services.AddSignalR(options => { });

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services,
          ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);


        services.AddSingleton(Options.Create(jwtSettings));
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer=jwtSettings.Issuer,
                    //ValidAudience=jwtSettings.Audience,
                    // this for mapping the user Id to NameIdentifier claim
                    // NameClaimType="uid",
                    IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }

}
