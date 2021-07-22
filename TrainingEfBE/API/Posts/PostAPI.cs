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
            return _postData.GetPosts();
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
    }
}
