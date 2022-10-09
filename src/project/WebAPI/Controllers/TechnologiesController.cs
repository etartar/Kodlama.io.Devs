using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetByIdTechnology;
using Application.Features.Technologies.Queries.GetListTechnology;
using Application.Features.Technologies.Queries.GetListTechnologyByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologiesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            TechnologyListModel technologyListModel = await Mediator.Send(new GetListTechnologyQuery
            {
                PageRequest = pageRequest,
            });

            return Ok(technologyListModel);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            TechnologyListModel result = await Mediator.Send(new GetListTechnologyByDynamicQuery
            {
                PageRequest = pageRequest,
                Dynamic = dynamic
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            TechnologyGetByIdDto result = await Mediator.Send(new GetByIdTechnologyQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTechnologyCommand createTechnologyCommand)
        {
            CreatedTechnologyDto result = await Mediator.Send(createTechnologyCommand);
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTechnologyCommand updateTechnologyCommand)
        {
            UpdatedTechnologyDto result = await Mediator.Send(updateTechnologyCommand);
            return Created("", result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeletedTechnologyDto result = await Mediator.Send(new DeleteTechnologyByIdCommand(id));
            return Created("", result);
        }
    }
}
