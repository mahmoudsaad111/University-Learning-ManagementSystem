using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Config
{
    public class StudentCourseCycleConfig : IEntityTypeConfiguration<StudentCourseCycle>
    {
        public void Configure(EntityTypeBuilder<StudentCourseCycle> builder)

        {
            builder.ToTable("StudentCourseCycle");

            builder.Property(scc => scc.StudentId).IsRequired(true) ;
            builder.Property(scc=>scc.CourseCycleId).IsRequired(true) ;

            builder.HasOne(scc => scc.Student).WithMany(s => s.CourseCycleOfStudent).HasForeignKey(scc => scc.StudentId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(scc => scc.CourseCycle).WithMany(cc => cc.StudentsInCourseCycle).HasForeignKey(scc => scc.CourseCycleId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(scc=>scc.SectionsOfStudent).WithOne(ss=>ss.StudentCourseCycle).HasForeignKey(ss=>ss.StudentCourseCycleId).OnDelete(DeleteBehavior.NoAction); 

            builder.HasIndex(scc => new { scc.StudentId, scc.StudentCourseCycleId }).IsUnique(true);
        }
    }
}
