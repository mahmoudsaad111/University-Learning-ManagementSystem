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
    public class StudentAnswerInMCQConfig : IEntityTypeConfiguration<StudentAnswerInMCQ>
    {
        public void Configure(EntityTypeBuilder<StudentAnswerInMCQ> builder)
        {
            builder.HasKey(smcq => smcq.StudentAnswerInMCQId);

            builder.Property(smcq => smcq.OptionSelectedByStudent).IsRequired();

            builder.Property(smcq=>smcq.MultipleChoiceQuestionId).IsRequired();
            builder.Property(smcq=>smcq.StudentExamId).IsRequired();


            builder.HasOne(smcq=> smcq.StudentExam).WithMany(se=>se.StudentMcqAnswersOfExam).
                 HasForeignKey(smcq=>smcq.StudentExamId).
                 OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(smcq=>smcq.MultipleChoiceQuestion).WithMany(mcq=>mcq.StudentAnswerInMCQ).
                HasForeignKey(smcq=>smcq.MultipleChoiceQuestionId).
                OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(smsq => new { smsq.StudentExamId, smsq.MultipleChoiceQuestionId }).IsUnique();
        }
    }
}
