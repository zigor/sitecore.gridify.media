namespace Sitecore.Gridify.Media.Driver
{
    /// <summary>
    ///     Database context
    /// </summary>
    public class DbContext
    {
        /// <summary>
        ///     Gets or sets the media.
        /// </summary>
        /// <value>
        ///     The media.
        /// </value>
        public IMediaDataRepository Media
        {
            get;
            protected set;
        }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="DbContext" /> is enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Available
        {
            get
            {
                return this.Media.Available;
            }
        }
    }
}