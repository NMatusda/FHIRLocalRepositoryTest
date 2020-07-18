using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LocalFHIRRepository.Model;
using LocalFHIRRepository.ParameterContainer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LocalFHIRRepository.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ObservationController : ControllerBase
    {
        // GET: Observation
        [HttpGet]
        public IEnumerable<string> Get()
        {
            // 未実装
            return new string[] { "value1", "value2" };
        }

        // GET: Observation/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            // 未実装
            return "value";
        }

        // POST: Observation
        [HttpPost]
        public HttpResponseMessage Post([FromBody] JObject value)
        {
            try
            {
                BasePostModel postmodel;
                bool setvalue;
                if (bool.TryParse(Startup.GetSettings("CoordinateToDirectDB"), out setvalue))
                {
                    if (setvalue)
                    {
                        postmodel = new DataBasePostModel();
                    }
                    else
                    {
                        postmodel = new SynchronizeIOPostModel();
                    }
                }
                else
                {
                    postmodel = new SynchronizeIOPostModel();
                }

                if (!postmodel.TryPost(new ObservationParameterContainer() { JsonParameter = value, }))
                {
                    return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest, };
                }
                else
                {
                    return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, };
                }
            }
            catch
            {
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.InternalServerError, };
            }
        }

        // PUT: Observation/5
        [HttpPut("{id}")]
        public void Put([FromBody] JObject value)
        {
            // 未実装
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // 未実装
        }
    }
}
