using StackExchange.Redis;
using Udemy.Services.Basket.Serttings;

namespace Udemy.Services.Basket.Services
{
    public class RedisService
    {
        private readonly string Host;
        private readonly int Port;
        private ConnectionMultiplexer _connection;
        public RedisService(RedisSettings redisSettings)
        {
            Host = redisSettings.Host;
            Port = redisSettings.Port;

        }
        public void Connect() => _connection = ConnectionMultiplexer.Connect($"{Host}:{Port}");
        public IDatabase Database(int db = 1) => _connection.GetDatabase(db);
    }
}
