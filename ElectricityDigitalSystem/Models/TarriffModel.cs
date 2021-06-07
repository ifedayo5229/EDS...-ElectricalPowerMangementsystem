using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricityDigitalSystem.Models
{
    public class TarriffModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal PricePerUnit { get; set; }

        //public TarriffModel()
        //{
        //    Id = "TRF-" + Guid.NewGuid().ToString();
        //}

    }
}
