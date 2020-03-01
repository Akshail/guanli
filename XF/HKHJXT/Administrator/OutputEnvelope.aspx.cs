using DataModel.DataModel;
using Lephone.Data;
using Lephone.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessSupply.BusinessOper;

namespace XF.HKHJXT.Administrator
{
    public partial class OutputEnvelope : System.Web.UI.Page
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
            //if (uid == null)
            //{
            //    Response.Redirect("../../LogInPage.aspx");
            //}
            if (!IsPostBack)
            {
                RefreshTable(0);
            }
        }

        private void RefreshTable(int NewIndex)
        {
            try
            {
                int size = this.gv_Member.PageSize;
                string name = tb_PersonName.Text.Trim();
                var ps = BasicInfoBusiness.GetBasicInfos(size, name, "", "");
                this.gv_Member.PageIndex = NewIndex;
                this.gv_Member.DataSource = new PagedCollection<BasicInfo>(ps, NewIndex);
                this.gv_Member.DataBind();
                this.Label_ResultCount.Text = ps.GetResultCount().ToString();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('数据加载错误！')", true);
            }
        }

        protected void gv_Member_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RefreshTable(e.NewPageIndex);
        }

        protected void gv_Member_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            RefreshTable(0);
        }

        protected void btn_OutPut_Click(object sender, EventArgs e)
        {
            try
            {
                string personname = this.tb_PersonName.Text.Trim();
                DataTable dt_pa = BasicInfoBusiness.GetBasicInfoDataTable(personname);

                string browser = Request.UserAgent.ToLower();
                //export.ExportClass("性别分类统计", "性别分类统计.xls", browser, statistical_xbfl);
                string tempPath = Server.MapPath("../Envelope1.xls");
                ExcelHelper1.ExportExcelForDtByNPOI_envelope(dt_pa, "信封贴.xls", tempPath, 1, "列表", browser);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);

            }

        }

        protected void gv_Member_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "go")
                {

                    TextBox tb = (TextBox)gv_Member.BottomPagerRow.FindControl("inPageNum");

                    int num = Int32.Parse(tb.Text);
                    GridViewPageEventArgs ea = null;
                    if (num <= gv_Member.PageCount)
                    {

                        ea = new GridViewPageEventArgs(num - 1);

                    }
                    else
                    {
                        ea = new GridViewPageEventArgs(gv_Member.PageCount - 1);
                    }
                    tb.Text = (this.gv_Member.PageIndex + 1).ToString();
                    gv_Member_PageIndexChanging(null, ea);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);

            }
        }
    }
}