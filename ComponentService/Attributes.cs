using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartsTest
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ElasticIndexDetailsAttribute : Attribute
    {
        private readonly string _indexName;
        private readonly bool _timeSeries;

        public ElasticIndexDetailsAttribute(string name, bool timeSeries)
        {
            _indexName = name;
            _timeSeries = timeSeries;
        }

        //Name of the index
        public string IndexName
        {
            get { return _indexName; }
        }

        //Flag for time based index
        public bool IsTimeSeries
        {
            get { return _timeSeries; }
        }
    }
}