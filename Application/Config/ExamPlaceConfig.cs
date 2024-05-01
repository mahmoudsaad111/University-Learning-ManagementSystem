using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Application.Config
{
    public class ExamPlaceConfig : IEntityTypeConfiguration<ExamPlace>
    {
        public void Configure(EntityTypeBuilder<ExamPlace> builder)
        {
            builder.HasKey(e => e.ExamPlaceId);

            builder.Property(e => e.ExamType)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.SectionId).IsRequired(false);
            builder.Property(e=>e.CourseCycleId).IsRequired(false);
            builder.Property(e=>e.CourseId).IsRequired(false);


            builder.HasOne(e => e.CourseCycle).WithMany().HasForeignKey(cc => cc.CourseCycleId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(e=>e.Course).WithMany().HasForeignKey(c=>c.CourseId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(e => e.Section).WithMany().HasForeignKey(s => s.SectionId).OnDelete(DeleteBehavior.NoAction) ;
            builder.HasMany(ep=>ep.ExamsInExamPlace).WithOne(e=>e.ExamPlace).HasForeignKey(e=>e.ExamPlaceId).OnDelete(DeleteBehavior.Cascade);


            builder.HasIndex(e => e.CourseId);
            builder.HasIndex(e => e.CourseCycleId);
            builder.HasIndex(e => e.SectionId);
        }
    }
}
