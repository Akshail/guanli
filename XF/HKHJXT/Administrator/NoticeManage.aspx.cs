using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataModel.DataModel;
using Lephone.Data;
using BusinessSupply.BusinessOper;
using Lephone.Extra;


namespace XF.HKHJXT.Administrator
{
    public partial class NoticeManage : System.Web.UI.Page
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
            if (uid == null)
            {
                Response.Redirect("../../LogInPage.aspx");
            }
            if (!IsPostBack)
            {
                if (uType != null && uType.Equals("2"))
                {
                    gv_Notice.Columns[4].Visible = false;
                    gv_Notice.Columns[3].Visible = false;
                    btn_Add.Visible = false;
                }
                RefreshTable(0);
            }

        }

        protected void Unnamed1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString();
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            RefreshTable(0);
        }

        //protected void btn_OutPut_Click(object sender, EventArgs e)
        //{

        //}

        private void RefreshTable(int NewIndex)
        {
            try
            {
                int size = this.gv_Notice.PageSize;
                string start = "";
                string end = "";
                if (this.input_StartDate.Value != "" && this.input_EndDate.Value != "")
                {
                    start = this.input_StartDate.Value;
                    end = this.input_EndDate.Value;
                }

                var ps = BusinessSupply.BusinessOper.NoticeBusiness.GetNoticePs(start, end, size);


                this.gv_Notice.PageIndex = NewIndex;
                this.gv_Notice.DataSource = new PagedCollection<TNotice>(ps, NewIndex);
                this.gv_Notice.DataBind();
                this.ResultCount.Text = ps.GetResultCount().ToString();
            }
            catch
            {
                this.gv_Notice.DataSource = new DataTable();
                this.gv_Notice.DataBind();
            }
        }

        protected void gv_Notice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RefreshTable(e.NewPageIndex);
        }

        protected void gv_Notice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#99ceff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
                e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#FFFFCC'");
                e.Row.Attributes["style"] = "Cursor:hand";

                //e.Row.Cells[1].Text = "本系统测试通知";
                string temp = e.Row.Cells[2].Text;
                DateTime dt_temp = Convert.ToDateTime(temp);

                //e.Row.Cells[2].Text = BusinessSupply.Helper.ChangeTimeDisplay(temp);
                e.Row.Cells[2].Text = dt_temp.ToString("yyyy年MM月dd日");
            }
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("NoticeManage_Add.aspx");
        }

        protected void gv_Notice_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int nid = Convert.ToInt32(this.gv_Notice.DataKeys[e.RowIndex]["Id"].ToString());
                int duid = Convert.ToInt32(uid);
                DateTime deltime = DateTime.Now;
                string r = BusinessSupply.BusinessOper.NoticeBusiness.DeleteNoticeById(nid, duid, deltime);
                if (r == "1")
                {
                    //记录日志
                    TUserInfo userinfo = BusinessSupply.BusinessOper.UserBusiness.GetUserById(Convert.ToInt32(uid));
                    //string operateobject = this.Page.Title;
                    string logresult = BusinessSupply.BusinessOper.LogInfoBusiness.AddLogInfo(Convert.ToInt32(uid), deltime, 1, userinfo.Name, 3, "通知公告", ip, userinfo.Account);
                    if (logresult == "0")
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel2, this.GetType(), "alter", "alert('删除失败！')", true);
                        //WebAlart("数据加载错误");
                    }
                    RefreshTable(0);

                    ScriptManager.RegisterStartupScript(UpdatePanel2, this.GetType(), "alter", "alert('删除成功！')", true);
                    return;
                    //WebAlart("删除成功");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel2, this.GetType(), "alter", "alert('删除失败！')", true);
                    WebAlart("删除失败");
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel2, this.GetType(), "alter", "alert('删除失败！')", true);
                //WebAlart("数据加载错误");
            }
        }
        public void WebAlart(string t)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "key", "<script>alert('" + t + "');</script>");
        }

        protected void gv_Notice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "go")
                {

                    TextBox tb = (TextBox)gv_Notice.BottomPagerRow.FindControl("inPageNum");

                    int num = Int32.Parse(tb.Text);
                    GridViewPageEventArgs ea = null;
                    if (num <= gv_Notice.PageCount)
                    {

                        ea = new GridViewPageEventArgs(num - 1);

                    }
                    else
                    {
                        ea = new GridViewPageEventArgs(gv_Notice.PageCount - 1);
                    }
                    tb.Text = (this.gv_Notice.PageIndex + 1).ToString();
                    gv_Notice_PageIndexChanging(null, ea);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);

            }
        }




        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("NoticeContent.aspx");
        //}
    }
}