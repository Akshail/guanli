using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataModel.DataModel;
using Lephone.Data;
using Lephone.Extra;

namespace XF.HKHJXT.Administrator
{
    public partial class VipMemberManage : System.Web.UI.Page
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

        public int VipId
        {
            get
            {
                if (ViewState["VipId"] == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(ViewState["VipId"]);
                }
            }
            set
            {
                ViewState["VipId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (uid == null)
            {
                Response.Redirect("../../LogInPage.aspx");
            }
            if (!IsPostBack)
            {
                RefreshView(0);
            }
        }
        private void RefreshView(int NewIndex)
        {
            try
            {

                int size = this.gv_Vip.PageSize;
                string vipname = null;
                IPagedSelector<TUserInfo> ps;
                if (this.tb_VipName.Text.Length != 0)
                {
                    vipname = this.tb_VipName.Text.Trim();
                    ps = BusinessSupply.BusinessOper.UserBusiness.GetUserList(vipname, size);
                }
                else
                {
                    ps = BusinessSupply.BusinessOper.UserBusiness.GetUserList(size);

                }

                this.gv_Vip.PageIndex = NewIndex;
                this.gv_Vip.DataSource = new PagedCollection<TUserInfo>(ps, NewIndex);
                this.gv_Vip.DataBind();
                this.Label_VipCount.Text = ps.GetResultCount().ToString();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);
            }
        }
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            RefreshView(0);
        }

        protected void btn_OutPut_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "AddKeyPerson", "btn_addUser_click(0)", true);
        }



        //protected void btn_Add_Vip_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string account = this.tb_Account.Text.Trim();
        //        string pwd = this.tb_Pwd.Text.Trim();
        //        string name = this.tb_UserName.Text.Trim();

        //        int createuserid = Convert.ToInt32(uid);
        //        DateTime createtime = DateTime.Now;

        //        if (BusinessSupply.BusinessOper.UserBusiness.HasPersonAccount(account))
        //        {
        //            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('登录名已存在！')", true);
        //            return;
        //        }

        //        string r = BusinessSupply.BusinessOper.UserBusiness.AddVip(account, pwd, name, createuserid, createtime);
        //        if (r == "1")
        //        {
        //            //记录日志
        //            TUserInfo userinfo = BusinessSupply.BusinessOper.UserBusiness.GetUserById(Convert.ToInt32(uid));
        //            //string operateobject = this.Page.Title;
        //            string logresult = BusinessSupply.BusinessOper.LogInfoBusiness.AddLogInfo(Convert.ToInt32(uid), createtime, 1, userinfo.Name, 1, "VIP信息", ip, userinfo.Account);
        //            if (logresult == "0")
        //            {
        //                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('日志写入错误！')", true);
        //            }
        //            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('添加成功！')", true);
        //            RefreshView(0);
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('添加失败！')", true);
        //        }

        //    }
        //    catch
        //    {
        //        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);

        //    }
        //}

        //protected void btn_Clear_Vip_Click(object sender, EventArgs e)
        //{
        //    this.tb_Account.Text = null;
        //    this.tb_VipName.Text = null;
        //    //this.tb_Pwd.Text = null;
        //}

        protected void gv_Vip_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#99ceff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
                e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#FFFFCC'");
                e.Row.Attributes["style"] = "Cursor:hand";

                switch (e.Row.Cells[2].Text.Trim())
                {
                    case "1":
                        e.Row.Cells[2].Text = "系统管理员";
                        break;
                    case "2":
                        e.Row.Cells[2].Text = "基础操作员";
                        break;
                    case "3":
                        e.Row.Cells[2].Text = "管理员";
                        break;
                }
            }
        }

        protected void gv_Vip_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RefreshView(e.NewPageIndex);
        }


        protected void gv_Vip_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                VipId = Convert.ToInt32(e.CommandArgument.ToString());
                int deleteuserid = Convert.ToInt32(uid);
                DateTime deletetime = DateTime.Now;
                string r = BusinessSupply.BusinessOper.UserBusiness.DeleteUserById(VipId, deleteuserid, deletetime);
                if (r == "1")
                {
                    //记录日志
                    TUserInfo userinfo = BusinessSupply.BusinessOper.UserBusiness.GetUserById(Convert.ToInt32(uid));
                    //string operateobject = this.Page.Title;
                    string logresult = BusinessSupply.BusinessOper.LogInfoBusiness.AddLogInfo(Convert.ToInt32(uid), deletetime, 1, userinfo.Name, 3, "用户信息", ip, userinfo.Account);
                    if (logresult == "0")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('日志写入错误！')", true);
                    }
                    RefreshView(0);
                    //ClearAchievementText();
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('删除成功！')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('删除失败！')", true);
                }
            }
            if (e.CommandName == "repwd")
            {
                VipId = Convert.ToInt32(e.CommandArgument.ToString());
                string rp = BusinessSupply.BusinessOper.UserBusiness.ResetPwdById(VipId);
                if (rp == "1")
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('重置成功！')", true);
                    RefreshView(0);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('重置失败！')", true);

                }
            }
            if (e.CommandName == "upd")
            {
                VipId = Convert.ToInt32(e.CommandArgument.ToString());
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "AddKeyPerson", "btn_addUser_click(" + VipId + ")", true);
            }
            if (e.CommandName == "go")
            {
                try
                {
                    TextBox tb = (TextBox)gv_Vip.BottomPagerRow.FindControl("inPageNum");

                    int num = Int32.Parse(tb.Text);
                    GridViewPageEventArgs ea = null;
                    if (num <= gv_Vip.PageCount)
                    {

                        ea = new GridViewPageEventArgs(num - 1);

                    }
                    else
                    {
                        ea = new GridViewPageEventArgs(gv_Vip.PageCount - 1);
                    }
                    tb.Text = (this.gv_Vip.PageIndex + 1).ToString();
                    gv_Vip_PageIndexChanging(null, ea);

                }
                catch
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);

                }
            }
        }

    }
}