using System.IO;
using System.Threading.Tasks;

namespace Sitecore.Gridify.Media.Driver
{
    /// <summary>
    /// IMediaDataRepository
    /// </summary>
    public interface IMediaDataRepository
    {
        /// <summary>
        /// Sets the specified t.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        Task<bool> Set(string id, Stream stream);

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Stream> Get(string id);

        /// <summary>
        /// Determines whether [contains] [the specified t].
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified t]; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> Contains(string id);

        /// <summary>
        /// Removes the specified t.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<bool> Remove(string id);

        /// <summary>
        /// Gets a value indicating whether this <see cref="IMediaDataRepository"/> is available.
        /// </summary>
        /// <value>
        ///   <c>true</c> if available; otherwise, <c>false</c>.
        /// </value>
        bool Available { get; }
    }
}