using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricityDigitalSystem.Models
{

    public class CustomerSubcription
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string TariffId { get; set; }
        public string CustomerId { get; set; }
        public string AgentId { get; set; }
        public DateTime SubcriptionDateTime { get; set; }
        public string SubscriptionStatus { get; set; }


       
    }
}
