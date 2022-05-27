using DataaParserService.Common.ResponseType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScoreData.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreDataService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeScoreController : ControllerBase
    {
        private readonly IScoreDataParser _scoreDataParser;

        public GradeScoreController(IScoreDataParser scoreDataParser)
        {
            _scoreDataParser = scoreDataParser;
        }

        [HttpPost]

        public IActionResult Get([FromBody]int[] subjectIds)
        {
            ResponseDetails response = new ResponseDetails();
            
            try
            {
                response.ResponseData = _scoreDataParser.GetSubjectScoreData(subjectIds);

                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    return StatusCode(500, response.ResponseMessage);
                }
                if (response.ResponseStatus == ResponseStatus.Notfound)
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
