using Application.Common.Interfaces.InterfacesForRepository;
using Domain.Models;
using Domain.Shared;
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
	public class FacultyRepository : BaseRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(AppDbContext _appDbContext) : base(_appDbContext)
		{
		}

 
        public async Task<Result<Faculty>> GetFacultyHasThisDepartementAsync(int deptId)
        {
            try
            {
                var faculty = await _appDbContext.Faculties.AsNoTracking().FirstOrDefaultAsync(f => f.Departements.Any(d => d.DepartementId == deptId));
                if (faculty is null)
                    return Result.Failure<Faculty>(new Error(code: "GetFacultyHasThisDepartementAsync", message : "There is no Faculty has this Dept Id"));
                return Result.Success<Faculty>(faculty); 
            }
            catch (Exception ex) 
            { 
                return Result.Failure<Faculty>(new Error (code : "GetFacultyHasThisDepartementAsync", message: ex.Message.ToString()));
            }

        }
    }
}
