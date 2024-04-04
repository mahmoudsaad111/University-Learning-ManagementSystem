using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Application.Config
{
	public class DepartementConfig : IEntityTypeConfiguration<Departement>
	{
		public void Configure(EntityTypeBuilder<Departement> builder)
		{
			//Properties
			builder.Property(d=>d.Name).IsRequired(true).HasMaxLength(50);
			builder.Property(d => d.StudentServiceNumber).IsRequired(false).HasMaxLength(20);
			builder.Property(d => d.ProfHeadName).IsRequired(false).HasMaxLength(50);
			builder.Property(d=>d.FacultyId).IsRequired(true);

			//Keys
			builder.HasKey(d => d.DepartementId);

			//Rlationships (foreign keys)
			//builder.HasOne(d => d.Faculty).WithMany(f => f.Departements).HasForeignKey(d=>d.FacultyId);
			builder.HasMany(d => d.AcadimicYears).WithOne(a => a.Departement).HasForeignKey(a => a.DepartementId).OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(d => d.Students).WithOne(s => s.Departement).HasForeignKey(s => s.DepartementId).OnDelete(DeleteBehavior.NoAction);
			builder.HasMany(d => d.Professors).WithOne(p => p.Departement).HasForeignKey(p => p.DepartementId).OnDelete(DeleteBehavior.NoAction);
			builder.HasMany(d => d.Instructors).WithOne(i => i.Departement).HasForeignKey(i => i.DepartementId).OnDelete(DeleteBehavior.NoAction);
			builder.HasMany(d => d.CoursesCategories).WithOne(Cc => Cc.Departement).HasForeignKey(Cc => Cc.DepartementId).OnDelete(DeleteBehavior.NoAction);

			//indexes and constrtains
			builder.HasIndex(i => new { i.Name, i.FacultyId }).IsUnique(true);
		}
	}
}
