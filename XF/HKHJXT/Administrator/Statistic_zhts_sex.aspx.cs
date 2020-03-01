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
    public partial class Statistic_zhts_sex : PageBase
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
                if (uType != null && uType.Equals("2"))
                {
                    menu_zxgzxx.Visible = false;
                }
                Bind_statistic();
            }
        }

        private void Bind_basic()
        {

            DataTable dt_School = School.Find(CK.K["IsDelete"] == false).ToDataTable();
            ddl_School.DataSource = dt_School;
            ddl_School.DataValueField = "SchoolName";
            ddl_School.DataTextField = "SchoolName";
            ddl_School.DataBind();
            ddl_School.Items.Insert(0, new ListItem("不限", "0"));
            ddl_School.SelectedValue = "0";
        }

        private void Bind_statistic()
        {
            try
            {
                string school = ddl_School.SelectedValue;
                string startYear = input_start.Value;
                string endYear = input_end.Value;
                string label = "";
                if (!startYear.IsNullOrEmpty())
                {
                    label += "【出生年份>=" + startYear + "年】";
                }
                if (!endYear.IsNullOrEmpty())
                {
                    label += "【出生年份<=" + endYear + "年】";
                }
                if (!school.Equals("0"))
                {
                    label += "【" + ddl_School.SelectedItem.Text + "】";
                }
                if (label == "")
                {
                    label = "【全体】";
                }
                label += "在校本科生性别统计";
                label_head.Text = label;
                DataTable dt = StudentBusiness.GetStatisticBySex(school, startYear, endYear);
                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {
                        if (dt.Rows[0]["Column"].Equals("True"))
                        {
                            DataRow dr = dt.NewRow();
                            dr["Column"] = "False";
                            dr["Count"] = 0;
                            dt.Rows.InsertAt(dr, 0);
                        }
                        else
                        {
                            DataRow dr1 = dt.NewRow();
                            dr1["Column"] = "True";
                            dr1["Count"] = 0;
                            dt.Rows.Add(dr1);
                        }
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Column"] = "False";
                        dr["Count"] = 0;
                        dt.Rows.Add(dr);
                        DataRow dr1 = dt.NewRow();
                        dr1["Column"] = "True";
                        dr1["Count"] = 0;
                        dt.Rows.Add(dr1);
                    }
                    dt.Columns["Column"].ColumnName = "Sex";
                    dt.Columns.Add("ItemName");//item名称
                    dt.Columns.Add("Percent");//百分比
                    int itemCount = dt.Rows.Count;//总项数
                    int totals = 0;//总人数
                    foreach (DataRow dr in dt.Rows)
                    {
                        totals += Convert.ToInt32(dr["Count"].ToString());
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["ItemName"] = dr["Sex"].ToString().Equals("True") ? "女" : "男";
                        double percent = totals == 0 ? 0.0000 : Math.Round(Convert.ToDouble(dr["Count"].ToString()) / totals, 4);
                        dr["Percent"] = percent * 100 + "%";
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
                MemoryStream ms = (MemoryStream)RenderDataTableToExcel(new string[] { "性别", "总人数（名）", "所占比例" }, GetDataTable());
                HttpContext curContext = HttpContext.Current;
                // 设置编码和附件格式
                curContext.Response.ContentType = "application/vnd.ms-excel";
                curContext.Response.ContentEncoding = Encoding.UTF8;
                curContext.Response.Charset = "";
                curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("在校本科生性别统计.xls", Encoding.UTF8));
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
                string school = ddl_School.SelectedValue;
                string startYear = input_start.Value;
                string endYear = input_end.Value;
                DataTable dt = StudentBusiness.GetStatisticBySex(school, startYear, endYear);
                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {
                        if (dt.Rows[0]["Column"].Equals("True"))
                        {
                            DataRow dr = dt.NewRow();
                            dr["Column"] = "False";
                            dr["Count"] = 0;
                            dt.Rows.InsertAt(dr, 0);
                        }
                        else
                        {
                            DataRow dr1 = dt.NewRow();
                            dr1["Column"] = "True";
                            dr1["Count"] = 0;
                            dt.Rows.Add(dr1);
                        }
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Column"] = "False";
                        dr["Count"] = 0;
                        dt.Rows.Add(dr);
                        DataRow dr1 = dt.NewRow();
                        dr1["Column"] = "True";
                        dr1["Count"] = 0;
                        dt.Rows.Add(dr1);
                    }
                    dt.Columns["Column"].ColumnName = "Sex";
                    dt.Columns.Add("ItemName");//item名称
                    dt.Columns.Add("Percent");//百分比
                    int itemCount = dt.Rows.Count;//总项数
                    int totals = 0;//总人数
                    foreach (DataRow dr in dt.Rows)
                    {
                        totals += Convert.ToInt32(dr["Count"].ToString());
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["ItemName"] = dr["Sex"].ToString().Equals("True") ? "女" : "男";
                        double percent = totals == 0 ? 0.0000 : Math.Round(Convert.ToDouble(dr["Count"].ToString()) / totals, 4);
                        dr["Percent"] = percent * 100 + "%";
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