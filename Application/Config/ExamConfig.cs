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
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(200) ;
            builder.Property(x=>x.FullMark).IsRequired(true);

            //keys
            builder.HasKey(x => x.ExamId);
            // relationships
           
            
            // this relation in ExamAnswerConfig
            // builder.HasOne(x => x.ExamAnswer).WithOne(y => y.Exam).OnDelete(DeleteBehavior.Cascade);
            
            //indexes
            
            builder.HasIndex(x => x.ExamPlaceId).IsUnique(false);

        }
    }
}
