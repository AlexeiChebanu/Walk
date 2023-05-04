using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.CustomActionFilters;
using WebApi.Data;
using WebApi.Models.Domain;
using WebApi.Models.DTO;
using WebApi.Repository;
using WebApi.Services.RegionServices;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly IRegionServices regionServices;
    private readonly IMapper mapper;
    private readonly ILogger<RegionsController> logger;

    public RegionsController(IRegionServices regionServices, IMapper mapper, ILogger<RegionsController> logger)
    {
        this.regionServices = regionServices;
        this.mapper = mapper;
        this.logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = "Reader")]
    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation("GetAllAsync Regions Action was invoked");
        
        var regionsDomain = await regionServices.GetAllAsync();

        logger.LogInformation($"Finished GetAllAsync request with data: {JsonSerializer.Serialize(regionsDomain)}");

        return Ok(mapper.Map<List<RegionDTO>>(regionsDomain));
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Reader")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var region = await regionServices.GetByIdAsync(id);

        if (region == null) return NotFound();
        
        return Ok(mapper.Map<RegionDTO>(region));            
    }

    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "Writer,Reader")]
    public async Task<IActionResult> CreateAsync([FromBody] AddRegionRequestDTO addRegionRequestDTO)
    {    
        var regionDomain = await regionServices.CreateAsync(addRegionRequestDTO);

        var regionDto = mapper.Map<RegionDTO>(regionDomain);

        return CreatedAtAction(nameof(GetById), new { id = regionDomain.Id }, regionDomain);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    [Authorize(Roles = "Writer,Reader")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdRegionRequest? updRegionRequest)
    {
        var regionDomainModel = await regionServices.UpdateAsync(updRegionRequest);

        if (regionDomainModel == null) return NotFound();

        return Ok(mapper.Map<RegionDTO>(regionDomainModel));
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer,Reader")]
    public async Task<IActionResult> DeleteAsync([FromRoute]Guid? id)
    {
        var regionDomainModel =  await regionServices.DeleteAsync(id);      

        return Ok();
    }

}
