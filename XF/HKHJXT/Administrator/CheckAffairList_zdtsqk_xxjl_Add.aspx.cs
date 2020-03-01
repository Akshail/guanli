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
    public partial class CheckAffairList_zdtsqk_xxjl_Add : PageBase
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
        private int idExperience
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString["idExperience"]);
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
                if (idExperience != 0)//修改
                {
                    btn_Add.Text = "确认修改";
                    //回填
                    try
                    {
                        Experience exp = Experience.FindById(idExperience);
                        input_RegisterDate.Value = DateTime.Compare((DateTime)exp.StartTime, new DateTime(1900, 1, 1)) > 0 ? ((DateTime)exp.StartTime).ToString("yyyy年MM月") : "";
                        Text1.Value = DateTime.Compare((DateTime)exp.EndTime, new DateTime(1900, 1, 1)) > 0 ? ((DateTime)exp.EndTime).ToString("yyyy年MM月") : "";
                        tb_PublishPeriod.Text = exp.School;
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
            DateTime timeStart = new DateTime(1900, 1, 1);
            DateTime timeEnd = new DateTime(1900, 1, 2);
            try
            {
                timeStart = input_RegisterDate.Value.Equals("") ? timeStart : Convert.ToDateTime(input_RegisterDate.Value);
                timeEnd = Text1.Value.Equals("") ? timeEnd : Convert.ToDateTime(Text1.Value);
            }
            catch (Exception ex)
            {
                text(ex.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('时间输入格式不规范，请重新输入！')", true);
                return;
            }
            string school = tb_PublishPeriod.Text;
            bool result = false;
            if (idExperience != 0)//修改
            {
                result = ExperienceBusiness.UpdateExperience(idExperience, timeStart, timeEnd, school);
                if (!result)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('修改失败，请重新再试！')", true);
                    return;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('修改成功', 2, -1);close();", true);
            }
            else//新增
            {
                result = ExperienceBusiness.AddExperience(studentId, timeStart, timeEnd, school);
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