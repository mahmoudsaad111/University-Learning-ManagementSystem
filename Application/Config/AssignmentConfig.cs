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
    public class AssignmentConfig : IEntityTypeConfiguration<Assignment>

    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.ToTable("Assignments"); 
            // properties 
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.FullMark).IsRequired(true);

            //The next two lines are commented because are not requied and i will updata databse
          //  builder.Property(x => x.ModelAnswerUrl).IsRequired(false); 
           // builder.Property(x=>x.UrlOfResource).IsRequired(true);

            //keys
             builder.HasKey(x => x.AssignmentId);
            // relationships
            builder.HasMany(x => x.AssignmentAnswers).WithOne(y => y.Assignment).OnDelete(DeleteBehavior.Cascade); 
           
            // the next line is commented because we delete relation between assignment and Lecture and make the relation betwen section and Assignmnet
           //builder.HasOne(a=>a.lecture).WithMany(l=>l.Assignments).HasForeignKey(a=>a.LectureId); 
            //indexes
             
           
        }
    }
}
