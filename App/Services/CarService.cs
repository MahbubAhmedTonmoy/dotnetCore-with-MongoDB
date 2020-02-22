using System.Collections.Generic;
using App.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace App.Services
{
    public class CarService
    {
        private readonly IMongoCollection<Car> cars;
        public CarService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("CarGalleryDB"));
            IMongoDatabase database = client.GetDatabase("CarGalleryDB");
            cars = database.GetCollection<Car>("Cars");
            
        }
        public List<Car> GetAll()
        {
            return cars.Find(c => true).ToList();
        }

        public Car Get(string id)
        {
            return cars.Find(c => c.Id == id).FirstOrDefault();
        }

        public Car Create(Car car)
        {
            cars.InsertOne(car);
            return car;
        }

        public void Update(string id, Car car)
        {
            cars.ReplaceOne(s => s.Id == id, car);
        }
        public void Remove(string id)
        {
            cars.DeleteOne(c => c.Id == id);
        }

        public void Remove(Car car)
        {
            cars.DeleteOne(c => c.Id == car.Id);
        }
    }
}