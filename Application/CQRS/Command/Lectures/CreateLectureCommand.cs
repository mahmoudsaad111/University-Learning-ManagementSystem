﻿
using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Lectures;
using Domain.Models;


namespace Application.CQRS.Command.Lectures
{
    public class CreateLectureCommand : ICommand<Lecture>
	{
		public string CreatorUserName { get; set; }
		public bool IsProfessor { get; set; }	
		public LectureDto LectureDto { get; set; }
	}
}
