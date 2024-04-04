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
    public class StudentNoteConfig: IEntityTypeConfiguration<StudentNote>

    {
        public void Configure(EntityTypeBuilder<StudentNote> builder)
        {
            builder.ToTable("StudentNotes");
            // properties 
            builder.Property(sn => sn.Content).IsRequired(true).HasMaxLength(150);
  

            //keys
            builder.HasKey(sn => sn.StudentNoteId);
            // relationships
            builder.HasOne(sn => sn.Lecture).WithMany(l=>l.StudentNotes).HasForeignKey(l=>l.LectureId);
            builder.HasOne(sn => sn.Student).WithMany(s => s.StudentNotes).HasForeignKey(sn => sn.StudentId);
            //indexes
            builder.HasIndex(x => new { x.StudentId, x.LectureId }).IsUnique(true); 


        }
    }
}
