using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using LocalFHIRRepository.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using LocalFHIRRepository.ParameterContainer;
using Newtonsoft.Json.Linq;

namespace LocalFHIRRepository.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IOptions<RepositorySetting> _repSetting;

        public PatientController(IOptions<RepositorySetting> repSetting)
        {
            this._repSetting = repSetting;
        }

        // GET: Patient/5
        [HttpGet("{family}")]
        public ActionResult<string> Get(string family)
        {
            // 未実装
            return $"value{family}";
        }

        // POST: Patient
        [HttpPost]
        public HttpResponseMessage Post([FromBody] JObject value)
        {
            try
            {
                // Settingにより処理方式を変更する
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
                // POST処理実行
                if (!postmodel.TryPost(new PatientParameterContainer() { JsonParameter = value, }))
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

        // PUT: Patient/5
        [HttpPut("{id}")]
        public HttpResponseMessage Put(int id, [FromBody] string value)
        {
            // 未実装
            return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, };
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            // 未実装
            return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, };
        }
    }
}
