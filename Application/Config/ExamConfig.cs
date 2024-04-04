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
	public class ExamConfig : IEntityTypeConfiguration<Exam>

    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.ToTable("Exams");
            // properties 
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.ModelAnswerUrl).IsRequired(false);
            builder.Property(x => x.Url).IsRequired(true); 
            builder.Property(x=>x.FullMark).IsRequired(true);

            //keys
            builder.HasKey(x => x.ExamId);
            // relationships
            builder.HasMany(x => x.ExamAnswers).WithOne(y => y.Exam).OnDelete(DeleteBehavior.Cascade); 
            builder.HasOne(x=>x.CourseCycle).WithMany(y=>y.Exams).HasForeignKey(y=>y.ExamId);   
            //indexes
            
            builder.HasIndex(x => x.CourseCycleId).IsUnique(false);

        }
    }
}
