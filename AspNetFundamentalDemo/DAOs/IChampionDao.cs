using AspNetFundamentalDemo.Entities;
using System;
using System.Collections.Generic;

namespace AspNetFundamentalDemo.DAOs
{
    public interface IChampionDao
    {
        Champion GetChampion(Guid Id);
        IEnumerable<Champion> GetChampions();

        void CreateChampion(Champion champion);

        void UpdateChampion(Champion champion);

        void DeleteChampion(Guid id);

        IEnumerable<Champion> BasicChampions();
    }
}