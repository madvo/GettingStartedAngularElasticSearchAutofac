using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartsTest.DTO
{
   //[ElasticsearchType(Name = "Component_Info", IdProperty = "Id")]
    public class EntityDTO
    {
        [Text]
        public string Id { get; set; }
    }
}