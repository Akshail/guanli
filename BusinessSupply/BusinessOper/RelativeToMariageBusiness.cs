using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class RelativeToMariageBusiness
    {
        public static bool AddRelative(int mariageId, string name, bool sex, string relationship, string address, string tip)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    RelativeToMariage rea = new RelativeToMariage();
                    rea.Mariage_Id = mariageId;
                    rea.Name = name;
                    rea.Sex = sex;
                    rea.Relationship = relationship;
                    rea.Address = address;
                    rea.Tip = tip;
                    rea.IsDelete = false;
                    rea.Save();
                });
            }
            catch { return false; }
            return true;
        }
        public static bool UpdateRelative(int relativeId, string name, bool sex, string relationship, string address, string tip)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    RelativeToMariage rea = RelativeToMariage.FindById(relativeId);
                    rea.Name = name;
                    rea.Sex = sex;
                    rea.Relationship = relationship;
                    rea.Address = address;
                    rea.Tip = tip;
                    rea.Save();
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
                    RelativeToMariage rea = RelativeToMariage.FindById(relativeId);
                    rea.IsDelete = true;
                    rea.Save();
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
        public static IPagedSelector<RelativeToMariage> GetRelatives(int mariageId, int pagesize)
        {
            //查询
            try
            {
                var ps = DbEntry
                    .From<RelativeToMariage>()
                    .Where(CK.K["Mariage_Id"] == mariageId && CK.K["IsDelete"] == false)
                    .OrderBy((ASC)"Name")
                    .PageSize(pagesize)
                    .GetPagedSelector();
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetRelativesAsDataTable(int mariageId)
        {
            //查询
            try
            {
                var ps = DbEntry.From<RelativeToMariage>().Where(CK.K["Mariage_Id"] == mariageId && CK.K["IsDelete"] == false).OrderBy((ASC)"Name").Select().ToDataTable();
                return ps;
            }
            catch
            {
                return null;
            }
        }
    }
}
