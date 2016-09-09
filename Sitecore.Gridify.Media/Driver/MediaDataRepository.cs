using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Sitecore.Diagnostics;
using Sitecore.Gridify.Media.Configuration;

namespace Sitecore.Gridify.Media.Driver
{
    /// <summary>
    /// Media data repository
    /// </summary>
    /// <seealso cref="Sitecore.Gridify.Media.Driver.IMediaDataRepository" />
    public class MediaDataRepository : IMediaDataRepository
    {
        /// <summary>
        ///     The database
        /// </summary>
        private readonly IMongoDatabase database;

        /// <summary>
        ///     The options
        /// </summary>
        private readonly GridFSBucketOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaDataRepository" /> class.
        /// </summary>
        /// <param name="connectionStringName">The connection string.</param>
        /// <param name="bucketName">Name of the bucket.</param>
        public MediaDataRepository(string connectionStringName, string bucketName)
        {
            Assert.ArgumentNotNullOrEmpty(connectionStringName, nameof(connectionStringName));
            Assert.ArgumentNotNullOrEmpty(bucketName, nameof(bucketName));

            this.database = MongoDbDriver.GetDatabaseFromConnectionStringName(connectionStringName);

            this.options = new GridFSBucketOptions
            {
                BucketName = bucketName,
                ChunkSizeBytes = Settings.GridFSBucket.ChunkSizeBytes
            };
        }

        /// <summary>
        ///     Sets the specified t.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public async Task<bool> Set(string id, Stream stream)
        {
            Assert.ArgumentNotNullOrEmpty(id, nameof(id));
            Assert.ArgumentNotNull(stream, nameof(stream));

            await this.GetBucket().UploadFromStreamAsync(id, stream, new GridFSUploadOptions
            {
                ChunkSizeBytes = this.options.ChunkSizeBytes
            });

            return true;
        }

        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<Stream> Get(string id)
        {
            Assert.ArgumentNotNullOrEmpty(id, nameof(id));

            return await this.GetBucket().OpenDownloadStreamByNameAsync(id);
        }

        /// <summary>
        ///     Determines whether [contains] [the specified t].
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     <c>true</c> if [contains] [the specified t]; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> Contains(string id)
        {
            Assert.ArgumentNotNullOrEmpty(id, nameof(id));

            using (var cursor = await this.GetCursor(id))
            {
                return (await cursor.ToListAsync()).Any();
            }
        }

        /// <summary>
        ///     Removes the specified t.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> Remove(string id)
        {
            Assert.ArgumentNotNullOrEmpty(id, nameof(id));

            using (var cursor = await this.GetCursor(id))
            {
                await this.GetBucket().DeleteAsync(cursor.FirstOrDefault().Id);
            }

            return true;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IMediaDataRepository" /> is available.
        /// </summary>
        /// <value>
        ///   <c>true</c> if available; otherwise, <c>false</c>.
        /// </value>
        public bool Available
        {
            get
            {
                return this.database != null;
            }
        }

        /// <summary>
        ///     Gets the bucket.
        /// </summary>
        /// <returns></returns>
        private IGridFSBucket GetBucket()
        {
            return new GridFSBucket(this.database, this.options);
        }

        /// <summary>
        /// Gets the cursor.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        private Task<IAsyncCursor<GridFSFileInfo>> GetCursor(string fileName, int limit = 1)
        {
            return this.GetBucket().FindAsync(
                Builders<GridFSFileInfo>.Filter.Eq(f => f.Filename, fileName),
                new GridFSFindOptions {Limit = limit});
        }
    }
}