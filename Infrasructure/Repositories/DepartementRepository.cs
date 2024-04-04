using Application.Common.Interfaces.InterfacesForRepository;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
	public class DepartementRepository : BaseRepository<Departement>, IDepartementRepository
	{
		public DepartementRepository(AppDbContext _appDbContext) : base(_appDbContext)
		{
		}

        public async Task<Departement> GetDepartementHasAcadimicYearId(int acadimicyearId)
        {
            var Departement= await _appDbContext.Departements.AsNoTracking().
                                            FirstOrDefaultAsync(d => d.AcadimicYears.Any(ay => ay.AcadimicYearId == acadimicyearId));
            return Departement;
        }
    }
}
