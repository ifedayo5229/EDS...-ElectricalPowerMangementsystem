using EDSCustomerPortal.Menu;
using EDSCustomerPortal.Services;
using ElectricityDigitalSystem.ClientServices;
using System;

namespace EDSCustomerPortal
{
    class Program
    {
        static void Main(string[] args)
        {

            TariffService tariff = new TariffService();
            tariff.AddTariffToService();
            AuthenticationDisplay.LoginScreen();
        }
    }
}
