using LiteDB;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using WebcomicNotify.Models;

namespace WebcomicNotify.Services
{
    public class DataService : IDisposable
    {
        private readonly ILogger _logger;
        private readonly LiteDatabase _db;

        public DataService(ILogger<DataService> logger)
        {
            _logger = logger;
            _db = new("webcomics.db");

            var mapper = BsonMapper.Global;

            mapper.Entity<Webcomic>()
                .DbRef(x => x.CustomWebhook);
            mapper.Entity<Webhook>()
                .DbRef(x => x.Webcomics);
        }
        public void Dispose()
            => _db.Dispose();

        public void Add<T>(T entity) where T : IEntity
        {
            var table = _db.GetCollection<T>();
            table.EnsureIndex(x => x.Id, true);
            table.Insert(entity);
            _logger.LogDebug("Added entity ({entity}) to the database: {id}", entity.GetType().FullName, entity.Id);
        }

        public void Remove<T>(T entity) where T : IEntity
        {
            var table = _db.GetCollection<T>();
            table.Delete(entity.Id);
            _logger.LogDebug("Removed entity ({entity}) from the database: {id}", entity.GetType().FullName, entity.Id);
        }

        public IEnumerable<T> Get<T>(Expression<Func<T, bool>> predicate, int skip = 0, int limit = int.MaxValue)
        {
            var table = _db.GetCollection<T>();
            return table.Find(predicate, skip, limit);
        }

        public IEnumerable<Webcomic> GetSimilar(string query, int limit = 25)
        {
            query = query.ToLower();
            var table = _db.GetCollection<Webcomic>();
            return table.FindAll().Where(x => x.Name.ToLower().Contains(query)).Take(limit);
        }
    }
}
