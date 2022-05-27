using DataaParserService.Common.ResponseType;
using Microsoft.AspNetCore.Mvc;
using StudentData.Business.Interfaces;
using System.IO;

namespace StudentDataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class StudentDataController : ControllerBase
    {
        private readonly IStudentFileDataParser _dataParser;

        public StudentDataController(IStudentFileDataParser studentFileDataParser)
        {
            _dataParser = studentFileDataParser;
        }
        [HttpPost]
        public IActionResult Post()
        {
            ResponseDetails response;
            try
            {
                var files = Request.Form.Files[0];
                if (files == null)
                {
                    return BadRequest("File not found");
                }

                Stream stream = files.OpenReadStream();

                response = _dataParser.ParseStudentDataFromFile(stream);

                if(response.ResponseStatus == ResponseStatus.Error)
                { 
                    return StatusCode(500, response.ResponseMessage);
                }
                if(response.ResponseStatus == ResponseStatus.Notfound)
                {
                    return NotFound(response);
                }
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

            return Ok(response);

        }
    }
}
