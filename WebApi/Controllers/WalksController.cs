using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.WebSockets;
using WebApi.CustomActionFilters;
using WebApi.Models.Domain;
using WebApi.Models.DTO;
using WebApi.Repository;
using WebApi.Services.WalkServices;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalksController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IWalksService walksService;

    public WalksController(IMapper mapper, IWalksService walksService)
    {
        this.mapper = mapper;
        this.walksService = walksService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] AddWalkRequestDTO addWalkRequestDTO)
    {
        Walk walkDomainModel = await walksService.CreateAsync(addWalkRequestDTO);

        return Ok(walkDomainModel);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
                                                [FromQuery] string? sortBy, [FromQuery] bool IsAscending,
                                                [FromQuery] int pageNumber = 1, [FromQuery] int pageSize= 1000)
    {
            var walksDomainModel = await walksService.GetAllAsync(filterOn, filterQuery, sortBy, IsAscending, pageNumber, pageSize);

            return Ok(mapper.Map<List<WalkDTO>>(walksDomainModel));     
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var walkDomainModel = await walksService.GetByIdAsync(id);

        if (walkDomainModel == null)
            return NotFound();

        return Ok(mapper.Map<WalkDTO>(walkDomainModel));
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    public async Task<IActionResult> UpdateAsync(UpdateWalkRequestDTO updateWalkRequest)
    {    
       var walkDomainModel = await walksService.UpdateAsync(updateWalkRequest);

        if (walkDomainModel == null)
            return NotFound();

        return Ok(mapper.Map<WalkDTO>(walkDomainModel));
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var deleteWalkDomainModel = await walksService.DeleteAsync(id);

        if (deleteWalkDomainModel == null)
            return NotFound();

        return Ok(mapper.Map<WalkDTO>(deleteWalkDomainModel));
    }
}
