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
    public class UserController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (id == 5)
            {
                return Ok(new User()
                {
                    firstName = "Brandon",
                    lastName = "Zuech",
                    email = "test@hotmail.com"
                });
            }

            return Unauthorized();
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] User user)
        {
            if (id == 5)
            {
                return Ok();
            }

            return Unauthorized();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 5)
            {
                return Ok();
            }

            return Unauthorized();
        }
    }
}