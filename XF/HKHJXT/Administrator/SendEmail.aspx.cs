using BusinessSupply.BusinessOper;
using DataModel.DataModel;
using Lephone.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XF.HKHJXT.Administrator
{
    public partial class SendEmail : System.Web.UI.Page
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
                EmailAccount email = EmailAccountBusiness.GetEmailAccount(Convert.ToInt32(uid));
                if (email != null)
                {
                    ddl_EmailServer.SelectedValue = email.EmailServer;
                    tb_Account.Text = email.Account;
                    tb_Password.Value = email.Password;
                    tb_Password.Attributes.Add("value", "hgknight");
                }
            }
        }

        private void RefreshTable(int NewIndex)
        {
            try
            {
                int size = this.gv_Email.PageSize;
                string start = "";
                string end = "";
                if (this.input_StartDate.Value != "" && this.input_EndDate.Value != "")
                {
                    start = this.input_StartDate.Value;
                    end = this.input_EndDate.Value;
                }
                var ps = BusinessSupply.BusinessOper.EmailBusiness.GetEmailByAssociationId(0, start, end, size);
                this.gv_Email.PageIndex = NewIndex;

                this.gv_Email.DataSource = new PagedCollection<TEmail>(ps, NewIndex);
                this.gv_Email.DataBind();
                this.Label_ResultCount.Text = ps.GetResultCount().ToString();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('数据加载错误！')", true);
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            RefreshTable(0);

        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            EmailAccount email = EmailAccountBusiness.GetEmailAccount(Convert.ToInt32(uid));
            if(email == null)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('请先保存发件人的邮箱账号！')", true);
                return;
            }
            Response.Redirect("SendEmail_SelectPerson.aspx");
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
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('数据加载错误！')", true);

            }
        }

        protected void btn_SaveAccount_Click(object sender, EventArgs e)
        {
            string account = tb_Account.Text;
            string password = tb_Password.Value;
            string emailServer = ddl_EmailServer.SelectedValue;
            if (account.IsNullOrEmpty())
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('请输入邮箱账号！')", true);
            }
            if (password.IsNullOrEmpty())
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('请输入邮箱密码！')", true);
            }
            bool result = EmailAccountBusiness.SaveEmailAccount(Convert.ToInt32(uid), account, password, emailServer);
            if (result)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('保存成功！')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('保存失败！')", true);
            }
        }
    }
}