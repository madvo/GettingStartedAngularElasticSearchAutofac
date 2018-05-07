using PartsTest.DTO;
using PartsTest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ComponentService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ComponentService" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ComponentService.svc o ComponentService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ComponentService : IComponentService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        // private IRepositoryBase<ComponentDTO, string> _ComponentDTORepo;
        private RepositoryBase<ComponentDTO, string> _ComponentDTORepo = null;

        public ComponentService(IComponentRepository repos)

        {
            _ComponentDTORepo = new RepositoryBase<ComponentDTO, string>();

        }
        /// <summary>
        /// Inserts the ComponentDTO record on ElasticDB
        /// </summary>
        /// <param name="ComponentDTO"></param>
        /// <returns></returns>
     
        public string CreateComponentDTO(ComponentDTO ComponentDTO)
        {
            var result = _ComponentDTORepo.Insert(ComponentDTO);
            return result;
        }
        /// <summary>
        /// Retrieves all Components from ElasticDB
        /// </summary>
        /// <returns></returns>
       
        public IOrderedEnumerable<ComponentDTO> GetAllComponents()
        {
            var result = _ComponentDTORepo.GetAll().OrderBy(a=>a.Id);

            return result; ;
        }

        
        public ComponentDTO GetComponentDTOById(string id)
        {
            ComponentDTO component = _ComponentDTORepo.GetById(id);
            if (component != null)
            {

                return component;
            }
            else
                return new ComponentDTO();
        }

        public void DeleteComponentDTO(string id)
        {
            ComponentDTO component = _ComponentDTORepo.GetById(id);
            if (component != null)
            {
                _ComponentDTORepo.Delete(id);
            }
            
        }

       
        public void PutEmployeeInfo(string id, ComponentDTO component)
        {

            if (id == component.Id)
            {

                try
                {
                    _ComponentDTORepo.Update(id, component);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
    }
}
