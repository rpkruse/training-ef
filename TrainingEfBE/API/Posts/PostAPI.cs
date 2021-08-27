 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingEfBE.Models;

namespace TrainingEfBE.API.Posts
{
    public class PostAPI
    {
        private readonly IPostData _postData;

        public PostAPI(IPostData PostData)
        {
            _postData = PostData;
        }

        public List<Post> GetPosts()
        {
            var posts = _postData.GetPosts();
            posts = posts.OrderByDescending(val => val.Rating).ToList();
            return posts;
        }

        public List<Post> GetPostByUID(int UserID) 
        {
            return _postData.GetPostByUID(UserID);
        }

        public Post GetPostByPID(int PostID)
        {
            return _postData.GetPostByPID(PostID);
        }

        public Post AddPost(Post Post)
        {   

            return _postData.AddPost(Post);
        }

        public Post UpdatePost(Post Post)
        {
            return _postData.UpdatePost(Post);
        }

        public bool DeletePost(int PostID)
        {
            return _postData.DeletePost(PostID);
        }

        public List<Post> GetPostsByRoomID(int RoomID)
        {
            //return _postData.GetPostsByRoomID(RoomID);
            return _postData.GetPostsByRoomID(RoomID).OrderByDescending(val => val.Rating).ToList();

        }

        public bool AddUserUpvote(int postID, int userID)
        {
            return _postData.AddUpvote(postID, userID);
        }

        public bool AddUserDownvote(int postID, int userID)
        {
            return _postData.AddDownvote(postID, userID);
        }
    }
}
