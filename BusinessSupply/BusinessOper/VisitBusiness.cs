using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
   public  class VisitBusiness
    {
       public static bool AddVisit(int people_Id, string name, string relationship, DateTime arriveTime, DateTime leaveTime, string address, string phone)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Visit st = new Visit();
                   st.People_Id = people_Id;
                   st.Name = name;
                   st.Relationship = relationship;
                   st.ArriveTime = arriveTime;
                   st.LeaveTime = leaveTime;
                   st.Address = address;
                   st.Phone = phone;
                   st.IsDelete = false;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool AddVisitNull(int people_Id)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Visit st = new Visit();
                   st.People_Id = people_Id;
                   st.IsDelete = false;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool UpdateVisit(int visit_Id, string name, string relationship, DateTime arriveTime, DateTime leaveTime, string address, string phone)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Visit st = Visit.FindById(visit_Id);
                   st.Name = name;
                   st.Relationship = relationship;
                   st.ArriveTime = arriveTime;
                   st.LeaveTime = leaveTime;
                   st.Address = address;
                   st.Phone = phone;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool UpdateVisitByPeople(int people_Id, string name, string relationship, DateTime arriveTime, DateTime leaveTime, string address, string phone)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Visit st = Visit.FindOne(CK.K["People_Id"] == people_Id && CK.K["IsDelete"] == false);
                   st.Name = name;
                   st.Relationship = relationship;
                   st.ArriveTime = arriveTime;
                   st.LeaveTime = leaveTime;
                   st.Address = address;
                   st.Phone = phone;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool DeleteVisit(int visit_Id)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Visit st = Visit.FindById(visit_Id);
                   st.IsDelete = true;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }
    }
}
