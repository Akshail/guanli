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
    public partial class BasicInfoManage_Add : PageBase
    {
        private int basicInfoId
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString["BasicInfoId"]);
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
                if (basicInfoId != 0)//修改
                {
                    btn_Add.Text = "确认修改";
                    //回填
                    try
                    {
                        BasicInfo basic = BasicInfo.FindById(basicInfoId);
                        tb_name.Text = basic.Name;
                        tb_phone.Text = basic.Phone;
                        tb_email.Text = basic.Email;
                    }
                    catch (Exception ex)
                    {
                        text(ex.ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
                    }
                }
            }
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            string name = tb_name.Text.Trim();
            string phone = tb_phone.Text.Trim();
            string email = tb_email.Text.Trim();
            string address = tb_address.Text.Trim();
            string zipcode = tb_zipcode.Text.Trim();
            bool result = false;
            if (basicInfoId != 0)//修改
            {
                result = BasicInfoBusiness.UpdateBasicInfo(basicInfoId, name, phone, email, address, zipcode);
                if (!result)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('修改失败，请重新再试！')", true);
                    return;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('修改成功', 2, -1);close();", true);
            }
            else//新增
            {
                result = BasicInfoBusiness.AddBasicInfo(name, phone, email, address, zipcode);
                if (!result)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('新增失败，请重新再试！')", true);
                    return;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('新增成功', 2, -1);close();", true);
            }
        }
    }
}