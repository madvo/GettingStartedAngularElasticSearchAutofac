using Elasticsearch.Net;
using Nest;
using PartsTest.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartsTest.Repositories
{

    //public class RepositoryBase<TEntity, TIdentifier> : IRepositoryBase<TEntity, TIdentifier> where TEntity : EntityDTO
    public class RepositoryBase<TEntity, TIdentifier> where TEntity : EntityDTO

    {
        protected IElasticClient _client;
            //private static readonly string _scrollTime = "5m";
            //private string _connectionName;
            private object _lockObj = new object();

            public RepositoryBase()
            {
                CreateElasticSearchClient();
            }
            private void CreateElasticSearchClient()
            {
                lock (_lockObj)
                {
                    if (_client == null)
                    {
                        var connectionString = "http://localhost:9200";
                        var uri = new Uri(connectionString, UriKind.Absolute);
                        _client = CreateClient(uri);
                    }
                }
            }
            private static IElasticClient CreateClient(Uri connectionString)
            {
                var node = new UriBuilder(connectionString);
                var connectionPool = new SingleNodeConnectionPool(node.Uri);
                var connectionSettings = new ConnectionSettings(connectionPool);
                return new ElasticClient(connectionSettings);
            }

            public TEntity GetById(TIdentifier id)
            {
                var response = _client.Get<TEntity>(new DocumentPath<TEntity>(id.ToString()), g => g.Index("components"));
                if (response.Source != null)
                    response.Source.Id = response.Id;
                return response.Source;
            }

        public virtual IDeleteResponse Delete(TIdentifier id)
        {
            var response = _client.Delete<TEntity>(new DocumentPath<TEntity>(id.ToString()), g => g.Index("components"));      
            return response;
        }

        public virtual IUpdateResponse<TEntity> Update(TIdentifier id, TEntity component)
        {
            var response = _client.Update<TEntity>(new DocumentPath<TEntity>(id.ToString()), g => g.Index("components").Doc(component));
            return response;
        }

        public virtual List<TEntity> GetAll()
            {
                var result = GetPaginatedData(0, 2000);
                List<TEntity> all = result.Results;
                return all;
            }

          

            private SearchResultDTO<TEntity> GetPaginatedData(int from, int size, QueryContainer query = null)
            {
            ISearchResponse<TEntity> response = _client.Search<TEntity>(s => s
               .Index("components")
               .From(from)
               .Size(size)
               .Query(q => query)
               .SearchType(Elasticsearch.Net.SearchType.QueryThenFetch));         
            return GetSearchResultDTOs(response);
            }

            private static SearchResultDTO<TEntity> GetSearchResultDTOs(ISearchResponse<TEntity> response)
            {
                var documents = response.Hits.Select(hit =>
                {
                    var result = hit.Source;
                    result.Id = hit.Id;
                    return result;
                }).ToList();
                return new SearchResultDTO<TEntity>(documents, response.ScrollId);
            }

            public virtual string Insert(TEntity doc)
            {
            if (!_client.IndexExists("components").Exists)
            {
                _client.CreateIndex("components");
            }
            var response = _client
                .Index<TEntity>(doc, g => g.Index("components")
                .Type("Component_Info"))
                ;
            return response.Id;
            }

            private static string GetIndex<T>()
            {
            var details = GetCustomAttribute<T, ElasticIndexDetailsAttribute>();

            if (details == null)
                {
                    throw new Exception("ElasticIndexDetailsAttribute has not been set for the type " + typeof(T).FullName);
                }
                var index = details.IndexName;
                if (!details.IsTimeSeries)
                {
                    return index;
                }
                var date = DateTime.UtcNow.Date.ToShortDateString().Replace('-', '_');
            //return $"{index}_{date}";
            return index;
            }

            private static TAttr GetCustomAttribute<T, TAttr>()
            {
                Type type = typeof(T);
                TAttr[] attribs = type.GetCustomAttributes(
                     typeof(TAttr), false) as TAttr[];
                return attribs[0];
            }
        }
    
}