using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartsTest.DTO
{
    [ElasticIndexDetails("components", "Component_Info")]
    [ElasticsearchType(Name = "Component_Info", IdProperty = "Id")]
    public class ComponentDTO:EntityDTO
    {
        //название, цена, артикул, страна изготовления, срок службы        
        [Text]
        public string Name { get; set; }
        [Number(NumberType.Double, Coerce = true, DocValues = true)]
        public string Price { get; set; }
        [Text]
        public string Type { get; set; }
        [Text]
        public string Country { get; set; }
        [Date(Format = "mmddyyyy")]
        public DateTime DueDate { get; set; }
      
    }
}