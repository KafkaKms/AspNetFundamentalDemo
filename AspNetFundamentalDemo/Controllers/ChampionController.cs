using AspNetFundamentalDemo.DAOs;
using AspNetFundamentalDemo.DTOs;
using AspNetFundamentalDemo.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetFundamentalDemo.Controllers
{
    [ApiController]
    [Route("champions")]
    public class ChampionController : ControllerBase
    {
        private IChampionDao championDao;
        private ILogger<ChampionController> Logger;

        public ChampionController(IChampionDao championDao, ILogger<ChampionController> logger)
        {
            this.championDao = championDao;
            Logger = logger;
        }

        [HttpGet]
        public IEnumerable<ChampionDto> GetChampions([FromQuery] string position)
        {
            var items = championDao.GetChampions().Select(champ => champ.AsDto());

            Logger.LogInformation(new EventId(555, "ChampionController"), $"position query: {position}");

            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ChampionDto> GetChampion(Guid id)
        {
            var champ = championDao.GetChampion(id);
            if(champ is null)
            {
                return NotFound();
            }

            return champ.AsDto();
        }

        [HttpPost]
        public ActionResult<ChampionDto> CreateChampion(CreateChampionDto createChampionDto)
        {
            var champ = new Champion
            {
                Id = Guid.NewGuid(),
                Name = createChampionDto.Name,
                Ultimate = createChampionDto.Ultimate
            };

            this.championDao.CreateChampion(champ);

            return CreatedAtAction(nameof(GetChampion), new
            {
                Id = champ.Id
            }, champ.AsDto());
        }

        [HttpPut]
        public ActionResult UpdateChampion(UpdateChampionDto updateChampionDto)
        {
            Champion champion = new()
            {
                Id = updateChampionDto.Id,
                Name = updateChampionDto.Name,
                Ultimate = updateChampionDto.Ultimate
            };

            championDao.UpdateChampion(champion);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult DeleteChampion(Guid id)
        {
            championDao.DeleteChampion(id);
            return NoContent();
        }

        [HttpGet("init")]
        public ActionResult<IEnumerable<ChampionDto>> BasicChamp()
        {
            return Ok(championDao.BasicChampions().Select(champ => champ.AsDto()));
        }
    }
}
