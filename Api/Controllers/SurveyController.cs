using Application.Commands.Surveys;
using Application.Queries.Surveys;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyController : ControllerBase
    {
        private readonly IMediator _mediator;

        [HttpPost]
        public async Task<IActionResult> AddSurvey([FromBody] CreateSurveyCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetSurveys()
        {
            var query = new GetSurveysQuery();

            var response = await _mediator.Send(query);
            return Ok(response);
        } 
    }
}
