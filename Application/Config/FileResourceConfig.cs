using Domain.Models;
using Domain.TmpFilesProcessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Config
{
    public class FileResourceConfig : IEntityTypeConfiguration<FileResource>
    {
        public void Configure(EntityTypeBuilder<FileResource> builder)
        {
            var fileTypeConverter = new EnumToStringConverter<FileType>();

            builder.HasKey(fr => fr.FileResourceId);
            builder.Property(fr => fr.Name).IsRequired(true).HasMaxLength(120);
            builder.Property(fr => fr.Url).IsRequired(true);
            builder.Property(fr => fr.FileType).HasConversion(fileTypeConverter).HasMaxLength(10);

            
            builder.HasDiscriminator<string>("Discriminator")
                   .HasValue<LectureResource>("LR")
                   .HasValue<AssignmentResource>("AR")
                   .HasValue<AssignmentAnswerResource>("AAR");

            builder.Property("Discriminator").HasMaxLength(5);
            
        }
    }
}
