using DataModel.DataModel;
using Lephone.Data;
using Lephone.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XF.HKHJXT.Administrator
{
    public partial class CollegeManage : System.Web.UI.Page
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
                RefreshView(0);
            }

        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            RefreshView(0);
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "AddKeyPerson", "btn_addUser_click(0)", true);
        }

        private void RefreshView(int NewIndex)
        {
            try
            {

                int size = this.gv_Vip.PageSize;
                string schoolname = null;
                IPagedSelector<School> ps;
                if (this.tb_VipName.Text.Length != 0)
                {
                    schoolname = this.tb_VipName.Text.Trim();
                    ps = BusinessSupply.BusinessOper.SchoolBusiness.GetSchoolList(schoolname, size);
                }
                else
                {
                    ps = BusinessSupply.BusinessOper.SchoolBusiness.GetSchoolList(size);

                }

                this.gv_Vip.PageIndex = NewIndex;
                this.gv_Vip.DataSource = new PagedCollection<School>(ps, NewIndex);
                this.gv_Vip.DataBind();
                this.Label_VipCount.Text = ps.GetResultCount().ToString();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);
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
                int SchoolId = Convert.ToInt32(e.CommandArgument.ToString());
                string r = BusinessSupply.BusinessOper.SchoolBusiness.DeleteUserById(SchoolId);
                if (r == "1")
                {
                    RefreshView(0);
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('删除成功！')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('删除失败！')", true);
                }
            }
            if (e.CommandName == "upd")
            {
                int SchoolId = Convert.ToInt32(e.CommandArgument.ToString());
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "AddKeyPerson", "btn_addUser_click(" + SchoolId + ")", true);
            }
        }
    }
}