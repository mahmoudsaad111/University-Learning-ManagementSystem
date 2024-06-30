using Application.Common.Interfaces.InterfacesForRepository;
using Contract.Dto.ReturnedDtos;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>,IPostRepository
    {
        public PostRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        public async Task<IEnumerable<Post>> GetCourseCyclePosts(int CourseCycleId)
        {
            try
            {
                var posts = await (from p in _appDbContext.Posts
                                   where p.CourseCycleId == CourseCycleId
                                   join pr in _appDbContext.postReplies on p.PostId equals pr.PostId
                                   select new Post
                                   {
                                       PostId = p.PostId,
                                       ReplaysOnPost = p.ReplaysOnPost,
                                       PublisherId = p.PublisherId,
                                       Content = p.Content, 
                                       CreatedAt = p.CreatedAt,
                                       UpdatedAt=p.UpdatedAt,
                                       CreatedBy = p.CreatedBy, 

                                   }).ToListAsync();
                return posts;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Post>();
            }
        }

        public async Task<IEnumerable<Post>> GetSectionPosts(int SectionId)
        {
            try
            {
                var posts = await (from p in _appDbContext.Posts
                                   where p.SectionId == SectionId
                                   join pr in _appDbContext.postReplies on p.PostId equals pr.PostId
                                   select new Post
                                   {
                                       PostId = p.PostId,
                                       ReplaysOnPost = p.ReplaysOnPost,
                                       PublisherId = p.PublisherId,
                                       Content = p.Content,
                                       CreatedAt = p.CreatedAt,
                                       UpdatedAt = p.UpdatedAt,
                                       CreatedBy = p.CreatedBy,

                                   }).ToListAsync();
                return posts;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Post>();
            }
        }
    }
}
