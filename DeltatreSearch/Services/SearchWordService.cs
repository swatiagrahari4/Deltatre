using DeltatreSearch.Contracts;
using DeltatreSearch.Server;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltatreSearch.Services
{
    public class SearchWordService: SearchWord.SearchWordBase
    {
        private readonly ILogger<SearchWordService> _logger;
        private readonly ICacheService _cache;
        public SearchWordService(ILogger<SearchWordService> logger, ICacheService cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public override Task<WordReply> SearchKeyWord(WordModel request, ServerCallContext context)
        {
            WordReply reply = new WordReply();
            var resultWord = _cache.Get(request.Word);
            if (resultWord != null)
                reply.Result = "Found";
            else
                reply.Result = "Not Found";

            //return base.SearchKeyWord(request, context);
            return Task.FromResult(reply);
        }

        public override Task<WordReply> UpdateKeyWord(WordModel request, ServerCallContext context)
        {
            WordReply reply = new WordReply();
            reply.Result = "Updated";
            var resultWord = _cache.Set(request.Word,1);            
                
            return Task.FromResult(reply);
        }

        public override Task FindTopFiveKeyWord(ReturnTopFiveModel request, IServerStreamWriter<ReturnTopFiveReply> responseStream, ServerCallContext context)
        {
            List<SearchClass> listreply = new List<SearchClass>();
            listreply = _cache.GetTopFive();
            return Task.FromResult(listreply);
        }
    }
}
