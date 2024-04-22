using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 

namespace Application.Config
{
    public class AcadimicYearConfig : IEntityTypeConfiguration<AcadimicYear>     
    {
        public void Configure(EntityTypeBuilder<AcadimicYear> builder)
        {
            builder.HasKey(a => a.AcadimicYearId);


            builder.HasMany(AC => AC.Students).WithOne(S => S.AcadimicYear).HasForeignKey(S => S.AcadimicYearId).OnDelete(DeleteBehavior.Restrict) ;    
            builder.HasMany(a=>a.Courses).WithOne(c=>c.AcadimicYear).HasForeignKey(c=>c.AcadimicYearId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(a => a.Groups).WithOne(g => g.AcadimicYear).HasForeignKey(g => g.AcadimicYearId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
