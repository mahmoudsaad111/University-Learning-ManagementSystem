using Application.Common.Interfaces.Presistance;
 
using Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
 
namespace Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());
		//	services.AddMediatR(Assembly.FacultyAddCommand);
			return services;
		}

		 
	}
}
