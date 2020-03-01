using DataModel.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GSMMODEM;
using System.Threading;
using BusinessSupply.BusinessOper;

namespace XF.HKHJXT.Administrator
{
    public partial class SendMessage_NewMessage : System.Web.UI.Page
    {
        private GsmModem TextGSM;
        private int BaudRate = 9600;
        private string ComPort = "COM4";//默认短信猫接口

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
                    string receiver_text = null;
                    string[] temp = receiverid.Split(',');
                    for (int i = 0; i < temp.Length; i++)
                    {
                        int id = Convert.ToInt32(temp[i]);
                        BasicInfo personinfo = BasicInfoBusiness.GetBasicInfoById(id);
                        if (personinfo.Phone != null && personinfo.Phone.ToString() != "")
                        {
                            receiver += personinfo.Phone + ",";
                            receiver_text += personinfo.Phone + ",";
                        }
                    }
                    receiver = receiver.Substring(0, receiver.Length - 1);
                    this.tb_To.Text = receiver_text.Substring(0, receiver_text.Length - 1);
                    //XF.ServiceReference3.MsgModemWebServerSoapClient com = new ServiceReference3.MsgModemWebServerSoapClient();
                    //this.TB_Content.Text = com.GetComNum();
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误')", true);

                }
            }
        }

        protected void btn_Send_Click(object sender, EventArgs e)
        {
            try
            {
                string MessageTo = this.tb_To.Text.Trim();
                //XF.ServiceReference3.MsgModemWebServerSoapClient com = new ServiceReference3.MsgModemWebServerSoapClient();
                //string MessageContent = com.GetComNum();
                string MessageContent = TB_Content.Text.Trim();
                DateTime createtime = DateTime.Now;

                //短信接口1
                XF.ServiceReference1.ShuSoapHeader header = new ServiceReference1.ShuSoapHeader();
                header.UserID = "D2B2613C-FFF1-4331-B541-79154B6D3C54";
                header.PassWord = "hairunMobile";
                //header.NMsg = "虹口区科协";
                XF.ServiceReference1.SendMsgSoapClient msg = new XF.ServiceReference1.SendMsgSoapClient();
                //msg.SendMsgChannel1(header, "18721687868,18721687868", content);
                msg.SendMsgChannel1(header, MessageTo, MessageContent);
                //string useraccount = "test";

                //短信接口2   
                //XF.ServiceReference2.MessageSendSoapClient MSG = new ServiceReference2.MessageSendSoapClient();
                //MSG.JWSend("content", "18721687868", "useraccount");

                //13501734232
                ////收件人电话号码
                //XF.ServiceReference3.MsgModemWebServerSoapClient send = new ServiceReference3.MsgModemWebServerSoapClient();
                ////string result = send.SendMessageEachMinute(MessageTo, MessageContent);
                //string result = send.SendMessage(MessageTo, MessageContent);

                //if (result != "1")
                //{

                //    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('短信发送失败')", true);
                //    return;
                //}

                //短信猫
                //初始化短信猫串口
                //iniChuanKou();
                //string[] temp1_cellphone = receiver.Split(',');
                //for (int t = 0; t < temp1_cellphone.Length; t++)
                //{
                //    //过滤无效号码
                //    if (temp1_cellphone[t].ToString() != "" && temp1_cellphone[t].ToString().Length == 11)
                //    {

                //        ////调用短信猫发送短信
                //        //if (!SendMessage(temp1_cellphone[t].ToString(), MessageContent))
                //        //{
                //        //    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('短信发送失败')", true);
                //        //    //return;
                //        //}
                //        //Thread.Sleep(60000);//每次发邮件后休息1分钟，以免发送短信过快
                //    }
                //}
                ////关闭短信猫串口
                //CloseChuanKou();

                //新建Message数据库记录
                TUserInfo u = BusinessSupply.BusinessOper.UserBusiness.GetUserById(Convert.ToInt32(uid));
                //int associationid = Convert.ToInt32(u.AssociationId.ToString());
                TMessage send_r = BusinessSupply.BusinessOper.MessageBusiness.AddMessage(0, Convert.ToInt32(uid), createtime, MessageContent);

                if (send_r != null)
                {
                    //收件人Id
                    string[] temp_id = receiverid.Split(',');
                    //收件人电话号码
                    string[] temp_cellphone = receiver.Split(',');
                    int emailid = send_r.Id;

                    //有效手机号码数目小于等于传入收件人Id数目
                    for (int j = 0; j < temp_cellphone.Length; j++)
                    {
                        int messageid = send_r.Id;
                        if (temp_cellphone[j] != "")
                        {
                            string a = BusinessSupply.BusinessOper.Message_PersonBusiness.AddMessage_Person(messageid, Convert.ToInt32(temp_id[j]), temp_cellphone[j]);
                            if (a != "1")
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('记录短信联系人失败')", true);
                                return;
                            }
                        }

                    }
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('发送成功！')", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('记录短信信息失败')", true);
                    return;
                }

            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('发送失败')", true);
            }
        }



        /// <summary>
        /// 根据配置文件初始化串口信息
        /// </summary>
        private void iniChuanKou()
        {
            TextGSM = new GsmModem();
            TextGSM.BaudRate = BaudRate;
            TextGSM.ComPort = ComPort;
            //最多尝试5次连接某一串口
            for (int i = 0; i != 5; i++)
            {
                try
                {

                    TextGSM.Open();
                    break;


                }
                catch (Exception e)
                {
                    //string errorStr = "打开短信猫串口失败，错误：" + e.Message.ToString().Replace("'", "_");
                    //string errorSql = "insert into tab_ErrorLog(cDetailError,doccurTime) values('" + errorStr + "','" + DateTime.Now.ToString() + "')";
                    ////ExeSql(errorSql);
                    ////refreshListView(DateTime.Now.ToString(), errorStr);
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('"+errorSql+"')", true);

                    continue;
                }
            }
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="PhoneNum">收件人号码</param>
        /// <param name="TextMessage">短信内容</param>
        /// <returns></returns>
        private bool SendMessage(string PhoneNum, string TextMessage)
        {
            if (!TextGSM.IsOpen)
            {
                try
                {
                    TextGSM.Open();
                }
                catch (Exception)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('连接失败！')", true);

                    return false;
                }
            }
            try
            {
                TextGSM.SendMsg(PhoneNum, TextMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        private void CloseChuanKou()
        {
            try
            {
                if (TextGSM.IsOpen)
                {
                    TextGSM.Close();
                    TextGSM = null;
                }
            }
            catch (Exception)
            { ;}
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SendMessage.aspx");

        }
    }
}