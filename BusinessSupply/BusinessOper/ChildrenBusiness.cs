using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
   public class ChildrenBusiness
    {
       public static bool AddChildren(int mariageId, string name, bool sex, DateTime birthday, string address, string tip)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Children chi = new Children();
                   chi.Mariage_Id = mariageId;
                   chi.Name = name;
                   chi.Sex = sex;
                   //if(birthday != new DateTime(1900,1,1))
                   //{
                   //    chi.Birthday = birthday;
                   //}
                   //else
                   //{
                   //    chi.Birthday = null;
                   //}
                   chi.Birthday = birthday;
                   chi.Address = address;
                   chi.Tip = tip;
                   chi.IsDelete = false;
                   chi.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool UpdateChildren(int childrenId, string name, bool sex, DateTime birthday, string address, string tip)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Children chi = Children.FindById(childrenId);
                   chi.Name = name;
                   chi.Sex = sex;
                   //if (birthday != new DateTime(1900, 1, 1))
                   //{
                   //    chi.Birthday = birthday;
                   //}
                   //else
                   //{
                   //    chi.Birthday = null;
                   //}
                   chi.Birthday = birthday;
                   chi.Address = address;
                   chi.Tip = tip;
                   chi.Save();
               });
           }
           catch { return false; }
           return true;
       }

       public static bool DeleteChildren(int childrenId)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Children chi = Children.FindById(childrenId);
                   chi.IsDelete = true;
                   chi.Save();
               });
           }
           catch { return false; }
           return true;
       }
       /// <summary>
       /// DbEntry分页查询数据，
       /// </summary>
       /// <param name=""></param>
       /// <param name=""></param>
       /// <param name="pagesize">gridview每页数据行数</param>
       /// <returns></returns>
       public static IPagedSelector<Children> GetChildrens(int mariageId, int pagesize)
       {
           //查询
           try
           {
               var ps = DbEntry
                   .From<Children>()
                   .Where(CK.K["Mariage_Id"] == mariageId && CK.K["IsDelete"] == false)
                   .OrderBy((DESC)"Birthday")
                   .PageSize(pagesize)
                   .GetPagedSelector();
               return ps;
           }
           catch
           {
               return null;
           }
       }

       public static DataTable GetChildrensAsDataTable(int mariageId)
       {
           //查询
           try
           {
               var ps = DbEntry.From<Children>().Where(CK.K["Mariage_Id"] == mariageId && CK.K["IsDelete"] == false).OrderBy((DESC)"Birthday").Select().ToDataTable();
               return ps;
           }
           catch
           {
               return null;
           }
       }
    }
}
