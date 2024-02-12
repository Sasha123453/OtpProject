using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OtpProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly IOtpSender _sender;
        private readonly IDbEditor _dbEditor;
        public OtpController(IOtpSender sender, IDbEditor dbEditor)
        {
            _sender = sender;
            _dbEditor = dbEditor;
        }
        [Route("/sendotp")]
        [HttpPost]
        public async Task<IActionResult> Create(string number)
        {
            try
            {
                await _sender.Send(number);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("/verifyotp")]
        [HttpPost]
        public async Task<IActionResult> Verify(string number, string code)
        {
            try
            {
                _dbEditor.Verify(number,code);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
