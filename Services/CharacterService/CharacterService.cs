using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _netCourse.Data;
using _netCourse.Dtos.Character;
using _netCourse.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using _netCourse.Dtos.Skills;

namespace _netCourse.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character characterId = _mapper.Map<Character>(character);
            characterId.User = await _context.users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.characters.Add(characterId);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.characters
                .Where(u => u.User.Id == GetUserId())
                .Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _context.characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
                if (character != null)
                {
                    _context.characters.Remove(character);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _context.characters
                        .Where(c => c.User.Id == GetUserId())
                        .Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found";
                }
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
            var dbCharacters = await _context.characters
                .Include(c => c.weapon)
                .Include(c => c.Skill)
                .Where(u => u.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterDto(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacters = await _context.characters
                .Include(c => c.weapon)
                .Include(c => c.Skill)
                .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacters);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacterDto)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.characters
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == updateCharacterDto.Id);
                if (character.User.Id == GetUserId())
                {
                    character.Name = updateCharacterDto.Name;
                    character.Strength = updateCharacterDto.Strength;
                    character.Defense = updateCharacterDto.Defense;
                    character.HitPoints = updateCharacterDto.HitPoints;
                    character.Intelligence = updateCharacterDto.Intelligence;
                    character.Class = updateCharacterDto.Class;

                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillsDto newCharacterSkills)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.characters
                   .Include(c => c.weapon)
                   .Include(c => c.Skill)
                   .FirstOrDefaultAsync(c => c.Id == newCharacterSkills.CharacterId && c.User.Id == GetUserId());

                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }
                var skill = await _context.skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkills.SkillsId);
                if (skill == null)
                {
                    response.Success = false;
                    response.Message = "Skill not found";
                    return response;
                }

                character.Skill.Add(skill);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}