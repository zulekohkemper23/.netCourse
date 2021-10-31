using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _netCourse.Data;
using _netCourse.Dtos.Character;
using _netCourse.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _netCourse.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly DataContext context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character characterId = _mapper.Map<Character>(character);
            _context.characters.Add(characterId);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _context.characters.FirstAsync(c => c.Id == id);
                _context.characters.Remove(character);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterDto(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacters = await _context.characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacters);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacterDto)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.characters.FirstOrDefaultAsync(c => c.Id == updateCharacterDto.Id);

                character.Name = updateCharacterDto.Name;
                character.Strength = updateCharacterDto.Strength;
                character.Defense = updateCharacterDto.Defense;
                character.HitPoints = updateCharacterDto.HitPoints;
                character.Intelligence = updateCharacterDto.Intelligence;
                character.Class = updateCharacterDto.Class;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}