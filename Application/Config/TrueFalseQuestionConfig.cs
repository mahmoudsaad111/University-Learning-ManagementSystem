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
    public class TrueFalseQuestionConfig : IEntityTypeConfiguration<TrueFalseQuestion>
    {
        public void Configure(EntityTypeBuilder<TrueFalseQuestion> builder)
        {
            builder.HasKey(tfq => tfq.QuestionId);
            builder.Property(tfq => tfq.Text).IsRequired(true);
            builder.Property(tfq => tfq.ExamId).IsRequired(true);
            builder.Property(tfq=>tfq.Degree).IsRequired(true);

            builder.Property(tfq => tfq.IsTrue).IsRequired(true) ;
            builder.HasOne(tfq => tfq.Exam).WithMany(e => e.TrueFalseQuestions).HasForeignKey(tfq => tfq.ExamId) ;
        }
    }
}
