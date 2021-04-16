using AspNetFundamentalDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetFundamentalDemo.DAOs
{
    public class ChampionDao : IChampionDao
    {
        private List<Champion> champions = new()
        {
            new Champion
            {
                Id = Guid.NewGuid(),
                Name = "Jhin",
                Ultimate = "The Curtain Call"
            },
            new Champion
            {
                Id = Guid.NewGuid(),
                Name = "Kai'sar",
                Ultimate = "Killer's Instinct"
            },
            new Champion
            {
                Id = Guid.NewGuid(),
                Name = "Aphelios",
                Ultimate = "Moonlight Vigil"
            }
        };


        public IEnumerable<Champion> GetChampions()
        {
            return champions;
        }

        public Champion GetChampion(Guid Id)
        {
            return champions.Where(champ => champ.Id == Id).SingleOrDefault();
        }

        public void CreateChampion(Champion champion)
        {
            champions.Add(champion);
        }

        public void UpdateChampion(Champion champion)
        {
            var i = champions.FindIndex(champ => champ.Id == champion.Id);
            champions[i] = champion;
        }

        public void DeleteChampion(Guid id)
        {
            var i = champions.FindIndex(champ => champ.Id == id);
            champions.RemoveAt(i);
        }

        public IEnumerable<Champion> BasicChampions()
        {
            throw new NotImplementedException();
        }
    }
}
