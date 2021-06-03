using System;
using Api.Data.Collections;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectedController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infected> _infectedsCollection;

        public InfectedController(Data.MongoDB mongo)
        {
            this._mongoDB = mongo;
            this._infectedsCollection = this._mongoDB.DB.GetCollection<Infected>(typeof(Infected).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SaveInfected([FromBody] InfectedViewModel view)
        {
            var infected = new Infected(view.BirthDate, view.Gender, view.Latitude, view.Longitude);

            this._infectedsCollection.InsertOne(infected);
            
            return StatusCode(201, "Infectado Adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult GetInfected()
        {
            var infecteds = this._infectedsCollection.Find(Builders<Infected>.Filter.Empty).ToList();
            
            return Ok(infecteds);
        }

        [HttpPut]
        public ActionResult updateInfected([FromBody] InfectedViewModel view)
        {
            _infectedsCollection.UpdateOne(Builders<Infected>.Filter.Where(_ => _.BirthDate == view.BirthDate), Builders<Infected>.Update.Set("Gender", view.Gender));

            return Ok("Update sucess.");
        }

        [HttpDelete("{birthDate}")]
        public ActionResult DeleteInfected(DateTime birthDate)
        {
            _infectedsCollection.DeleteOne(Builders<Infected>.Filter.Where(_ => _.BirthDate == birthDate));
            return Ok("Deleted");
        }
    }
}