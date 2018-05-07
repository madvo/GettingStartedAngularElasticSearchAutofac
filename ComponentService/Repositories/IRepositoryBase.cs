﻿using Nest;
using PartsTest.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PartsTest.Repositories
{
    public interface IRepositoryBase<TEntity, TIdentifier> where TEntity : EntityDTO
    {

        IDeleteResponse Delete(TIdentifier id);
        IUpdateResponse<TEntity> Update(TIdentifier id, TEntity component);
        TEntity GetById(TIdentifier id);
        string Insert(TEntity doc);
        List<TEntity> GetAll();
    }
}
