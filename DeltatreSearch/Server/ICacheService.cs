using DeltatreSearch.Contracts;
using System.Collections.Generic;

namespace DeltatreSearch.Server
{
    public interface ICacheService
    {
        public string Get(string key);
        public List<SearchClass> GetTopFive();
        public string Set(string key, int value);
    }
}