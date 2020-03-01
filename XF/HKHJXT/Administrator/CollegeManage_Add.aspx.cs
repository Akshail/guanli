using BusinessSupply.BusinessOper;
using DataModel.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XF.HKHJXT.Administrator
{

    public partial class CollegeManage_Add : System.Web.UI.Page
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
                    School school = School.FindById(request_id);
                    if (school != null)
                    {
                        tb_account.Text = school.SchoolName;
                        tb_password.Text = school.SchoolCode;
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
                string schoolname = tb_account.Text.Trim();
                string schoolcode = tb_password.Text.Trim();
                string addresult = "0";
                if (request_id != 0)//修改
                {
                    addresult = SchoolBusiness.UpdateSchool(request_id, schoolname, schoolcode);
                    if (addresult == "0")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('修改失败', 1, -1);", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('修改成功', 2, -1);close();", true);
                    }
                }
                else//新增
                {
                    addresult = SchoolBusiness.AddSchool(schoolname, schoolcode);
                    if (addresult == "0")
                    {
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

    }
}