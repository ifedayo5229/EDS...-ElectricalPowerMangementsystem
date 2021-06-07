using ElectricityDigitalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectricityDigitalSystem.ClientServices
{
    public class TariffService : ClientServiceAPI
    {
        public void AddTariffToService()
        {
            List<TarriffModel> mockData = new List<TarriffModel>();

            TarriffModel tariff1 = new TarriffModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Better Home Plan",
                PricePerUnit = 150.40M,

            };
            TarriffModel tariff2 = new TarriffModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Best Home Plan",
                PricePerUnit = 250.84M,
            };
            TarriffModel tariff3 = new TarriffModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Basic Home Plan",
                PricePerUnit = 120.90M,
            };
            TarriffModel tariff4 = new TarriffModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Premium Home Plan",
                PricePerUnit = 300.60M,
            };

            PushToDb(tariff1);
            PushToDb(tariff2);
            PushToDb(tariff3);
            PushToDb(tariff4);
        }

        private void PushToDb(TarriffModel model)
        {
            if(fileService.Database.Tariffs.Count <= 3)
            {
                fileService.Database.Tariffs.Add(model);
                fileService.SaveChanges();
                
            }
   
        }
        public List<TarriffModel> GetAllTariff()
        {
            var Tariffs = fileService.Database.Tariffs.ToList();
            return Tariffs;
        }

        public TarriffModel GetTarriffByName(string name)
        {
            var tariff = fileService.Database.Tariffs.Find(t => t.Name == name);
            return tariff;
        }

        public TarriffModel GetTarriffById(string id)
        {
            var tariff = fileService.Database.Tariffs.Find(t => t.Id == id);
            return tariff;
        }
    }
}
