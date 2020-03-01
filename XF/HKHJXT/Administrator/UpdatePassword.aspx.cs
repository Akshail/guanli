using DataModel.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XF.HKHJXT.Administrator
{
    public partial class UpdatePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                int id = int.Parse(Session["UserId"].ToString());
                TUserInfo dt = BusinessSupply.BusinessOper.UserBusiness.GetUserById(id);

                if (dt.Pwd == this.txtOldPwd.Text.Trim())
                {
                    string r_reset = BusinessSupply.BusinessOper.UserBusiness.UpdatePwdById(id, txtNewPwd.Text.Trim());
                    if (r_reset == "1")
                    {
                        this.WebAlart("修改成功,请重新登录", "../../LoginPage.aspx");
                    }
                    else
                    {
                        this.WebAlart("修改失败");
                    }
                }
                else
                {
                    this.WebAlart("旧密码填写错误");
                }
            }
        }
        public void WebAlart(string t)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "key", "<script>alert('" + t + "');</script>");
        }

        public void WebAlart(string t, string url)
        {
            Response.Write("<script>alert('" + t + "');document.location.replace('" + url + "');</script>");
        }
    }
}