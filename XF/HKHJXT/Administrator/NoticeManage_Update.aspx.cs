using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessSupply.BusinessOper;
using DataModel.DataModel;
using Lephone.Core;

namespace XF.HKHJXT.Administrator
{
    public partial class NoticeManage_Update : System.Web.UI.Page
    {
        private string uid
        {
            get
            {
                try
                {
                    return Session["UserID"].ToString();
                }
                catch
                {
                    return null;
                }

            }
        }
        string noticeid
        {
            get
            {
                try
                {
                    return this.Request.QueryString["Id"];
                }
                catch
                {
                    return null;
                }
            }
        }
        //private string ip
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Session["IP"].ToString().Trim();
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            lblWeek.Text = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek); ;
            timer1.Interval = 1000;
            lblDateTime.Text = DateTime.Now.ToString();

            if (uid == null)
            {
                Response.Redirect("../../LogInPage.aspx");
            }
            if (noticeid == null)
            {
                Response.Redirect("NoticeManage.aspx");
            }
            if (!IsPostBack)
            {

                DataTable dt = new DataTable();
                dt = BusinessSupply.BusinessOper.NoticeBusiness.GetNoticeById(Convert.ToInt32(noticeid));
                this.tb_Title.Text = dt.Rows[0]["Title"].ToString();
                //string content = dt.Rows[0]["NoticeContent"].ToString().Replace("<br/>", "\r\n").Replace("&nbsp"," " );
                string content = dt.Rows[0]["NoticeContent"].ToString();

                this.tb_Content.InnerHtml = content;

                //string temp =BusinessSupply.Helper.ConvertTime(dt.Rows[0]["ShowTime"].ToString());
                string temp = dt.Rows[0]["ShowTime"].ToString();
                DateTime dt_temp = Convert.ToDateTime(temp);

                this.input_ShowTime.Value = dt_temp.ToString("yyyy年MM月dd日 HH:mm:ss");

            }
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                string title = this.tb_Title.Text.Trim();
                string content = this.tb_Content.InnerText;
                DateTime showtime = Convert.ToDateTime(this.input_ShowTime.Value);
                DateTime updatetime = DateTime.Now;
                string r = BusinessSupply.BusinessOper.NoticeBusiness.UpdateNoticeById(Convert.ToInt32(noticeid), title, content, Convert.ToInt32(uid), updatetime, showtime);
                if (r == "1")
                {
                    ////记录删除日志
                    //TUserInfo userinfo = BusinessSupply.BusinessOper.UserBusiness.GetUserById(Convert.ToInt32(uid));
                    //string logresult = BusinessSupply.BusinessOper.LogInfoBusiness.AddLogInfo(Convert.ToInt32(uid), DateTime.Now, 1, userinfo.Name, 2, "通知公告", ip, userinfo.Account);
                    //if (logresult == "0")
                    //{
                    //    WebAlart("数据加载错误");
                    //}
                    WebAlart("更新成功");
                }
                else
                {
                    WebAlart("更新失败");

                }
            }
            catch
            {
                WebAlart("数据加载错误");
            }

        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("NoticeManage.aspx");
        }
        public void WebAlart(string t)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "key", "<script>alert('" + t + "');</script>");
        }

        ////处理公告标题和公告内容中的非法字符串（HTML标签）
        //protected void Page_Error(object sender, EventArgs e)
        //{


        //    Exception ex = Server.GetLastError();


        //    if (HttpContext.Current.Server.GetLastError() is HttpRequestValidationException)
        //    {


        //        HttpContext.Current.Response.Write("请输入合法的字符串【<a href=\"javascript:history.back(0);\">返回</a>】");


        //        HttpContext.Current.Server.ClearError();

        //    }

        protected void Unnamed1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString();
        }

        //}
    }
}