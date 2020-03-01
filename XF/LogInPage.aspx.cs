using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lephone.Data;
using BusinessSupply.BusinessOper;
using Lephone.Extra;
using DataModel.DataModel;

namespace XF.HKHJXT
{
    public partial class LogInPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshTable(0);

            }
        }
        private void RefreshTable(int NewIndex)
        {
            int size = this.gv_Notice.PageSize;

            var ps = BusinessSupply.BusinessOper.NoticeBusiness.GetNoticePs("", "", size);

            try
            {
                this.gv_Notice.PageIndex = NewIndex;
                this.gv_Notice.DataSource = new PagedCollection<TNotice>(ps, NewIndex);
                this.gv_Notice.DataBind();
                //this.ResultCount.Text = ps.GetResultCount().ToString();
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


                string temp = e.Row.Cells[2].Text;
                //e.Row.Cells[2].Text = BusinessSupply.Helper.ChangeTimeDisplay(temp);
  
                DateTime dt_temp = Convert.ToDateTime(temp);
                e.Row.Cells[2].Text = dt_temp.ToString("yyyy年MM月dd日");

            }
        }
    }
}