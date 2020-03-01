using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.DataModel;
using Lephone.Data;
using System.Data;
using Lephone.Extra;

namespace BusinessSupply.BusinessOper
{
    public class NoticeBusiness
    {

















        //前科协代码
        //将需要的代码都放在上面，日后将一下代码全部删除
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///获取所有公告
        /// </summary>
        /// <returns></returns>
        public static DataTable GetNotice()
        {
            try
            {
                DataTable dt_notice = DbEntry.From<TNotice>().Where(CK.K["IsDel"] == "False").Select().ToDataTable();
                return dt_notice;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 增加Notice记录
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="createuserid"></param>
        /// <param name="showtime"></param>
        /// <returns></returns>
        public static string AddNotice(string title, string content, int createuserid, string showtime)
        {
            try
            {
                var notice = new TNotice();
                notice.CreateTime = DateTime.Now;
                notice.CreateUserId = createuserid;
                notice.Title = title;
                notice.NoticeContent = content;
                notice.ShowTime = Convert.ToDateTime(showtime);
                notice.IsDel = false;
                notice.Save();
                return "1";

            }
            catch
            {
                return "0";
            }
        }

        /// <summary>
        /// DbEntry分页查询数据，查询有效公告
        /// </summary>
        /// <param name="start">查询起始日期</param>
        /// <param name="end">查询截至日期</param>
        /// <param name="pagesize">gridview每页数据行数</param>
        /// <returns></returns>
        public static IPagedSelector<TNotice> GetNoticePs(string start, string end, int pagesize)
        {
            //查询某时间区间内的公告
            if (start != "" && end != "")
            {
                DateTime s = Convert.ToDateTime(start);
                DateTime e = Convert.ToDateTime(end);
                e = e.AddDays(1);
                try
                {
                    var ps_date = DbEntry
                        .From<TNotice>()
                        .Where(CK.K["ShowTime"] >= s && CK.K["ShowTime"] <= e && CK.K["IsDel"] == "False")
                        .OrderBy((DESC)"ShowTime")
                        .PageSize(pagesize)
                        .GetPagedSelector();
                    return ps_date;
                }
                catch
                {
                    return null;
                }
            }

            //查询所有未删除公告
            try
            {
                var pageselector = DbEntry
                             .From<TNotice>()
                             .Where(CK.K["IsDel"] == "False")
                             .OrderBy((DESC)"ShowTime")
                             .PageSize(pagesize)
                             .GetPagedSelector();
                return pageselector;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 根据主键Id查找公告
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        public static DataTable GetNoticeById(int noticeid)
        {
            try
            {
                DataTable notice = DbEntry.From<TNotice>().Where(CK.K["Id"] == noticeid && CK.K["IsDel"] == "False").Select().ToDataTable();
                return notice;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 根据主键Id更新公告,更新成功返回success，失败返回error
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="updateid"></param>
        /// <param name="showtime"></param>
        public static string UpdateNoticeById(int noticeid, string title, string content, int updateid, DateTime updatetime, DateTime showtime)
        {
            try
            {
                var notice = TNotice.FindById(noticeid);
                notice.Title = title;
                notice.NoticeContent = content;
                //notice.UpdateTime = DateTime.Now;
                notice.UpdataUserId = updateid;
                notice.UpdateTime = updatetime;
                notice.ShowTime = showtime;

                notice.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }
        /// <summary>
        /// 根据主键Id删除公告（软删除），成功返回"success"，失败返回"error"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string DeleteNoticeById(int noticeid, int deluserid, DateTime deltime)
        {
            try
            {
                var notice = TNotice.FindById(noticeid);
                notice.IsDel = true;
                notice.DelTime = deltime;
                notice.DelUserId = deluserid;
                notice.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }


    }
}
