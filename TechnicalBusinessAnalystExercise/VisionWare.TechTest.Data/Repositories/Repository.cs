using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionWare.TechTest.Data.Repositories
{
    /// <summary>
    /// The <see cref="Repository"/> class.
    /// </summary>
    public abstract class Repository
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly BlogContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        protected Repository()
        {
            this.dbContext = new BlogContext();
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        public BlogContext DbContext => this.dbContext;
    }
}
