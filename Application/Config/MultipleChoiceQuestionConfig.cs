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
    public class MultipleChoiceQuestionConfig : IEntityTypeConfiguration<MultipleChoiceQuestion>
    {
        public void Configure(EntityTypeBuilder<MultipleChoiceQuestion> builder)
        {
            builder.HasKey(mcq => mcq.QuestionId);
            builder.Property(mcq => mcq.Text).IsRequired(true);
            builder.Property (mcq => mcq.ExamId).IsRequired(true);
            builder.Property(mcq=>mcq.Degree).IsRequired(true); 
            
            builder.Property(mcq => mcq.OptionA).IsRequired(true);
            builder.Property(mcq => mcq.OptionB).IsRequired(true);
            builder.Property(mcq => mcq.OptionC).IsRequired(true);
            builder.Property(mcq => mcq.OptionD).IsRequired(true);

            builder.HasOne(mcq => mcq.Exam).WithMany(ex => ex.MultipleChoiceQuestions).HasForeignKey(mcq => mcq.ExamId);
            builder.Property(mcq=>mcq.CorrectAnswer).IsRequired(true);  

        }
    }
}
