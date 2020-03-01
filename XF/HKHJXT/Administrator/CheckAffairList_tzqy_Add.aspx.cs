using Aspose.Words;
using BusinessSupply.BusinessOper;
using DataModel.DataModel;
using Lephone.Data;
using Lephone.Extra;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XF.HKHJXT.Administrator
{
    public partial class CheckAffairList_tzqy_Add : PageBase
    {
        private int id
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString["Id"]);
                }
                catch
                {
                    return 0;
                }
            }
        }

        private int isNew
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString["IsNew"]);
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
                Bind_basic();
                if (isNew == 0)//修改  
                {
                    Button3.Text = "确认修改";
                    Button4.Enabled = true;
                    try
                    {
                        Enterprise ent = Enterprise.FindById(id);
                        tb_AssociationName.Text = ent.Name;
                        TextBox2.Text = ent.Domicile;
                        TextBox4.Text = ent.Referee;
                        tb_RegisteredFund.Text = ent.Contacts;
                        tb_ContactName.Text = ent.PhoneNo;
                        tb_Telephone.Text = ent.Email;
                        tb_Fax.Text = ent.Type;
                        TextBox6.Text = ent.InvestorOfTW;
                        TextBox1.Text = ent.InvestorOfH;
                        TextBox9.Text = ent.Scope;
                        TextBox10.Text = ent.Product;
                    }
                    catch (Exception ex)
                    {
                        text(ex.ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
                    }
                }
            }
        }

        private void Bind_basic()
        {

            DataTable dt_MoneyType = MoneyType.Find(CK.K["IsDelete"] == false).ToDataTable();

            //导出文件年份
            int earliestYear = 2000;
            int nowYear = DateTime.Now.Year;
            DataTable dt_Year = new DataTable();
            dt_Year.Columns.Add("Year");
            dt_Year.Columns.Add("YearText");
            for (int i = earliestYear; i <= nowYear; i++)
            {
                DataRow dr_Year = dt_Year.NewRow();
                dr_Year["Year"] = i;
                dr_Year["YearText"] = i + "年";
                dt_Year.Rows.Add(dr_Year);
            }

            ddl_year.DataSource = dt_Year;
            ddl_year.DataTextField = "YearText";
            ddl_year.DataValueField = "Year";
            ddl_year.DataBind();
            ddl_year.SelectedValue = nowYear.ToString();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                string name = tb_AssociationName.Text;
                string domicile = TextBox2.Text;
                string referee = TextBox4.Text;
                string contacts = tb_RegisteredFund.Text;
                string phoneNo = tb_ContactName.Text;
                string email = tb_Telephone.Text;
                string type = tb_Fax.Text;
                string investorOfTW = TextBox6.Text;
                string investorOfH = TextBox1.Text;
                string scope = TextBox9.Text;
                string product = TextBox10.Text;
                if (isNew == 0) //修改
                {
                    EnterpriseBusiness.UpdateEnterprise(id, name, domicile, referee, contacts, phoneNo, email, type, investorOfTW, investorOfH, scope, product);
                    LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 4, "合作大学信息", log_ip, log_account);//修改添加日志 操作类型->4
                }
                else
                {
                    EnterpriseBusiness.AddEnterpriseForNew(id, name, domicile, referee, contacts, phoneNo, email, type, investorOfTW, investorOfH, scope, product);
                    LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 2, "合作大学信息", log_ip, log_account);//新增添加日志 操作类型->2
                }
                Response.Redirect("./CheckAffairList_tzqy.aspx");
            }
            catch (Exception ex)
            {
                //Page.RegisterStartupScript("alert", "<script>alert('数据加载失败！')</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('数据加载错误！')", true);
                text(ex.ToString());
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("./CheckAffairList_tzqy.aspx");
        }
        protected void gv_Periodical_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            int idInspect = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("Del"))  //删除检查记录
            {
                bool result = InspectBusiness.DeleteInspect(idInspect);
                if (!result)
                {
                    return;
                }
            }
            else if (e.CommandName.Equals("Udp"))   //修改检查记录
            {
                ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "btn_Add_jcjl_Click('修改检查记录','./CheckAffairList_tzqy_jcjl_Add.aspx?idInspect=" + idInspect + "')", true);
            }
        }

        protected void btn_Add_jcjl_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "btn_Add_jcjl_Click('添加检查记录','./CheckAffairList_tzqy_jcjl_Add.aspx?enterpriseId=" + id + "')", true);
        }
        protected void btn_Add_wtxt_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "btn_Add_xxjl_Click('添加问题协调','./CheckAffairList_tzqy_wtxt_Add.aspx?enterpriseId=" + id + "')", true);
        }
        protected void gv_Periodical_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Text = Convert.ToDateTime(e.Row.Cells[0].Text).ToString("yyyy/MM/dd");
                    Image image_aqsczd = e.Row.FindControl("image_aqsczd") as Image;
                    if (image_aqsczd.ImageUrl.Length <= ("../../upload/photos/").Length)
                    {
                        image_aqsczd.ImageUrl = "../../images/AddPhoto.jpg";
                    }
                }
            }
            catch (Exception ex)
            {
                text(ex.ToString());
                ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "alert('时间格式错误！')", true);
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Text = Convert.ToDateTime(e.Row.Cells[0].Text).ToString("yyyy/MM/dd");
                }
            }
            catch (Exception ex)
            {
                text(ex.ToString());
                ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "alert('时间格式错误！')", true);
            }

        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            Output_Word_zdts();
        }
        public void Output_Word_zdts()
        {
            try
            {
                int selectedYear = Convert.ToInt32(ddl_year.SelectedValue);

                Enterprise ent = Enterprise.FindById(id);
                string basePath = Server.MapPath("../../");
                Document doc = new Document(basePath + "Template/合作大学情况表.doc");
                //企业名称
                Bookmark bookmark = doc.Range.Bookmarks["Name"];
                bookmark.Text = ent.Name == null ? "" : ent.Name;
                //企业地址
                bookmark = doc.Range.Bookmarks["Domicile"];
                bookmark.Text = ent.Domicile == null ? "" : ent.Domicile;
                //法定代表人
                bookmark = doc.Range.Bookmarks["Referee"];
                bookmark.Text = ent.Referee == null ? "" : ent.Referee;
                //联系人
                bookmark = doc.Range.Bookmarks["Contacts"];
                bookmark.Text = ent.Contacts == null ? "" : ent.Contacts;
                //联系电话
                bookmark = doc.Range.Bookmarks["PhoneNo"];
                bookmark.Text = ent.PhoneNo == null ? "" : ent.PhoneNo;
                //邮箱地址
                bookmark = doc.Range.Bookmarks["Email"];
                bookmark.Text = ent.Email == null ? "" : ent.Email;
                //性质
                bookmark = doc.Range.Bookmarks["Type"];
                bookmark.Text = ent.Type == null ? "" : ent.Type;
                //台投资人
                bookmark = doc.Range.Bookmarks["InvestorOfTW"];
                bookmark.Text = ent.InvestorOfTW == null ? "" : ent.InvestorOfTW;
                //陆投资人
                bookmark = doc.Range.Bookmarks["InvestorOfH"];
                bookmark.Text = ent.InvestorOfH == null ? "" : ent.InvestorOfH;
                //经营范围
                bookmark = doc.Range.Bookmarks["Scope"];
                bookmark.Text = ent.Scope == null ? "" : ent.Scope;
                //主要业务及产品
                bookmark = doc.Range.Bookmarks["Product"];
                bookmark.Text = ent.Product == null ? "" : ent.Product;
                //检查记录
                DataTable dt_fa = Inspect.Find(CK.K["Enterprise_Id"] == id && CK.K["IsDelete"] == false && CK.K["Time"] >= (new DateTime(selectedYear, 1, 1)) && CK.K["Time"] <= (new DateTime(selectedYear, 12, 31))).ToDataTable();
                if (dt_fa.Rows.Count > 0)
                {
                    int fa_count = dt_fa.Rows.Count <= 5 ? dt_fa.Rows.Count : 5;
                    for (int i = 0; i < fa_count; i++)
                    {
                        //时间
                        bookmark = doc.Range.Bookmarks["In_" + i + "_Time"];
                        bookmark.Text = dt_fa.Rows[i]["Time"].ToString().IsNullOrEmpty() ? "" : Convert.ToDateTime(dt_fa.Rows[i]["Time"].ToString()).ToString("yyyy.MM.dd");
                        //检查单位
                        bookmark = doc.Range.Bookmarks["In_" + i + "_Unit"];
                        bookmark.Text = dt_fa.Rows[i]["Unit"].ToString();
                        //检查结果
                        bookmark = doc.Range.Bookmarks["In_" + i + "_Res"];
                        bookmark.Text = dt_fa.Rows[i]["Result"].ToString();
                    }
                }
                //协调记录
                DataTable dt_Exp = Coordinate.Find(CK.K["Enterprise_Id"] == id && CK.K["IsDelete"] == false && CK.K["Time"] >= (new DateTime(selectedYear, 1, 1)) && CK.K["Time"] <= (new DateTime(selectedYear, 12, 31))).ToDataTable();
                if (dt_Exp.Rows.Count > 0)
                {
                    int exp_count = dt_Exp.Rows.Count <= 5 ? dt_Exp.Rows.Count : 5;
                    for (int i = 0; i < exp_count; i++)
                    {
                        //时间
                        bookmark = doc.Range.Bookmarks["Pr_" + i + "_Time"];
                        bookmark.Text = dt_Exp.Rows[i]["Time"].ToString().IsNullOrEmpty() ? "" : Convert.ToDateTime(dt_Exp.Rows[i]["Time"].ToString()).ToString("yyyy.MM.dd");
                        //问题及协调情况
                        bookmark = doc.Range.Bookmarks["Pr_" + i + "_Pr"];
                        bookmark.Text = dt_Exp.Rows[i]["Problem"].ToString();
                    }
                }
                string fileNameWithOutExtention = Guid.NewGuid().ToString();
                doc.Save(basePath + "upload/word_tzqy/" + fileNameWithOutExtention + ".pdf", SaveFormat.Pdf);
                FileStream fs = new FileStream(basePath + "upload/word_tzqy/" + fileNameWithOutExtention + ".pdf", FileMode.Open);
                byte[] file = new byte[fs.Length];
                fs.Read(file, 0, file.Length);
                fs.Close();
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("台资企业情况表-" + ddl_year.SelectedItem.Text + "-" + ent.Name + ".pdf", System.Text.Encoding.UTF8));
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(file);
                Response.End();
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}