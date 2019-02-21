using MongoDB.Driver;
using System.Threading.Tasks;
using VisionWare.TechTest.Common.Models;

namespace VisionWare.TechTest.Data.Repositories
{
    /// <summary>
    /// The <see cref="UserRepository"/>
    /// </summary>
    /// <seealso cref="IUserRepository" />
    public class UserRepository : Repository, IUserRepository
    {
        /// <summary>
        /// Finds the user by email.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns>
        /// The <see cref="User" />
        /// </returns>
        public async Task<User> FindUserByEmail(string emailAddress)
        {
            return await this.DbContext.Users.Find(x => x.Email == emailAddress).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="email">The email.</param>
        public void AddUser(string username, string email)
        {
            this.DbContext.Users.InsertOne(new User { Email = email, Name = username });
        }
    }
}
