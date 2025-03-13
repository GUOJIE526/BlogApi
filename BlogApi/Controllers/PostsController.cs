using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogApi.Models;
using BlogApi.Models.Interface;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using BlogApi.DTO;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly BlogContext _context;
        private readonly IBlogPosts _blogPosts;

        public PostsController(BlogContext context, IBlogPosts blogPosts)
        {
            _context = context;
            _blogPosts = blogPosts;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<List<Posts>>> GetPosts()
        {
            return await _blogPosts.AllPosts();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Posts>> GetPosts(int id)
        {
            var post = await _blogPosts.OnePosts(id);
            if (post == null)
            {
                return BadRequest(new {message = "查無此文章"});
            }
            return post;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosts(int id, [FromForm] PostDTO postDTO)
        {
            var RenewPosts = await _blogPosts.UpdatePosts(id, postDTO);
            return Ok(RenewPosts);
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Posts>> PostPosts([FromBody] PostDTO postDTO)
        {
            return await _blogPosts.AddPosts(postDTO);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosts(int id)
        {
            return Ok(await _blogPosts.DeletePosts(id));
        }

        private bool PostsExists(int id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}
