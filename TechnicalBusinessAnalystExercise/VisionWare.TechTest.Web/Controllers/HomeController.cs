using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using VisionWare.TechTest.Common.Models;
using VisionWare.TechTest.Data.Repositories;
using VisionWare.TechTest.Web.Models.Home;

namespace VisionWare.TechTest.Web.Controllers
{
    /// <summary>
    /// The <see cref="HomeController"/>
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// The post repository
        /// </summary>
        private readonly IPostRepository postRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public HomeController()
        {
            this.postRepository = new PostRepository(); // IoC usually...
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>The view.</returns>
        public async Task<ActionResult> Index()
        {
            var recentPosts = await this.postRepository.GetRecentPosts().ConfigureAwait(false);

            var model = new IndexModel
            {
                RecentPosts = recentPosts
            };

            return View(model);
        }

        /// <summary>
        /// News the post.
        /// </summary>
        /// <returns>The view.</returns>
        [HttpGet]
        public ActionResult NewPost()
        {
            return View(new NewPostModel());
        }

        /// <summary>
        /// News the post.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The view.</returns>
        [HttpPost]
        public async Task<ActionResult> NewPost(NewPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var post = new Post
            {
                Author = User.Identity.Name,
                Title = model.Title,
                Content = model.Content,
                Tags = model.Tags.Split(' ', ',', ';'),
                CreatedAtUtc = DateTime.UtcNow,
                Comments = new List<Comment>()
            };

            await this.postRepository.CreatePost(post).ConfigureAwait(false);

            return RedirectToAction("Post", new { id = post.Id });
        }

        /// <summary>
        /// Posts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The view.</returns>
        [HttpGet]
        public async Task<ActionResult> Post(string id)
        {
            var post = await this.postRepository.FindPost(id).ConfigureAwait(false);

            if (post == null)
            {
                return RedirectToAction("Index");
            }

            var model = new PostModel
            {
                Post = post,
                NewComment = new NewCommentModel
                {
                    PostId = id
                }
            };

            return View(model);
        }

        /// <summary>
        /// Postses the specified tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>The view.</returns>
        [HttpGet]
        public async Task<ActionResult> Posts(string tag = null)
        {
            var posts = await this.postRepository.GetRecentPosts(tag).ConfigureAwait(false);
            return View(posts);
        }

        /// <summary>
        /// News the comment.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The view.</returns>
        [HttpPost]
        public async Task<ActionResult> NewComment(NewCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Post", new { id = model.PostId });
            }

            var comment = new Comment
            {
                Author = User.Identity.Name,
                Content = model.Content,
                CreatedAtUtc = DateTime.UtcNow
            };

            await this.postRepository.AddComment(model.PostId, comment).ConfigureAwait(false);

            return RedirectToAction("Post", new { id = model.PostId });
        }
    }
}