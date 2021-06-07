using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricityDigitalSystem.Models
{
    public class MeterModel
    {
        public string Id { get; set; }
        public string MeterNumber { get; set; }
        public MeterType Type { get; set; }
        public string ProductName { get; set; }

    }

    
}
