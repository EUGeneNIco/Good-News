using Microsoft.AspNetCore.Mvc;

namespace GoodNewsApp.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class SystemController : ControllerBase
  {
    public SystemController()
    {
    }

    [HttpGet("getDateStatement")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDateStatement()
    {
      try
      {
        var dateNow = DateTime.Now;
        var dayOfTheWeek = dateNow.DayOfWeek.ToString();
        var longDateTimeString = dateNow.ToString("MMMM dd, yyyy");

        // Thursday, March 21 2024
        var dateStatement = string.Format("{0}, {1}", dayOfTheWeek, longDateTimeString);
        return Ok(new { dateStatement });
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }
  }
}
