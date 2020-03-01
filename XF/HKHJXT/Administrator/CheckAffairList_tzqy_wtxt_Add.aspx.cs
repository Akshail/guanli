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
    public partial class CheckAffairList_tzqy_wtxt_Add : PageBase
    {
        private int enterpriseId//只有新增才传此值
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString["enterpriseId"]);
                }
                catch
                {
                    return 0;
                }
            }
        }
        private int idCoordinate
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString["idCoordinate"]);
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
                if (idCoordinate != 0)//修改
                {
                    btn_Add.Text = "确认修改";
                    //回填
                    try
                    {
                        Coordinate ins = Coordinate.FindById(idCoordinate);
                        input_RegisterDate.Value = DateTime.Compare((DateTime)ins.Time, new DateTime(1900, 1, 1)) > 0 ? ((DateTime)ins.Time).ToString("yyyy/MM/dd") : "";
                        TextBox1.Text = ins.Problem;
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
                time = input_RegisterDate.Value.Equals("") ? time : Convert.ToDateTime(input_RegisterDate.Value);
            }
            catch (Exception ex)
            {
                text(ex.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('时间输入格式不规范，请重新输入！')", true);
                return;
            }
            string problem = TextBox1.Text;
            bool res = false;
            if (idCoordinate != 0)//修改
            {
                res = CoordinateBusiness.UpdateCoordinate(idCoordinate, time, problem);
                if (!res)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('修改失败，请重新再试！')", true);
                    return;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('修改成功', 2, -1);close();", true);
            }
            else//新增
            {
                res = CoordinateBusiness.AddCoordinate(enterpriseId, time, problem);
                if (!res)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('新增失败，请重新再试！')", true);
                    return;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('新增成功', 2, -1);close();", true);
            }

        }
    }
}