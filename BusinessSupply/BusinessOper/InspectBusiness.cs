using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class InspectBusiness
    {
        public static bool AddInspect(int enterpriseId, DateTime time, string unit, string result, string photoName)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Inspect st = new Inspect();
                    st.Enterprise_Id = enterpriseId;
                    st.Time = time;
                    st.Unit = unit;
                    st.Result = result;
                    st.PhotoName = photoName;
                    st.IsDelete = false;
                    st.Save();
                });
            }
            catch { return false; }
            return true;
        }

        public static bool UpdateInspect(int inspectId, DateTime time, string unit, string result, string photoName)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Inspect st = Inspect.FindById(inspectId);
                    st.Time = time;
                    st.Unit = unit;
                    st.Result = result;
                    st.PhotoName = photoName;
                    st.Save();
                });
            }
            catch { return false; }
            return true;
        }

        public static bool DeleteInspect(int inspectId)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Inspect st = Inspect.FindById(inspectId);
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
        public static IPagedSelector<Inspect> GetInspects(int enterprise_Id, int pagesize)
        {
            //查询
            try
            {
                var ps = DbEntry
                    .From<Inspect>()
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
