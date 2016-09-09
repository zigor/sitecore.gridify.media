using MongoDB.Driver.GridFS;
using SitecoreSettings = Sitecore.Configuration.Settings;

namespace Sitecore.Gridify.Media.Configuration
{
    /// <summary>
    /// Settings
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// GridFs Bucket default settings
        /// </summary>
        public class GridFSBucket
        {
            /// <summary>
            /// Gets the chunk size bytes.
            /// </summary>
            /// <value>
            /// The chunk size bytes.
            /// </value>
            public static int ChunkSizeBytes
            {
                get
                {
                    return SitecoreSettings.GetIntSetting("GridFSBucket.ChunkSizeBytes", ImmutableGridFSBucketOptions.Defaults.ChunkSizeBytes);
                }
            }
        }
    }
}