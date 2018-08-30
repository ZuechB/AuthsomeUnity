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
        // POST api/values
        [HttpPost]
        public ActionResult Post(Login login)
        {
            if (login.Email == "test@hotmail.com" && login.Password == "test1234")
            {
                return Ok(new User()
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

    }
}
