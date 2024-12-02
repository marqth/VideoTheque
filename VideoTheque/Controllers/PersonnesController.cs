using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Personnes;
using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonnesController : ControllerBase
    {
        private readonly IPersonnesBusiness _personnesBusiness;

        public PersonnesController(IPersonnesBusiness personnesBusiness)
        {
            _personnesBusiness = personnesBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonneViewModel>>> GetPersonnes()
        {
            var personnes = await _personnesBusiness.GetPersonnes();
            return Ok(personnes.Select(PersonneViewModel.ToModel).ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<PersonneViewModel> GetPersonne(int id)
        {
            var personne = _personnesBusiness.GetPersonne(id);
            return Ok(PersonneViewModel.ToModel(personne));
        }

        [HttpPost]
        public async Task<ActionResult> InsertPersonne([FromBody] PersonneViewModel personneViewModel)
        {
            var personneDto = personneViewModel.ToDto();
            await _personnesBusiness.InsertPersonne(personneDto);
            return CreatedAtAction(nameof(GetPersonne), new { id = personneDto.Id }, personneDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePersonne(int id, [FromBody] PersonneViewModel personneViewModel)
        {
            var personneDto = personneViewModel.ToDto();
            await _personnesBusiness.UpdatePersonne(id, personneDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePersonne(int id)
        {
            await _personnesBusiness.DeletePersonne(id);
            return NoContent();
        }
    }
}