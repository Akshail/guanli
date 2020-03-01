using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessSupply.BusinessOper;
using System.Data;

namespace XF.HKHJXT.Administrator
{
    public partial class NoticeContent : System.Web.UI.Page
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
            lblWeek.Text = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek); ;
            timer1.Interval = 1000;
            lblDateTime.Text = DateTime.Now.ToString();
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
                    Response.Redirect("NoticeManage.aspx");
                }
            }
        }
        protected void Unnamed1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString();
        }

        //protected void btn_Output_Click(object sender, EventArgs e)
        //{

        //}

        protected void btn_Cancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("NoticeManage.aspx");
        }
    }
}