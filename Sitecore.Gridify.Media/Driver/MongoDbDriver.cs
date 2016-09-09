using System.Configuration;
using MongoDB.Driver;

namespace Sitecore.Gridify.Media.Driver
{
    /// <summary>
    ///     Mongo DB driver
    /// </summary>
    /// <seealso cref="Sitecore.Analytics.Data.DataAccess.MongoDb.MongoDbDriver" />
    internal class MongoDbDriver
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MongoDbDriver" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public MongoDbDriver(string connectionString) 
        {
            MongoUrl mongoUrl = new MongoUrl(connectionString);
            MongoClientSettings.FromUrl(mongoUrl);
            MongoClient mongoClient = new MongoClient();
            this.Database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public IMongoDatabase Database { get; private set; }

        /// <summary>
        ///     Gets the name of the database from connection string.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <returns></returns>
        public static IMongoDatabase GetDatabaseFromConnectionStringName(string connectionStringName)
        {
            if (ConfigurationManager.ConnectionStrings[connectionStringName] == null)
            {
                return null;
            }

            var driver = new MongoDbDriver(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString);
            return driver.Database;
        }
    }
}