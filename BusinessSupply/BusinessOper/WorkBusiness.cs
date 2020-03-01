using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
   public class WorkBusiness
    {
       public static bool AddWork(int people_Id, string company, string post,string address,string type)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Work st = new Work();
                   st.People_Id = people_Id;
                   st.Company = company;
                   st.Post = post;
                   st.Address = address;
                   st.Type = type;
                   st.IsDelete = false;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool AddWorkNull(int people_Id)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Work st = new Work();
                   st.People_Id = people_Id;
                   st.IsDelete = false;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool UpdateWork(int workId, string company, string post, string address, string type)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Work st = Work.FindById(workId);
                   st.Company = company;
                   st.Post = post;
                   st.Address = address;
                   st.Type = type;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool UpdateWorkByPeople(int people_Id, string company, string post, string address, string type)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Work st = Work.FindOne(CK.K["People_Id"] == people_Id && CK.K["IsDelete"] == false);
                   st.Company = company;
                   st.Post = post;
                   st.Address = address;
                   st.Type = type;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool DeleteWork(int workId)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Work st = Work.FindById(workId);
                   st.IsDelete = true;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }
    }
}
