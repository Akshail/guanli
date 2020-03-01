using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessSupply.BusinessOper;
using DataModel.DataModel;
using System.Web.Services;
using System.Data;

namespace XF
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserId"] = null;
            Session["IP"] = null;
            Session["UserType"] = null;
            Session["UserName"] = null;
            Session["Account"] = null;
        }
        [WebMethod]
        public static string Login(string Account, string Password, string YZM)
        {
            if (HttpContext.Current.Session["VNum"] != null && YZM.Equals(HttpContext.Current.Session["VNum"].ToString().Trim(), StringComparison.OrdinalIgnoreCase))
            {
                DataTable dt_user = UserBusiness.Login(Account, Password);
                if (dt_user.Rows.Count == 1)
                {
                    string usertypeid = dt_user.Rows[0]["UserTypeId"].ToString();
                    string name = dt_user.Rows[0]["Name"].ToString();
                    string userid = dt_user.Rows[0]["Id"].ToString();
                    //获取登录ip
                    string ip = GetClientIP();
                    System.Web.HttpContext.Current.Session["IP"] = GetClientIP();
                    //记录登录时间
                    DateTime dt_log = DateTime.Now;

                    System.Web.HttpContext.Current.Session["UserId"] = userid;
                    System.Web.HttpContext.Current.Session["UserName"] = name;
                    System.Web.HttpContext.Current.Session["Account"] = Account;
                    HttpContext.Current.Session["UserType"] = usertypeid;
                    //根据不同用户类型返回不同值
                    if (usertypeid == "1")
                    {
                        //HttpContext.Current.Session["UserType"] = 1;
                        //系统管理员登录                   
                        string logresult = BusinessSupply.BusinessOper.LogInfoBusiness.AddLogInfo(Convert.ToInt32(userid), dt_log, Convert.ToInt32(usertypeid), name, 1, "", ip, Account);
                        if (logresult == "0")
                        {
                            return "6";
                        }
                        return "1";
                    }
                    if (usertypeid == "2")
                    {
                        //HttpContext.Current.Session["UserType"] = 2;
                        //基础操作员登录                    
                        string logresult = BusinessSupply.BusinessOper.LogInfoBusiness.AddLogInfo(Convert.ToInt32(userid), dt_log, Convert.ToInt32(usertypeid), name, 1, "", ip, Account);
                        if (logresult == "0")
                        {
                            return "6";
                        }
                        return "2";
                    }
                    if (usertypeid == "3")
                    {
                        //HttpContext.Current.Session["UserType"] = 3;
                        //管理员登录
                        string logresult = BusinessSupply.BusinessOper.LogInfoBusiness.AddLogInfo(Convert.ToInt32(userid), dt_log, Convert.ToInt32(usertypeid), name, 1, "", ip, Account);
                        if (logresult == "0")
                        {
                            return "6";
                        }
                        return "3";
                    }
                    //if (usertypeid == "4")
                    //{
                    //    //个人会员登录
                    //    string logresult = BusinessSupply.BusinessOper.LogInfoBusiness.AddLogInfo(Convert.ToInt32(userid), dt_log, Convert.ToInt32(usertypeid), name, 1, "", ip, Account);
                    //    if (logresult == "0")
                    //    {
                    //        return "6";
                    //    }
                    //    return "4";
                    //}
                    //if (usertypeid == "5")
                    //{
                    //    //团体会员登录
                    //    string logresult = BusinessSupply.BusinessOper.LogInfoBusiness.AddLogInfo(Convert.ToInt32(userid), dt_log, Convert.ToInt32(usertypeid), name, 1, "", ip, Account);
                    //    if (logresult == "0")
                    //    {
                    //        return "6";
                    //    }
                    //    return "5";
                    //}
                    else
                    {
                        return "6";
                    }
                }
                else
                {
                    return "6";
                }

            }
            else
            {
                return "7";//验证码错误
            }
        }

        protected void ImageButton1_Click(object sender, EventArgs e)
        {

        }

        #region 获取客户端IP地址

        public static string GetClientIP()
        {             //获得IP地址            

            //string hostname;

            //System.Net.IPHostEntry localhost;

            //hostname = System.Net.Dns.GetHostName();

            //localhost = System.Net.Dns.GetHostEntry(hostname);

            //string ip = localhost.AddressList[0].ToString();

            //int i = 1;

            //while (ip.Contains(":"))
            //{

            //    if (i == localhost.AddressList.Length)
            //    {

            //        ip = "";

            //        break;

            //    }

            //    ip = localhost.AddressList[i].ToString();

            //    if (ip.Contains(":"))
            //    {

            //        i++;

            //    }

            //    else

            //        break;

            //}

            //return ip;

            string l_ret = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                l_ret = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);

            if (string.IsNullOrEmpty(l_ret))
                l_ret = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return l_ret;

        }

        #endregion

    }
}