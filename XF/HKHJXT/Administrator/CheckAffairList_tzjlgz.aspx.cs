using BusinessSupply.BusinessOper;
using DataModel.DataModel;
using Lephone.Data;
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
    public partial class CheckAffairList_tzjlgz : PageBase
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
                Bind_basic();
                RefreshView(0);
            }

        }

        private void Bind_basic()
        {
            DataTable dt_communicationType = CommunicationType.Find(CK.K["IsDelete"] == false).ToDataTable();
            ddl_communicationType.DataSource = dt_communicationType;
            ddl_communicationType.DataValueField = "CommunicationTypeId";
            ddl_communicationType.DataTextField = "CommunicationTypeText";
            ddl_communicationType.DataBind();
            ddl_communicationType.Items.Insert(0, new ListItem("请选择", "0"));
            ddl_communicationType.SelectedValue = "0";
        }


        private void RefreshView(int NewIndex)
        {
            try
            {
                int size = this.gv_Result.PageSize;
                int communicationTypeId = Convert.ToInt32(ddl_communicationType.SelectedValue);
                string groupArea = tb_GroupArea.Text;
                var ps = GroupCommunicationWorkBusiness.GetCommunications(size, communicationTypeId, groupArea);
                this.gv_Result.PageIndex = NewIndex;
                this.gv_Result.DataSource = new PagedCollection<GroupCommunicationWork>(ps, NewIndex);
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
            Response.Redirect("./CheckAffairList_tzjlgz_Add.aspx");
        }

        protected void gv_Result_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = CommunicationType.FindOne(CK.K["CommunicationTypeId"] == e.Row.Cells[1].Text).CommunicationTypeText;
            }

        }

        protected void gv_Result_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Del"))
            {
                string id = e.CommandArgument.ToString();
                string result = GroupCommunicationWorkBusiness.DeleteGroupCommunication(Int32.Parse(id));
                if (!result.Equals("ok"))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('删除失败，请重新再试！')", true);
                    return;
                }
                LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 3, "团组交流工作信息", log_ip, log_account);//删除添加日志 操作类型->3
                RefreshView(0);
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            RefreshView(0);
        }

        protected void btn_Download_Click(object sender, EventArgs e)
        {

        }

        protected void btn_importEvent_Click(object sender, EventArgs e)
        {

        }


    }
}