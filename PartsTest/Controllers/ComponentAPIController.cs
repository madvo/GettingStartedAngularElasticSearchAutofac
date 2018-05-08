using Nest;
using PartsTest.DTO;
using PartsTest.Helpers;
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

        private IBaseRepository<ComponentDTO, string> _ComponentDTORepo = null;

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
        //[ResponseType((typeof(ComponentDTO)))]
        public IHttpActionResult CreateComponentDTO(ComponentDTO ComponentDTO)
        {
            Response _res = new Response();

            try
            {
                var result = _ComponentDTORepo.Insert(ComponentDTO);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _res.IsSuccess = false;
                _res.ErrorMessage = ex;
                return InternalServerError(new ApplicationException("Something went wrong in this request. internal exception: " + ex.Message));

            }
        }

        /// <summary>
        /// Retrieves all Components from ElasticDB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllComponents()
        {
            Response _res = new Response();

            try
            {
                var result = _ComponentDTORepo.GetAll().OrderBy(a => a.Name);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _res.IsSuccess = false;
                _res.ErrorMessage = ex;
                return InternalServerError(new ApplicationException("Something went wrong in this request. internal exception: " + ex.Message));
            }
        }

        /// <summary>
        /// Retrieves ComponentDTO by Id
        /// </summary>
        /// <param name="id">id of the element</param>
        /// <returns></returns>
        [HttpGet]
        //[ResponseType(typeof(ComponentDTO))]
        public IHttpActionResult GetComponentDTOById(string id)
        {
            Response _res = new Response();

            ComponentDTO component = new ComponentDTO();
            try
            {
                component = _ComponentDTORepo.GetById(id);

                if (component == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _res.IsSuccess = false;
                _res.ErrorMessage = ex;
                return InternalServerError(new ApplicationException("Something went wrong in this request. internal exception: " + ex.Message));
            }
            return Ok(component);
        }

        /// <summary>
        /// Deletes register in the ElasticDB
        /// </summary>
        /// <param name="id">id of the element</param>
        /// <returns></returns>
        [HttpDelete]
        //[ResponseType(typeof(void))]
        public IHttpActionResult DeleteComponentDTO(string id)
        {
            Response _res = new Response();

            try
            {
                ComponentDTO component = _ComponentDTORepo.GetById(id);
                if (component == null)
                {
                    return NotFound();
                }
                IDeleteResponse resp = _ComponentDTORepo.Delete(id);
                _res.IsSuccess = true;
                return Ok<Response>(_res);
            }

            catch (Exception ex)
            {
                _res.IsSuccess = false;
                _res.ErrorMessage = ex;
                return InternalServerError(new ApplicationException("Something went wrong in this request. internal exception: " + ex.Message));
            }

        }

        /// <summary>
        /// Updates component in the ElasticDB
        /// </summary>
        /// <param name="id">id of the element</param>
        /// <param name="component"></param>
        /// <returns></returns>
        [HttpPut]
        //[ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeInfo(string id, ComponentDTO component)
        {
            Response _res = new Response();
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
                _res.IsSuccess = true;
                return Ok<Response>(_res);
            }
            catch (Exception ex)
            {
                _res.IsSuccess = false;
                _res.ErrorMessage = ex;
                return InternalServerError(new ApplicationException("Something went wrong in this request. internal exception: " + ex.Message));

            }

        }


    }
}
