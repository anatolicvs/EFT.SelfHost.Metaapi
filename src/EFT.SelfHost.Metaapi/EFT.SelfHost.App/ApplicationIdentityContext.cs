namespace EFT.Meta.SelfHost.Api
{
    using AspNet.Identity.MongoDB;
    using MongoDB.Driver;

    public class ApplicationIdentityContext : IdentityContext
    {
        public ApplicationIdentityContext(IMongoContext mongoContext)
            : this(mongoContext.Users, mongoContext.Roles)
        {
        }

        public ApplicationIdentityContext(MongoCollection users, MongoCollection roles)
            : base(users, roles)
        {
        }
    }
}
