using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Application.Config
{
	public class FacultyConfig : IEntityTypeConfiguration<Faculty>
	{
		public void Configure(EntityTypeBuilder<Faculty> builder)
		{
			//Properites
			builder.Property(f => f.Name).IsRequired(true).HasMaxLength(50);
			builder.Property(f => f.StudentServiceNumber).IsRequired(false).HasMaxLength(20);	
			builder.Property(f => f.ProfHeadName).IsRequired(false).HasMaxLength(60);

			//Keys
			builder.HasKey(f => f.FacultyId);

			//Relations
			builder.HasMany(f => f.Departements).WithOne(d => d.Faculty).HasForeignKey(d => d.FacultyId).OnDelete(DeleteBehavior.NoAction);

			//Indexes and constrains
			builder.HasIndex(f => f.Name).IsUnique(true);
		}
	}
}
