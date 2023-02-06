using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace CrediPayPublic.Models.BLL.Redis
{
    public interface IRedisCache
    {
        Task<bool> SetCache(string key, string data);

        Task<string> GetCache(string key);

        Task<bool> DeleteCache(string key);

        Task<string> GenerateCode(string code);

        Task<bool> SetCacheIfNotExistAsync(string key, string data);
    }

    public class RedisCache : IRedisCache
    {
        private readonly IDatabase _cache;
        private IConfiguration _config;
        private string _env;
        private readonly ILogger<RedisCache> _logger;

        public RedisCache(IConfiguration config, ILogger<RedisCache> logger)
        {
            _config = config;
            _cache = ConnectionMultiplexer.Connect(_config.GetConnectionString("CacheConnection")).GetDatabase();
            _env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            _logger = logger;
        }

        public async Task<bool> SetCache(string key, string data)
        {
            try
            {
                _logger.LogInformation($"{_env}-{key}", data);
                await _cache.StringSetAsync($"{_env}-{key}", data);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{_env}-{key}{Environment.NewLine}{data}", ex);

                return false;
            }
        }

        public async Task<bool> SetCacheIfNotExistAsync(string key, string data)
        {
            try
            {
                _logger.LogInformation($"{_env}-{key}", data);
                await _cache.StringGetSetAsync($"{_env}-{key}", data);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{_env}-{key}{Environment.NewLine}{data}", ex);

                return false;
            }
        }

        public async Task<string> GetCache(string key)
        {
            try
            {
                if (await _cache.KeyExistsAsync($"{_env}-{key}"))
                {
                    var data = await _cache.StringGetAsync($"{_env}-{key}");
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "redisGet");

                return "";
            }
        }

        public async Task<bool> DeleteCache(string key)
        {
            try
            {
                if (await _cache.KeyExistsAsync($"{_env}-{key}"))
                {
                    var data = await _cache.KeyDeleteAsync($"{_env}-{key}");
                    return data;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "redisGet");

                return false;
            }
        }

        public async Task<string> GenerateCode(string code)
        {
            var lastCustomerNumber = await GetCache($"{_env}-{code}");
            int newCustomerNumber = 1;
            if (string.IsNullOrEmpty(lastCustomerNumber))
            {
                bool result = await SetCache($"{_env}-{code}", newCustomerNumber.ToString());
            }
            else
            {
                newCustomerNumber = int.Parse(lastCustomerNumber) + 1;
                bool result = await SetCache($"{_env}-{code}", newCustomerNumber.ToString());
            }
            return $"CRD-{newCustomerNumber.ToString().PadLeft(5, '0')}{code}";
        }
    }
}