using AspNetFundamentalDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetFundamentalDemo.DAOs
{
    public class CHAMPIONS
    {
        private static List<Champion> champions = new()
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
            },
            new Champion
            {
                Id = Guid.NewGuid(),
                Name = "Garen",
                Ultimate = "DEMACIAAAAAAAA"
            },
            new Champion
            {
                Id = Guid.NewGuid(),
                Name = "Darius",
                Ultimate = "Noxus Guillotine"
            }
        };

        public static List<Champion> GetBasicChampions { get => CHAMPIONS.champions;  }
    }
}
