using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace Application.Config
{
    public class StudentLectureConfig : IEntityTypeConfiguration<Student_Lecture>

    {
        public void Configure(EntityTypeBuilder<Student_Lecture> builder)
        {
            builder.ToTable("Student_Lecture");
            // properties 

            //keys
            builder.HasKey(sl => sl.Student_LectureId);
            // relationships
            builder.HasOne(sl => sl.lecture).WithMany(l => l.Student_Lectures).HasForeignKey(sl => sl.LectureId);
            builder.HasOne(sl => sl.Student).WithMany(s => s.StudentLecture).HasForeignKey(sl => sl.StudentId);
            //indexes
            builder.HasIndex(x => new { x.StudentId, x.LectureId }).IsUnique(true); 

        }
    }
}
