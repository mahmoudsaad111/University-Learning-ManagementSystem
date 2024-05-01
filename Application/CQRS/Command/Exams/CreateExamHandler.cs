using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;


namespace Application.CQRS.Command.Exams
{
    public class CreateExamHandler : ICommandHandler<CreateExamCommand, Exam>
    {
        private readonly IUnitOfwork unitOfwork;

        public CreateExamHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
 
        public async Task<Result<Exam>> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.ExamDto.SartedAt < DateTime.Now)
                    return Result.Failure<Exam>(new Error(code: "Create Exam", message: "Can not create exam with this data time"));

               

              
                var ExamDto = request.ExamDto; // type of ExamDto
                Exam ExamWillbeAdded= ExamDto.GetExam();
              
                ExamPlace ValidExamPalce = null;
                bool IfValidExamPalceExist = false; // To insure that combination between foreign keys and ExamType i correct for Example (ExamType.Semester && CourseId=0 && sectionId!=0) refused


                //check if examPlace for course (semester or final type) exist or not
                //if not exist create one and get Id 
                var TupleOfBoolAndExamPlace = await unitOfwork.ExamRepository.CheckIfExamDataValidHasValidExamPlace(ExamDto);
                /*
                 * " CheckIfExamDataValidHasValidExamPlace " this function has three cases
                 * case 1: if the data is valid and this exam place is not exist it well return (false , exam place)
                 * case 2 :if the data is valid and the exam place is exist it well return (true , exam place)
                 * case 3: if data is no valid return (false ,null)
                 */

                IfValidExamPalceExist = TupleOfBoolAndExamPlace.Item1;
                ValidExamPalce = TupleOfBoolAndExamPlace.Item2;


                if (ValidExamPalce is not null  && !IfValidExamPalceExist )
                {
                    ValidExamPalce = await unitOfwork.ExamPlaceRepository.CreateAsync(ValidExamPalce);
                    await unitOfwork.SaveChangesAsync();

                    IfValidExamPalceExist = true; 
                }

                if (IfValidExamPalceExist  ) 
                {
                    ExamWillbeAdded.ExamPlaceId = ValidExamPalce.ExamPlaceId;
                    ExamWillbeAdded.FullMark = 0; // well computed as the marks of each Question ; 
                    Exam ExamAdded =await unitOfwork.ExamRepository.CreateAsync(ExamWillbeAdded); 
                    await unitOfwork.SaveChangesAsync();    
                    
                    if(ExamAdded is not null) 
                        return Result.Success<Exam>(ExamAdded);
                }

                return Result.Failure<Exam>(new Error(code: "Create Exam", message: "Can not create exam with this data"));


            }
            catch (Exception ex)
            {
                return Result.Failure<Exam>(new Error(code: "Create Exam", message: ex.Message.ToString())) ;
            }
        }
    }
}
