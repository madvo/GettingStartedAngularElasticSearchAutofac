using PartsTest.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartsTest.Repositories
{
   public interface IComponentRepository
    {

        List<ComponentDTO> GetAll();
    }
}
