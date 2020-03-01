using DataModel.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XF.HKHJXT.Administrator
{
    public partial class SentMessageContent : System.Web.UI.Page
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
        string msgid
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
            if (uid == null)
            {
                Response.Redirect("../../LogInPage.aspx");
            }
            if (!IsPostBack)
            {
                try
                {
                    TMessage info = BusinessSupply.BusinessOper.MessageBusiness.GetMessageById(Convert.ToInt32(msgid));
                    this.Label_Body.Text = info.MessageContent; ;

                    DataTable To = BusinessSupply.BusinessOper.Message_PersonBusiness.GetMessage_PersonByMessageId(Convert.ToInt32(msgid));

                    string to = "";
                    foreach (DataRow dr in To.Rows)
                    {
                        if (dr["Cellphone"].ToString() != "")
                        {
                            to += dr["Cellphone"].ToString() + ",";
                        }
                    }
                    this.Label_MessageTo.Text = to.Substring(0, to.Length - 1);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);

                }
            }
        }
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SendMessage.aspx");
        }
    }
}