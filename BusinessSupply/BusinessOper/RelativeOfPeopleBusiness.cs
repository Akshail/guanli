using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
   public class RelativeOfPeopleBusiness
    {
       public static bool AddRelative(int people_Id, string name, string phoneNo, string workingCondition, string company, string relationship)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Relative st = new Relative();
                    st.People_Id = people_Id;
                    st.Name = name;
                    st.PhoneNo = phoneNo;
                    st.WorkingCondition = workingCondition;
                    st.Company = company;
                    st.Relationship = relationship;
                    st.IsDelete = false;
                    st.Save();
                });
            }
            catch { return false; }
            return true;
        }
       public static bool AddRelativeNull(int people_Id)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Relative st = new Relative();
                   st.People_Id = people_Id;
                   st.IsDelete = false;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool UpdateRelative(int relativeId, string name, string phoneNo, string workingCondition, string company, string relationship)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Relative st = Relative.FindById(relativeId);
                   st.Name = name;
                   st.PhoneNo = phoneNo;
                   st.WorkingCondition = workingCondition;
                   st.Company = company;
                   st.Relationship = relationship;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool UpdateRelativeByPeople(int people_Id, string name, string phoneNo, string workingCondition, string company, string relationship)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Relative st = Relative.FindOne(CK.K["People_Id"] == people_Id && CK.K["IsDelete"] == false);
                   st.Name = name;
                   st.PhoneNo = phoneNo;
                   st.WorkingCondition = workingCondition;
                   st.Company = company;
                   st.Relationship = relationship;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool DeleteRelative(int relativeId)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Relative st = Relative.FindById(relativeId);
                   st.IsDelete = true;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }
    }
}
