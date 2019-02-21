using VisionWare.TechTest.Common.Models;

namespace VisionWare.TechTest.Web.Models.Home
{
    /// <summary>
    /// The <see cref="PostModel"/> viewmodel class.
    /// </summary>
    public class PostModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostModel"/> class.
        /// </summary>
        public PostModel()
        {
            this.Post = new Post();
            this.NewComment = new NewCommentModel();
        }

        /// <summary>
        /// Gets or sets the post.
        /// </summary>
        /// <value>
        /// The post.
        /// </value>
        public Post Post { get; set; }

        /// <summary>
        /// Gets or sets the new comment.
        /// </summary>
        /// <value>
        /// The new comment.
        /// </value>
        public NewCommentModel NewComment { get; set; }
    }
}