using System;
using System.IO;
using Sitecore.Data.DataProviders;
using Sitecore.Diagnostics;
using Sitecore.Gridify.Media.Driver;

namespace Sitecore.Gridify.Media.Provider
{
    /// <summary>
    ///     Sets the MongoDB GridFS storage for BLOBs data
    /// </summary>
    /// <seealso cref="Sitecore.Data.DataProviders.DataProvider" />
    public class GridFSMediaProvider : DataProvider
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GridFSMediaProvider" /> class.
        /// </summary>
        /// <param name="dbContext">The dbContext.</param>
        public GridFSMediaProvider(DbContext dbContext)
        {
            Assert.ArgumentNotNull(dbContext, nameof(dbContext));
            this.DbContext = dbContext;
        }

        /// <summary>
        ///     Gets the dbContext.
        /// </summary>
        /// <value>
        ///     The dbContext.
        /// </value>
        protected DbContext DbContext { get; }

        /// <summary>
        ///     BLOBs the stream exists.
        /// </summary>
        /// <param name="blobId">The BLOB identifier.</param>
        /// <param name="context">The dbContext.</param>
        /// <returns></returns>
        public override bool BlobStreamExists(Guid blobId, CallContext context)
        {
            Assert.ArgumentNotNull(blobId, nameof(blobId));
            Assert.ArgumentNotNull(context, nameof(context));

            if (!this.DbContext.Available)
            {
                return false;
            }

            context.Abort();

            return this.DbContext.Media.Contains(blobId.ToString("N")).Result;
        }

        /// <summary>
        ///     Gets the BLOB stream.
        /// </summary>
        /// <param name="blobId">The BLOB identifier.</param>
        /// <param name="context">The dbContext.</param>
        /// <returns></returns>
        public override Stream GetBlobStream(Guid blobId, CallContext context)
        {
            Assert.ArgumentNotNull(blobId, nameof(blobId));
            Assert.ArgumentNotNull(context, nameof(context));

            if (!this.DbContext.Available)
            {
                return null;
            }

            if (this.DbContext.Media.Contains(blobId.ToString("N")).Result)
            {
                return this.DbContext.Media.Get(blobId.ToString("N")).Result;
            }
            return null;
        }

        /// <summary>
        ///     Sets the BLOB stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="blobId">The BLOB identifier.</param>
        /// <param name="context">The dbContext.</param>
        /// <returns></returns>
        public override bool SetBlobStream(Stream stream, Guid blobId, CallContext context)
        {
            Assert.ArgumentNotNull(context, nameof(stream));
            Assert.ArgumentNotNull(blobId, nameof(blobId));
            Assert.ArgumentNotNull(context, nameof(context));

            if (!this.DbContext.Available)
            {
                return false;
            }

            return this.DbContext.Media.Set(blobId.ToString("N"), stream).Result;
        }

        /// <summary>
        ///     Removes the BLOB stream.
        /// </summary>
        /// <param name="blobId">The BLOB identifier.</param>
        /// <param name="context">The dbContext.</param>
        /// <returns></returns>
        public override bool RemoveBlobStream(Guid blobId, CallContext context)
        {
            Assert.ArgumentNotNull(blobId, nameof(blobId));
            Assert.ArgumentNotNull(context, nameof(context));

            if (!this.DbContext.Available)
            {
                return false;
            }

            context.Abort();

            return this.DbContext.Media.Remove(blobId.ToString("N")).Result;
        }
    }
}