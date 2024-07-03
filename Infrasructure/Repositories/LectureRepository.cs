using Application.Common.Interfaces.InterfacesForRepository;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Infrastructure.Repositories
{
    public class LectureRepository : BaseRepository<Lecture>,ILectureRepository
    {
        public LectureRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        public async Task<IEnumerable<Comment>> GetLectureComments(int LectureId)
        {
            try
            {
                var Comments = await(from c in _appDbContext.Comments
                                  where c.LectureId == LectureId
                                  join cr in _appDbContext.CommentReplies on c.CommentId equals cr.CommentId
                                  select new Comment
                                  {
                                      CommentId = c.CommentId, 
                                      Content = c.Content,
                                      CommentReplies = c.CommentReplies,
                                      CreatedBy=c.CreatedBy,    
                                      CreatedAt=c.CreatedAt,

                                  }).ToListAsync();
                return Comments;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Comment>();
            }
        }

        public async Task<IEnumerable<Lecture>> GetLecturesOfCourseCycle(int CourseCycleId)
        {
            try
            {
                var Lectures = await _appDbContext.Lectures.Where(l => l.CourseCycleId == CourseCycleId).ToListAsync();
                return Lectures;    
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Lecture>();
            }
        }

        public async Task<IEnumerable<Lecture>> GetLecturesOfSection(int SectionId)
        {
            try
            {
                var Lectures = await _appDbContext.Lectures.Where(l => l.SectionId == SectionId).ToListAsync();
                return Lectures;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Lecture>();
            }
        }
    }
}
