using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _netCourse.Data;
using _netCourse.Dtos.Fights;
using _netCourse.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _netCourse.Services
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public FightService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto result)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.characters
                   .Include(c => c.weapon)
                   .FirstOrDefaultAsync(c => c.Id == result.AttackerId);

                var opponent = await _context.characters
                    .FirstOrDefaultAsync(c => c.Id == result.OpponentId);

                int damage = DoWeaponAttack(attacker, opponent);
                if (opponent.HitPoints <= 0)
                {
                    response.Message = $"{opponent.Name} has been defeated!";
                }

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHp = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHp = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private static int DoWeaponAttack(Character attacker, Character opponent)
        {
            int damage = attacker.weapon.Damage + (new Random().Next(attacker.Strength));
            damage -= new Random().Next(opponent.Defense);

            if (damage > 0)
            {
                opponent.HitPoints -= damage;
            }

            return damage;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto result)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.characters
                   .Include(c => c.Skill)
                   .FirstOrDefaultAsync(c => c.Id == result.AttackerId);

                var opponent = await _context.characters
                    .FirstOrDefaultAsync(c => c.Id == result.OpponentId);

                var skill = attacker.Skill.FirstOrDefault(s => s.Id == result.SkillId);
                if (skill == null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} does not know this skill";
                    return response;
                }

                int damage = DoSkillAttack(attacker, opponent, skill);
                if (opponent.HitPoints <= 0)
                {
                    response.Message = $"{opponent.Name} has been defeated!";
                }

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHp = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHp = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private static int DoSkillAttack(Character attacker, Character opponent, Skills skill)
        {
            int damage = skill.damage + (new Random().Next(attacker.Intelligence));
            damage -= new Random().Next(opponent.Defense);

            if (damage > 0)
            {
                opponent.HitPoints -= damage;
            }

            return damage;
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
        {
            var response = new ServiceResponse<FightResultDto>
            {
                Data = new FightResultDto()
            };
            try
            {
                var Characters = await _context.characters
                .Include(c => c.weapon)
                .Include(c => c.Skill)
                .Where(c => request.CharactersId.Contains(c.Id)).ToListAsync();

                bool defeated = false;

                while (!defeated)
                {
                    foreach (var attacker in Characters)
                    {
                        var opponents = Characters.Where(c => c.Id != attacker.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count)];

                        int damage = 0;
                        string AttackUsed = string.Empty;

                        bool useWeapon = new Random().Next(2) == 0;
                        if (useWeapon)
                        {
                            AttackUsed = attacker.weapon.Name;
                            damage = DoWeaponAttack(attacker, opponent);
                        }
                        else
                        {
                            var skill = attacker.Skill[new Random().Next(attacker.Skill.Count)];
                            AttackUsed = skill.name;
                            damage = DoSkillAttack(attacker, opponent, skill);
                        }
                        response.Data.Log.Add($"{attacker.Name} attacks {opponent.Name} using {AttackUsed} with {(damage >= 0 ? damage : 0)} damage.");

                        if (opponent.HitPoints <= 0)
                        {
                            defeated = true;
                            attacker.Victories++;
                            opponent.defeats++;
                            response.Data.Log.Add($"{opponent.Name} has been defeated!");
                            response.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} HP left!");
                            break;
                        }
                    }
                }

                Characters.ForEach(c =>
                {
                    c.Fights++;
                    c.HitPoints = 100;
                });

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<HighScoreDto>>> HighScore()
        {
            var characters = await _context.characters.Where(c => c.Fights > 0).OrderByDescending(c => c.Victories)
            .ThenBy(c => c.defeats).ToListAsync();

            var response = new ServiceResponse<List<HighScoreDto>>
            {
                Data = characters.Select(c => _mapper.Map<HighScoreDto>(c)).ToList()
            };

            return response;
        }
    }
}