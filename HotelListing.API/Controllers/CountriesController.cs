using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using AutoMapper;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Exceptions;
using HotelListing.API.Models;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Core.Repository;

namespace HotelListing.API.Controllers
{
    [Route("api/v{version:apiVersion}/countries")]
    //[Route("api/countries")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class CountriesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICountriesRepository countriesRepository;
        private readonly ILogger<CountriesController> logger;

        public CountriesController(IMapper mapper, ICountriesRepository countriesRepository, ILogger<CountriesController> logger)
        {
            this.countriesRepository = countriesRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Countries/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            //var countries = await countriesRepository.GetAllAsync();
            //var records = mapper.Map<List<GetCountryDto>>(countries);
            //return Ok(records);

            var countries = await countriesRepository.GetAllAsync<GetCountryDto>();
            return Ok(countries);
        }

        // GET: api/Countries/?StartIndex=0&pagesize=25&PageNumber=1
        // GET:  https://localhost:7120/api/v1/Countries?StartIndex=0&pagesize=2&PageNumber=1
        [HttpGet]
        public async Task<ActionResult<PagedResult<GetCountryDto>>> GetPagedCountries([FromQuery] QueryParameters queryParameters)
        {
            var pagedCountriesResult = await countriesRepository.GetAllAsync<GetCountryDto>(queryParameters);
            return Ok(pagedCountriesResult);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            //var country = await countriesRepository.GetDetails(id);

            //if (country == null)
            //{
            //    throw new NotFoundException(nameof(GetCountry), id);

            //    logger.LogWarning($"Record not found {nameof(GetCountry)} with id = {id}.");
            //    return NotFound();
            //}

            //var countryDto = mapper.Map<CountryDto>(country);

            //return Ok(countryDto);

            var country = await countriesRepository.GetDetails(id);
            return Ok(country);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            var country = await countriesRepository.GetAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            mapper.Map(updateCountryDto, country);

            try
            {
                await countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize]
        [AllowAnonymous]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountryDto)
        {
            //if (context.Countries == null)
            //{
            //    return Problem("Entity set 'HotelDbContext.Countries' is null.");
            //}

            //var country = mapper.Map<Country>(createCountryDto);

            //await countriesRepository.AddAsync(country);

            //return CreatedAtAction("GetCountry", new { id = country.Id }, country);

            //return new JsonResult(country);

            var country = await countriesRepository.AddAsync<CreateCountryDto, GetCountryDto>(createCountryDto);
            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            //var country = await countriesRepository.GetAsync(id);

            //if (country == null)
            //{
            //    return NotFound();
            //}

            //await countriesRepository.DeleteAsync(id);

            //return NoContent();

            await countriesRepository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await countriesRepository.Exists(id);
        }
    }
}