using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewApiApp.Services;

namespace ReviewApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesController : ControllerBase
    {
        [HttpPost]

        public ActionResult Create([FromServices] IMailService mailService,[FromHeader] int id)
        {

            mailService.Send("hello you", $"this is a test for {id}");
            return Ok();
        }
    }

    public class Person
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
    } }

/*
we have many ways to assignment values to api parameters
1.[Fromheader]
2.[FromBody]
3.[FromRoute]
4.[FromForm]
5.[FromQuery]
6.[FromService]
*/