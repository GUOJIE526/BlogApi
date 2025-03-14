using BlogApi.DTO;
using BlogApi.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Models.Repository
{
    public class BlogRepositories : IBlogPosts
    {
        private readonly BlogContext _context;
        public BlogRepositories(BlogContext context)
        {
            _context = context;
        }
        public async Task<List<Posts>> AllPosts()
        {
            return await _context.Posts.ToListAsync();
        }
        public async Task<Posts> OnePosts(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return null;
            }
            return post;
        }
        public async Task<Posts> AddPosts(PostDTO postDTO)
        {
            var post = new Posts
            {
                Title = postDTO.Title,
                Content = postDTO.Content,
                CategoryId = postDTO.CategoryId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }
        public async Task<Posts> UpdatePosts(int id, PostDTO postDTO)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return null;
            }
            post.Title = postDTO.Title;
            post.Content = postDTO.Content;
            post.CategoryId = postDTO.CategoryId;
            post.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return post;
        }
        public async Task<bool> DeletePosts(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return false;
            }
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
