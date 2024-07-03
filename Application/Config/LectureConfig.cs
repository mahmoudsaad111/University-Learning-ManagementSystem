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
    public class LectureConfig : IEntityTypeConfiguration<Lecture>

    {
        public void Configure(EntityTypeBuilder<Lecture> builder)
        {
            builder.ToTable("Lectures");
            // properties 
            builder.Property(l => l.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(l => l.VedioUrl).IsRequired(true).HasMaxLength(150);     

            //keys
            builder.HasKey(l => l.LectureId);
            // relationships
            builder.HasMany(l => l.LectureResources).WithOne(ls => ls.Lecture).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(l => l.Comments).WithOne(c => c.lecture).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(l => l.StudentNotes).WithOne(sn => sn.Lecture).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(l => l.Student_Lectures).WithOne(sl => sl.lecture).OnDelete(DeleteBehavior.Cascade);


            //& the next line is commented because we make the relation between Section and assignment not between lecture and assignments
            //builder.HasMany(l => l.Assignments).WithOne(a => a.lecture).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(l=>l.Section).WithMany(s=>s.Lectures).HasForeignKey(l => l.LectureId);
            builder.HasOne(l => l.CourseCycle).WithMany(Cc => Cc.Lectures).HasForeignKey(l => l.CourseCycleId);


            builder.HasMany(l => l.LectureResources).WithOne(lr => lr.Lecture).HasForeignKey(lr => lr.LectureId).OnDelete(DeleteBehavior.NoAction);
            //indexes
            builder.HasIndex(l => l.SectionId).IsUnique(false);
            builder.HasIndex(l=>l.CourseCycleId).IsUnique(false);
            

        }
    }
}
