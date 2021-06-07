using ElectricityDigitalSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricityDigitalSystem.ClientServices
{
    public class CustomerService :ClientServiceAPI
    {
        public string RegisterCustomer(CustomerModel customer)
        {
            if(customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            //This ill Handle registration of a customer
            else
            {
                
                fileService.Database.Customers.Add(customer);
                fileService.SaveChanges();
                return customer.EmailAddress;
            }
        }

        public CustomerModel GetCustomerById(string customerId)
        {
            CustomerModel foundcustomer = fileService.Database.Customers.Find(c => c.Id == customerId);
            if (foundcustomer != null)
            {
                return foundcustomer;
            }
            return null;
        }

        //Find Customer via Email
        public CustomerModel GetCustomerByEmail(string email)
        {
            CustomerModel foundcustomer = fileService.Database.Customers.Find(c => c.EmailAddress == email);
            if (foundcustomer != null)
            {
                return foundcustomer;
            }
            return null;
        }

        public string UpdateCustomer(CustomerModel modifiedCustomer)
        {
            CustomerModel customer = this.GetCustomerById(modifiedCustomer.Id);
            if(customer != null)
            {
                int indexOfCustomer = fileService.Database.Customers.IndexOf(customer);
                //fileService.database.Customers.Insert(indexOfCustomer, modifiedCustomer);
                fileService.SaveChanges();
                return "SUCCESSFULLY UPDATED";
            }
            return "Failed, Customer not found";
        }
    }
}
