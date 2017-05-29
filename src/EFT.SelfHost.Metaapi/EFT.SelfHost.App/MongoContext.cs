namespace EFT.Meta.SelfHost.Api
{
    using Entities;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Driver;
    using System.Configuration;

    public class MongoContext : IMongoContext
    {
        private readonly MongoCollection<User> userCollection;
        private readonly MongoCollection<Role> roleCollection;
        private readonly MongoCollection<Client> clientCollection;
        private readonly MongoCollection<RefreshToken> refreshTokenCollection;

        public MongoContext()
        {
            var pack = new ConventionPack()
            {
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register("CamelCaseConvensions", pack, t => true);

            
            var mongoUrlBuilder = new MongoUrlBuilder(Properties.Settings.Default.AuthContext);

            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());
            var server = mongoClient.GetServer();

            Database = server.GetDatabase(mongoUrlBuilder.DatabaseName);

            userCollection = Database.GetCollection<User>("users");
            roleCollection = Database.GetCollection<Role>("roles");
            clientCollection = Database.GetCollection<Client>("clients");
            refreshTokenCollection = Database.GetCollection<RefreshToken>("refreshTokens");
        }

        public MongoDatabase Database { get; private set; }

        public MongoCollection<User> Users
        {
            get { return userCollection; }
        }

        public MongoCollection<Role> Roles
        {
            get { return roleCollection; }
        }

        public MongoCollection<Client> Clients
        {
            get { return clientCollection; }
        }

        public MongoCollection<RefreshToken> RefreshTokens
        {
            get { return refreshTokenCollection; }
        }
    }
}
