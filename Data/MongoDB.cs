using System;
using Api.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Api.Data
{
    public class MongoDB{
        public IMongoDatabase DB { get; }

        public MongoDB(IConfiguration configuration)
        {
            try
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration.GetConnectionString("Mongo")));
                var client = new MongoClient(settings);
                DB = client.GetDatabase(configuration["DBname:Mongo"]);
                MapClasses();
            }
            catch(Exception ex)
            {
                throw new MongoException("Não foi possivel conectar ao Mongo", ex);
            }

        }


        private void MapClasses(){
            var conventionPack = new ConventionPack {new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            if(!BsonClassMap.IsClassMapRegistered(typeof(Infected)))
            {
                BsonClassMap.RegisterClassMap<Infected>( i => 
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}