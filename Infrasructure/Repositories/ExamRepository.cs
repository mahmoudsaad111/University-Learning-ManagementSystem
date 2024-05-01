using Application.Common.Interfaces.InterfacesForRepository;
using Contract.Dto;
using Contract.Dto.Exams;
using Contract.Dto.MCQs;
 
using Contract.Dto.TFQs;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.EntityFrameworkCore;
 

namespace Infrastructure.Repositories
{
    public class ExamRepository : BaseRepository<Exam>, IExamRepository
    {
        public ExamRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }


        
        public async Task<bool> UpdateExamFullMarksAccordingToCommandOfQuestion(int ExamId, int NewQuestionDegree, int OldQuestionDegree )
        {

            /*
             * the values of (NewQuestionDegree , OldQuestionDegree)
             * when create new  Question should pass (NewQuestionDegree , 0)
             * when update exist Question should pass (NewQuestionDegree, OldQuestionDegree)
             * when delete Question should pass (0 ,OldQuestionDegree )
             */
            var exam = await _appDbContext.Exams.FirstOrDefaultAsync(e => e.ExamId == ExamId); 
            if (exam == null)
            {
                return false ; 
            }
            exam.FullMark += NewQuestionDegree;
            exam.FullMark -= OldQuestionDegree;
            return true; 
            // save changes using unit of work ; 
        }

        public async Task<Tuple<bool, ExamPlace>> CheckIfExamDataValidHasValidExamPlace(ExamDto ExamDto)
        {

            /*
             * this function to check if the Foreign keys of Exam is valid to make -or retuen if exist- Exam place
             * 
             * case 1: if the data is valid and this exam place is not exist it well return (false , exam place)
             * case 2 :if the data is valid and the exam place is exist it well return (true , exam place)
             * case 3: if data is no valid return (false ,null)
             * there is no meaning for (true,null)
             */


            // in case 2 : the exam place is not found in data base , so the client of this serves should create the exam place i return to him (i will create it here without added on database)

            ExamPlace currentExamPlace = null;
            ExamPlace IfExamPalceExist = null;

            bool ValidData = false;

            //check if examPlace for course (semester or final type) exist or not
            //if not exist create one and get Id 
            if (ExamDto.ExamType == ExamType.Semester && ExamDto.CourseId != 0)
            {
                currentExamPlace = ExamDto.GetExamPlaceDto().GetExamPlaceOfCourseSemester();
                IfExamPalceExist = await _appDbContext.ExamPlaces.Where(ep => ep.ExamType == currentExamPlace.ExamType && ep.CourseId == currentExamPlace.CourseId).FirstOrDefaultAsync();
                ValidData = true;
            }

            else if (ExamDto.ExamType == ExamType.Final && ExamDto.CourseId != 0)
            {
                currentExamPlace = ExamDto.GetExamPlaceDto().GetExamPlaceOfCourseFinal();
                IfExamPalceExist = await _appDbContext.ExamPlaces.Where(ep => ep.ExamType == currentExamPlace.ExamType && ep.CourseId == currentExamPlace.CourseId).FirstOrDefaultAsync(); ;
                ValidData = true;
            }

            else if (ExamDto.ExamType == ExamType.Midterm && ExamDto.CourseCycleId != 0)
            {
                currentExamPlace = ExamDto.GetExamPlaceDto().GetExamPlaceOfCourseCycleMidterm();
                IfExamPalceExist = await _appDbContext.ExamPlaces.Where(ep => ep.ExamType == currentExamPlace.ExamType && ep.CourseCycleId == currentExamPlace.CourseCycleId).FirstOrDefaultAsync(); ;
                ValidData = true;
            }

            else if (ExamDto.ExamType == ExamType.Quiz && ExamDto.CourseCycleId != 0)
            {
                currentExamPlace = ExamDto.GetExamPlaceDto().GetExamPlaceOfCourseCycleQuiz();
                IfExamPalceExist = await _appDbContext.ExamPlaces.Where(ep => ep.ExamType == currentExamPlace.ExamType && ep.CourseCycleId == currentExamPlace.CourseCycleId).FirstOrDefaultAsync(); ;
                ValidData = true;
            }
            else if (ExamDto.ExamType == ExamType.Quiz && ExamDto.SectionId != 0)
            {
                currentExamPlace = ExamDto.GetExamPlaceDto().GetExamPlaceOfSectioneQuiz();
                IfExamPalceExist = await _appDbContext.ExamPlaces.Where(ep => ep.ExamType == currentExamPlace.ExamType && ep.SectionId == currentExamPlace.SectionId).FirstOrDefaultAsync(); ;
                ValidData = true;
            }


            if (ValidData)
            {
                if (IfExamPalceExist is not null)
                    return new Tuple<bool, ExamPlace>(true, IfExamPalceExist);
                else
                    return new Tuple<bool, ExamPlace>(false, currentExamPlace);
            }

            return new Tuple<bool, ExamPlace>(false, null);
        }

        public async Task<IEnumerable<QuizsToSectionOfInstructorDto>> GetQuizsToSectionOfInstructors(int sectionId)
        {
            var QuizsToSectionOfInstructors = await (from section in _appDbContext.Sections
                                                     where section.SectionId == sectionId
                                                     join examPlace in _appDbContext.ExamPlaces on section.SectionId equals examPlace.SectionId
                                                     join exam in _appDbContext.Exams on examPlace.ExamPlaceId equals exam.ExamPlaceId
                                                     //  join studentInExam in _appDbContext.StudentExams on exam.ExamId equals studentInExam.ExamId
                                                     // join MCQs in _appDbContext.MultipleChoiceQuestions on exam.ExamId equals MCQs.ExamId
                                                     //join TFQs in _appDbContext.TrueFalseQuestions on exam.ExamId equals TFQs.ExamId

                                                     select new QuizsToSectionOfInstructorDto
                                                     {
                                                         SectionId = sectionId,
                                                         SectionName = section.Name,
                                                         ExamId = exam.ExamId,
                                                         ExamName = exam.Name,
                                                         ExamFullMarks=exam.FullMark,
                                                         StartedAt = exam.StratedAt,
                                                         DeadLine = exam.DeadLine,
                                                         TFQs = (from tfq in _appDbContext.TrueFalseQuestions
                                                                 where tfq.ExamId == exam.ExamId
                                                                 select new TFQTextAsnwerDto
                                                                 {
                                                                     QuestionId = tfq.QuestionId,
                                                                     Text = tfq.Text,
                                                                     IsTrue = tfq.IsTrue,
                                                                     Degree=tfq.Degree
                                                                     
                                                                 }).ToList(),

                                                         MCQs = (from mcq in _appDbContext.MultipleChoiceQuestions
                                                                 where mcq.ExamId == exam.ExamId
                                                                 select new MCQTextOPtionsAnswerDto
                                                                 {
                                                                     QuestionId = mcq.QuestionId,
                                                                     Text = mcq.Text,
                                                                     OptionA = mcq.OptionA,
                                                                     OptionB = mcq.OptionB,
                                                                     OptionC = mcq.OptionC,
                                                                     OptionD = mcq.OptionD,
                                                                     CorrectAnswer = mcq.CorrectAnswer,
                                                                     Degree=mcq.Degree

                                                                 }).ToList(),

                                                         StudentsAttendExam = (from sie in _appDbContext.StudentExams
                                                                               where sie.ExamId == exam.ExamId
                                                                               join user in _appDbContext.Users on sie.StudentId equals user.Id
                                                                               select new NameIdDto
                                                                               {
                                                                                   Id = user.Id,
                                                                                   Name = user.FullName
                                                                               }
                                                                               ).ToList()


                                                     }).ToListAsync();
            return QuizsToSectionOfInstructors; 

        }

        public async Task<Exam> GetExamIncludinigExamPlaceByExamId(int ExamId)
        {
            return await _appDbContext.Exams.AsNoTracking().Include(x => x.ExamPlace).FirstOrDefaultAsync(e => e.ExamId == ExamId);
        }

        public async Task<ExamWrokNowDto> GetExamWorkNow(int ExamId)
        {
            var examWorkNow= await (from e in _appDbContext.Exams
                          where e.ExamId == ExamId
                          select new ExamWrokNowDto
                          {
                              ExamId = e.ExamId,
                              ExamName = e.Name,
                              ExamTitle = e.Title,
                              ExamFullMarks = e.FullMark,
                              ExamDateTime = e.StratedAt,
                              ExamDeadLine = e.DeadLine,
                              mCQTextOptionsDtos =  (from mcq in _appDbContext.MultipleChoiceQuestions
                                                    where e.ExamId == mcq.ExamId
                                                    select new MCQTextOptionsDto
                                                    {
                                                        QuestionId = mcq.QuestionId,
                                                        Text = mcq.Text,
                                                        OptionA = mcq.OptionA,
                                                        OptionB = mcq.OptionB,
                                                        OptionC = mcq.OptionC,
                                                        OptionD = mcq.OptionD,
                                                        Degree = mcq.Degree
                                                    }).ToList(),
                              tFQTextOptionsDtos =  ( from tfq in _appDbContext.TrueFalseQuestions
                                                    where e.ExamId == tfq.ExamId
                                                    select new TFQTextDto
                                                    {
                                                        QuestionId = tfq.QuestionId,
                                                        Text = tfq.Text,
                                                        Degree = tfq.Degree
                                                    }).ToList()

                          } ).FirstOrDefaultAsync();

            return examWorkNow;
        }

    

    }
}
