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
        private readonly string _typeName;

        public ElasticIndexDetailsAttribute(string name, string type)
        {
            _indexName = name;
            _typeName = type;
        }

        //Name of the index
        public string IndexName
        {
            get { return _indexName; }
        }
        //Name of the type
        public string TypeName
        {
            get { return _typeName; }
        }

    }
}