using DataModel.DataModel;
using Lephone.Data;
using Lephone.Extra;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XF.HKHJXT.Administrator
{
    public partial class SendMessage : System.Web.UI.Page
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
                //gv_Email.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
            }
        }

        private void RefreshTable(int NewIndex)
        {
            try
            {
                IPagedSelector<TMessage> ps;
                //TUserInfo u = BusinessSupply.BusinessOper.UserBusiness.GetUserById(Convert.ToInt32(uid));
                //int associationid = Convert.ToInt32(u.AssociationId.ToString());
                int size = this.gv_Email.PageSize;
                string start = "";
                string end = "";
                if (this.input_StartDate.Value != "" && this.input_EndDate.Value != "")
                {
                    start = this.input_StartDate.Value;
                    end = this.input_EndDate.Value;
                }
                ps = BusinessSupply.BusinessOper.MessageBusiness.GetMessageByAssociationId(0, start, end, size);

                this.gv_Email.PageIndex = NewIndex;

                this.gv_Email.DataSource = new PagedCollection<TMessage>(ps, NewIndex);
                this.gv_Email.DataBind();
                this.Label_ResultCount.Text = ps.GetResultCount().ToString();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            RefreshTable(0);
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("SendMessage_SelectPerson.aspx");

        }

        protected void gv_Email_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RefreshTable(e.NewPageIndex);

        }

        protected void gv_Email_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#99ceff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
                e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#FFFFCC'");
                e.Row.Attributes["style"] = "Cursor:hand";

                string t = e.Row.Cells[2].Text.Trim();
                DateTime temp = Convert.ToDateTime(t);
                e.Row.Cells[2].Text = temp.ToString("yyyy年MM月dd日");
            }
        }

        protected void gv_Email_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "go")
                {

                    TextBox tb = (TextBox)gv_Email.BottomPagerRow.FindControl("inPageNum");
                    int num = Int32.Parse(tb.Text);
                    GridViewPageEventArgs ea = null;
                    if (num <= gv_Email.PageCount)
                    {
                        ea = new GridViewPageEventArgs(num - 1);
                    }
                    else
                    {
                        ea = new GridViewPageEventArgs(gv_Email.PageCount - 1);
                    }
                    tb.Text = (this.gv_Email.PageIndex + 1).ToString();
                    gv_Email_PageIndexChanging(null, ea);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);

            }
        }


    }
}