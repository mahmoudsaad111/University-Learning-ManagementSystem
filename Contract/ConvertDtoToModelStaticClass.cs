using Contract.Dto.Faculties;
using Contract.Dto.ReturnedDtos;
using Domain.Models;
 

namespace Contract
{
	public static class ConvertDtoToModelStaticClass
	{
		public static ReturnedStudentDto ConvertStudnetToReutrnedStudentDto (this User user)
		{
			if (user.Student is null)
				throw new ArgumentNullException();
			return new ReturnedStudentDto
			{
				FirstName = user.FirstName,
				SecondName = user.SecondName,
				ThirdName = user.ThirdName,
				FourthName = user.FourthName,
				Email = user.Email,
				UserName = user.UserName,
				Address = user.Address,
				Gender = user.Gender,
				BirthDay = user.BirthDay,
				AcadimicYearId=  user.Student.AcadimicYearId,
				GPA= user.Student.GPA,
				DepartementId=user.Student.DepartementId,
				GroupId=user.Student.GroupId					 
			};
		}
		public static ReturnedInstructorDto ConvertInstructorToReutrnedInsructorDto(this User user)
		{
			if (user.Instructor is null)
				throw new ArgumentNullException();
			return new ReturnedInstructorDto
			{
				FirstName = user.FirstName,
				SecondName = user.SecondName,
				ThirdName = user.ThirdName,
				FourthName = user.FourthName,
				Email = user.Email,
				UserName = user.UserName,
				Address = user.Address,
				Gender = user.Gender,
				BirthDay = user.BirthDay,
				DepartementId = user.Instructor.DepartementId,
				Specification = user.Instructor.Specification
			};
		}
		public static ReturnedProfessorDto ConvertProfessorToReutrnedProfessorDto(this User user)
		{
			if (user.Professor is null)
				throw new ArgumentNullException();
			return new ReturnedProfessorDto
			{
				FirstName = user.FirstName,
				SecondName = user.SecondName,
				ThirdName = user.ThirdName,
				FourthName = user.FourthName,
				Email= user.Email,
				UserName = user.UserName,
				Address = user.Address,
				Gender = user.Gender,
				BirthDay = user.BirthDay,
				DepartementId = user.Professor.DepartementId,
				Specification = user.Professor.Specification
			};
		}
		public static FacultyDto ConvertFacultyToDto(this Faculty faculty)
		{
			return new FacultyDto
			{
				Name = faculty.Name,
				ProfHeadName = faculty.ProfHeadName,
				NumOfYears = faculty.NumOfYears,
				StudentServiceNumber = faculty.StudentServiceNumber,
			};
		}
	}
}
