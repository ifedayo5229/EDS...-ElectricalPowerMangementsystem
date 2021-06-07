using EDSCustomerPortal.Services;
using System;



namespace EDSCustomerPortal.Menu
{
    public class AuthenticationDisplay 
    {  
            
        public static void LoginScreen()
        {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t\tWelcome To EDS CUSTOMER PORTAL.\n\n");
                Console.WriteLine($"\t\t\t\t\t\t   What would you like to do?\n");
                Console.WriteLine("\t\t1 : Login\n\t\t2 : Register\n\t\t3 : Exit");
                Console.Write($"\t\t  : ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        AuthenticationService.LoginUser();
                        break;
                    case "2":
                        Console.Clear();
                        AuthenticationService.RegisterationForUsers();
                        break;
                case "3":
                    Environment.Exit(0);
                    break;
                    default:
                    LoginScreen();
                        break;
                }
            

            



        }
        
        
            

        
        private void UpdateCustomer()
        {

        }
    }
}
