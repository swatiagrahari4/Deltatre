using DeltatreSearch.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DeltatreSearch.Server
{
    public class CacheService: ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;

        public CacheService(IDistributedCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration;
        }

        public string Get(string key)
        {
            var value = _cache.GetString(key);

            if (value != null)
            {
                _cache.SetString(key, value+1);
                return key;
            }

            return default;
        }

        public List<SearchClass> GetTopFive()
        {
            //var value = _;
            //var server = _cache.GetServer("localhost", 5003);
            ConfigurationOptions options = ConfigurationOptions.Parse(_configuration.GetConnectionString("DefaultConnection"));
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(options);
            IDatabase db = connection.GetDatabase();
            EndPoint endPoint = connection.GetEndPoints().First();
            RedisKey[] keys = connection.GetServer(endPoint).Keys().ToArray();
            var server = connection.GetServer(endPoint);

            List<SearchClass> allWords = new List<SearchClass>();
            foreach (string key in keys)
            {
                allWords.Add(new SearchClass { word = key, count = Convert.ToInt32(_cache.GetString(key)) });
            }
            allWords.OrderByDescending(u => u.count).Take(5);
          

            return allWords;
        }


        public string Set(string key, int value)
        {
            var options = new DistributedCacheEntryOptions();

            _cache.SetString(key, JsonConvert.SerializeObject(value), options);

            return key;
        }
    }
}
