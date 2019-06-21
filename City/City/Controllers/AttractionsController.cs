using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using City.DTOs;
using City.Models;
using City.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace City.Controllers
{
    [Route("api/cities/{citiId}/[controller]")]
    [ApiController]
    public class AttractionsController : ControllerBase
    {
        public ICityRepository Repository { get; }
        public IMapper Mapper { get; }

        public AttractionsController(ICityRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<AttractionDto>>> GetAttractionsAsync(int citiId)
        {
            if (!await Repository.CitiExistsAsync(citiId))
                return BadRequest();
            var attractions = await Repository.GetAttractionsAsync(citiId);
            if (attractions == null)
                return NotFound();
            var attractionsDto = Mapper.Map<IEnumerable<AttractionDto>>(attractions)
                .ToList();
            return attractionsDto;
        }

        [HttpGet("{id}", Name = "GetAttraction")]
        public async Task<ActionResult<AttractionDto>> GetAttractionAsync(int citiId, int id)
        {
            if (!await Repository.CitiExistsAsync(citiId))
                return BadRequest();

            var attraction = await Repository.GetAttractionAsync(citiId, id);

            if (attraction == null)
                return NotFound();

            var attractionDto = Mapper.Map<AttractionDto>(attraction);

            return attractionDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(int citiId, [FromBody] AttractionCreateDto attractionCreateDto)
        {
            if (citiId == 0 || attractionCreateDto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var attraction = Mapper.Map<Attraction>(attractionCreateDto);

            await Repository.CreateAttractionAsync(citiId, attraction);

            var attractionDto = Mapper.Map<AttractionDto>(attraction);

            return CreatedAtRoute("GetAttraction", new { citiId, id = attraction.ID }, attractionDto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int citiId, int id, [FromBody] AttractionUpdateDto attractionUpdateDto)
        {
            if (citiId == 0 || id == 0 || attractionUpdateDto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var attraction = await Repository.GetAttractionAsync(citiId, id);

            if (attraction == null)
                return NotFound();

            Mapper.Map(attractionUpdateDto, attraction);

            await Repository.UpdateAttractionAsync(attraction);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchAsync(int citiId, int id, [FromBody]JsonPatchDocument<AttractionUpdateDto> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();

            if (!await Repository.CitiExistsAsync(citiId))
                return NotFound();

            var attraction = await Repository.GetAttractionAsync(citiId, id);

            if (attraction == null)
                return NotFound();

            var attractionUpdateDto = Mapper.Map<AttractionUpdateDto>(attraction);

            patchDocument.ApplyTo(attractionUpdateDto);

            Mapper.Map(attraction, attractionUpdateDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int citiId, int id)
        {
            if (!await Repository.CitiExistsAsync(citiId))
                return NotFound();

            var attraction = await Repository.GetAttractionAsync(citiId, id);

            if (attraction == null) return NotFound();

            await Repository.DeleteAttractionAsync(attraction);

            return NoContent();
        }
    }
}
