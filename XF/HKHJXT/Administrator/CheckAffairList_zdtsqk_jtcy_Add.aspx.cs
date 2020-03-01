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
    public partial class CheckAffairList_zdtsqk_jtcy_Add : PageBase
    {
        private int studentId//只有新增才传此值
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString["studentId"]);
                }
                catch
                {
                    return 0;
                }
            }
        }
        private int familyId
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString["idFamily"]);
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
                if (familyId != 0)//修改
                {
                    btn_Add.Text = "确认修改";
                    //回填
                    try
                    {
                        FamilyOfStudent fam = FamilyOfStudent.FindById(familyId);
                        tb_PeriodicalName.Text = fam.Name;
                        radio_1.SelectedValue = (bool)fam.Sex ? "2" : "1";
                        //input_RegisterDate.Value = fam.Birthday.ToString().Substring(0, 9);
                        input_RegisterDate.Value = DateTime.Compare((DateTime)fam.Birthday, new DateTime(1900, 1, 1)) > 0 ? ((DateTime)fam.Birthday).ToString("yyyy/MM/dd") : "";
                        tb_PublishCount.Text = fam.Relationship;
                        tb_EIC_Name.Text = fam.Company;
                        tb_EO_Address.Text = fam.Tip;
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
                string name = tb_PeriodicalName.Text;
                bool sex = !radio_1.SelectedValue.Equals("1");
                //DateTime birthday = Convert.ToDateTime(input_RegisterDate.Value);
                DateTime birthday = new DateTime(1900, 1, 2);
                try
                {
                    birthday = input_RegisterDate.Value.Equals("") ? birthday : Convert.ToDateTime(input_RegisterDate.Value);
                }
                catch (Exception ex)
                {
                    text(ex.ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('时间输入格式不规范，请重新输入！')", true);
                    return;
                }
                String relationship = tb_PublishCount.Text;
                String company = tb_EIC_Name.Text;
                String tip = tb_EO_Address.Text;
                bool result = false;
                if (familyId != 0) //修改
                {
                    result = FamilyOfStudentBusiness.UpdateFamilyOfStudent(familyId, name, sex, birthday, relationship, company, tip);
                    if (!result)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('修改失败，请重新再试！')", true);
                        return;
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('修改成功', 2, -1);close();", true);
                }
                else  //新增
                {
                    result = FamilyOfStudentBusiness.AddFamilyOfStudent(studentId, name, sex, birthday, relationship, company, tip);
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