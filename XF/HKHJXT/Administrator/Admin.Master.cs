using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XF.HKKJXT.Administrator
{
    public partial class Admin : System.Web.UI.MasterPage
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

        private string uType
        {
            get
            {
                try
                {
                    return Session["UserType"].ToString().Trim();
                }
                catch
                {
                    return null;
                }

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    if (Session["name"] != null)
            //    {
            //        //login.Visible = false;
            //        usercenter.Visible = true;
            //        userlogin.Visible = true;
            //        //AuditUser.Text = "欢迎：" + Session["name"].ToString();
            //        //LinkButton1.Text = "退出系统";
            //    }
            //    else
            //    {
            //        usercenter.Visible = false;
            //        Li2.Visible = false;
            //        Li3.Visible = false;
            //        Li4.Visible = false;
            //    }
            //    lblWeek.Text = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek); ;
            //    timer1.Interval = 1000;
            //    lblDateTime.Text = DateTime.Now.ToString();
            //}
            if(!IsPostBack)
            {
                if (uType != null && (uType.Equals("2") || uType.Equals("3")))
                {
                    HyperLink2.Visible = false;
                    HyperLink5.Visible = false;
                    HyperLink8.Visible = false;
                }
            }
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            //Response.Redirect("../../LogInPage.aspx");
            //Response.Redirect("../../File/上海市杨浦区台湾事务办公室综合信息管理系统后台操作文档.doc");
            Response.Redirect("../../Template/上海市杨浦区台湾事务办公室综合信息管理系统数据导入说明文档.doc");
        }

        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdatePassword.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../LogInPage.aspx");
        }
        //protected void lbLogOut_Click(object sender, EventArgs e)
        //{
        //    if (LinkButton1.Text == "登录")
        //    {
        //        Response.Redirect("../../Login.aspx");
        //    }
        //    else
        //    {
        //        Session.Clear();
        //        //Response.Redirect("./News.aspx");
        //    }
        //}
        //protected void Unnamed1_Tick(object sender, EventArgs e)
        //{
        //    lblDateTime.Text = DateTime.Now.ToString();
        //}

    }
}