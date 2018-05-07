using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartsTest.DTO
{
    public class SearchResultDTO<T>
    {

            public SearchResultDTO(List<T> results, string scrollId)
            {
                Results = results;
                ScrollId = scrollId;
            }

            public List<T> Results { get; set; }
            public string ScrollId { get; set; }
        
    }
}