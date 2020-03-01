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
    public partial class CheckAffairList_zxgz_jlqk_Add : PageBase
    {
        private int situationId
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString["SituationId"]);
                }
                catch
                {
                    return 0;
                }
            }
        }

        private int communicationId//只有新增才传此值
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString["CommunicationId"]);
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
                if (situationId != 0)//修改
                {
                    btn_Add.Text = "确认修改";
                    //回填
                    try
                    {
                        SituationToSpecialWork st = SituationToSpecialWork.FindById(situationId);
                        tb_time.Value = DateTime.Compare((DateTime)st.Time, new DateTime(1900, 1, 1)) > 0 ? ((DateTime)st.Time).ToString("yyyy/MM/dd") : "";
                        tb_location.Text = st.Location;
                        tb_Situation.Text = st.SituationName;
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
            DateTime time = new DateTime(1900, 1, 1);
            try
            {
                time = tb_time.Value.Equals("") ? time : Convert.ToDateTime(tb_time.Value);
            }
            catch (Exception ex)
            {
                text(ex.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('时间输入格式不规范，请重新输入！')", true);
                return;
            }
            string location = tb_location.Text;
            string situationName = tb_Situation.Text;
            bool result = false;
            if (situationId != 0)//修改
            {
                result = SituationToSpecialWorkBusiness.UpdateSituation(situationId, time, location, situationName);
                if (!result)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('修改失败，请重新再试！')", true);
                    return;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('修改成功', 2, -1);close();", true);
            }
            else//新增
            {
                result = SituationToSpecialWorkBusiness.AddSituation(communicationId, time, location, situationName);
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