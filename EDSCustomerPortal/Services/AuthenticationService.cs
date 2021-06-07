using ElectricityDigitalSystem.ClientServices;
using ElectricityDigitalSystem.Models;
using System;
using EDSCustomerPortal.Menu;
using ElectricityDigitalSystem.Data;
using EDSCustomerPortal.AppData;
using System.Collections.Generic;

namespace EDSCustomerPortal.Services
{
    public class AuthenticationService
    {
        private static CustomerModel LoggedInCustomer;
        private static CustomerModel AttemptingUser = new CustomerModel();
        private static CustomerService customerService = new CustomerService();
        private static readonly Random r = new Random(DateTime.Now.Second);
        public static void LoginUser()
        {
            bool IsLoggedIn = false;
            while (!IsLoggedIn)
            {
                Console.WriteLine("\t\t\t\t\t\tWelcome To EDS CUSTOMER PORTAL.\n\n");
                Console.WriteLine("\t\t\t\t\tLogin with your Email and Password");
                Console.Write("\t\tEmail : ");
                string Emailvalue = Console.ReadLine();
                Console.WriteLine();
                Console.Write("\t\tPassword : ");
                string PasswordValue = Security.ReadPassword();
                Console.Write("\n\n\t\tProccessing");
                Security.PrintDotAnimation();
                Console.Clear();

                AttemptingUser = customerService.GetCustomerByEmail(Emailvalue);
                if (AttemptingUser != null)
                {
                    if (PasswordValue==AttemptingUser.Password)
                    {                                             
                        LoggedInCustomer =AttemptingUser;

                        // Console.WriteLine($"\t\t\t\t\t\t\tWelcome {LoggedInCustomer.FirstName} {LoggedInCustomer.LastName}");
                        CustomerApplicationData.CurrentCustomerId = LoggedInCustomer.Id;
                        HomeMenu.HomeMenuDisplay(LoggedInCustomer);
                        //HomeMenu.CurrentStage = 2;
                       
                        IsLoggedIn = true;
                    }
                    else
                    {
                        IsLoggedIn = false;
                        Console.Clear();
                        Console.WriteLine("\t\tUsername or Password does not exist\n\t\tCreate an account");
                        Console.Write($"\n\t\tProccessing");
                        Security.LongerPrintDotAnimation();
                        AuthenticationDisplay.LoginScreen();
                    }
                    
                }
                else
                {
                    IsLoggedIn = false;
                    Console.Clear();
                    Console.WriteLine("\t\tUsername or Password does not exist\n\t\tCreate an account");
                    Console.Write($"\n\t\tProccessing");
                    Security.LongerPrintDotAnimation();
                    
                    AuthenticationDisplay.LoginScreen();

                }
                


            }



        }

        public static string RegisterUser(CustomerModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                //CustomerService service = new CustomerService();
                string email = customerService.RegisterCustomer(model);
                return email == null ? "Failed" : "Success";
            }
        }
        public static void RegisterationForUsers()
        {
            Dictionary<string, string> navItemDIc = new Dictionary<string, string>();
            List<string> navigationItems = new List<string>
            {
                "FirstName", "LastName", "Email", "Password", "PhoneNumber"
            };
            Console.Clear();
            Console.WriteLine("\t\tPlease Provide your Details");

            for (int i = 0; i < navigationItems.Count; i++)
            {
                Console.Write($"\t\t{navigationItems[i]} : ");
                var value = Console.ReadLine();
                      
                navItemDIc.Add(navigationItems[i], value);
            }

            string FirstName, LastName, Email, Password,PhoneNumber;

            FirstName = navItemDIc["FirstName"];
            LastName = navItemDIc["LastName"];
            Email = navItemDIc["Email"];
            Password = navItemDIc["Password"];
            PhoneNumber = navItemDIc["PhoneNumber"];
            while (string.IsNullOrEmpty(FirstName))
            {
                Console.WriteLine("\n\n\t\tFirst name cannot be left blank");
                Console.Write("\t\tFirst Name : ");
                FirstName = Console.ReadLine();
            }
            
            while(string.IsNullOrEmpty(LastName))
            {
                Console.WriteLine("\n\t\tLast name cannot be left blank");
                Console.Write("\t\tLast Name : ");
                LastName = Console.ReadLine();
            }
            
            while (string.IsNullOrEmpty(Email))
            {
                Console.WriteLine("\n\t\tEmail cannot be left blank");
                Console.Write("\t\tEmail : ");
                Email = Console.ReadLine();
            }
          
            while (string.IsNullOrEmpty(Password))
            {
                Console.WriteLine("\n\t\tPassword cannot be left blank");
                Console.Write("\t\tPassword : ");
                Password = Console.ReadLine();
            }
           
            ulong number;
            while(!ulong.TryParse(PhoneNumber,out number))
            {
                Console.WriteLine("\n\t\tPlease enter an 11 digit number");
                Console.Write("\t\tPhone Number : ");
                PhoneNumber = Console.ReadLine();
            }

            

            navItemDIc.Add("Firstname", FirstName);
            navItemDIc.TryAdd("LastName", LastName);
            navItemDIc.TryAdd("LastName", LastName);
            navItemDIc.TryAdd("Email", Email);
            navItemDIc.TryAdd("Password", Password);
            navItemDIc.TryAdd("PhoneNumber", number.ToString("00000000000"));


            //public int SeatNumber => r.Next(1, 19);
            CustomerModel model = new CustomerModel
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = FirstName,
                LastName = LastName,

                EmailAddress = Email,
                Password = Password,
                MeterNumber = string.Concat("MTR-" + r.Next(100000000, 999999999)),
                PhoneNumber = number.ToString("00000000000"),
            };

            string registrationResponds = RegisterUser(model);
            if (registrationResponds == "Success")
            {
                Console.WriteLine("\t\tRegistration Successful");
                Console.Write("\t\tRedirecting you to Home Page");
                Security.PrintDotAnimation();
                AuthenticationDisplay.LoginScreen();
                
                
            }
            else
            {

                Console.WriteLine("An Error occured While Trying to Create your Account Please try Again");
                Security.PrintDotAnimation();
                
                AuthenticationDisplay.LoginScreen();
            }

        }


    }
}

