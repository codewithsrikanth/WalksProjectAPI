using Microsoft.AspNetCore.Mvc;

namespace DemoProjectAPI.Controllers
{
    //https://localhost:portnumber/api/Student
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        //https://localhost:portnumber/api/Student
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] { "Charan","Sathvik","Archana","Gayathri","Sudeepthi" };
            return Ok(studentNames);
        }
    }
}
