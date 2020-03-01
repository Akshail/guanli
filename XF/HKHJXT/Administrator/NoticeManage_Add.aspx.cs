using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessSupply.BusinessOper;
using DataModel.DataModel;

namespace XF.HKHJXT.Administrator
{
    public partial class NoticeManage_Add : System.Web.UI.Page
    {
        private string uid
        {
            get
            {
                try
                {
                    return Session["UserID"].ToString().Trim();
                }
                catch
                {
                    return null;
                }

            }
        }
        private string ip
        {
            get
            {
                try
                {
                    return Session["IP"].ToString().Trim();
                }
                catch
                {
                    return null;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblWeek.Text = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek); ;
            timer1.Interval = 1000;
            lblDateTime.Text = DateTime.Now.ToString();
            if (!IsPostBack)
            {
                if (uid == null)
                {
                    Response.Redirect("../../LogInPage.aspx");
                }
                this.input_ShowTime.Value = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
            }

        }

        protected void Unnamed1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString();
        }


        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                string title = tb_Title.Text.Trim();
                if(title.IsNullOrEmpty())
                {
                    WebAlart("标题不能为空，请重新输入！");
                    return;
                }
                int createuserid = Convert.ToInt32(uid.Trim());
                //string content = this.tb_Content.Text.Replace("\r\n", "<br/>").Replace(" ", "&nbsp");
                string content = this.tb_Content.InnerText;

                string r = BusinessSupply.BusinessOper.NoticeBusiness.AddNotice(title, content, createuserid, this.input_ShowTime.Value);
                if (r == "1")
                {
                    //记录删除日志
                    TUserInfo userinfo = BusinessSupply.BusinessOper.UserBusiness.GetUserById(Convert.ToInt32(uid));                   
                    string logresult = BusinessSupply.BusinessOper.LogInfoBusiness.AddLogInfo(Convert.ToInt32(uid), DateTime.Now, 1, userinfo.Name, 2, "通知公告", ip, userinfo.Account);
                    if (logresult == "0")
                    {
                        WebAlart("数据加载错误");
                    }
                    else
                    {
                        WebAlart("添加成功", "NoticeManage.aspx");
                    }
                }
                else
                {
                    WebAlart("添加失败", "NoticeManage.aspx");
                }
            }
            catch
            {
                WebAlart("数据加载错误", "NoticeManage.aspx");
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
        public void WebAlart(string t, string url)
        {
            Response.Write("<script>alert('" + t + "');document.location.replace('" + url + "');</script>");
        }
    }
}