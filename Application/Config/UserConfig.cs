using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Config
{
	/*
	 * all comments has perfix $% is commented because it deleted from its model because already exist on IOdentityUsesr
	 */
	public class UserConfig : IEntityTypeConfiguration<User>
    {
		public void Configure(EntityTypeBuilder<User> builder)
		{
			//Global
			//builder.UseTptMappingStrategy();
			builder.ToTable("Users");
			//Properites

			builder.Property(u => u.FirstName).IsRequired(true).HasMaxLength(15);
			builder.Property(u => u.SecondName).IsRequired(true).HasMaxLength(15);
			builder.Property(u => u.ThirdName).IsRequired(true).HasMaxLength(15);
			builder.Property(u => u.FourthName).IsRequired(true).HasMaxLength(15);
			builder.Property(u => u.ImageUrl).IsRequired(false).HasMaxLength(150) ;
			//
		//	builder.Property(u => u.Photo).IsRequired(false); 
			//	builder.Property(u => u.FullName).
			//			    HasDefaultValueSql("CONCAT([FirstName], ' ', [SecondName], ' ', [ThirdName] ,' ',[FourthName])");


			//$%	builder.Property(u=>u.Email).IsRequired(true).HasMaxLength(100);
			//$%	builder.Property(u=>u.PhoneNumber).IsRequired(false).HasMaxLength(20);
			//$%    builder.Property(u => u.PhoneNumber).IsRequired(false).HasMaxLength(100);
			builder.Property(u => u.BirthDay).IsRequired(true);
		//	builder.Property(u=>u.Photo).IsRequired(false);

			//Keys
			//$%	builder.HasKey(u => u.UserId);

			//Relsationships
			builder.HasMany(u => u.Froms).WithOne(n => n.UserFrom).HasForeignKey(n => n.UserIdFrom).OnDelete(DeleteBehavior.NoAction);
			builder.HasMany(u => u.Tos).WithOne(n => n.UserTo).HasForeignKey(n => n.UserIdTo).OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(u => u.Posts).WithOne(p => p.Publisher).HasForeignKey(p => p.PublisherId).OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(u => u.ReplaysOnPosts).WithOne(rp => rp.Replier).HasForeignKey(rp => rp.ReplierId).OnDelete(DeleteBehavior.NoAction);
			builder.HasMany(u => u.Comments).WithOne(c => c.User).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.NoAction) ;
			builder.HasMany(u => u.CommentReplies).WithOne(cr => cr.User).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.NoAction);

			//indexes and constrains 
			//$%	builder.HasIndex(u => u.Email).IsUnique(true);
			builder.HasIndex(u => new {u.FirstName , u.SecondName , u.ThirdName , u.FourthName}).IsUnique(false);
			


		}
	}
}
