using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllCars()
        {
            string[] cars = new string[] { "renault", "honda", "ford", "ferari" };
            return Ok(cars);

        }
    }
}
