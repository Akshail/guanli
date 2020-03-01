using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
   public class BusinessBusiness
    {
       public static bool AddBusiness(int people_Id, string scope, string type, string registeredCapital, string registeredPlace, string name, string address, string phoneNo, string postalcode)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Business bus = new Business();
                   bus.People_Id = people_Id;
                   bus.Scope = scope;
                   bus.Type = type;
                   bus.RegisteredCapital = registeredCapital;
                   bus.RegisteredPlace = registeredPlace;
                   bus.Name = name;
                   bus.Address = address;
                   bus.PhoneNo = phoneNo;
                   bus.Postalcode = postalcode;
                   bus.IsDelete = false;
                   bus.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool AddBusinessNull(int people_Id)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Business bus = new Business();
                   bus.People_Id = people_Id;
                   bus.IsDelete = false;
                   bus.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool UpdateBusiness(int business_Id, string scope, string type, string registeredCapital, string registeredPlace, string name, string address, string phoneNo, string postalcode)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Business bus = Business.FindById(business_Id);
                   bus.Scope = scope;
                   bus.Type = type;
                   bus.RegisteredCapital = registeredCapital;
                   bus.RegisteredPlace = registeredPlace;
                   bus.Name = name;
                   bus.Address = address;
                   bus.PhoneNo = phoneNo;
                   bus.Postalcode = postalcode;
                   bus.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool UpdateBusinessByPeople(int people_Id, string scope, string type, string registeredCapital, string registeredPlace, string name, string address, string phoneNo, string postalcode)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Business bus = Business.FindOne(CK.K["People_Id"] == people_Id && CK.K["IsDelete"] == false);
                   bus.Scope = scope;
                   bus.Type = type;
                   bus.RegisteredCapital = registeredCapital;
                   bus.RegisteredPlace = registeredPlace;
                   bus.Name = name;
                   bus.Address = address;
                   bus.PhoneNo = phoneNo;
                   bus.Postalcode = postalcode;
                   bus.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool DeleteBusiness(int business_Id)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Business bus = Business.FindById(business_Id);
                    bus.IsDelete = true;
                    bus.Save();
                });
            }
            catch { return false; }
            return true;
        }
    }
}
