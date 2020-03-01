using BusinessSupply.BusinessOper;
using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XF.HKHJXT.Administrator
{
    public partial class Statistic_cztb_age : PageBase
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

        private string uType
        {
            get
            {
                try
                {
                    return Session["UserType"].ToString().Trim();
                }
                catch
                {
                    return null;
                }

            }
        }
        protected string xAxis
        {
            get
            {
                if (ViewState["xAxis"] == null)
                {
                    return "";
                }
                else
                {
                    return (string)ViewState["xAxis"];
                }
            }
            set
            {
                ViewState["xAxis"] = value;
            }
        }

        protected string Data_Sum
        {
            get
            {
                if (ViewState["Data_Sum"] == null)
                {
                    return "";
                }
                else
                {
                    return (string)ViewState["Data_Sum"];
                }
            }
            set
            {
                ViewState["Data_Sum"] = value;
            }
        }

        protected string Data_Percent
        {
            get
            {
                if (ViewState["Data_Percent"] == null)
                {
                    return "";
                }
                else
                {
                    return (string)ViewState["Data_Percent"];
                }
            }
            set
            {
                ViewState["Data_Percent"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (uid == null)
            {
                Response.Redirect("../../LogInPage.aspx");
            }
            xAxis = "";
            Data_Sum = "";
            Data_Percent = "";
            if (!IsPostBack)
            {
                Bind_basic();
                if (uType != null && uType.Equals("2"))
                {
                    menu_zxgzxx.Visible = false;
                }
                Bind_statistic();
            }
        }
        private void Bind_basic()
        {
        }
        private void Bind_statistic()
        {
            try
            {
                string sex = ddl_sex.SelectedValue;
                string comereason = ddl_ComeReason.SelectedValue;
                string label = "";
                if (!sex.Equals("99"))
                {
                    label += "【" + ddl_sex.SelectedItem.Text + "性】";
                }
                if (!comereason.Equals("99"))
                {
                    label += "【" + ddl_ComeReason.SelectedItem.Text + "】";
                }
                if (label == "")
                {
                    label = "【全体】";
                }
                label += "教职工年龄统计";
                label_head.Text = label;
                DataTable dt_people = PeopleBusiness.GetAllData(sex, comereason, "", "");
                int thisYear = DateTime.Now.Year;
                if (dt_people != null)
                {
                    DataTable dt_grid = new DataTable();
                    dt_grid.Columns.Add("ItemName");//item名称
                    dt_grid.Columns.Add("Count");//item 人数
                    dt_grid.Columns.Add("Percent");//百分比
                    int itemCount = 8;//总项数 20岁及以下、21-25岁、26-30岁、31-35岁、36-40岁、40-45岁、46-50岁、50岁以上
                    int totals = dt_people.Rows.Count;//总人数
                    StringBuilder sb_pic_x = new StringBuilder();
                    //sb_pic_x.Append("[");
                    StringBuilder sb_pic_sum = new StringBuilder();
                    //sb_pic_sum.Append("{name: '总人数',data: [");
                    StringBuilder sb_pic_percent = new StringBuilder();
                    //sb_pic_percent.Append("{name: '所占比例',data: [");
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();//grid 行数据
                        dr["ItemName"] = "20岁及以下";
                        int count = dt_people.Select("Birthday >= '" + new DateTime(thisYear - 20, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);

                        if (sb_pic_x.Length == 0)//柱状图 横坐标
                        {
                            sb_pic_x.Append("'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append(count);
                            sb_pic_percent.Append(percent);
                        }
                        else
                        {
                            sb_pic_x.Append(",'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append("," + count);
                            sb_pic_percent.Append("," + percent);
                        }
                    }
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();
                        dr["ItemName"] = "21-25岁";
                        int count = dt_people.Select("Birthday < '" + new DateTime(thisYear - 20, 1, 1).ToString() + "' and Birthday >= '" + new DateTime(thisYear - 25, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);

                        if (sb_pic_x.Length == 0)//柱状图 横坐标
                        {
                            sb_pic_x.Append("'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append(count);
                            sb_pic_percent.Append(percent);
                        }
                        else
                        {
                            sb_pic_x.Append(",'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append("," + count);
                            sb_pic_percent.Append("," + percent);
                        }
                    }
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();
                        dr["ItemName"] = "26-30岁";
                        int count = dt_people.Select("Birthday < '" + new DateTime(thisYear - 25, 1, 1).ToString() + "' and Birthday >= '" + new DateTime(thisYear - 30, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);

                        if (sb_pic_x.Length == 0)//柱状图 横坐标
                        {
                            sb_pic_x.Append("'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append(count);
                            sb_pic_percent.Append(percent);
                        }
                        else
                        {
                            sb_pic_x.Append(",'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append("," + count);
                            sb_pic_percent.Append("," + percent);
                        }
                    }
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();
                        dr["ItemName"] = "31-35岁";
                        int count = dt_people.Select("Birthday < '" + new DateTime(thisYear - 30, 1, 1).ToString() + "' and Birthday >= '" + new DateTime(thisYear - 35, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);

                        if (sb_pic_x.Length == 0)//柱状图 横坐标
                        {
                            sb_pic_x.Append("'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append(count);
                            sb_pic_percent.Append(percent);
                        }
                        else
                        {
                            sb_pic_x.Append(",'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append("," + count);
                            sb_pic_percent.Append("," + percent);
                        }
                    }
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();
                        dr["ItemName"] = "36-40岁";
                        int count = dt_people.Select("Birthday < '" + new DateTime(thisYear - 35, 1, 1).ToString() + "' and Birthday >= '" + new DateTime(thisYear - 40, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);

                        if (sb_pic_x.Length == 0)//柱状图 横坐标
                        {
                            sb_pic_x.Append("'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append(count);
                            sb_pic_percent.Append(percent);
                        }
                        else
                        {
                            sb_pic_x.Append(",'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append("," + count);
                            sb_pic_percent.Append("," + percent);
                        }
                    }
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();
                        dr["ItemName"] = "41-45岁";
                        int count = dt_people.Select("Birthday < '" + new DateTime(thisYear - 40, 1, 1).ToString() + "' and Birthday >= '" + new DateTime(thisYear - 45, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);

                        if (sb_pic_x.Length == 0)//柱状图 横坐标
                        {
                            sb_pic_x.Append("'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append(count);
                            sb_pic_percent.Append(percent);
                        }
                        else
                        {
                            sb_pic_x.Append(",'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append("," + count);
                            sb_pic_percent.Append("," + percent);
                        }
                    }
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();
                        dr["ItemName"] = "46-50岁";
                        int count = dt_people.Select("Birthday < '" + new DateTime(thisYear - 45, 1, 1).ToString() + "' and Birthday >= '" + new DateTime(thisYear - 50, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);

                        if (sb_pic_x.Length == 0)//柱状图 横坐标
                        {
                            sb_pic_x.Append("'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append(count);
                            sb_pic_percent.Append(percent);
                        }
                        else
                        {
                            sb_pic_x.Append(",'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append("," + count);
                            sb_pic_percent.Append("," + percent);
                        }
                    }
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();
                        dr["ItemName"] = "50岁以上";
                        int count = dt_people.Select("Birthday < '" + new DateTime(thisYear - 50, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);

                        if (sb_pic_x.Length == 0)//柱状图 横坐标
                        {
                            sb_pic_x.Append("'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append(count);
                            sb_pic_percent.Append(percent);
                        }
                        else
                        {
                            sb_pic_x.Append(",'" + dr["ItemName"].ToString() + "'");
                            sb_pic_sum.Append("," + count);
                            sb_pic_percent.Append("," + percent);
                        }
                    }

                    DataRow dr_total = dt_grid.NewRow();//添加一条 总计
                    dr_total["ItemName"] = "总计";
                    dr_total["Count"] = totals;
                    dr_total["Percent"] = totals == 0 ? "0.00%" : "100%";
                    dt_grid.Rows.Add(dr_total);

                    gv_result.DataSource = dt_grid;
                    gv_result.DataBind();
                    Label_Result.Text = itemCount.ToString();

                    xAxis = sb_pic_x.ToString();
                    Data_Sum = "{name: '总人数',data: [" + sb_pic_sum + "]}";
                    Data_Percent = "{name: '所占比例',data: [" + sb_pic_sum + "]}";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('数据加载错误！')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('数据加载错误！')", true);
            }
        }

        public string GetxAxis()
        {
            return xAxis;
        }

        public string GetData()
        {
            //return Data_Sum + "," + Data_Percent;
            return Data_Sum;
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            Bind_statistic();
        }

        protected void btn_Output_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream ms = (MemoryStream)RenderDataTableToExcel(new string[] { "年龄", "总人数（名）", "所占比例" }, GetDataTable());
                HttpContext curContext = HttpContext.Current;
                // 设置编码和附件格式
                curContext.Response.ContentType = "application/vnd.ms-excel";
                curContext.Response.ContentEncoding = Encoding.UTF8;
                curContext.Response.Charset = "";
                curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("教职工年龄统计.xls", Encoding.UTF8));
                curContext.Response.Clear();
                curContext.Response.BinaryWrite(ms.ToArray());
                curContext.ApplicationInstance.CompleteRequest();
                //curContext.Response.End();
            }
            catch (Exception ex)
            {

            }
        }

        private DataTable GetDataTable()
        {
            try
            {
                string sex = ddl_sex.SelectedValue;
                string comereason = ddl_ComeReason.SelectedValue;
                DataTable dt_people = PeopleBusiness.GetAllData(sex, comereason, "", "");
                int thisYear = DateTime.Now.Year;
                if (dt_people != null)
                {
                    DataTable dt_grid = new DataTable();
                    dt_grid.Columns.Add("ItemName");//item名称
                    dt_grid.Columns.Add("Count");//item 人数
                    dt_grid.Columns.Add("Percent");//百分比
                    int itemCount = 8;//总项数 20岁及以下、21-25岁、26-30岁、31-35岁、36-40岁、40-45岁、46-50岁、50岁以上
                    int totals = dt_people.Rows.Count;//总人数
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();//grid 行数据
                        dr["ItemName"] = "20岁及以下";
                        int count = dt_people.Select("Birthday >= '" + new DateTime(thisYear - 20, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);
                    }
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();
                        dr["ItemName"] = "21-25岁";
                        int count = dt_people.Select("Birthday < '" + new DateTime(thisYear - 20, 1, 1).ToString() + "' and Birthday >= '" + new DateTime(thisYear - 25, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);
                    }
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();
                        dr["ItemName"] = "26-30岁";
                        int count = dt_people.Select("Birthday < '" + new DateTime(thisYear - 25, 1, 1).ToString() + "' and Birthday >= '" + new DateTime(thisYear - 30, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);
                    }
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();
                        dr["ItemName"] = "31-35岁";
                        int count = dt_people.Select("Birthday < '" + new DateTime(thisYear - 30, 1, 1).ToString() + "' and Birthday >= '" + new DateTime(thisYear - 35, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);
                    }
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();
                        dr["ItemName"] = "36-40岁";
                        int count = dt_people.Select("Birthday < '" + new DateTime(thisYear - 35, 1, 1).ToString() + "' and Birthday >= '" + new DateTime(thisYear - 40, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);
                    }
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();
                        dr["ItemName"] = "41-45岁";
                        int count = dt_people.Select("Birthday < '" + new DateTime(thisYear - 40, 1, 1).ToString() + "' and Birthday >= '" + new DateTime(thisYear - 45, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);
                    }
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();
                        dr["ItemName"] = "46-50岁";
                        int count = dt_people.Select("Birthday < '" + new DateTime(thisYear - 45, 1, 1).ToString() + "' and Birthday >= '" + new DateTime(thisYear - 50, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);
                    }
                    if (true)
                    {
                        DataRow dr = dt_grid.NewRow();
                        dr["ItemName"] = "50岁以上";
                        int count = dt_people.Select("Birthday < '" + new DateTime(thisYear - 50, 1, 1).ToString() + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(1.0 * count / totals, 4);
                        dr["Count"] = count;
                        dr["Percent"] = percent * 100 + "%";
                        dt_grid.Rows.Add(dr);
                    }

                    DataRow dr_total = dt_grid.NewRow();//添加一条 总计
                    dr_total["ItemName"] = "总计";
                    dr_total["Count"] = totals;
                    dr_total["Percent"] = totals == 0 ? "0.00%" : "100%";
                    dt_grid.Rows.Add(dr_total);

                    return dt_grid;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('数据加载错误！')", true);
                    return new DataTable();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('数据加载错误！')", true);
                return new DataTable();
            }
        }
    }
}