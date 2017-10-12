using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Completor;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        /// <summary>
        /// Use this method
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        // GET api/values/line/str
        [Route("line/{value}")]
        [HttpGet]
        public async Task<IEnumerable<string>> Get(string value)
        {
            return await Program.Processor.Response(value);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
