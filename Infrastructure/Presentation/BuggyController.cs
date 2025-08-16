using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController :ControllerBase
    {
        [HttpGet("not-found")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }



        [HttpGet("servererror")]
        public IActionResult GetServerErorrRequest()
        {
            throw new Exception();
            return Ok(); 
        }


        [HttpGet("badrequest)")]
        public IActionResult GetBadReuest()
        {
            return BadRequest();
        }


        [HttpGet("badrequest/{id}/{age}")]
        public IActionResult GetBadReuest(int id,int age ) //Validation error
        {
            return BadRequest();
        }



        [HttpGet(template:"unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }

    }
}
