using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessSupply.BusinessOper;
using System.Data;

namespace XF
{
    public partial class LogInNoticeContent : System.Web.UI.Page
    {
        string noticeid
        {
            get
            {
                try
                {
                    return this.Request.QueryString["Id"];
                }
                catch
                {
                    return null;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (noticeid != null)
                {
                    DataTable dt_notice = new DataTable();
                    dt_notice = BusinessSupply.BusinessOper.NoticeBusiness.GetNoticeById(Convert.ToInt32(noticeid));
                    if (dt_notice.Rows.Count != 0)
                    {
                        this.label_Title.Text = dt_notice.Rows[0]["Title"].ToString();
                        this.label_Content.Text = dt_notice.Rows[0]["NoticeContent"].ToString();
                        string temp = dt_notice.Rows[0]["ShowTime"].ToString();

                        DateTime dt_temp = Convert.ToDateTime(temp);
                        this.label_ShowTime.Text = dt_temp.ToString("yyyy年MM月dd日 HH:mm:ss");

                    }
                }
                else
                {
                    Response.Redirect("LogInPage.aspx");
                }
            }
        }


        //protected void btn_Output_Click(object sender, EventArgs e)
        //{

        //}

        protected void btn_Cancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogInPage.aspx");
        }
    }
}