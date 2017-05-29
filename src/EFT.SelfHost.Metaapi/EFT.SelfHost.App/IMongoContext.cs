namespace EFT.Meta.SelfHost.Api
{
    using Entities;
    using MongoDB.Driver;
    using MongoDB.Driver.GridFS;

    public interface IMongoContext
    {
        MongoDatabase Database { get; }

        MongoCollection<User> Users { get; }
        MongoCollection<Role> Roles { get; }
        MongoCollection<Client> Clients { get; }
        MongoCollection<RefreshToken> RefreshTokens { get; }
    }
}
