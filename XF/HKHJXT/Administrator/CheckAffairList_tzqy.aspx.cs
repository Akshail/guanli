using BusinessSupply.BusinessOper;
using DataModel.DataModel;
using Lephone.Extra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XF.HKHJXT.Administrator
{
    public partial class CheckAffair_tzqy : PageBase
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
            if (uid == null)
            {
                Response.Redirect("../../LogInPage.aspx");
            }
            if (!IsPostBack)
            {
                if (uType != null && uType.Equals("2"))
                {
                    HyperLink7.Visible = false;
                }
                RefreshView(0);
            }


        }

        private void RefreshView(int NewIndex)
        {
            try
            {
                int size = this.gv_Result.PageSize;
                string name = tb_Name.Text.Trim();
                string referee = tb_Referee.Text.Trim();
                string contact = tb_Contact.Text.Trim();
                var ps = EnterpriseBusiness.GetEnterprises(size, name, referee, contact);
                this.gv_Result.PageIndex = NewIndex;
                this.gv_Result.DataSource = new PagedCollection<Enterprise>(ps, NewIndex);
                this.gv_Result.DataBind();
                this.Label_Result.Text = ps.GetResultCount().ToString();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);
            }
        }

        protected void gv_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RefreshView(e.NewPageIndex);
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            RefreshView(0);
        }
        protected void btn_AddSimple_Click(object sender, EventArgs e)
        {
            int id = EnterpriseBusiness.AddEnterpriseForId();
            Response.Redirect("./CheckAffairList_tzqy_Add.aspx?Id=" + id + "&IsNew=1");
        }

        protected void gv_Result_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Del"))
            {
                string id = e.CommandArgument.ToString();
                bool result = EnterpriseBusiness.deleteEnterprise(Int32.Parse(id));
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('删除失败，请重新再试！')", true);
                    return;
                }
                LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 3, "台资企业信息", log_ip, log_account);//删除添加日志 操作类型->3
                RefreshView(0);
            }
        }
    }
}