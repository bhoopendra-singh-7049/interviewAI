using DocumentFormat.OpenXml.Packaging;
using InterviewAI.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.CompilerServices;

namespace InterviewAI.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MasterController : ControllerBase
  {
        private readonly IMainManager _manager;

        public MasterController(IMainManager manager)
        {
            _manager = manager;
        }


        [HttpPost("GetFile")]
        public async Task<IActionResult> GetFile([FromForm] IFormFile file)
        {

            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Invalid file");
                }

                var result = "";

                if (file.FileName.Contains(".pdf"))
                {
                     result = await _manager.ExtractFromPdf(file);
                }
                else
                {
                     result = await _manager.ExtractFromDocx(file);
                }
                
                return Ok(result);
               
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {e.Message}");
            }
        }
  }
}
