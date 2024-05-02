using Api.Pages;
using Application.Common.Interfaces.Presistance;
using Infrastructure.Common;
 

namespace Api
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApiLayerServices(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfwork, UnitOfwork>();

            services.AddRazorPages();
            services.AddTransient<IRazorPartialToStringRenderer, RazorPartialToStringRenderer>();

            return services;
		}
	}
}