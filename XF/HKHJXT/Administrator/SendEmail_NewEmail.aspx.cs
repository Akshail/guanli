using BusinessSupply.BusinessOper;
using DataModel.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XF.HKHJXT.Administrator
{
    public partial class SendEmail_NewEmail : System.Web.UI.Page
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

        private string receiverid
        {
            get
            {
                try
                {
                    return Session["ReceiverId"].ToString().Trim();
                }
                catch
                {
                    return null;
                }

            }
        }
        public string receiver
        {
            get
            {
                if (ViewState["receiver"] == null)
                {
                    return "";
                }
                else
                {
                    return (string)(ViewState["receiver"]);
                }
            }
            set
            {
                ViewState["receiver"] = value;
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
                    string text = null;

                    string[] temp = receiverid.Split(',');
                    for (int i = 0; i < temp.Length; i++)
                    {

                        int personmemberid = Convert.ToInt32(temp[i]);
                        BasicInfo personinfo = BasicInfoBusiness.GetBasicInfoById(personmemberid);
                        if (temp[i] != "" && personinfo != null)
                        {
                            text += personinfo.Email + ",";
                            receiver += personinfo.Email + ",";
                        }
                    }
                    receiver = receiver.Substring(0, receiver.Length - 1);
                    this.Label_MessageTo.Text = text.Substring(0, text.Length - 1);
                }
                catch
                {

                }
            }
        }

        protected void btn_Send_Click(object sender, EventArgs e)
        {
            try
            {
                EmailAccount email = EmailAccountBusiness.GetEmailAccount(Convert.ToInt32(uid));
                if (email == null)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('请先保存发件人的邮箱账号！')", true);
                    return;
                }
                SmtpClient client = new SmtpClient();
                client.Host = email.EmailServer;//"smtp.163.com";
                client.UseDefaultCredentials = false;
                //发件人邮箱账号、密码
                //client.Credentials = new System.Net.NetworkCredential("zhangpeicheng32@163.com", "zpc0622wangyi");
                client.Credentials = new System.Net.NetworkCredential(email.Account, email.Password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                //发件人地址、收件人地址

                MailMessage message = new MailMessage(email.Account, this.Label_MessageTo.Text.Trim());

                //string To = this.Label_MessageTo.Text.Trim();

                //message.To.Add(To);
                message.BodyEncoding = System.Text.Encoding.UTF8;   //设置编码
                message.IsBodyHtml = true;  //设置正文为HTML格式
                message.Subject = tb_Subject.Text.Trim();
                message.Body = tb_Body.Text.Trim();
                client.Send(message);

                DateTime createtime = DateTime.Now;
                string title = tb_Subject.Text.Trim();
                string content = tb_Body.Text.Trim();
                //新建邮件数据库记录
                TEmail send_r = BusinessSupply.BusinessOper.EmailBusiness.AddEmail(0, Convert.ToInt32(uid), createtime, title, content);
                if (send_r != null)
                {
                    //写入邮件人收件人数据库记录
                    //收件人personmemberid
                    string[] temp_id = receiverid.Split(',');
                    //收件人email地址
                    string[] temp_emailadress = receiver.Split(',');
                    int emailid = send_r.Id;
                    for (int j = 0; j < temp_id.Length; j++)
                    {
                        if (temp_emailadress[j] != "")
                        {
                            string a = BusinessSupply.BusinessOper.Email_PersonBusiness.AddEmail_Person(emailid, Convert.ToInt32(temp_id[j]), temp_emailadress[j]);
                            if (a != "1")
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('发送失败！')", true);
                                return;
                            }
                        }

                    }

                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('发送成功！')", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('发送失败！')", true);
            }
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SendEmail.aspx");

        }
    }
}