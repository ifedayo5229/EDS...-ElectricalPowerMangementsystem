using EDSCustomerPortal.AppData;
using EDSCustomerPortal.Interfaces;
using ElectricityDigitalSystem.ClientServices;
using System;
using System.Collections.Generic;
using System.Text;
using ElectricityDigitalSystem.Models;
using EDSCustomerPortal.Menu;
using ElectricityDigitalSystem.Data;

namespace EDSCustomerPortal.Services
{
    public class CustomerAccess : ICustomerAccess
    {
        ElectricityDigitalSystem.ClientServices.SubscriptionService subscriptionService = new SubscriptionService();
        CustomerService service = new CustomerService();
        TariffService tariffService = new TariffService();
        decimal t1, t2, t3, t4 = default;
        string a1, a2, a3, a4 = default;
        public void CancelCustomerSubscription()
        {
            Console.Clear();
            Console.WriteLine("\t\tWould you like to cancel your active subscription?\n1 : Yes\n\t\t2 : No");
            Console.Write($"\t\t  : ");
            var entry = Console.ReadLine();
            switch(entry)
            {
                case "1":
                    CancelSub();
                    break;
                case "2":              
                    HomeMenu.CurrentStage = 1;
                    break;
                default:
                    CancelCustomerSubscription();
                    break;
            }
            
        }
        public void CancelSub()
        {
            var customerId = CustomerApplicationData.CurrentCustomerId;
            var active = subscriptionService.CheckActiveSubscription(customerId);
            if(active.Count == 0)
            {
                Console.WriteLine("\n\n\t\tYou do not have an Active subscription");
                HomeMenu.UserContinuation();
            }
            else
            {
                foreach(var item in active)
                {
                    item.SubscriptionStatus = "Inactive";
                    subscriptionService.UpdateSubscription(item);
                    Console.WriteLine("\n\n\t\tSubscription cancelled successfully");
                    
                }
                HomeMenu.UserContinuation();
            }
            
        }

        public void GetCustomerInformation()
        {
            Console.Clear();
            var foundCustomer = service.GetCustomerById(CustomerApplicationData.CurrentCustomerId);
            Console.WriteLine($"\t\t\t\t\t\tWelcome {foundCustomer.FirstName} {foundCustomer.LastName}\n\n\t\t\t\t\t\tPortal Dashboard");
            Console.WriteLine($"\t\tLast Name: {foundCustomer.FirstName}\n");
            Console.WriteLine($"\t\tLast Name: {foundCustomer.LastName}\n");
            Console.WriteLine($"\t\tEmail Address: {foundCustomer.EmailAddress}\n");
            Console.WriteLine($"\t\tPhone Number: {foundCustomer.PhoneNumber}\n");
            Console.WriteLine($"\t\tMeter Number: {foundCustomer.MeterNumber}\n");

            Console.WriteLine("\n\t\tDo you want to update your information?\n\t\t1 : Yes\n\t\t2 : No");
            Console.Write($"\t\t  : ");
            string entry = Console.ReadLine();
            switch(entry)
            {
                case "1":
                    UpdateDetails();
                    HomeMenu.UserContinuation();
                    break;
                case "2":
                    HomeMenu.UserContinuation();
                    break;
                default:
                    GetCustomerInformation();
                    break;
            }
           
        }

       public void UpdateDetails()
        {
            var foundCustomer = service.GetCustomerById(CustomerApplicationData.CurrentCustomerId);
            UpdateCustomerInformation(foundCustomer);
        }
        public void UpdateCustomerInformation(CustomerModel customer)
        {
            Console.WriteLine("\t\tWhat would like to update?\n\n");
            Console.WriteLine("\t\t1 : First Name \n\t\t2 : Last Name\n\t\t3 : Email Address\n\t\t4 : Phone Number");
            Console.Write($"\t\t  : ");
            string entry = Console.ReadLine();
            switch(entry)
            {
                case "1":
                    Console.Write("\t\tNew First Name : ");
                    string newFirstName = Console.ReadLine();
                    while (string.IsNullOrEmpty(newFirstName))
                    {
                        Console.WriteLine("\n\n\t\tFirst name cannot be left blank");
                        Console.Write("\t\tFirst Name : ");
                        newFirstName = Console.ReadLine();
                    }
                    customer.FirstName = newFirstName;
                    customer.ModifiedDateTime = DateTime.Now;
                    service.UpdateCustomer(customer);
                    Console.Write("\n\n\t\tChanged Successfully");
                    break;
                case "2":
                    Console.Write("\t\tNew Last Name : ");
                    string newLastName = Console.ReadLine();
                    while (string.IsNullOrEmpty(newLastName))
                    {
                        Console.WriteLine("\n\t\tLast name cannot be left blank");
                        Console.Write("\t\tLast Name : ");
                        newLastName = Console.ReadLine();
                    }
                    customer.LastName = newLastName;
                    customer.ModifiedDateTime = DateTime.Now;
                    service.UpdateCustomer(customer);
                    Console.Write("\n\n\t\tChanged Successfully");
                    break;
                case "3":
                    Console.Write("\t\tNew Email Address : ");
                    string newEmail = Console.ReadLine();
                    while (string.IsNullOrEmpty(newEmail))
                    {
                        Console.WriteLine("\n\t\tEmail cannot be left blank");
                        Console.Write("\t\tEmail : ");
                        newEmail = Console.ReadLine();
                    }
                    customer.EmailAddress = newEmail;
                    customer.ModifiedDateTime = DateTime.Now;
                    service.UpdateCustomer(customer);
                    Console.Write("\n\n\t\tChanged Successfully");
                    break;
                
                case "4":
                    Console.Write("\t\tNew Phone Number : ");
                    string NewPhoneNumber = Console.ReadLine();
                    ulong number;
                    while (!ulong.TryParse(NewPhoneNumber, out number))
                    {
                        Console.WriteLine("\n\t\tPlease enter an 11 digit number");
                        Console.Write("\t\tPhone Number : ");
                        NewPhoneNumber = Console.ReadLine();
                    }
                    customer.PhoneNumber = NewPhoneNumber;
                    customer.ModifiedDateTime = DateTime.Now;
                    service.UpdateCustomer(customer);
                    Console.Write("\n\nChanged Successfully");
                    break;
                default:
                    break;
            }
        }

        public void MakeSubscription()
        {
            //Show all Tariff
            Console.Clear();
            var user = service.GetCustomerById(CustomerApplicationData.CurrentCustomerId);
            var activeSub = subscriptionService.CheckActiveSubscription(user.Id);
            
            if(activeSub.Count != 0)
            {
                Console.WriteLine("\t\tYou currently have an active subscription \n\t\tBuying a new Subscription will deactivate your previous subscription");
                Console.WriteLine("\n\t\t1 : Continue\n\t\t2 : Back to Home ");
                Console.Write($"\t\t  : ");
                string entry = Console.ReadLine();
                Console.Write($"\n\t\tProccessing");
                Security.PrintDotAnimation();
                switch (entry)
                {
                    case "1":
                        MakeSubscriptionPayment();
                        break;
                    case "2":
                        HomeMenu.CurrentStage = 1;
                        break;
                    default:
                        MakeSubscription();
                        break;
                }
               
            }
            else
            {
                MakeSubscriptionPayment();
            }
        }

        private void MakeSubscriptionPayment()
        {
            Console.Clear();
            var tariffs = tariffService.GetAllTariff();
            Console.WriteLine("\t\tWe have four Tariffs");
            Console.WriteLine("\n\n\t\t\tTariff Name \t\tTariff Price");
            for (var i = 0; i < tariffs.Count; i++)
            {
                Console.Write($"\t\t{i + 1} : {tariffs[i].Name}\t\t#{tariffs[i].PricePerUnit} \n");
                if (i == 0)
                {
                    t1 = tariffs[i].PricePerUnit;
                    a1 = tariffs[i].Id;

                }
                else if (i == 1)
                {
                    t2 = tariffs[i].PricePerUnit;
                    a2 = tariffs[i].Id;
                }
                else if (i == 2)
                {
                    t3 = tariffs[i].PricePerUnit;
                    a3 = tariffs[i].Id;

                }
                else if (i == 3)
                {
                    t4 = tariffs[i].PricePerUnit; ;
                    a4 = tariffs[i].Id;
                }
            }
            Console.Write($"\t\t  : ");
            string userInput = Console.ReadLine();
            Console.Write($"\n\t\tProccessing");
            Security.PrintDotAnimation();
            Console.Clear();
            int amountToBuy = default;
            switch (userInput)
            {
                case "1":
                    Console.WriteLine("\t\tHow many units would you like to purchase?");
                    Console.Write($"\t\t  : ");
                    string firstInput = Console.ReadLine();
                    Console.Write($"\n\t\tProccessing");
                    Security.PrintDotAnimation();
                    while (!int.TryParse(firstInput, out amountToBuy))
                    {
                        Console.WriteLine("\t\tInvalid Input\n\t\tHow many units would you like to purchase?");
                        Console.Write($"\t\t  : ");
                        firstInput = Console.ReadLine();
                    }
                    CalculateTotalUnit(amountToBuy, t1, a1);
                    break;
                case "2":
                    Console.WriteLine("\t\tHow many units would you like to purchase?");
                    Console.Write($"\t\t  : ");
                    string secondInput = Console.ReadLine();
                    while (!int.TryParse(secondInput, out amountToBuy))
                    {
                        Console.WriteLine("\t\tInvalid Input\n\t\tHow many units would you like to purchase?");
                        Console.Write($"\t\t  : ");
                        secondInput = Console.ReadLine();
                    }
                    CalculateTotalUnit(amountToBuy, t2, a2);
                    break;
                case "3":
                    Console.WriteLine("\t\tHow many units would you like to purchase?");
                    Console.Write($"\t\t  : ");
                    string thirdInput = Console.ReadLine();
                    while (!int.TryParse(thirdInput, out amountToBuy))
                    {
                        Console.WriteLine("\t\tInvalid Input\n\t\tHow many units would you like to purchase?");
                        Console.Write($"\t\t  : ");
                        thirdInput = Console.ReadLine();
                    }
                    CalculateTotalUnit(amountToBuy, t3, a3);
                    break;
                case "4":
                    Console.WriteLine("\t\tHow many units would you like to purchase?");
                    Console.Write($"\t\t  : ");
                    string fourthInput = Console.ReadLine();
                    while (!int.TryParse(fourthInput, out amountToBuy))
                    {
                        Console.WriteLine("\t\tInvalid Input\n\t\tHow many units would you like to purchase?");
                        Console.Write($"\t\t  : ");
                        fourthInput = Console.ReadLine();
                    }
                    CalculateTotalUnit(amountToBuy, t4, a4);
                    break;
                default:
                    MakeSubscriptionPayment();
                    break;
            }
            
        }
        private void CalculateTotalUnit(int unitToBePurchased, decimal pricePerUnit, string tariffId)
        {
            decimal totalAmountPurchased = Convert.ToDecimal(unitToBePurchased) * pricePerUnit;
            Console.WriteLine($"\t\tYou are about to pay #{totalAmountPurchased} \n\t\tDo you want to proceed? \n\t\t1 : Yes\n\t\t2 : No  ");
            Console.Write($"\t\t  : ");
            string entry = Console.ReadLine();
            Console.Write($"\n\t\tProccessing");
            Security.PrintDotAnimation();
            switch (entry)
            {
                case "1":
                    Console.WriteLine($"\t\tYou have Successfully purchased {unitToBePurchased} Units");
                    
                    var currentCustomer = service.GetCustomerById(CustomerApplicationData.CurrentCustomerId);
                    var sub = subscriptionService.CheckActiveSubscription(currentCustomer.Id);
                    foreach(var item in sub)
                    {
                        item.SubscriptionStatus = "Inactive";
                        subscriptionService.UpdateSubscription(item);
                    }

                    CustomerSubcription customerSubcription = new CustomerSubcription
                    {
                        Id = Guid.NewGuid().ToString(),
                        SubscriptionStatus = "Active",
                        CustomerId = currentCustomer.Id,
                        TariffId = tariffId,
                        SubcriptionDateTime = DateTime.Now,
                        Amount = totalAmountPurchased,
                    };
                    subscriptionService.MakeSubscription(customerSubcription);
                    HomeMenu.UserContinuation();
                    break;
                case "2":
                    HomeMenu.CurrentStage=1;
                    break;
                default:
                   
                    break;
            }
            
        }

        public void ViewSubscriptionsHistory()
        {
            Console.Clear();
            string tariffId = default;
            var foundCustomer = service.GetCustomerById(CustomerApplicationData.CurrentCustomerId);
            var subscriptions = subscriptionService.GetCustomerSubscription(foundCustomer.Id);
            if (subscriptions.Count == 0)
            {
                Console.WriteLine("\t\tYou have not made any Subscription");
                HomeMenu.UserContinuation();
            }
            else
            {
                Console.WriteLine("\t\tSubscription History\n\n");
                Console.WriteLine("Tariff Name\t\tAmount\t\t\tSubscription Date\t\tSubscription Status\n\n");
                foreach (var subscription in subscriptions)
                {
                    tariffId = subscription.TariffId;
                    var customerTariff = tariffService.GetTarriffById(tariffId);
                    Console.Write($"{customerTariff.Name}\t#{subscription.Amount}\t\t{subscription.SubcriptionDateTime}\t\t{subscription.SubscriptionStatus}\n\n");
                }

                HomeMenu.UserContinuation();
                
            }

        }

        
    }
}
