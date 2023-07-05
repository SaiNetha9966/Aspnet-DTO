using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksApi.Data;
using NZWalksApi.DTO;
using NZWalksApi.Models.Domains;

namespace NZWalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        // private readonly is used inside action methods
        private readonly NZWalksDbContext dbContext;

        // Controller for using DbContext to access the database
        public RegionController(NZWalksDbContext DbContext)
        {
            this.dbContext = DbContext;
        }
        // https://localhost:7135/api/Region this is the end point of the below method 
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            // by using dbContext we are talk to db and get the data and assign to a variable
            // getting the data from database - domain model
            var regionsDomain = dbContext.regions.ToList();
            // map Domain model to DTO

            var regionDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                });
            }

            //return DTO
            return Ok(regionDto);
        }

        // get a specific data by using the id 
        // Get : http:// localhost/api/regions/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute]Guid id)
        {
            // getting data through only id we can use var region  = dbContext.regions.Find(id)
            var region = dbContext.regions.Find(id);
            // if we find a data by name are code or not unified dat we need to use below statement
            // var region = dbContext.regions.FirstOrDefault(x => x.Id == id); 

            if(region == null)
            {
                return NotFound();
            }
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };
            return Ok(regionDto);
        }
    }
}
