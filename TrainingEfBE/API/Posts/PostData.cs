using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingEfBE.Models;

namespace TrainingEfBE.API.Posts
{
    public class PostData : IPostData
    {

        private readonly DataContext _context;

        public PostData(DataContext context)
        {
            _context = context;
        }

        public Post AddPost(Post post)
        {
            _context.Post.Add(post);

            
            _context.SaveChanges();

            return post;
        }

        public bool DeletePost(int PostID)
        {
            Post post = GetPostByPID(PostID);

            if (post == null)
            {
                throw new Exception($"Unable to find user to delete with post ID {PostID}");
            }

            _context.Post.Remove(post);
            _context.SaveChanges();

            return true;
        }

        public Post GetPostByPID(int PostID)
        {
            return _context.Post.AsNoTracking().SingleOrDefault(p => p.PostID == PostID);
        }

        public List<Post> GetPostByUID(int UserID)
        {
            return _context.Post.AsNoTracking().Where(p => p.CreatedBy == UserID).ToList();
        }

        public List<Post> GetPosts()
        {
            return _context.Post.ToList();
        }

        public Post UpdatePost(Post post)
        {
            _context.Post.Update(post);
            _context.SaveChanges();

            return post;
        }
    }
}
