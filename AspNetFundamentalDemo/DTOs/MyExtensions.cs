using AspNetFundamentalDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetFundamentalDemo.DTOs
{
    public static class MyExtensions
    {
        public static ChampionDto AsDto(this Champion champion)
        {
            return new ChampionDto
            {
                Id = champion.Id,
                Name = champion.Name,
                Ultimate = champion.Ultimate
            };
        }
    }
}
