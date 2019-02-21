using System.Collections.Generic;
using System.Threading.Tasks;
using VisionWare.TechTest.Common.Models;

namespace VisionWare.TechTest.Data.Repositories
{
    /// <summary>
    /// The <see cref="IPostRepository"/>
    /// </summary>
    public interface IPostRepository
    {
        /// <summary>
        /// Finds the recent posts.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>
        /// The list of recent posts.
        /// </returns>
        Task<List<Post>> GetRecentPosts(string tag = "");

        /// <summary>
        /// Finds the post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The <see cref="Post"/></returns>
        Task<Post> FindPost(string id);

        /// <summary>
        /// Creates the post.
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task CreatePost(Post post);

        /// <summary>
        /// Initializes a new instance of the <see cref="IPostRepository" /> interface.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>
        /// The task,
        /// </returns>
        Task AddComment(string postId, Comment comment);
    }
}
