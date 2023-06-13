using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NZWalks.Api.CustomActionFilter;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domains;
using NZWalks.Api.Models.DTO;
using NZWalks.Api.Repositories;

namespace NZWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, 
            IMapper mapper, ILogger<RegionsController> logger)
        {

            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        [Authorize(Roles="Reader")]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from the database

            var regionsDomain = await regionRepository.GetAllAsync();

            //Mapping domain model to DTO

            // Create an exception
            


            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);


            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            //Converting Domain model to DTO

            


            return Ok(mapper.Map<RegionDto>(regionDomain));



        }
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] RegionRequestDto regionRequestDto)
        {
            
                var regionRequest = mapper.Map<Region>(regionRequestDto);


                //Use Domain Model to create DTO

                regionRequest = await regionRepository.CreateAsync(regionRequest);

                //Map Domain Model back to DTO

                var regionDto = mapper.Map<RegionDto>(regionRequest);


                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            
            
            
        }
        [HttpPut]
        [Route("{id:Guid}")]
        
        [ValidateModel]
        [Authorize(Roles = "Writer")]


        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDtoRequest updateDtoRequest)
        {
            

                var regionDomainModel = mapper.Map<Region>(updateDtoRequest);

                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                await dbContext.SaveChangesAsync();

                //Convert Domain Model to DTO

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return Ok(regionDto);
            
            

            
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if(regionDomainModel==null)
            {
                return NotFound();

            }



            //It is optional, without adding the below part we can get the same result.

            //Mapping domain model to Dto

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }


    }
    
}

