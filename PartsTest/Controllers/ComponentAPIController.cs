using Nest;
using PartsTest.DTO;
using PartsTest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace PartsTest.Controllers
{
    public class ComponentAPIController : ApiController
    {

        private IBaseRepository<ComponentDTO, string> _ComponentDTORepo=null;

        public ComponentAPIController(IBaseRepository<ComponentDTO, string> repositoryBase)

        {
            _ComponentDTORepo = repositoryBase;          

        }


        /// <summary>
        /// Inserts the ComponentDTO record on ElasticDB
        /// </summary>
        /// <param name="ComponentDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType((typeof(ComponentDTO)))]
        public IHttpActionResult CreateComponentDTO(ComponentDTO ComponentDTO)
        {
  
            var result = _ComponentDTORepo.Insert(ComponentDTO);

            return Ok(result);
        }


        ///// <summary>
        ///// Retrieves all Components from ElasticDB
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IHttpActionResult> GetAllComponents()
        //{

        //    var t1 = Task.Run(() => _ComponentDTORepo.GetAll().OrderBy(a => a.Id));

        //    await Task.FromResult(t1);

        //    var result = t1.Result;

        //    return Ok(result); ;
        //}

        /// <summary>
        /// Retrieves all Components from ElasticDB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllComponents()
        {
            var result = _ComponentDTORepo.GetAll().OrderBy(a=>a.Name);
            
            return Ok(result); ;
        }
        /// <summary>
        /// Retrieves ComponentDTO by Id
        /// </summary>
        /// <param name="id">id of the element</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(ComponentDTO))]
        public IHttpActionResult GetComponentDTOById(string id)
        {
            string result = "";
            ComponentDTO component = new ComponentDTO();
            try
            {
                 component = _ComponentDTORepo.GetById(id);
                if (component == null)
                {
                    return NotFound();
                }
                result = "ok";
            }catch(Exception ex)
            {
                result = "ko";
                throw ex;
            }
            return Ok(component);
        }
        /// <summary>
        /// Deletes register in the ElasticDB
        /// </summary>
        /// <param name="id">id of the element</param>
        /// <returns></returns>
        [HttpDelete]
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteComponentDTO(string id)
        {
            string result = "";
            try
            {
                ComponentDTO component = _ComponentDTORepo.GetById(id);
                if (component == null)
                {
                    return NotFound();
                }
                IDeleteResponse resp= _ComponentDTORepo.Delete(id);
                result = "ok";
            }
            catch(Exception ex)
            {
                result = "ko";
                throw ex;
            }
            return Ok(result);
        }
        /// <summary>
        /// Updates component in the ElasticDB
        /// </summary>
        /// <param name="id">id of the element</param>
        /// <param name="component"></param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeInfo(string id, ComponentDTO component)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != component.Id)
            {
                return BadRequest();
            }

            try
            {
                _ComponentDTORepo.Update(id, component);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


    }
}
