using Contract.Dto.UsersRegisterDtos;
using Microsoft.Extensions.DependencyInjection;
 

namespace Contract
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddContractLayerServices(this IServiceCollection services)
		{
			services.AddScoped<StudentRegisterDto>();
			services.AddScoped<ProfessorRegisterDto>();
			services.AddScoped<InstructorRegisterDto>();

			return services;
		}
	}
}
