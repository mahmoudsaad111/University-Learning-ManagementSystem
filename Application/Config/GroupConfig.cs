using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Application.Config
{
	public class GroupConfig : IEntityTypeConfiguration<Group>
	{
		public void Configure(EntityTypeBuilder<Group> builder)
		{
			//Global 
			builder.ToTable("Groups");

			//Properties
			builder.Property(g => g.Name).IsRequired(true).HasMaxLength(50);
			builder.Property(g => g.StudentHeadName).IsRequired(false).HasMaxLength(50);
			builder.Property(g => g.StudentHeadPhone).IsRequired(false).HasMaxLength(20);
			builder.Property(g => g.NumberOfStudent).HasDefaultValue(0);
		  //HasDefaultValueSql("SELECT COUNT(*) FROM dbo.Students join dbo.Group WHERE Group.GroupId=Student.GroupId");


			// Keys
			builder.HasKey(g => g.GroupId);

			//Realationships (foreign keys)
			builder.HasMany(g => g.CourseCycles).WithOne(cc => cc.Group).HasForeignKey(cc => cc.GroupId).OnDelete(DeleteBehavior.NoAction);
			//builder.HasOne(g => g.Departement).WithMany(d => d.Groups).HasForeignKey(g => g.DepartementId).OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(g => g.Students).WithOne(s => s.Group).HasForeignKey(s => s.GroupId).OnDelete(DeleteBehavior.NoAction);
		
			//indexs and constrains
			
		}
	}
}
