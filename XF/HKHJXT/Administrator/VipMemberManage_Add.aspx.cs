using BusinessSupply.BusinessOper;
using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XF.HKHJXT.Administrator
{
    public partial class VipMemberManage_Add : System.Web.UI.Page
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

        private int request_id
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request["UserId"]);
                }
                catch
                {
                    return 0;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (request_id != 0)//修改
                {
                    btn_AddUser.Text = "确认修改";
                    TUserInfo user = TUserInfo.FindById(request_id);
                    if (user != null)
                    {
                        tb_account.Text = user.Account;
                        tb_password.Text = user.Pwd;
                        tb_name.Text = user.Name;
                        ddl_UserType.SelectedValue = user.UserTypeId.ToString();
                        tb_IDCardNum.Text = user.IDCardNum;
                        tb_Phone.Text = user.Phone;
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('数据加载错误，请重新再试！', 1, -1);", true);
                        return;
                    }
                }
            }
        }

        protected void btn_AddUser_Click(object sender, EventArgs e)
        {
            try
            {
                string validateResult = ValidateInput();
                if (!validateResult.Equals("ok"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('" + validateResult + "不能为空!', 1, -1);", true);
                    return;
                }
                string account = tb_account.Text.Trim();
                string password = tb_password.Text.Trim();
                string name = tb_name.Text.Trim();
                int userTypeId = Convert.ToInt32(ddl_UserType.SelectedValue);
                string IDCardNum = tb_IDCardNum.Text;
                string Phone = tb_Phone.Text;
                string addresult = "0";
                if (UserBusiness.IsExist(account, request_id))//除了request_id之外是否存在
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('该登录名已存在！', 1, -1);", true);
                    return;
                }
                int createuserid = Convert.ToInt32(uid);
                if (request_id != 0)//修改
                {
                    addresult = UserBusiness.UpdateUser(request_id, account, password, name, userTypeId, IDCardNum, Phone, createuserid, DateTime.Now);
                    if (addresult == "0")
                    {
                        //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('添加失败！')", true);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('修改失败', 1, -1);", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('修改成功', 2, -1);close();", true);
                    }
                }
                else//新增
                {
                    addresult = UserBusiness.AddUser(account, password, name, userTypeId, IDCardNum, Phone, createuserid, DateTime.Now);
                    if (addresult == "0")
                    {
                        //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('添加失败！')", true);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('添加失败', 1, -1);", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('添加成功', 2, -1);close();", true);
                    }
                }


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg(''数据加载失败，请重新再试！', 1, -1);", true);
            }

        }

        private string ValidateInput()
        {
            if (tb_account.Text.Trim().Equals(""))
            {
                return "登录名";
            }
            else if (tb_password.Text.Trim().Equals(""))
            {
                return "密码";
            }
            return "ok";
        }
    }
}