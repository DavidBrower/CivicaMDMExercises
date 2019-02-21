using System.Threading.Tasks;
using VisionWare.TechTest.Common.Models;

namespace VisionWare.TechTest.Data.Repositories
{
    /// <summary>
    /// The <see cref="IUserRepository"/>
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Finds the user by email.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns>The <see cref="User"/></returns>
        Task<User> FindUserByEmail(string emailAddress);

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="email">The email.</param>
        void AddUser(string username, string email);
    }
}
