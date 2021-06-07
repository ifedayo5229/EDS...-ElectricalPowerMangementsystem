using System;
using System.Collections.Generic;
using System.Text;
using EDSCustomerPortal.Services;
using ElectricityDigitalSystem.Data;
using ElectricityDigitalSystem.Models;

namespace EDSCustomerPortal.Menu
{
    public class HomeMenu
    {
        public static int CurrentStage { get; set; } = 1;
        
        public static void HomeMenuDisplay(CustomerModel customer)
        {
            bool appIsRunning = true;
            while (appIsRunning)
            {
                  
                while(CurrentStage==1)
                {
                    
                    Console.Clear();
                    Console.WriteLine($"\t\t\t\t\t\tWelcome {customer.FirstName} {customer.LastName}\n\n\t\t\t\t\t\tPortal Dashboard");
                    Console.WriteLine($"\n\n\t\t1 : View Information\n\n\t\t2 : Buy Subscription\n\n\t\t3 : Cancel Subscription\n\n\t\t4 : View Subscription History\n\n\t\t5 : Exit");
                    Console.Write($"\n\t\t  : ");
                    string selection = Console.ReadLine();
                    Console.Write($"\n\t\tProccessing");
                    Security.PrintDotAnimation();
                    
                    switch (selection)
                    {
                       
                        case "1":

                            CustomerAccess accessToCustomerSercives = new CustomerAccess();
                            accessToCustomerSercives.GetCustomerInformation();
                            break;
                        case "2":
                            CustomerAccess CustomerSercives = new CustomerAccess();
                            CustomerSercives.MakeSubscription();
                            
                            break;
                        case "3":
                            CustomerAccess Customer = new CustomerAccess();
                            Customer.CancelCustomerSubscription();
                            
                            break;
                        case "4":
                            CustomerAccess Sercives = new CustomerAccess();
                            Sercives.ViewSubscriptionsHistory();
                            
                            break;
                        case "5":
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                    Console.Clear();
                }
               
            }
            

        }

        public static void UserContinuation()
        {
            Console.WriteLine("\n\n\t\t1 : Home Menu\n\t\t2 : Logout\n\t\t3 : Exit");
            Console.Write($"\t\t  : ");
            string optionSelected = Console.ReadLine();
            Console.Write("\n\n\t\tProccessing");
            Security.PrintDotAnimation();
            switch (optionSelected)
            {
                case "1":
                    CurrentStage = 1;
                    break;
                case "2":
                    AuthenticationDisplay.LoginScreen();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    break;

            }
            Console.Clear();
        }

    }
}

