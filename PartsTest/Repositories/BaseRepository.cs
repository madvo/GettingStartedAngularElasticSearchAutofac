using Elasticsearch.Net;
using Nest;
using PartsTest.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;

namespace PartsTest.Repositories
{

    public class BaseRepository<TEntity, TIdentifier> : IBaseRepository<TEntity, TIdentifier> where TEntity : EntityDTO

    {
        protected IElasticClient _client;

        protected Refresh _refresh = 0;

        private object _lock = new object();

        public BaseRepository()
        {
            CreateElasticSearchClient();
        }

        private void CreateElasticSearchClient()
        {
            lock (_lock)
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
            var response = _client.Get<TEntity>
                (new DocumentPath<TEntity>(id.ToString()), g => g.Index(GetIndex<TEntity>()));
            if (response.Source != null)
                response.Source.Id = response.Id;
            return response.Source;
        }

        public  IDeleteResponse Delete(TIdentifier id)
        {
            var response = _client.Delete<TEntity>
                (new DocumentPath<TEntity>(id.ToString()), g => g.Index(GetIndex<TEntity>()).Refresh(_refresh));
            return response;
        }

        public  IUpdateResponse<TEntity> Update(TIdentifier id, TEntity component)
        {
            var response = _client.Update<TEntity>
                (new DocumentPath<TEntity>(id.ToString()), g => g.Index(GetIndex<TEntity>()).Doc(component).Refresh(_refresh));
            return response;
        }

        public  List<TEntity> GetAll()
        {
            var result = GetData(0, 2000);
            List<TEntity> data = result.Results;
            return data;
        }

        public string Insert(TEntity doc)
        {
            if (!_client.IndexExists(GetIndex<TEntity>()).Exists)
            {
                _client.CreateIndex(GetIndex<TEntity>());
            }
            doc.Id = System.Guid.NewGuid().ToString();
            var response = _client
                .Index<TEntity>(doc, g => g.Index(GetIndex<TEntity>())
                .Type(GetType<TEntity>())
                .Refresh(_refresh));

            return response.Id;
        }

        #region helpers

        private SearchResultDTO<TEntity> GetData(int from, int size, QueryContainer query = null)
        {
            ISearchResponse<TEntity> response = _client.Search<TEntity>(s => s
               .Index(GetIndex<TEntity>())
               .From(from)
               .Size(size)
               .Query(q => query)
               .SearchType(Elasticsearch.Net.SearchType.QueryThenFetch)
               .Scroll("5m")
              );
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

        private static string GetIndex<T>()
        {
            var details = GetCustomAttribute<T, ElasticIndexDetailsAttribute>();

            if (details == null)
            {
                throw new Exception("ElasticIndexDetailsAttribute has not been set for the type " + typeof(T).FullName);
            }
            var index = details.IndexName;
           
            return index;
        }

        private static string GetType<T>()
        {
            var details = GetCustomAttribute<T, ElasticIndexDetailsAttribute>();

            if (details == null)
            {
                throw new Exception("ElasticIndexDetailsAttribute has not been set for the type " + typeof(T).FullName);
            }
            var type = details.TypeName;

            return type;
        }

        private static TAttr GetCustomAttribute<T, TAttr>()
        {
            Type type = typeof(T);
            TAttr[] attribs = type.GetCustomAttributes(
                 typeof(TAttr), false) as TAttr[];
            return attribs[0];
        }
        #endregion
    }

}