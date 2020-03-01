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
    public partial class CheckAffairList_zxgz : PageBase
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
                string sex = ddl_sex.SelectedValue;
                var ps = SpecialWorkBusiness.GetCommunications(size, name, sex);
                this.gv_Result.PageIndex = NewIndex;
                this.gv_Result.DataSource = new PagedCollection<SpecialWork>(ps, NewIndex);
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

        protected void btn_AddSimple_Click(object sender, EventArgs e)
        {
            int communicationId = SpecialWorkBusiness.AddCommunication();
            Response.Redirect("CheckAffairList_zxgz_Add.aspx?CommunicationId=" + communicationId + "&IsNew=True");
        }

        protected void gv_Result_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string command = e.CommandName;
            int id = Convert.ToInt32(e.CommandArgument);
            if (command.Equals("Chk"))//查看
            {
                Response.Redirect("CheckAffairList_zxgz_Add.aspx?CommunicationId=" + id);
            }
            else if (command.Equals("Upd"))//修改
            {
                Response.Redirect("CheckAffairList_zxgz_Add.aspx?CommunicationId=" + id);
            }
            else if (command.Equals("Del"))//删除
            {
                bool result = SpecialWorkBusiness.DeleteCommunication(id);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('删除失败，请重新再试！')", true);
                    return;
                }
                LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 3, "专项工作信息", log_ip, log_account);//删除添加日志 操作类型->3
                RefreshView(0);
            }
        }

        protected void gv_Result_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string temp = e.Row.Cells[2].Text;

                e.Row.Cells[2].Text = e.Row.Cells[2].Text.Equals("True") ? "女" : "男";
                e.Row.Cells[3].Text = e.Row.Cells[3].Text.Equals("&nbsp;")? "" : Convert.ToDateTime(e.Row.Cells[3].Text).ToString("yyyy/MM/dd");
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            RefreshView(0);
        }

    }
}