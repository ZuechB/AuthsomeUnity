using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthsomeServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthsomeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post(Login login)
        {
            if (login.Email == "test@hotmail.com" && login.Password == "test1234")
            {
                return Ok(new LoginResult()
                {
                    firstName = "Brandon",
                    lastName = "Zuech",
                    email = "test@hotmail.com"
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
