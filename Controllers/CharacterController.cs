using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _netCourse.Dtos.Character;
using _netCourse.Models;
using _netCourse.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace _netCourse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;

        }

        [HttpGet("GetAll")]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            var getAllCharacters = await _characterService.GetAll();
            return Ok(getAllCharacters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            var getCharacterById = await _characterService.GetCharacterDto(id);
            return Ok(getCharacterById);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> addCharacter(AddCharacterDto character)
        {
            var addNewCharacter = await _characterService.AddCharacter(character);
            return Ok(addNewCharacter);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updateCharacterDto)
        {
            var response = await _characterService.UpdateCharacter(updateCharacterDto);
            if (response.Data != null)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> DeleteSingle(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if (response.Data != null)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}