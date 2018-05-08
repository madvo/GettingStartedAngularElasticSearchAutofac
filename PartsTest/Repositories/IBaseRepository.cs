using Nest;
using PartsTest.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PartsTest.Repositories
{
    public interface IBaseRepository<TEntity, TIdentifier> where TEntity : EntityDTO
    {
        /// <summary>
        /// deletes an item from the db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IDeleteResponse Delete(TIdentifier id);

        /// <summary>
        /// updates an item 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        IUpdateResponse<TEntity> Update(TIdentifier id, TEntity entity);

        /// <summary>
        /// gets entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(TIdentifier id);

        /// <summary>
        /// inserts an entity
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string Insert(TEntity data);

        /// <summary>
        /// gets a list of entities
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAll();
    }
}
