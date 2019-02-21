using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using System.Threading.Tasks;
using VisionWare.TechTest.Common.Models;

namespace VisionWare.TechTest.Data.Repositories
{
    /// <summary>
    /// The <see cref="PostRepository"/>
    /// </summary>
    /// <seealso cref="VisionWare.TechTest.Data.Repositories.Repository" />
    /// <seealso cref="VisionWare.TechTest.Data.Repositories.IPostRepository" />
    public class PostRepository : Repository, IPostRepository
    {
        /// <summary>
        /// Finds the recent posts.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>
        /// The list of recent posts.
        /// </returns>
        public async Task<List<Post>> GetRecentPosts(string tag = "")
        {
            Expression<Func<Post, bool>> filter = x => true;

            if (!string.IsNullOrWhiteSpace(tag))
            {
                filter = x => x.Tags.Contains(tag);
            }

            return await this.DbContext.Posts.Find(filter)
                .SortByDescending(x => x.CreatedAtUtc)
                .Limit(10)
                .ToListAsync();
        }

        /// <summary>
        /// Finds the post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The <see cref="Post"/></returns>
        public async Task<Post> FindPost(string id)
        {
            return await this.DbContext.Posts.Find(x => x.Id == id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Creates the post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <returns></returns>
        public async Task CreatePost(Post post)
        {
            await this.DbContext.Posts.InsertOneAsync(post);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IPostRepository" /> interface.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>
        /// The task,
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task AddComment(string postId, Comment comment)
        {
            UpdateDefinition<Post> update = Builders<Post>.Update.Push(x => x.Comments, comment);
            await this.DbContext.Posts.UpdateOneAsync(x => x.Id == postId, update);
        }
    }
}
