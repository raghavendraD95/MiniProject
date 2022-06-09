using DataaParserService.Common.ResponseType;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StudentData.Business.Interfaces;
using StudentDataService.WebSocketUtility.Interface;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudentDataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class StudentDataController : ControllerBase
    {
        private readonly IStudentFileDataParser _dataParser;
        private readonly IWebSocketManager _webSocketManager;

        public StudentDataController(IStudentFileDataParser studentFileDataParser, IWebSocketManager webSocketManager)
        {
            _dataParser = studentFileDataParser;
            _webSocketManager = webSocketManager;
        }
        [HttpPost]
        public async Task<IActionResult> Post()
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

                foreach (var socket in _webSocketManager.GetAllActiveWebSockets())
                {
                    await socket.SendAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response, new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
                    })),
                        WebSocketMessageType.Text,
                        true,
                        CancellationToken.None) ;
                    System.Console.WriteLine("Sent data for the socket");
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
