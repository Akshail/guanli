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
    public partial class CheckAffairList_cztb_look : PageBase
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
                if (isNew == 0)//修改  查看
                {
                    Button4.Enabled = true;
                    Button3.Text = "确认修改";
                    try
                    {
                        People peo = People.FindById(id);
                        tb_AssociationName.Text = peo.Name;
                        radio_1.SelectedValue = (bool)peo.Sex ? "2" : "1";
                        Text1.Value = DateTime.Compare((DateTime)peo.ArriveDate, new DateTime(1900, 1, 1)) > 0 ? ((DateTime)peo.ArriveDate).ToString("yyyy/MM/dd") : "";
                        input_RegisterDate.Value = DateTime.Compare((DateTime)peo.Birthday, new DateTime(1900, 1, 1)) > 0 ? ((DateTime)peo.Birthday).ToString("yyyy/MM/dd") : "";
                        tb_RegisteredFund.Text = peo.IdentityNo;
                        tb_ContactName.Text = peo.PhoneNo;
                        TextBox6.Text = peo.Address;
                        TextBox7.Text = peo.TempApplication;
                        ddl.SelectedValue = peo.ComeReason.ToString();
                        TextBox3.Text = peo.Tip;
                        Bind_Family(0);
                    }
                    catch (Exception ex)
                    {
                        text(ex.ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
                    }
                }

            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "lhmd_change('" + ddl.SelectedValue + "')", true);
        }
        private void Bind_basic()
        {
        }
        public void Bind_Family(int NewIndex)
        {
            try
            {
                int size = this.gv_Periodical.PageSize;
                var ps = FamilyOfPeopleBusiness.GetFamilys(id, size);
                this.gv_Periodical.PageIndex = NewIndex;
                this.gv_Periodical.DataSource = new PagedCollection<Family>(ps, NewIndex);
                this.gv_Periodical.DataBind();
                this.label_ResultCount.Text = ps.GetResultCount().ToString();
            }
            catch (Exception ex)
            {
                text(ex.ToString());
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("./CheckAffairList_cztb.aspx");
        }

        protected void gv_Periodical_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                switch (e.Row.Cells[1].Text.Trim())
                {
                    case "False":
                        e.Row.Cells[1].Text = "男";
                        break;
                    case "True":
                        e.Row.Cells[1].Text = "女";
                        break;
                }
                if (!e.Row.Cells[2].Text.Equals("&nbsp;"))
                {
                    //e.Row.Cells[2].Text = e.Row.Cells[2].Text.Substring(0, 10);
                    e.Row.Cells[2].Text = Convert.ToDateTime(e.Row.Cells[2].Text).ToString("yyyy/MM/dd");
                }
                
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                //新增时判重，通过姓名，证件类型，证件号码进行唯一性判别别。
                if (isNew == 1 && People.FindOne(CK.K["Name"] == tb_AssociationName.Text && CK.K["IdentityNo"] == tb_RegisteredFund.Text && CK.K["IsDelete"] == false) != null)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('此人信息已存在，请勿重复添加！')", true);
                    return;
                }
                string name = tb_AssociationName.Text;
                bool sex = !radio_1.SelectedValue.Equals("1");
                DateTime arriveDate = new DateTime(1900, 1, 2);
                try
                {
                    arriveDate = Text1.Value.Equals("") ? arriveDate : Convert.ToDateTime(Text1.Value);
                }
                catch (Exception ex)
                {
                    text(ex.ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('时间输入格式不规范，请重新输入！')", true);
                    return;
                }
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
                String identityNo = tb_RegisteredFund.Text;
                String phoneNo = tb_ContactName.Text;
                DateTime validityOfTW = new DateTime(1900, 1, 2);
                String address = TextBox6.Text;
                String tempApplication = TextBox7.Text;
                int comeReason = int.Parse(ddl.SelectedValue);
                String tip = TextBox3.Text;
                if (isNew == 0) //修改
                {
                    PeopleBusiness.UpdatePeople(id, name, sex, arriveDate, birthday, identityNo, phoneNo, address, tempApplication, comeReason, tip);
                    LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 4, "教职工信息", log_ip, log_account);//修改添加日志 操作类型->4
                }
                else
                {
                    string result = PeopleBusiness.AddPeopleForNew(id, name, sex, arriveDate, birthday, identityNo, phoneNo, address, tempApplication, comeReason, tip);
                    LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 2, "教职工信息", log_ip, log_account);//新增添加日志 操作类型->2
                    BusinessBusiness.AddBusinessNull(id);
                    AgedBusiness.AddAgedNull(id);
                    RelativeOfPeopleBusiness.AddRelativeNull(id);
                    StudyBusiness.AddStudyNull(id);
                    VisitBusiness.AddVisitNull(id);
                    WorkBusiness.AddWorkNull(id);
                }
                Response.Redirect("./CheckAffairList_cztb.aspx");
            }
            catch (Exception ex)
            {
                //Page.RegisterStartupScript("alert", "<script>alert('数据加载失败！')</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('数据加载错误！')", true);
                text(ex.ToString());
            }
        }

        protected void gv_Periodical_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            int idFamily = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("Del"))  //删除家庭成员
            {
                bool result = FamilyOfPeopleBusiness.deleteFamilyOfPeople(idFamily);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('删除失败，请重新再试！')", true);
                    return;
                }
                Bind_Family(0);
            }
            else if (e.CommandName.Equals("Udp"))   //修改家庭成员
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "btn_Add_jlqk_Click('添加家庭成员','./CheckAffairList_cztb_jtcy_Add.aspx?idFamily=" + idFamily + "')", true);
            }
        }
        protected void btn_Refresh_Click(object sender, EventArgs e)  //刷新家庭成员列表
        {
            Bind_Family(0);
        }
        protected void btn_Add_jlqk_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "btn_Add_jlqk_Click('添加家庭成员','./CheckAffairList_cztb_jtcy_Add.aspx?peopleId=" + id + "')", true);
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            Output_Word_zdts();
        }
        public void Output_Word_zdts()
        {
            try
            {
                if (id == 0)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
                    return;
                }
                People peo = People.FindById(id);
                string basePath = Server.MapPath("../../");
                Document doc = new Document(basePath + "Template/常住台胞信息表.doc");
                //姓名
                Bookmark bookmark = doc.Range.Bookmarks["Name"];
                bookmark.Text = peo.Name == null ? "" : peo.Name;
                //性别
                bookmark = doc.Range.Bookmarks["Sex"];
                bookmark.Text = peo.Sex == null ? "" : (bool)peo.Sex ? "女" : "男";
                //出生年月
                bookmark = doc.Range.Bookmarks["Birthday"];
                bookmark.Text = peo.Birthday == null ? "" : ((DateTime)peo.Birthday).ToString("yyyy.MM.dd");
                //入职时间
                bookmark = doc.Range.Bookmarks["ArriveDate"];
                bookmark.Text = peo.ArriveDate == null ? "" : ((DateTime)peo.ArriveDate).ToString("yyyy.MM.dd");
                //身份证号
                bookmark = doc.Range.Bookmarks["IdentityNo"];
                bookmark.Text = peo.IdentityNo == null ? "" : peo.IdentityNo;
                //联系电话
                bookmark = doc.Range.Bookmarks["PhoneNo"];
                bookmark.Text = peo.PhoneNo == null ? "" : peo.PhoneNo;
                //现住址
                bookmark = doc.Range.Bookmarks["Address"];
                bookmark.Text = peo.Address == null ? "" : peo.Address;
                //户籍地址
                bookmark = doc.Range.Bookmarks["TempApplication"];
                bookmark.Text = peo.TempApplication == null ? "" : peo.TempApplication;
                //备注
                bookmark = doc.Range.Bookmarks["Tip"];
                bookmark.Text = peo.Tip == null ? "" : peo.Tip;
                //"√""□"担任职务
                for (int i = 1; i <= 7; i++)
                {
                    bookmark = doc.Range.Bookmarks["LHMD_" + i];
                    bookmark.Text = "□";
                }
                string lhmd = peo.ComeReason.ToString();
                bookmark = doc.Range.Bookmarks["LHMD_" + lhmd];
                bookmark.Text = "√";
                //家庭主要成员
                DataTable dt_fa = Family.Find(CK.K["People_Id"] == id && CK.K["IsDelete"] == false).ToDataTable();
                if (dt_fa.Rows.Count > 0)
                {
                    int fa_count = dt_fa.Rows.Count <= 7 ? dt_fa.Rows.Count : 7;
                    for (int i = 0; i < fa_count; i++)
                    {
                        //姓名
                        bookmark = doc.Range.Bookmarks["Fa_" + i + "_Name"];
                        bookmark.Text = dt_fa.Rows[i]["Name"].ToString();
                        //性别
                        bookmark = doc.Range.Bookmarks["Fa_" + i + "_Sex"];
                        bookmark.Text = dt_fa.Rows[i]["Sex"].ToString().IsNullOrEmpty() ? "" : Convert.ToBoolean(dt_fa.Rows[i]["Sex"].ToString()) ? "女" : "男"; ;
                        //出生年月
                        bookmark = doc.Range.Bookmarks["Fa_" + i + "_Bi"];
                        bookmark.Text = dt_fa.Rows[i]["Birthday"].ToString().IsNullOrEmpty() ? "" : Convert.ToDateTime(dt_fa.Rows[i]["Birthday"].ToString()).ToString("yyyy.MM.dd");
                        //与本人关系
                        bookmark = doc.Range.Bookmarks["Fa_" + i + "_Re"];
                        bookmark.Text = dt_fa.Rows[i]["Relationship"].ToString();
                        //工作单位
                        bookmark = doc.Range.Bookmarks["Fa_" + i + "_Co"];
                        bookmark.Text = dt_fa.Rows[i]["Company"].ToString();
                        //备注
                        bookmark = doc.Range.Bookmarks["Fa_" + i + "_Ti"];
                        bookmark.Text = dt_fa.Rows[i]["Tip"].ToString();
                    }
                }
                string fileNameWithOutExtention = Guid.NewGuid().ToString();
                doc.Save(basePath + "upload/word_cztb/" + fileNameWithOutExtention + ".pdf", SaveFormat.Pdf);
                FileStream fs = new FileStream(basePath + "upload/word_cztb/" + fileNameWithOutExtention + ".pdf", FileMode.Open);
                byte[] file = new byte[fs.Length];
                fs.Read(file, 0, file.Length);
                fs.Close();
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("常住台胞信息表-" + peo.Name + ".pdf", System.Text.Encoding.UTF8));
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(file);
                Response.End();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载失败，请重新再试！')", true);
                return;
            }
        }
    }
}