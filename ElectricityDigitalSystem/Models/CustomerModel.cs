using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricityDigitalSystem.Models
{
    public class CustomerModel : BaseUserModel
    {
        public CustomerModel()
        {
            this.Id = "CUS-" + Guid.NewGuid().ToString();
        }
        public string MeterNumber { get; set; }
        public string Password { get; set; }
    }
}
