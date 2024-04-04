using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Application.Config
{
	public class NotificationConfig : IEntityTypeConfiguration<Notification>
	{
		public void Configure(EntityTypeBuilder<Notification> builder)
		{
			//Global 
			builder.ToTable("Notifications");

			//Properties
			builder.Property(n => n.FromUserId).IsRequired(true);
			builder.Property(n => n.ToUserId).IsRequired(true);
			builder.Property(n => n.Content).IsRequired(true).HasMaxLength(500);
			builder.Property(n => n.UpdatedAt).IsRequired(true);
			// Keys
			builder.HasKey(n => n.NotificationId);
			//Realationships (foreign keys)

			//builder.HasOne(n => n.UserFrom).WithMany(u => u.Froms).HasForeignKey(n => n.UserIdFrom).OnDelete(DeleteBehavior.NoAction);
			//builder.HasOne(n => n.UserTo).WithMany(u => u.Tos).HasForeignKey(n => n.UserIdTo).OnDelete(DeleteBehavior.NoAction);


			//indexs and constrains
			builder.HasIndex(n => n.UserIdFrom).IsUnique(false);
			builder.HasIndex(n => n.UserIdTo).IsUnique(false);
		}
	}
}
