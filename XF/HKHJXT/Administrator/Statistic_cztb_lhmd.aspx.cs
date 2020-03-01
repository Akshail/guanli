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
    public partial class Statistic_cztb_lhmd : PageBase
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

        protected string PercentString
        {
            get
            {
                if (ViewState["PercentString"] == null)
                {
                    return "";
                }
                else
                {
                    return (string)ViewState["PercentString"];
                }
            }
            set
            {
                ViewState["PercentString"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (uid == null)
            {
                Response.Redirect("../../LogInPage.aspx");
            }
            PercentString = "";
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
                string birth_start = input_birth_start.Value;
                string birth_end = input_birth_end.Value;
                string label = "";
                if (!birth_start.IsNullOrEmpty())
                {
                    label += "【出生年份>=" + birth_start + "年】";
                }
                if (!birth_end.IsNullOrEmpty())
                {
                    label += "【出生年份<=" + birth_end + "年】";
                }
                if (!sex.Equals("99"))
                {
                    label += "【" + ddl_sex.SelectedItem.Text + "性】";
                }
                if (label == "")
                {
                    label = "【全体】";
                }
                label += "教职工担任职务统计";
                label_head.Text = label;
                DataTable dt = GetAllEducation();
                DataTable dt_student = PeopleBusiness.GetAllData(sex, "", birth_start, birth_end);
                if (dt != null)
                {
                    dt.Columns.Add("Count");// 总人数
                    dt.Columns.Add("Percent");//百分比
                    int itemCount = dt.Rows.Count;//总项数
                    int totals = dt_student.Rows.Count;//总人数
                    foreach (DataRow dr in dt.Rows)
                    {
                        int count = dt_student.Select("ComeReason = '" + dr["ItemValue"] + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(count * 1.0 / totals, 4);
                        dr["Percent"] = percent * 100 + "%";
                        dr["Count"] = count.ToString();
                        if (PercentString.Equals(""))
                        {
                            PercentString = "['" + dr["ItemName"] + "'," + percent + "]";
                        }
                        else
                        {
                            PercentString += ",['" + dr["ItemName"] + "'," + percent + "]";
                        }
                    }

                    DataRow dr_total = dt.NewRow();//添加一条 总计
                    dr_total["ItemName"] = "总计";
                    dr_total["Count"] = totals;
                    dr_total["Percent"] = totals == 0 ? "0.00%" : "100%";
                    dt.Rows.Add(dr_total);

                    gv_result.DataSource = dt;
                    gv_result.DataBind();
                    Label_Result.Text = itemCount.ToString();


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

        private DataTable GetAllEducation()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemName");
            dt.Columns.Add("ItemValue");
            if (true)
            {
                DataRow dr = dt.NewRow();
                dr["ItemName"] = "教师";
                dr["ItemValue"] = "1";
                dt.Rows.Add(dr);
            }
            if (true)
            {
                DataRow dr = dt.NewRow();
                dr["ItemName"] = "主任";
                dr["ItemValue"] = "2";
                dt.Rows.Add(dr);
            }
            if (true)
            {
                DataRow dr = dt.NewRow();
                dr["ItemName"] = "保安";
                dr["ItemValue"] = "3";
                dt.Rows.Add(dr);
            }
            if (true)
            {
                DataRow dr = dt.NewRow();
                dr["ItemName"] = "清洁工";
                dr["ItemValue"] = "4";
                dt.Rows.Add(dr);
            }
            if (true)
            {
                DataRow dr = dt.NewRow();
                dr["ItemName"] = "后勤";
                dr["ItemValue"] = "5";
                dt.Rows.Add(dr);
            }
            if (true)
            {
                DataRow dr = dt.NewRow();
                dr["ItemName"] = "书记";
                dr["ItemValue"] = "6";
                dt.Rows.Add(dr);
            }
            if (true)
            {
                DataRow dr = dt.NewRow();
                dr["ItemName"] = "其它";
                dr["ItemValue"] = "7";
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public string GetPercentData()
        {
            return PercentString;
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            Bind_statistic();
        }

        protected void btn_Output_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream ms = (MemoryStream)RenderDataTableToExcel(new string[] { "担任职务", "总人数（名）", "所占比例" }, GetDataTable());
                HttpContext curContext = HttpContext.Current;
                // 设置编码和附件格式
                curContext.Response.ContentType = "application/vnd.ms-excel";
                curContext.Response.ContentEncoding = Encoding.UTF8;
                curContext.Response.Charset = "";
                curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("教职工担任职务统计.xls", Encoding.UTF8));
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
                string birth_start = input_birth_start.Value;
                string birth_end = input_birth_end.Value;
                DataTable dt = GetAllEducation();
                DataTable dt_student = PeopleBusiness.GetAllData(sex, "", birth_start, birth_end);
                if (dt != null)
                {
                    dt.Columns.Add("Count");// 总人数
                    dt.Columns.Add("Percent");//百分比
                    int itemCount = dt.Rows.Count;//总项数
                    int totals = dt_student.Rows.Count;//总人数
                    foreach (DataRow dr in dt.Rows)
                    {
                        int count = dt_student.Select("ComeReason = '" + dr["ItemValue"] + "'").Length;
                        double percent = totals == 0 ? 0.0000 : Math.Round(count * 1.0 / totals, 4);
                        dr["Percent"] = percent * 100 + "%";
                        dr["Count"] = count.ToString();
                    }

                    DataRow dr_total = dt.NewRow();//添加一条 总计
                    dr_total["ItemName"] = "总计";
                    dr_total["Count"] = totals;
                    dr_total["Percent"] = totals == 0 ? "0.00%" : "100%";
                    dt.Rows.Add(dr_total);

                    return dt;
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