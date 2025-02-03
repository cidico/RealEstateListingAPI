using Microsoft.AspNetCore.Mvc;
using RealEstateListingApi.Application.Interfaces;
using RealEstateListingApi.Core.Commands;
using RealEstateListingApi.Core.Entities;
using RealEstateListingApi.Core.Interfaces;

namespace RealEstateListingApi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListingsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICreateListingCommandHandler _createHandler;
        private readonly IDeleteListingCommandHandler _deleteHandler;

        public ListingsController(IUnitOfWork unitOfWork,
            ICreateListingCommandHandler createHandler,
            IDeleteListingCommandHandler deleteHandler)
        {
            _unitOfWork = unitOfWork;
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Listing>), 200)]
        [Tags("Listings Retrieval")]
        public async Task<ActionResult<IEnumerable<Listing>>> GetAllListingsAsync()
        {
            try
            {
                var listings = await _unitOfWork.Listings.GetAllAsync();
                return Ok(listings);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [Tags("Listings Management")]
        public async Task<ActionResult<string>> AddListingAsync([FromBody] CreateListingCommand command)
        {
            try
            {
                var listing = await _createHandler.Handle(command);
                return CreatedAtAction(nameof(GetListingByIdAsync), new { id = listing.Id }, listing);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetListingByIdAsync))]
        [ProducesResponseType(typeof(Listing), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Tags("Listings Retrieval")]
        public async Task<ActionResult<Listing>> GetListingByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            try
            {
                var listing = await _unitOfWork.Listings.GetByIdAsync(id);

                if (listing is null)
                    return NotFound();

                return Ok(listing);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Tags("Listings Management")]
        public async Task<IActionResult> DeleteListingAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            try
            {
                await _deleteHandler.Handle(new DeleteListingCommand { Id = id });
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}