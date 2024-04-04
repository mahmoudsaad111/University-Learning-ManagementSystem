using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Domain.Models;
namespace Contract.Dto.UsersRegisterDtos
{
    public abstract class UserRegisterDto
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string SecondName { get; set; } = null!;
        [Required]
        public string ThirdName { get; set; } = null!;
        [Required]
        public string FourthName { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string UserName { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime BirthDay { get; set; } = DateTime.MinValue;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public User GetUser()
        {
            return new User
            {
                // not assgin password because password is not hashed on DTO
                FirstName = this.FirstName,
                SecondName = this.SecondName,
                ThirdName = this.ThirdName,
                FourthName = this.FourthName,
                Address = this.Address,
                Gender = this.Gender,
                BirthDay = this.BirthDay,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt,
                Email = this.Email,
                UserName = this.UserName,
                PhoneNumber=this.PhoneNumber
            };
        }
    }
}