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
    public class StudentAnswerInTFQConfig : IEntityTypeConfiguration<StudentAnswerInTFQ>
    {
        public void Configure(EntityTypeBuilder<StudentAnswerInTFQ> builder)
        {
            builder.HasKey(stfq => stfq.StudentAnswersInTFQId);

            builder.Property(stfq=> stfq.StudentAnswer).IsRequired();  

            builder.Property(stfq=> stfq.StudentExamId).IsRequired();
            builder.Property(stfq => stfq.TrueFalseQuestionId).IsRequired();

            builder.HasOne(stfq => stfq.StudentExam).WithMany(se=>se.StudnetTfqAnswersOfExam).
                HasForeignKey(stfq=>stfq.StudentExamId). 
                OnDelete(DeleteBehavior.NoAction);

            builder.HasOne( stfq=>stfq.TrueFalseQuestion) .WithMany(tfq=>tfq.StudentAnswerInTFQs)
                   .HasForeignKey(stfq=>stfq.TrueFalseQuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(stfq => new { stfq.StudentExamId, stfq.TrueFalseQuestionId }).IsUnique();

        }
    }
}
