using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.UserUpdatedDto
{
    public class UserUpdatedDto
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
        public bool Gender { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime BirthDay { get; set; } = DateTime.MinValue;

        [Required]
        public string PhoneNumber { get; set; }
    }
}
