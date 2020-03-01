using BusinessSupply.BusinessOper;
using DataModel.DataModel;
using Lephone.Data;
using Lephone.Extra;
using NPOI.HSSF.UserModel;
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
    public partial class CheckAffairList_zdtsqk : PageBase
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (uid == null)
            {
                Response.Redirect("../../LogInPage.aspx");
            }
            if (!IsPostBack)
            {
                if (uType != null && uType.Equals("2"))
                {
                    HyperLink7.Visible = false;
                }
                Bind_basic();
                RefreshView(0);
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


        private void RefreshView(int NewIndex)
        {
            try
            {
                int size = this.gv_Result.PageSize;
                string name = tb_AddName.Text.Trim();
                string school = ddl_School.SelectedValue;
                string sex = ddl_sex.SelectedValue;
                string startYear = input_start.Value;
                string endYear = input_end.Value;
                var ps = StudentBusiness.GetStudents(size, name, school, sex, startYear, endYear);
                this.gv_Result.PageIndex = NewIndex;
                this.gv_Result.DataSource = new PagedCollection<Student>(ps, NewIndex);
                this.gv_Result.DataBind();
                this.Label_Result.Text = ps.GetResultCount().ToString();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);
            }
        }

        protected void gv_Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RefreshView(e.NewPageIndex);
        }

        protected void btn_AddSimple_Click(object sender, EventArgs e)
        {
            int stuId = StudentBusiness.AddStudentForId();
            Response.Redirect("./CheckAffairList_zdtsqk_Add.aspx?Id=" + stuId + "&IsNew=1");
        }

        protected void gv_Result_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                switch (e.Row.Cells[2].Text.Trim())
                {
                    case "False":
                        e.Row.Cells[2].Text = "男";
                        break;
                    case "True":
                        e.Row.Cells[2].Text = "女";
                        break;
                }
                e.Row.Cells[3].Text = e.Row.Cells[3].Text.Equals("&nbsp;") ? "" : Convert.ToDateTime(e.Row.Cells[3].Text).ToString("yyyy/MM/dd");
            }

        }

        protected void gv_Result_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Del"))
            {
                string id = e.CommandArgument.ToString();
                string result = StudentBusiness.deleteStudent(Int32.Parse(id));
                if (!result.Equals("ok"))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('删除失败，请重新再试！')", true);
                    return;
                }
                LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 3, "在校本科生信息", log_ip, log_account);//删除添加日志 操作类型->3
                RefreshView(0);
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            RefreshView(0);
        }

        protected void btn_Download_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "导入模板（在沪台生信息）.xls";//客户端保存的文件名
                string filePath = Server.MapPath("../../Template/导入模板（在沪台生信息）.xls");//路径

                FileInfo fileInfo = new FileInfo(filePath);
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                Response.AddHeader("Content-Transfer-Encoding", "binary");
                Response.ContentType = "application/octet-stream";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                Response.WriteFile(fileInfo.FullName);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                //X.Msg.Alert("提示", "加载错误，请重新尝试！").Show();
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);
            }
        }

        protected void btn_importEvent_Click(object sender, EventArgs e)
        {
            string fileExtension = null;
            string file = import_upload.FileName.Replace(" ", "");
            string filename = System.IO.Path.GetFileNameWithoutExtension(file) + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(" ", "").Replace("/", "").Replace(":", "").Replace("-", "");
            bool fileOK = false;
            string savepath = Server.MapPath("~/Upload/zdts_import/");
            //判断是否已选择上传文件
            if (import_upload.HasFile)
            {
                //获得文件后缀
                fileExtension = System.IO.Path.GetExtension(import_upload.FileName).ToLower();
                //允许上传文件后缀
                String[] allowedExtensions = { ".xls", ".xlsx" };
                //判断文件后缀是否被允许上传
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                        filename += fileExtension;
                    }
                }
                //文件类型合法
                if (fileOK)
                {
                    try
                    {
                        if (!Directory.Exists(savepath))
                        {
                            Directory.CreateDirectory(savepath);
                        }
                        import_upload.PostedFile.SaveAs(savepath + filename);

                        //DataTable dt = ReadExcelToTable(savepath + filename);  //读取Excel文件（.xls和.xlsx格式）

                        DataTable dt = ExcelToDataTable(GetNewDataTable(), null, savepath + filename, false);  //读取Excel文件（.xls和.xlsx格式）
                        int success = 0;
                        int fail = 0;
                        int repeat = 0;
                        StringBuilder sb_failIndex = new StringBuilder();
                        StringBuilder sb_repeatIndex = new StringBuilder();
                        string errorInfo = "。";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt.Rows[i];
                            //判重，通过姓名，证件类型，证件号码进行唯一性判别别。
                            if (Student.FindOne(CK.K["Name"] == dr["Name"].ToString() && CK.K["IdentityNo"] == dr["IdentityNo"].ToString() && CK.K["IsDelete"] == false) != null)
                            {
                                repeat++;
                                sb_repeatIndex.Append((i + 3) + "." + dr["Name"].ToString() + "、");
                                continue;
                            }

                            StringBuilder sb_DateLostInfo = new StringBuilder();//日期无法确定，填写为1900/01/01，需添加备注
                            //基本信息
                            bool sex = false;
                            DateTime birthday = new DateTime();
                            int studyTypeId = 0;
                            DateTime joinSchoolDate = new DateTime();
                            //家庭主要成员
                            bool fa_sex_1 = false;
                            DateTime fa_birthday_1 = new DateTime();
                            bool fa_sex_2 = false;
                            DateTime fa_birthday_2 = new DateTime();
                            bool fa_sex_3 = false;
                            DateTime fa_birthday_3 = new DateTime();
                            bool fa_sex_4 = false;
                            DateTime fa_birthday_4 = new DateTime();
                            bool fa_sex_5 = false;
                            DateTime fa_birthday_5 = new DateTime();
                            //学习经历
                            DateTime Exp_start_1 = new DateTime();
                            DateTime Exp_End_1 = new DateTime();
                            DateTime Exp_start_2 = new DateTime();
                            DateTime Exp_End_2 = new DateTime();
                            DateTime Exp_start_3 = new DateTime();
                            DateTime Exp_End_3 = new DateTime();
                            DateTime Exp_start_4 = new DateTime();
                            DateTime Exp_End_4 = new DateTime();
                            DateTime Exp_start_5 = new DateTime();
                            DateTime Exp_End_5 = new DateTime();
                            try//检验输入是否规范
                            {
                                if (dr["Sex"].Equals("男") || dr["Sex"].Equals("女"))
                                {
                                    sex = dr["Sex"].Equals("女");//女true   男false
                                }
                                else
                                {
                                    throw new Exception("性别输入错误!");
                                }
                                birthday = Convert.ToDateTime(dr["Birthday"].ToString());
                                if (birthday.Equals(new DateTime(1900, 1, 1)))
                                {
                                    sb_DateLostInfo.Append("'" + dr["Name"].ToString() + "'出生日期无法查实，");
                                }
                                studyTypeId = (int)(StudyType.FindOne(CK.K["StudyTypeText"] == dr["StudyTypeText"]).StudyTypeId);
                                joinSchoolDate = Convert.ToDateTime(dr["JoinSchoolDate"]);
                                if (joinSchoolDate.Equals(new DateTime(1900, 1, 1)))
                                {
                                    sb_DateLostInfo.Append("'" + dr["Name"].ToString() + "'入学日期无法查实，");
                                }
                                //if (tw_address.Length <= 3 || !tw_address.Substring(0, 3).Equals("台湾省") || (TaiWanCity.FindOne(CK.K["CityName"] == tw_address.Substring(3, tw_address.Length - 3)) == null))
                                //{
                                //    throw new Exception("台湾户籍地输入错误!");
                                //}

                                if (!dr["Fa_Name_1"].ToString().IsNullOrEmpty())
                                {
                                    if (dr["Fa_Sex_1"].Equals("男") || dr["Fa_Sex_1"].Equals("女"))
                                    {
                                        fa_sex_1 = dr["Fa_Sex_1"].Equals("女");//女true   男false
                                    }
                                    else
                                    {
                                        throw new Exception("性别输入错误!");
                                    }
                                    fa_birthday_1 = Convert.ToDateTime(dr["Fa_Birthday_1"].ToString());
                                    if (fa_birthday_1.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Fa_Name_1"].ToString() + "'出生日期无法查实，");
                                    }
                                }
                                if (!dr["Fa_Name_2"].ToString().IsNullOrEmpty())
                                {
                                    if (dr["Fa_Sex_2"].Equals("男") || dr["Fa_Sex_2"].Equals("女"))
                                    {
                                        fa_sex_2 = dr["Fa_Sex_2"].Equals("女");//女true   男false
                                    }
                                    else
                                    {
                                        throw new Exception("性别输入错误!");
                                    }
                                    fa_birthday_2 = Convert.ToDateTime(dr["Fa_Birthday_2"].ToString());
                                    if (fa_birthday_2.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Fa_Name_2"].ToString() + "'出生日期无法查实，");
                                    }
                                }
                                if (!dr["Fa_Name_3"].ToString().IsNullOrEmpty())
                                {
                                    if (dr["Fa_Sex_3"].Equals("男") || dr["Fa_Sex_3"].Equals("女"))
                                    {
                                        fa_sex_3 = dr["Fa_Sex_3"].Equals("女");//女true   男false
                                    }
                                    else
                                    {
                                        throw new Exception("性别输入错误!");
                                    }
                                    fa_birthday_3 = Convert.ToDateTime(dr["Fa_Birthday_3"].ToString());
                                    if (fa_birthday_3.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Fa_Name_3"].ToString() + "'出生日期无法查实，");
                                    }
                                }
                                if (!dr["Fa_Name_4"].ToString().IsNullOrEmpty())
                                {
                                    if (dr["Fa_Sex_4"].Equals("男") || dr["Fa_Sex_4"].Equals("女"))
                                    {
                                        fa_sex_4 = dr["Fa_Sex_4"].Equals("女");//女true   男false
                                    }
                                    else
                                    {
                                        throw new Exception("性别输入错误!");
                                    }
                                    fa_birthday_4 = Convert.ToDateTime(dr["Fa_Birthday_4"].ToString());
                                    if (fa_birthday_4.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Fa_Name_4"].ToString() + "'出生日期无法查实，");
                                    }
                                }
                                if (!dr["Fa_Name_5"].ToString().IsNullOrEmpty())
                                {
                                    if (dr["Fa_Sex_5"].Equals("男") || dr["Fa_Sex_5"].Equals("女"))
                                    {
                                        fa_sex_5 = dr["Fa_Sex_5"].Equals("女");//女true   男false
                                    }
                                    else
                                    {
                                        throw new Exception("性别输入错误!");
                                    }
                                    fa_birthday_5 = Convert.ToDateTime(dr["Fa_Birthday_5"].ToString());

                                    if (fa_birthday_5.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Fa_Name_5"].ToString() + "'出生日期无法查实，");
                                    }
                                }
                                if (!dr["Exp_School_1"].ToString().IsNullOrEmpty())
                                {
                                    Exp_start_1 = Convert.ToDateTime(dr["Exp_StartTime_1"].ToString());
                                    if (Exp_start_1.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Exp_School_1"].ToString() + "'起始日期无法查实，");
                                    }
                                    Exp_End_1 = Convert.ToDateTime(dr["Exp_EndTime_1"].ToString());
                                    if (Exp_End_1.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Exp_School_1"].ToString() + "'截止日期无法查实，");
                                    }
                                }
                                if (!dr["Exp_School_2"].ToString().IsNullOrEmpty())
                                {
                                    Exp_start_2 = Convert.ToDateTime(dr["Exp_StartTime_2"].ToString());
                                    if (Exp_start_2.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Exp_School_2"].ToString() + "'起始日期无法查实，");
                                    }
                                    Exp_End_2 = Convert.ToDateTime(dr["Exp_EndTime_2"].ToString());
                                    if (Exp_End_2.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Exp_School_2"].ToString() + "'截止日期无法查实，");
                                    }
                                }
                                if (!dr["Exp_School_3"].ToString().IsNullOrEmpty())
                                {
                                    Exp_start_3 = Convert.ToDateTime(dr["Exp_StartTime_3"].ToString());
                                    if (Exp_start_3.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Exp_School_3"].ToString() + "'起始日期无法查实，");
                                    }
                                    Exp_End_3 = Convert.ToDateTime(dr["Exp_EndTime_3"].ToString());
                                    if (Exp_End_3.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Exp_School_3"].ToString() + "'截止日期无法查实，");
                                    }
                                }
                                if (!dr["Exp_School_4"].ToString().IsNullOrEmpty())
                                {
                                    Exp_start_4 = Convert.ToDateTime(dr["Exp_StartTime_4"].ToString());
                                    if (Exp_start_4.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Exp_School_4"].ToString() + "'起始日期无法查实，");
                                    }
                                    Exp_End_4 = Convert.ToDateTime(dr["Exp_EndTime_4"].ToString());
                                    if (Exp_End_4.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Exp_School_4"].ToString() + "'截止日期无法查实，");
                                    }
                                }
                                if (!dr["Exp_School_5"].ToString().IsNullOrEmpty())
                                {
                                    Exp_start_5 = Convert.ToDateTime(dr["Exp_StartTime_5"].ToString());
                                    if (Exp_start_5.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Exp_School_5"].ToString() + "'起始日期无法查实，");
                                    }
                                    Exp_End_5 = Convert.ToDateTime(dr["Exp_EndTime_5"].ToString());
                                    if (Exp_End_5.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Exp_School_5"].ToString() + "'截止日期无法查实，");
                                    }

                                }
                                if (sb_DateLostInfo.Length > 0)
                                {
                                    sb_DateLostInfo.Remove(sb_DateLostInfo.Length - 1, 1);
                                    sb_DateLostInfo.Append("。");
                                }
                            }
                            catch (Exception ex)
                            {
                                fail++;
                                sb_failIndex.Append((i + 3) + "." + dr["Name"].ToString() + "、");
                                text_importLog("第" + (i + 3) + "行（" + dr["Name"].ToString() + "）添加失败：" + ex.Message);
                                continue;
                            }
                            try
                            {
                                DbEntry.UsingTransaction(delegate
                                {
                                    int stuId = StudentBusiness.AddStudentForImport(dr["Name"].ToString(), sex, dr["IdentityNo"].ToString(), birthday, dr["School"].ToString(), dr["Major"].ToString(), studyTypeId, joinSchoolDate, dr["SystemOfEdu"].ToString(), dr["PhoneNo"].ToString(), dr["AddressOfH"].ToString(), sb_DateLostInfo + dr["Tip"].ToString());
                                    if (!dr["Fa_Name_1"].ToString().IsNullOrEmpty())
                                    {
                                        bool result = FamilyOfStudentBusiness.AddFamilyOfStudent(stuId, dr["Fa_Name_1"].ToString(), fa_sex_1, fa_birthday_1, dr["Fa_Relationship_1"].ToString(), dr["Fa_Company_1"].ToString(), dr["Fa_Tip_1"].ToString());
                                        if (!result)
                                        {
                                            throw new Exception("家庭主要成员添加失败！");
                                        }
                                    }
                                    if (!dr["Fa_Name_2"].ToString().IsNullOrEmpty())
                                    {
                                        bool result = FamilyOfStudentBusiness.AddFamilyOfStudent(stuId, dr["Fa_Name_2"].ToString(), fa_sex_2, fa_birthday_2, dr["Fa_Relationship_2"].ToString(), dr["Fa_Company_2"].ToString(), dr["Fa_Tip_2"].ToString());
                                        if (!result)
                                        {
                                            throw new Exception("家庭主要成员添加失败！");
                                        }
                                    }
                                    if (!dr["Fa_Name_3"].ToString().IsNullOrEmpty())
                                    {
                                        bool result = FamilyOfStudentBusiness.AddFamilyOfStudent(stuId, dr["Fa_Name_3"].ToString(), fa_sex_3, fa_birthday_3, dr["Fa_Relationship_3"].ToString(), dr["Fa_Company_3"].ToString(), dr["Fa_Tip_3"].ToString());
                                        if (!result)
                                        {
                                            throw new Exception("家庭主要成员添加失败！");
                                        }
                                    }
                                    if (!dr["Fa_Name_4"].ToString().IsNullOrEmpty())
                                    {
                                        bool result = FamilyOfStudentBusiness.AddFamilyOfStudent(stuId, dr["Fa_Name_4"].ToString(), fa_sex_4, fa_birthday_4, dr["Fa_Relationship_4"].ToString(), dr["Fa_Company_4"].ToString(), dr["Fa_Tip_4"].ToString());
                                        if (!result)
                                        {
                                            throw new Exception("家庭主要成员添加失败！");
                                        }
                                    }
                                    if (!dr["Fa_Name_5"].ToString().IsNullOrEmpty())
                                    {
                                        bool result = FamilyOfStudentBusiness.AddFamilyOfStudent(stuId, dr["Fa_Name_5"].ToString(), fa_sex_5, fa_birthday_5, dr["Fa_Relationship_5"].ToString(), dr["Fa_Company_5"].ToString(), dr["Fa_Tip_5"].ToString());
                                        if (!result)
                                        {
                                            throw new Exception("家庭主要成员添加失败！");
                                        }
                                    }
                                    if (!dr["Exp_School_1"].ToString().IsNullOrEmpty())
                                    {
                                        bool result = ExperienceBusiness.AddExperience(stuId, Exp_start_1, Exp_End_1, dr["Exp_School_1"].ToString());
                                        if (!result)
                                        {
                                            throw new Exception("学习经历添加失败！");
                                        }
                                    }
                                    if (!dr["Exp_School_2"].ToString().IsNullOrEmpty())
                                    {
                                        bool result = ExperienceBusiness.AddExperience(stuId, Exp_start_2, Exp_End_2, dr["Exp_School_2"].ToString());
                                        if (!result)
                                        {
                                            throw new Exception("学习经历添加失败！");
                                        }
                                    }
                                    if (!dr["Exp_School_3"].ToString().IsNullOrEmpty())
                                    {
                                        bool result = ExperienceBusiness.AddExperience(stuId, Exp_start_3, Exp_End_3, dr["Exp_School_3"].ToString());
                                        if (!result)
                                        {
                                            throw new Exception("学习经历添加失败！");
                                        }
                                    }
                                    if (!dr["Exp_School_4"].ToString().IsNullOrEmpty())
                                    {
                                        bool result = ExperienceBusiness.AddExperience(stuId, Exp_start_4, Exp_End_4, dr["Exp_School_4"].ToString());
                                        if (!result)
                                        {
                                            throw new Exception("学习经历添加失败！");
                                        }
                                    }
                                    if (!dr["Exp_School_5"].ToString().IsNullOrEmpty())
                                    {
                                        bool result = ExperienceBusiness.AddExperience(stuId, Exp_start_5, Exp_End_5, dr["Exp_School_5"].ToString());
                                        if (!result)
                                        {
                                            throw new Exception("学习经历添加失败！");
                                        }
                                    }
                                });
                                success++;
                                continue;
                            }
                            catch (Exception ex)
                            {
                                fail++;
                                sb_failIndex.Append((i + 3) + "." + dr["Name"].ToString() + "、");
                                text_importLog("第" + (i + 3) + "行（" + dr["Name"].ToString() + "）添加失败：" + ex.Message);
                                continue;
                            }
                        }
                        string txt_log = "";
                        if (sb_failIndex.Length > 0)
                        {
                            sb_failIndex.Remove(sb_failIndex.Length - 1, 1);
                            errorInfo = "，数据错误导致失败excel序号（包含标题行）为：" + sb_failIndex.ToString();
                        }
                        if (sb_repeatIndex.Length > 0)
                        {
                            sb_repeatIndex.Remove(sb_repeatIndex.Length - 1, 1);
                            errorInfo = "，数据重复导致失败excel序号（包含标题行）为：" + sb_repeatIndex.ToString();
                        }
                        RefreshView(0);
                        string showMsg = "导入完成！总共" + (success + fail + repeat) + "条，其中成功导入" + success + "条，数据错误导致失败" + fail + "条，数据重复导致失败" + repeat + "条" + errorInfo;
                        text_importLog("导入完成！总共" + (success + fail + repeat) + "条，其中成功导入" + success + "条，数据错误导致失败" + fail + "条，数据重复导致失败" + repeat + "条。" + txt_log);
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "提示", "layer.alert('导入详情已存入日志文件：" + @Server.MapPath("~/import_log.txt") + "。" + showMsg + "')", true);
                        //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "提示", "layer.alert('" + "导入完成！总共" + (success + fail) + "条，其中成功导入" + success + "条，失败导入" + fail + "条" + errorInfo + "')", true);
                        return;
                    }
                    catch (Exception ex)
                    {
                        text(ex.ToString());
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "提示", "layer.alert('无法上传！')", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "提示", "layer.alert('未识别的文件类型！请使用.xls或.xlsx文件进行导入！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "提示", "layer.alert('请选择文件！')", true);
                return;
            }
        }

        private DataTable GetNewDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Sex");
            dt.Columns.Add("IdentityNo");
            dt.Columns.Add("Birthday");
            dt.Columns.Add("School");
            dt.Columns.Add("Major");
            dt.Columns.Add("StudyTypeText");
            dt.Columns.Add("JoinSchoolDate");
            dt.Columns.Add("SystemOfEdu");
            dt.Columns.Add("PhoneNo");
            dt.Columns.Add("AddressOfH");
            dt.Columns.Add("AddressOfTW");
            dt.Columns.Add("Tip");
            dt.Columns.Add("Fa_Name_1");
            dt.Columns.Add("Fa_Sex_1");
            dt.Columns.Add("Fa_Birthday_1");
            dt.Columns.Add("Fa_Relationship_1");
            dt.Columns.Add("Fa_Company_1");
            dt.Columns.Add("Fa_Tip_1");
            dt.Columns.Add("Fa_Name_2");
            dt.Columns.Add("Fa_Sex_2");
            dt.Columns.Add("Fa_Birthday_2");
            dt.Columns.Add("Fa_Relationship_2");
            dt.Columns.Add("Fa_Company_2");
            dt.Columns.Add("Fa_Tip_2");
            dt.Columns.Add("Fa_Name_3");
            dt.Columns.Add("Fa_Sex_3");
            dt.Columns.Add("Fa_Birthday_3");
            dt.Columns.Add("Fa_Relationship_3");
            dt.Columns.Add("Fa_Company_3");
            dt.Columns.Add("Fa_Tip_3");
            dt.Columns.Add("Fa_Name_4");
            dt.Columns.Add("Fa_Sex_4");
            dt.Columns.Add("Fa_Birthday_4");
            dt.Columns.Add("Fa_Relationship_4");
            dt.Columns.Add("Fa_Company_4");
            dt.Columns.Add("Fa_Tip_4");
            dt.Columns.Add("Fa_Name_5");
            dt.Columns.Add("Fa_Sex_5");
            dt.Columns.Add("Fa_Birthday_5");
            dt.Columns.Add("Fa_Relationship_5");
            dt.Columns.Add("Fa_Company_5");
            dt.Columns.Add("Fa_Tip_5");
            dt.Columns.Add("Exp_StartTime_1");
            dt.Columns.Add("Exp_EndTime_1");
            dt.Columns.Add("Exp_School_1");
            dt.Columns.Add("Exp_StartTime_2");
            dt.Columns.Add("Exp_EndTime_2");
            dt.Columns.Add("Exp_School_2");
            dt.Columns.Add("Exp_StartTime_3");
            dt.Columns.Add("Exp_EndTime_3");
            dt.Columns.Add("Exp_School_3");
            dt.Columns.Add("Exp_StartTime_4");
            dt.Columns.Add("Exp_EndTime_4");
            dt.Columns.Add("Exp_School_4");
            dt.Columns.Add("Exp_StartTime_5");
            dt.Columns.Add("Exp_EndTime_5");
            dt.Columns.Add("Exp_School_5");
            return dt;
        }

        private DataTable GetDataTableForOutput()
        {
            string name = tb_AddName.Text.Trim();
            string school = ddl_School.SelectedValue;
            string sex = ddl_sex.SelectedValue;
            string startYear = input_start.Value;
            string endYear = input_end.Value;
            return StudentBusiness.GetStudentsAsDataTable(name, school, sex, startYear, endYear);
        }

        protected void btn_Output_Click(object sender, EventArgs e)
        {
            DataTable dt = GetDataTableForOutput();
            string TempletFileName = Server.MapPath("~/Template/导出模板（在沪台生信息）.xls");      //模板文件  
            string ReportFileName = Server.MapPath("~/Template/导出Excel(空).xls");    //导出文件  
            FileStream file = null;
            try
            {
                file = new FileStream(TempletFileName, FileMode.Open, FileAccess.Read);
            }
            catch (Exception)
            {
                Response.Write("<script>alert('模板文件不存在或正在打开');</script>");
                return;
            }
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            HSSFSheet ws = (HSSFSheet)hssfworkbook.GetSheet("Sheet1");
            if (ws == null)//工作薄中没有工作表
            {
                Response.Write("<script>alert('工作薄中没有Sheet1工作表');</script>");
                return;
            }
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    int _row = i + 2; //i + 1;前两行为标题
                    HSSFRow row = (HSSFRow)ws.CreateRow(_row);
                    //姓名
                    row.CreateCell(0).SetCellValue(dt.Rows[i]["Name"].ToString().Trim());
                    //性别
                    row.CreateCell(1).SetCellValue(Convert.ToBoolean(dt.Rows[i]["Sex"].ToString().Trim()) ? "女" : "男");
                    //证件号码
                    row.CreateCell(2).SetCellValue(dt.Rows[i]["IdentityNo"].ToString().Trim());
                    //出生年月
                    row.CreateCell(3).SetCellValue((Convert.ToDateTime(dt.Rows[i]["Birthday"].ToString().Trim())).ToString("yyyy/MM/dd"));
                    //就读学校
                    row.CreateCell(4).SetCellValue(dt.Rows[i]["School"].ToString().Trim());
                    //专业
                    row.CreateCell(5).SetCellValue(dt.Rows[i]["Major"].ToString().Trim());
                    //就读类别
                    row.CreateCell(6).SetCellValue(StudyType.FindOne(CK.K["StudyTypeId"] == dt.Rows[i]["StudyTypeId"].ToString().Trim() && CK.K["IsDelete"] == false).StudyTypeText);
                    //入学日期
                    row.CreateCell(7).SetCellValue((Convert.ToDateTime(dt.Rows[i]["JoinSchoolDate"].ToString().Trim())).ToString("yyyy/MM/dd"));
                    //学制
                    row.CreateCell(8).SetCellValue(dt.Rows[i]["SystemOfEdu"].ToString().Trim());
                    //联系电话
                    row.CreateCell(9).SetCellValue(dt.Rows[i]["PhoneNo"].ToString().Trim());
                    //在沪居住地
                    row.CreateCell(10).SetCellValue(dt.Rows[i]["AddressOfH"].ToString().Trim());
                    //在台户籍地
                    row.CreateCell(11).SetCellValue(dt.Rows[i]["AddressOfTW"].ToString().Trim());
                    //备注
                    row.CreateCell(12).SetCellValue(dt.Rows[i]["Tip"].ToString().Trim());

                    //家庭主要成员 13-18 19-24 25-30 31-36 37-42
                    DataTable dt_fa = FamilyOfStudentBusiness.GetFamilysAsDataTable(Convert.ToInt32(dt.Rows[i]["Id"].ToString()));
                    if (dt_fa != null && dt_fa.Rows.Count > 0)
                    {
                        int start_no = 13;//家庭主要成员第一列index
                        int k = 6;//总共几列
                        for (int j = 0; j < dt_fa.Rows.Count; j++)
                        {
                            //姓名
                            row.CreateCell(start_no + 0 + j * k).SetCellValue(dt_fa.Rows[i]["Name"].ToString().Trim());
                            //性别
                            row.CreateCell(start_no + 1 + j * k).SetCellValue(Convert.ToBoolean(dt_fa.Rows[i]["Sex"].ToString().Trim()) ? "女" : "男");
                            //出生年月
                            row.CreateCell(start_no + 2 + j * k).SetCellValue((Convert.ToDateTime(dt_fa.Rows[i]["Birthday"].ToString().Trim())).ToString("yyyy/MM/dd"));
                            //与本人关系
                            row.CreateCell(start_no + 3 + j * k).SetCellValue(dt_fa.Rows[i]["Relationship"].ToString().Trim());
                            //工作单位
                            row.CreateCell(start_no + 4 + j * k).SetCellValue(dt_fa.Rows[i]["Company"].ToString().Trim());
                            //备注
                            row.CreateCell(start_no + 5 + j * k).SetCellValue(dt_fa.Rows[i]["Tip"].ToString().Trim());
                        }
                    }

                    //学习经历 43-45 46-48 49-51 52-54 55-57
                    DataTable dt_exp = ExperienceBusiness.GetExperiencesAsDataTable(Convert.ToInt32(dt.Rows[i]["Id"].ToString()));
                    if (dt_exp != null && dt_exp.Rows.Count > 0)
                    {
                        int start_no = 43;//家庭主要成员第一列index
                        int k = 3;//总共几列
                        for (int j = 0; j < dt_exp.Rows.Count; j++)
                        {
                            //开始时间
                            row.CreateCell(start_no + 0 + j * k).SetCellValue((Convert.ToDateTime(dt_exp.Rows[i]["StartTime"].ToString().Trim())).ToString("yyyy/MM/dd"));
                            //结束时间
                            row.CreateCell(start_no + 1 + j * k).SetCellValue((Convert.ToDateTime(dt_exp.Rows[i]["EndTime"].ToString().Trim())).ToString("yyyy/MM/dd"));
                            //学校
                            row.CreateCell(start_no + 2 + j * k).SetCellValue(dt_exp.Rows[i]["School"].ToString().Trim());
                        }
                    }

                    //ws.SetColumnWidth(0, 12 * 256);//设置列宽
                    //ws.SetColumnWidth(1, 12 * 256);//设置列宽
                }
            }
            ws.ForceFormulaRecalculation = true;

            using (FileStream filess = File.OpenWrite(ReportFileName))
            {
                hssfworkbook.Write(filess);
            }
            System.IO.FileInfo filet = new System.IO.FileInfo(ReportFileName);
            Response.Clear();
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode("在沪台生数据导出.xls"));
            Response.AddHeader("Content-Length", filet.Length.ToString());
            Response.ContentType = "application/ms-excel";
            Response.WriteFile(filet.FullName);
            Response.End();
        }
    }
}