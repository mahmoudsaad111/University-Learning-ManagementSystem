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
    public class StudentExamConfig : IEntityTypeConfiguration<StudentExam>
    {
        public void Configure(EntityTypeBuilder<StudentExam> builder)
        {
            builder.Property(se => se.StudentId).IsRequired(true);
            builder.Property(se => se.ExamId).IsRequired(true);
            builder.Property(se => se.SubmitedAt).IsRequired(true);
            builder.Property(se => se.MarkOfStudentInExam).IsRequired(true).HasDefaultValue(0);


            builder.HasOne(se=>se.Student).WithMany(s=>s.ExamsOfStudent).HasForeignKey(se=>se.StudentId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(se => se.Exam).WithMany(e => e.StudentsAttendExam).HasForeignKey(se => se.ExamId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(se => new { se.StudentId, se.ExamId }).IsUnique(true);    
          
        }
    }
}
