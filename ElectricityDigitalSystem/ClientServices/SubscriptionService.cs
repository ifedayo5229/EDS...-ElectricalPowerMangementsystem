using ElectricityDigitalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectricityDigitalSystem.ClientServices
{
    public class SubscriptionService : ClientServiceAPI
    {
        public void MakeSubscription(CustomerSubcription subcription)
        {
            fileService.Database.Subcriptions.Add(subcription);
            fileService.SaveChanges();
        }
        public List<CustomerSubcription> GetAllSubscription()
        {
            var customerSubscription = fileService.Database.Subcriptions.ToList();
            return customerSubscription;
        }

        public List<CustomerSubcription> GetCustomerSubscription(string customerId)
        {
            var customerSubscription = fileService.Database.Subcriptions.Where(c => c.CustomerId == customerId).ToList();
            return customerSubscription;
        }

        public CustomerSubcription FindSubscription(string customerId)
        {
            var subscription = fileService.Database.Subcriptions.Find(s => s.CustomerId == customerId);
            return subscription;
        }

        public void CancelSubcription(string id)
        {
            var subToCancel = FindSubscription(id);
            subToCancel.SubscriptionStatus = "InActive";   
        }

        public List<CustomerSubcription> CheckActiveSubscription(string customerId)
        {
            var activeSubscription = fileService.Database.Subcriptions.Where(x => x.CustomerId == customerId).ToList();
            var Sub = activeSubscription.Where(x => x.SubscriptionStatus == "Active").ToList();
            return Sub;
        }

        public string UpdateSubscription(CustomerSubcription modifiedSubscription)
        {
            
            if (modifiedSubscription != null)
            {
                int indexOfCustomer = fileService.Database.Subcriptions.IndexOf(modifiedSubscription);
                //fileService.database.Customers.Insert(indexOfCustomer, modifiedCustomer);
                fileService.SaveChanges();
                return "SUCCESSFULLY UPDATED";
            }
            return "Failed, Customer not found";
        }
    }
}
