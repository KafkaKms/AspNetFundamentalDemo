using AspNetFundamentalDemo.DTOs;
using AspNetFundamentalDemo.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetFundamentalDemo.DAOs
{
    public class ChampionModel : IChampionDao
    {
        string dbName = "DemoDB";
        string collectionName = "Champions";

        IMongoCollection<Champion> championsCollection;
        FilterDefinitionBuilder<Champion> filterDefinitionBuilder = Builders<Champion>.Filter;

        public ChampionModel(IMongoClient mongoClient)
        {
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(dbName);
            championsCollection = mongoDatabase.GetCollection<Champion>(collectionName);
        }

        /// <summary>
        /// Delete collections </br>
        /// Create a few champions and return them
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Champion> BasicChampions()
        {
            var champions = CHAMPIONS.GetBasicChampions;
            championsCollection.DeleteMany(_ => true);
            championsCollection.InsertMany(champions);
            return champions;
        }

        public void CreateChampion(Champion champion)
        {
            championsCollection.InsertOne(champion);
        }

        public void DeleteChampion(Guid id)
        {
            var filter = filterDefinitionBuilder.Eq(existed => existed.Id, id);
            championsCollection.DeleteOne(filter);
        }

        public Champion GetChampion(Guid Id)
        {
            var champion = championsCollection.Find(champ => champ.Id == Id).FirstOrDefault();
            return champion;
        }

        public IEnumerable<Champion> GetChampions()
        {
            var champions = championsCollection.Find(champ => true).ToList();
            return champions;
        }

        public void UpdateChampion(Champion champion)
        {
            var filter = filterDefinitionBuilder.Eq(existed => existed.Id, champion.Id);
            championsCollection.ReplaceOne(filter, champion);
        }
    }
}
