using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrainingEfBE.API.Posts;
using TrainingEfBE.API.Users;
using TrainingEfBE.Models;

namespace TrainingEfBE.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PostsController: ControllerBase
    {
        private readonly PostAPI _postAPI;
        private readonly UserAPI _userAPI;

        public PostsController(DataContext _context)
        {
            _postAPI = new PostAPI(new PostData(_context));
            _userAPI = new UserAPI(new UserData(_context));
        }

        [HttpGet]
        public List<Post> Get()
        {
            return _postAPI.GetPosts();
        }

        [HttpGet("byPID/{id}")]
        public IActionResult GetPostByPID([FromRoute] int id)
        {
            Post post = _postAPI.GetPostByPID(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpGet("byUID/{id}")]
        public IActionResult GetPostByUID([FromRoute] int id)
        {
            List<Post> posts = _postAPI.GetPostByUID(id);

            return Ok(posts);
        }

        [HttpPost]
        public IActionResult AddPost([FromBody] Post post)
        {

            User _user = _userAPI.GetUser(post.CreatedBy);

            if(_user == null)
            {
                ModelState.AddModelError("Error", "Invalid user ID");
                return BadRequest(ModelState);
            }
            Post _post = _postAPI.AddPost(post);
            
            if (_post == null)
            {
                ModelState.AddModelError("Error", "Invalid user payload");
                return BadRequest(ModelState);
            }

            return CreatedAtAction("GetPostByPID", new { id = post.PostID }, post);
        }

        [HttpDelete("byPID/{id}")]
        public IActionResult DeletePost([FromRoute] int id)
        {
            Post post = _postAPI.GetPostByPID(id);

            if (post == null)
            {
                return NotFound();
            }

            _postAPI.DeletePost(id);

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdatePost([FromBody] Post post)
        {
            Post _post = _postAPI.GetPostByPID(post.PostID);

            if (_post == null)
            {
                ModelState.AddModelError("Error", "Post not found");
                return BadRequest(ModelState);
            }

            _postAPI.UpdatePost(post);
            return Ok();

        }

    }


}
