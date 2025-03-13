using BlogApi.DTO;

namespace BlogApi.Models.Interface
{
    public interface IBlogPosts
    {
        Task<List<Posts>> AllPosts();
        Task<Posts> OnePosts(int id);
        Task<Posts> AddPosts(PostDTO postDTO);
        Task<Posts> UpdatePosts(int id, PostDTO postDTO);
        Task<bool> DeletePosts(int id);
    }
}
