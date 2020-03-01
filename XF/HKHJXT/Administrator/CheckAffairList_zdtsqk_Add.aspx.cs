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
    public partial class CheckAffairList_zdtsqk_Add : PageBase
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
                    Button4.Enabled = true;
                    Button3.Text = "确认修改";
                    try
                    {
                        Student stu = Student.FindById(id);
                        tb_AssociationName.Text = stu.Name;
                        radio_1.SelectedValue = (bool)stu.Sex ? "2" : "1";
                        tb_OrganizationCode.Text = stu.IdentityNo;
                        if (stu.Birthday != null)
                        {
                            input_RegisterDate.Value = ((DateTime)stu.Birthday).ToString("yyyy/MM/dd");
                        }
                        tb_ContactName.Text = stu.Major;
                        ddl_School.SelectedValue = stu.School;
                        if (stu.StudyTypeId != null)
                        {
                            ddl_StudyType.SelectedValue = stu.StudyTypeId.ToString();
                        }
                        if (stu.JoinSchoolDate != null)
                        {
                            input_JoinSchoolDate.Value = ((DateTime)stu.JoinSchoolDate).ToString("yyyy/MM/dd");
                        }
                        tb_Telephone.Text = stu.SystemOfEdu;
                        tb_Fax.Text = stu.PhoneNo;
                        tb_ScopeOfBusiness.Text = stu.AddressOfH;
                        //TextBox1.Text = stu.AddressOfTW;
                        TextBox3.Text = stu.Tip;
                        Bind_Family(0);
                        Bind_Experience(0);
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


            DataTable dt_School = School.Find(CK.K["IsDelete"] == false).ToDataTable();
            ddl_School.DataSource = dt_School;
            ddl_School.DataValueField = "SchoolName";
            ddl_School.DataTextField = "SchoolName";
            ddl_School.DataBind();

            DataTable dt_StudyType = StudyType.Find(CK.K["IsDelete"] == false).ToDataTable();
            ddl_StudyType.DataSource = dt_StudyType;
            ddl_StudyType.DataValueField = "StudyTypeId";
            ddl_StudyType.DataTextField = "StudyTypeText";
            ddl_StudyType.DataBind();


            ////导出文件年份
            //int earliestYear = 2000;
            //int nowYear = DateTime.Now.Year;
            //DataTable dt_Year = new DataTable();
            //dt_Year.Columns.Add("Year");
            //dt_Year.Columns.Add("YearText");
            //for (int i = earliestYear; i <= nowYear; i++)
            //{
            //    DataRow dr_Year = dt_Year.NewRow();
            //    dr_Year["Year"] = i;
            //    dr_Year["YearText"] = i + "年";
            //    dt_Year.Rows.Add(dr_Year);
            //}
            //ddl_year.DataSource = dt_Year;
            //ddl_year.DataTextField = "YearText";
            //ddl_year.DataValueField = "Year";
            //ddl_year.DataBind();
            //ddl_year.SelectedValue = nowYear.ToString();
        }

        public void Bind_Family(int NewIndex)
        {
            try
            {
                int size = this.gv_Periodical.PageSize;
                var ps = FamilyOfStudentBusiness.GetFamilys(id, size);
                this.gv_Periodical.PageIndex = NewIndex;
                this.gv_Periodical.DataSource = new PagedCollection<FamilyOfStudent>(ps, NewIndex);
                this.gv_Periodical.DataBind();
                this.label_ResultCount.Text = ps.GetResultCount().ToString();
            }
            catch (Exception ex)
            {
                text(ex.ToString());
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);
            }
        }
        public void Bind_Experience(int NewIndex)
        {
            try
            {
                int size = this.GridView1.PageSize;
                var ps = ExperienceBusiness.GetExperiences(id, size);
                this.GridView1.PageIndex = NewIndex;
                this.GridView1.DataSource = new PagedCollection<Experience>(ps, NewIndex);
                this.GridView1.DataBind();
                this.label6.Text = ps.GetResultCount().ToString();
            }
            catch (Exception ex)
            {
                text(ex.ToString());
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("./CheckAffairList_zdtsqk.aspx");
        }

        protected void gv_Periodical_RowDataBound1(object sender, GridViewRowEventArgs e)
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
                e.Row.Cells[2].Text = Convert.ToDateTime(e.Row.Cells[2].Text).ToString("yyyy/MM/dd");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = Convert.ToDateTime(e.Row.Cells[0].Text).ToString("yyyy/MM/dd");
                e.Row.Cells[1].Text = Convert.ToDateTime(e.Row.Cells[1].Text).ToString("yyyy/MM/dd");
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (Save().Equals("ok"))
            {
                Response.Redirect("./CheckAffairList_zdtsqk.aspx");
            }
        }

        private string Save()
        {
            try
            {
                //新增时判重，通过姓名，证件类型，证件号码进行唯一性判别别。
                if (isNew == 1 && Student.FindOne(CK.K["Name"] == tb_AssociationName.Text && CK.K["IdentityNo"] == tb_OrganizationCode.Text && CK.K["IsDelete"] == false) != null)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('此人信息已存在，请勿重复添加！')", true);
                    return "fail";
                }
                string name = tb_AssociationName.Text;
                bool sex = !radio_1.SelectedValue.Equals("1");
                String identityNo = tb_OrganizationCode.Text;
                DateTime birthday = input_RegisterDate.Value.Equals("") ? new DateTime(1900, 1, 1) : Convert.ToDateTime(input_RegisterDate.Value);
                String school = ddl_School.SelectedValue;
                int studyTypeId = Convert.ToInt32(ddl_StudyType.SelectedValue);
                DateTime joinSchoolDate = input_JoinSchoolDate.Value.Equals("") ? new DateTime(1900, 1, 1) : Convert.ToDateTime(input_JoinSchoolDate.Value);
                String major = tb_ContactName.Text;
                String systemOfEdu = tb_Telephone.Text;
                String phoneNo = tb_Fax.Text;
                String AddressOfH = tb_ScopeOfBusiness.Text;
                //String addressOfTW = "台湾省" + ddl_csmc.SelectedValue;//TextBox1.Text;
                String tip = TextBox3.Text;

                string result = "";
                if (isNew == 0) //修改
                {
                    result = StudentBusiness.UpdateStudent(id, name, sex, identityNo, birthday, school, studyTypeId, joinSchoolDate, major, systemOfEdu, phoneNo, AddressOfH, tip);
                    LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 4, "在校本科生信息", log_ip, log_account);//修改添加日志 操作类型->4
                }
                else
                {
                    result = StudentBusiness.AddStudentForNew(id, name, sex, identityNo, birthday, school, studyTypeId, joinSchoolDate, major, systemOfEdu, phoneNo, AddressOfH, tip);
                    LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 2, "在校本科生信息", log_ip, log_account);//新增添加日志 操作类型->2
                }
                return result;
            }
            catch (Exception ex)
            {
                //Page.RegisterStartupScript("alert", "<script>alert('数据加载失败！')</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('数据加载错误！')", true);
                text(ex.ToString());
                return "";
            }
        }

        protected void btn_Add_jlqk_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "btn_Add_jlqk_Click('添加家庭成员','./CheckAffairList_zdtsqk_jtcy_Add.aspx?studentId=" + id + "')", true);
        }

        protected void btn_Add_xxjl_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "btn_Add_xxjl_Click('添加学习经历','./CheckAffairList_zdtsqk_xxjl_Add.aspx?studentId=" + id + "')", true);
        }

        protected void btn_Refresh_Click(object sender, EventArgs e)  //刷新家庭成员列表
        {
            Bind_Family(0);
        }
        protected void btn_Refresh2_Click(object sender, EventArgs e)  //刷新学习记录列表
        {
            Bind_Experience(0);
        }
        protected void gv_Periodical_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            int idFamily = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("Del"))  //删除家庭成员
            {
                bool result = FamilyOfStudentBusiness.deleteFamilyOfStudent(idFamily);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('删除失败，请重新再试！')", true);
                    return;
                }
                Bind_Family(0);
            }
            else if (e.CommandName.Equals("Udp"))   //修改家庭成员
            {
                ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "btn_Add_jlqk_Click('添加家庭成员','./CheckAffairList_zdtsqk_jtcy_Add.aspx?idFamily=" + idFamily + "')", true);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idExperience = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("Del"))  //删除学习经历
            {
                bool result = ExperienceBusiness.DeleteExperience(idExperience);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('删除失败，请重新再试！')", true);
                    return;
                }
                Bind_Experience(0);
            }
            else if (e.CommandName.Equals("Udp"))   //修改学习经历
            {
                ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "btn_Add_xxjl_Click('添加学习经历','./CheckAffairList_zdtsqk_xxjl_Add.aspx?idExperience=" + idExperience + "')", true);
            }
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
                Student stu = Student.FindById(id);
                string basePath = Server.MapPath("../../");
                Document doc = new Document(basePath + "Template/在读台生情况信息表.doc");
                //姓名
                Bookmark bookmark = doc.Range.Bookmarks["Name"];
                bookmark.Text = stu.Name == null ? "" : stu.Name;
                //性别
                bookmark = doc.Range.Bookmarks["Sex"];
                bookmark.Text = stu.Sex == null ? "" : (bool)stu.Sex ? "女" : "男";
                //出生年月
                bookmark = doc.Range.Bookmarks["Birth"];
                bookmark.Text = stu.Birthday == null ? "" : ((DateTime)stu.Birthday).ToString("yyyy.MM.dd");
                //证件号码
                bookmark = doc.Range.Bookmarks["CertificateNum"];
                bookmark.Text = stu.IdentityNo == null ? "" : stu.IdentityNo;
                //就读学校
                bookmark = doc.Range.Bookmarks["School"];
                bookmark.Text = stu.School == null ? "" : stu.School;
                //专业
                bookmark = doc.Range.Bookmarks["Subject"];
                bookmark.Text = stu.Major == null ? "" : stu.Major;
                //学制
                bookmark = doc.Range.Bookmarks["StudyYear"];
                bookmark.Text = stu.SystemOfEdu == null ? "" : stu.SystemOfEdu;
                //联系电话
                bookmark = doc.Range.Bookmarks["Phone"];
                bookmark.Text = stu.PhoneNo == null ? "" : stu.PhoneNo;
                //就读类别
                bookmark = doc.Range.Bookmarks["StudyTypeText"];
                bookmark.Text = StudyType.FindOne(CK.K["StudyTypeId"] == stu.StudyTypeId && CK.K["IsDelete"] == false).StudyTypeText;
                //入学日期
                bookmark = doc.Range.Bookmarks["JoinSchoolDate"];
                bookmark.Text = stu.JoinSchoolDate == null ? "" : ((DateTime)stu.JoinSchoolDate).ToString("yyyy.MM.dd");
                //在沪居住地
                bookmark = doc.Range.Bookmarks["SHAddress"];
                bookmark.Text = stu.AddressOfH == null ? "" : stu.AddressOfH;
                //备注
                bookmark = doc.Range.Bookmarks["Memo"];
                bookmark.Text = stu.Tip == null ? "" : stu.Tip;
                //家庭主要成员
                DataTable dt_fa = FamilyOfStudent.Find(CK.K["Student_Id"] == id && CK.K["IsDelete"] == false).ToDataTable();
                if (dt_fa.Rows.Count > 0)
                {
                    int fa_count = dt_fa.Rows.Count <= 5 ? dt_fa.Rows.Count : 5;
                    for (int i = 0; i < fa_count; i++)
                    {
                        //姓名
                        bookmark = doc.Range.Bookmarks["Fa_" + i + "_Name"];
                        bookmark.Text = dt_fa.Rows[i]["Name"].ToString();
                        //性别
                        bookmark = doc.Range.Bookmarks["Fa_" + i + "_Sex"];
                        bookmark.Text = dt_fa.Rows[i]["Sex"].ToString().IsNullOrEmpty() ? "" : Convert.ToBoolean(dt_fa.Rows[i]["Sex"].ToString()) ? "女" : "男"; ;
                        //出生年月
                        bookmark = doc.Range.Bookmarks["Fa_" + i + "_Birth"];
                        bookmark.Text = dt_fa.Rows[i]["Birthday"].ToString().IsNullOrEmpty() ? "" : Convert.ToDateTime(dt_fa.Rows[i]["Birthday"].ToString()).ToString("yyyy.MM.dd");
                        //与本人关系
                        bookmark = doc.Range.Bookmarks["Fa_" + i + "_Relation"];
                        bookmark.Text = dt_fa.Rows[i]["Relationship"].ToString();
                        //工作单位
                        bookmark = doc.Range.Bookmarks["Fa_" + i + "_Department"];
                        bookmark.Text = dt_fa.Rows[i]["Company"].ToString();
                        //备注
                        bookmark = doc.Range.Bookmarks["Fa_" + i + "_Memo"];
                        bookmark.Text = dt_fa.Rows[i]["Tip"].ToString();
                    }
                }
                //学习经历
                DataTable dt_Exp = Experience.Find(CK.K["Student_Id"] == id && CK.K["IsDelete"] == false).ToDataTable();
                if (dt_Exp.Rows.Count > 0)
                {
                    int exp_count = dt_Exp.Rows.Count <= 10 ? dt_Exp.Rows.Count : 10;
                    for (int i = 0; i < exp_count; i++)
                    {
                        //姓名
                        bookmark = doc.Range.Bookmarks["Exp_" + i + "_Start"];
                        bookmark.Text = dt_Exp.Rows[i]["StartTime"].ToString().IsNullOrEmpty() ? "" : Convert.ToDateTime(dt_Exp.Rows[i]["StartTime"].ToString()).ToString("yyyy.MM.dd");
                        //性别
                        bookmark = doc.Range.Bookmarks["Exp_" + i + "_End"];
                        bookmark.Text = dt_Exp.Rows[i]["EndTime"].ToString().IsNullOrEmpty() ? "" : Convert.ToDateTime(dt_Exp.Rows[i]["EndTime"].ToString()).ToString("yyyy.MM.dd");
                        //出生年月
                        bookmark = doc.Range.Bookmarks["Exp_" + i + "_School"];
                        bookmark.Text = dt_Exp.Rows[i]["School"].ToString();
                    }
                }

                ////插入图片   
                //DocumentBuilder builder = new DocumentBuilder(doc);
                //builder.MoveToBookmark("Photo");
                //var img = builder.InsertImage(basePath + "upload/photos/" + photoName);
                //img.Width = 165;
                //img.Height = 240;
                //img.HorizontalAlignment = Aspose.Words.Drawing.HorizontalAlignment.Center;

                string fileNameWithOutExtention = Guid.NewGuid().ToString();
                doc.Save(basePath + "upload/word_zdts/" + fileNameWithOutExtention + ".pdf", SaveFormat.Pdf);
                FileStream fs = new FileStream(basePath + "upload/word_zdts/" + fileNameWithOutExtention + ".pdf", FileMode.Open);
                byte[] file = new byte[fs.Length];
                fs.Read(file, 0, file.Length);
                fs.Close();
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("在读台生情况信息表-" + stu.Name + ".pdf", System.Text.Encoding.UTF8));
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

        protected void Button4_Click(object sender, EventArgs e)
        {
            Output_Word_zdts();
        }
    }
}