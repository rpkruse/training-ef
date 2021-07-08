using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingEfBE.Models;

namespace TrainingEfBE.API.Posts
{
    public interface IPostData
    {
        List<Post> GetPosts();
        List<Post> GetPostByUID(int UserID);
        Post GetPostByPID(int PostID);
        Post AddPost(Post Post);
        Post UpdatePost(Post Post);
        bool DeletePost(int PostID);
    }
}
