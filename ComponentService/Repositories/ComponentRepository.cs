using PartsTest.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartsTest.Repositories
{
    public class ComponentRepository: RepositoryBase<ComponentDTO, string>, IComponentRepository
    {

        public override List<ComponentDTO> GetAll()
        {
            return base.GetAll();
        }
    }
}