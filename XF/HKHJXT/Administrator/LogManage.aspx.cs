using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lephone.Extra;
using DataModel.DataModel;

namespace XF.HKHJXT.Administrator
{
    public partial class LogManage : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (uid == null)
            {
                Response.Redirect("../../LogInPage.aspx");
            }
            if (!IsPostBack)
            {
                RefreshTable(0);
            }
        }
        private void RefreshTable(int NewIndex)
        {
            try
            {
                int size = this.gv_Result.PageSize;
                var ps = BusinessSupply.BusinessOper.LogInfoBusiness.GetLogInfoList(size);
                this.gv_Result.PageIndex = NewIndex;
                //this.gv_Member.DataSource = new PagedCollection<TPersonAssociation>(ps, NewIndex);
                this.gv_Result.DataSource = new PagedCollection<TLogInfo>(ps, NewIndex);

                this.gv_Result.DataBind();
                this.Label_ResultCount.Text = ps.GetResultCount().ToString();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);
            }
        }

        protected void gv_Result_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#99ceff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
                e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#FFFFCC'");
                e.Row.Attributes["style"] = "Cursor:hand";

                switch (e.Row.Cells[1].Text)
                {
                    case "1":
                        e.Row.Cells[1].Text = "系统管理员";
                        break;
                    case "2":
                        e.Row.Cells[1].Text = "基础操作员";
                        break;
                    case "3":
                        e.Row.Cells[1].Text = "管理员";
                        break;
                }
                switch (e.Row.Cells[3].Text)
                {
                    case "1":
                        e.Row.Cells[3].Text = "登录";
                        break;
                    case "2":
                        e.Row.Cells[3].Text = "新增";
                        break;
                    case "3":
                        e.Row.Cells[3].Text = "删除";
                        break;
                    case "4":
                        e.Row.Cells[3].Text = "修改";
                        break;
                }
            }
        }

        protected void gv_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RefreshTable(e.NewPageIndex);
        }

        protected void gv_Result_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "go")
                {
                    TextBox tb = (TextBox)gv_Result.BottomPagerRow.FindControl("inPageNum");

                    int num = Int32.Parse(tb.Text);
                    GridViewPageEventArgs ea = null;
                    if (num <= gv_Result.PageCount)
                    {

                        ea = new GridViewPageEventArgs(num - 1);

                    }
                    else
                    {
                        ea = new GridViewPageEventArgs(gv_Result.PageCount - 1);
                    }
                    tb.Text = (this.gv_Result.PageIndex + 1).ToString();
                    gv_Result_PageIndexChanging(null, ea);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);
            }
        }
    }
}