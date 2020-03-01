using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class SituationToSpecialWorkBusiness
    {
        public static bool AddSituation(int communicationId, DateTime time, string location, string situationName)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    SituationToSpecialWork st = new SituationToSpecialWork();
                    st.Communication_Id = communicationId;
                    st.Time = time;
                    st.Location = location;
                    st.SituationName = situationName;
                    st.IsDelete = false;
                    st.Save();
                });
            }
            catch { return false; }
            return true;
        }

        public static bool UpdateSituation(int situationId, DateTime time, string location, string situationName)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    SituationToSpecialWork st = SituationToSpecialWork.FindById(situationId);
                    st.Time = time;
                    st.Location = location;
                    st.SituationName = situationName;
                    st.Save();
                });
            }
            catch { return false; }
            return true;
        }
        public static bool DeleteSituation(int situationId)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    SituationToSpecialWork st = SituationToSpecialWork.FindById(situationId);
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
        public static IPagedSelector<SituationToSpecialWork> GetSituations(int communicationId, int pagesize)
        {
            //查询
            try
            {
                var ps = DbEntry
                    .From<SituationToSpecialWork>()
                    .Where(CK.K["Communication_Id"] == communicationId && CK.K["IsDelete"] == false)
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
