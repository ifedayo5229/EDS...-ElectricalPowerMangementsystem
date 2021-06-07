using System;
using System.Collections.Generic;
using System.Text;

namespace EDSCustomerPortal.Interfaces
{
    public interface ICustomerAccess
    {
        //View Information
        //Cancel Subcription
        //Make Subcription
        //View all Subcription

        void GetCustomerInformation();
        void CancelCustomerSubscription();
        //void InsertHistory();
        void MakeSubscription();
        void ViewSubscriptionsHistory();        
    }
}
