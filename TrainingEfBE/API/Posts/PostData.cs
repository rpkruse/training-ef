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
            var _post = _context.Post.Include(u => u.User).Include(c => c.Category).SingleOrDefault(p => p.PostID == post.PostID);

            return _post;
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
            return _context.Post.Include(u => u.User).Include(x => x.Category).ToList();
        }

        public Post UpdatePost(Post post)
        {
            _context.Post.Update(post);
            _context.SaveChanges();

            return post;
        }

        public List<Post> GetPostsByRoomID(int RoomID)
        {
            return _context.Post.AsNoTracking().Where(p => p.RoomID == RoomID).Include(u => u.User).Include(x => x.Category).ToList();
        }

        public bool AddUpvote(int postID, int userID)
        {
            bool hasUpvoted = _context.UpvotePost.AsNoTracking().SingleOrDefault(p => p.PostID == postID && p.UserID == userID) != null;
            if (hasUpvoted)
            {
                return false;
            }

            var newUpvote = new UpvotePost { UserID = userID, PostID = postID };

            _context.UpvotePost.Add(newUpvote);
            _context.SaveChanges();
            return true;
        }

        public bool AddDownvote(int postID, int userID)
        {
            bool hasDownvoted = _context.DownvotePost.AsNoTracking().SingleOrDefault(p => p.PostID == postID && p.UserID == userID) != null;
            if (hasDownvoted)
            {
                return false;
            }

            var newDownvote = new DownvotePost { UserID = userID, PostID = postID };

            _context.DownvotePost.Add(newDownvote);
            _context.SaveChanges();
            return true;
        }
    }
}
