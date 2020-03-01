using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
   public class CoordinateBusiness
    {
       public static bool AddCoordinate(int enterprise_Id, DateTime time, string problem)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Coordinate st = new Coordinate();
                   st.Enterprise_Id = enterprise_Id;
                   st.Time = time;
                   st.Problem = problem;
                   st.IsDelete = false;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }

       public static bool UpdateCoordinate(int CoordinateId, DateTime time, string problem)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Coordinate st = Coordinate.FindById(CoordinateId);
                   st.Time = time;
                   st.Problem = problem;
                   st.Save();
               });
           }
           catch { return false; }
           return true;
       }
       public static bool DeleteCoordinate(int CoordinateId)
       {
           try
           {
               DbEntry.UsingTransaction(delegate
               {
                   Coordinate st = Coordinate.FindById(CoordinateId);
                   st.IsDelete = true;
                   st.Save();
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
       public static IPagedSelector<Coordinate> GetCoordinates(int enterprise_Id, int pagesize)
       {
           //查询
           try
           {
               var ps = DbEntry
                   .From<Coordinate>()
                   .Where(CK.K["Enterprise_Id"] == enterprise_Id && CK.K["IsDelete"] == false)
                   .OrderBy((ASC)"Time")
                   .PageSize(pagesize)
                   .GetPagedSelector();
               return ps;
           }
           catch
           {
               return null;
           }
       }
    }
}
