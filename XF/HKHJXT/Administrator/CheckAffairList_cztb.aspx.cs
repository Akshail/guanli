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
    public partial class CheckAffairList_cztb : PageBase
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
                //DataTable people = People.Find(CK.K["IsDelete"] == false).ToDataTable();
                //gv_Result.DataSource = people;
                //gv_Result.DataBind();
                RefreshView(0);
            }


        }

        private void RefreshView(int NewIndex)
        {
            try
            {
                int size = this.gv_Result.PageSize;
                string name = tb_AddName.Text.Trim();
                string sex = ddl_sex.SelectedValue;
                string comereason = ddl_ComeReason.SelectedValue;
                string birth_start = input_birth_start.Value;
                string birth_end = input_birth_end.Value;
                string sh_start = input_sh_start.Value;
                string sh_end = input_sh_end.Value;
                var ps = PeopleBusiness.GetPeoples(size, name, sex, comereason, birth_start, birth_end, sh_start, sh_end);
                this.gv_Result.PageIndex = NewIndex;
                this.gv_Result.DataSource = new PagedCollection<People>(ps, NewIndex);
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
            int id = PeopleBusiness.AddPeopleForId();
            Response.Redirect("./CheckAffairList_cztb_look.aspx?Id=" + id + "&isNew=1");
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
                e.Row.Cells[5].Text = e.Row.Cells[5].Text.Equals("&nbsp;") ? "" : Convert.ToDateTime(e.Row.Cells[5].Text).ToString("yyyy/MM/dd");
            }
        }

        protected void gv_Result_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Del"))
            {
                string id = e.CommandArgument.ToString();
                bool result = PeopleBusiness.deletePeople(Int32.Parse(id));
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('删除失败，请重新再试！')", true);
                    return;
                }
                LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 3, "常住台胞信息", log_ip, log_account);//删除添加日志 操作类型->3
                RefreshView(0);
            }
        }
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            RefreshView(0);
        }

        protected void btn_Download_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string fileName = "导入模板（常驻台胞信息）.xls";//客户端保存的文件名
            //    string filePath = Server.MapPath("../../Template/导入模板（常驻台胞信息）.xls");//路径

            //    FileInfo fileInfo = new FileInfo(filePath);
            //    Response.Clear();
            //    Response.ClearContent();
            //    Response.ClearHeaders();
            //    Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            //    Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            //    Response.AddHeader("Content-Transfer-Encoding", "binary");
            //    Response.ContentType = "application/octet-stream";
            //    Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            //    Response.WriteFile(fileInfo.FullName);
            //    Response.Flush();
            //    Response.End();
            //}
            //catch (Exception ex)
            //{
            //    //X.Msg.Alert("提示", "加载错误，请重新尝试！").Show();
            //    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);
            //}

            //try
            //{
            //    long lngFileSize;
            //    byte[] bytBuffer;
            //    int iReading;
            //    string sFileName = Server.MapPath("../../Template/导入模板（常驻台胞信息）.xls");
            //    Stream outStream = Response.OutputStream;//get output stream

            //    //set htttp header
            //    Response.ContentType = "application/Zip";
            //    Response.AppendHeader("Connection", "close");
            //    Response.AppendHeader("Content-Disposition", "  attachment;  filename  =  导入模板（常驻台胞信息）.xls");//default file name when download
            //    FileStream fStream = new FileStream(sFileName, FileMode.OpenOrCreate, FileAccess.Read);
            //    lngFileSize = fStream.Length;
            //    bytBuffer = new byte[(int)lngFileSize];
            //    while ((iReading = fStream.Read(bytBuffer, 0, (int)lngFileSize)) > 0)
            //    {
            //        outStream.Write(bytBuffer, 0, iReading);
            //    }
            //    fStream.Close();
            //    outStream.Close();
            //    Response.End();
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);
            //}

            Response.ContentType = "application/x-zip-compressed";
            Response.AddHeader("Content-Disposition", "attachment;filename=导入模板（常驻台胞信息）.xls");
            string filename = Server.MapPath("../../Template/导入模板（常驻台胞信息）.xls");
            Response.TransmitFile(filename);

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
                        //long strLen = btn_upload.PostedFile.ContentLength;//字节
                        //if (strLen >= sizes)
                        //{
                        //    ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "alert('上传照片不能大于5M！')", true);
                        //    return;
                        //}
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
                            if (People.FindOne(CK.K["Name"] == dr["Name"].ToString() && CK.K["IdentityNo"] == dr["IdentityNo"].ToString() && CK.K["IsDelete"] == false) != null)
                            {
                                repeat++;
                                sb_repeatIndex.Append((i + 3) + "." + dr["Name"].ToString() + "、");
                                continue;
                            }

                            StringBuilder sb_DateLostInfo = new StringBuilder();//日期无法确定，填写为1900/01/01，需添加备注
                            //基本信息
                            bool sex = false;
                            DateTime birthday = new DateTime();
                            DateTime arriveDate = new DateTime();
                            DateTime validityOfTW = new DateTime();
                            int houseType = 0;
                            int comeReason = 0;
                            string tw_address = dr["AddressOfTW"].ToString();
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
                            //探亲
                            DateTime visi_ArriveTime = new DateTime();
                            DateTime visi_LeaveTime = new DateTime();
                            try//检验输入是否规范
                            {
                                //基本信息
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
                                arriveDate = Convert.ToDateTime(dr["ArriveDate"].ToString());
                                if (arriveDate.Equals(new DateTime(1900, 1, 1)))
                                {
                                    sb_DateLostInfo.Append("'" + dr["Name"].ToString() + "'来沪日期无法查实，");
                                }
                                validityOfTW = Convert.ToDateTime(dr["ValidityOfTW"].ToString());
                                if (validityOfTW.Equals(new DateTime(1900, 1, 1)))
                                {
                                    sb_DateLostInfo.Append("'" + dr["Name"].ToString() + "'台胞证有效日期无法查实，");
                                }
                                switch (dr["HouseType"].ToString())
                                {
                                    case "自购产权房":
                                        houseType = 1;
                                        break;
                                    case "租赁房":
                                        houseType = 2;
                                        break;
                                    case "住亲朋家":
                                        houseType = 3;
                                        break;
                                    case "单位（学校）宿舍":
                                        houseType = 4;
                                        break;
                                    case "其它":
                                        houseType = 5;
                                        break;
                                    default:
                                        throw new Exception("现住房性质输入错误！");
                                }
                                switch (dr["ComeReason"].ToString())
                                {
                                    case "投资经商":
                                        comeReason = 1;
                                        break;
                                    case "就读":
                                        comeReason = 2;
                                        break;
                                    case "工作":
                                        comeReason = 3;
                                        break;
                                    case "依亲":
                                        comeReason = 4;
                                        break;
                                    case "养老":
                                        comeReason = 5;
                                        break;
                                    case "探亲":
                                        comeReason = 6;
                                        break;
                                    case "其它":
                                        comeReason = 7;
                                        break;
                                    default:
                                        throw new Exception("来沪目的输入错误！");

                                }
                                //if (tw_address.Length <= 3 || !tw_address.Substring(0, 3).Equals("台湾省") || (TaiWanCity.FindOne(CK.K["CityName"] == tw_address.Substring(3, tw_address.Length - 3)) == null))
                                //{
                                //    throw new Exception("台湾户籍地输入错误!");
                                //}

                                //家庭主要成员
                                if (!dr["Fa_Name_1"].ToString().IsNullOrEmpty())
                                {
                                    if (dr["Fa_Sex_1"].Equals("男") || dr["Fa_Sex_1"].Equals("女"))
                                    {
                                        fa_sex_1 = dr["Fa_Sex_1"].Equals("女");//女true   男false
                                    }
                                    else
                                    {
                                        throw new Exception("子女情况性别输入错误!");
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
                                        throw new Exception("子女情况性别输入错误!");
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
                                        throw new Exception("子女情况性别输入错误!");
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
                                        throw new Exception("子女情况性别输入错误!");
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
                                        throw new Exception("子女情况性别输入错误!");
                                    }
                                    fa_birthday_5 = Convert.ToDateTime(dr["Fa_Birthday_5"].ToString());
                                    if (fa_birthday_5.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Fa_Name_5"].ToString() + "'出生日期无法查实，");
                                    }
                                }
                                if (houseType == 6)
                                {
                                    visi_ArriveTime = Convert.ToDateTime(dr["Visi_ArriveTime"].ToString());
                                    if (visi_ArriveTime.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Name"].ToString() + "'探亲日期（抵）无法查实，");
                                    }
                                    visi_LeaveTime = Convert.ToDateTime(dr["Visi_LeaveTime"].ToString());
                                    if (visi_LeaveTime.Equals(new DateTime(1900, 1, 1)))
                                    {
                                        sb_DateLostInfo.Append("'" + dr["Name"].ToString() + "'探亲日期（离）无法查实，");
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
                                    int people_id = PeopleBusiness.AddPeopleForImport(dr["Name"].ToString(), sex, arriveDate, birthday, dr["IdentityNo"].ToString(), dr["PhoneNo"].ToString(), dr["Address"].ToString(), dr["TempApplication"].ToString(), comeReason, sb_DateLostInfo.ToString() + dr["Tip"].ToString());
                                    LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 2, "教职工信息", log_ip, log_account);//新增添加日志 操作类型->2
                                    BusinessBusiness.AddBusinessNull(people_id);
                                    AgedBusiness.AddAgedNull(people_id);
                                    RelativeOfPeopleBusiness.AddRelativeNull(people_id);
                                    StudyBusiness.AddStudyNull(people_id);
                                    VisitBusiness.AddVisitNull(people_id);
                                    WorkBusiness.AddWorkNull(people_id);
                                    switch (comeReason)
                                    {
                                        case 1:
                                            BusinessBusiness.UpdateBusinessByPeople(people_id, dr["Busi_Scope"].ToString(), dr["Busi_Type"].ToString(), dr["Busi_RegisteredCapital"].ToString(), dr["Busi_RegisteredPlace"].ToString(), dr["Busi_Name"].ToString(), dr["Busi_Address"].ToString(), dr["Busi_PhoneNo"].ToString(), dr["Busi_Postalcode"].ToString());
                                            break;
                                        case 2:
                                            StudyBusiness.UpdateStudyByPeople(people_id, dr["Stud_School"].ToString(), dr["Stud_Grade"].ToString(), dr["Stud_Major"].ToString(), dr["Stud_Protector"].ToString(), dr["Stud_Relationship"].ToString(), dr["Stud_PhoneNo"].ToString());
                                            break;
                                        case 3:
                                            WorkBusiness.UpdateWorkByPeople(people_id, dr["Work_Company"].ToString(), dr["Work_Post"].ToString(), dr["Work_Address"].ToString(), dr["Work_Type"].ToString());
                                            break;
                                        case 4:
                                            RelativeOfPeopleBusiness.UpdateRelativeByPeople(people_id, dr["Rela_Name"].ToString(), dr["Rela_PhoneNo"].ToString(), dr["Rela_WorkingCondition"].ToString(), dr["Rela_Company"].ToString(), dr["Rela_Relationship"].ToString());
                                            break;
                                        case 5:
                                            AgedBusiness.UpdateAgedByPeople(people_id, dr["Age_Health"].ToString(), dr["Age_Tendance"].ToString(), dr["Age_Relationship"].ToString(), dr["Age_Phone"].ToString(), dr["Age_Type"].ToString());
                                            break;
                                        case 6:
                                            VisitBusiness.UpdateVisitByPeople(people_id, dr["Visi_Name"].ToString(), dr["Visi_Relationship"].ToString(), visi_ArriveTime, visi_LeaveTime, dr["Visi_Address"].ToString(), dr["Visi_Phone"].ToString());
                                            break;
                                    }
                                    if (!dr["Fa_Name_1"].ToString().IsNullOrEmpty())
                                    {
                                        bool result = FamilyOfPeopleBusiness.AddFamilyOfPeople(people_id, dr["Fa_Name_1"].ToString(), fa_sex_1, fa_birthday_1, dr["Fa_Relationship_1"].ToString(), dr["Fa_Company_1"].ToString(), dr["Fa_Tip_1"].ToString());
                                        if (!result)
                                        {
                                            throw new Exception("家庭主要成员添加失败！");
                                        }
                                    }
                                    if (!dr["Fa_Name_2"].ToString().IsNullOrEmpty())
                                    {
                                        bool result = FamilyOfPeopleBusiness.AddFamilyOfPeople(people_id, dr["Fa_Name_2"].ToString(), fa_sex_2, fa_birthday_2, dr["Fa_Relationship_2"].ToString(), dr["Fa_Company_2"].ToString(), dr["Fa_Tip_2"].ToString());
                                        if (!result)
                                        {
                                            throw new Exception("家庭主要成员添加失败！");
                                        }
                                    }
                                    if (!dr["Fa_Name_3"].ToString().IsNullOrEmpty())
                                    {
                                        bool result = FamilyOfPeopleBusiness.AddFamilyOfPeople(people_id, dr["Fa_Name_3"].ToString(), fa_sex_3, fa_birthday_3, dr["Fa_Relationship_3"].ToString(), dr["Fa_Company_3"].ToString(), dr["Fa_Tip_3"].ToString());
                                        if (!result)
                                        {
                                            throw new Exception("家庭主要成员添加失败！");
                                        }
                                    }
                                    if (!dr["Fa_Name_4"].ToString().IsNullOrEmpty())
                                    {
                                        bool result = FamilyOfPeopleBusiness.AddFamilyOfPeople(people_id, dr["Fa_Name_4"].ToString(), fa_sex_4, fa_birthday_4, dr["Fa_Relationship_4"].ToString(), dr["Fa_Company_4"].ToString(), dr["Fa_Tip_4"].ToString());
                                        if (!result)
                                        {
                                            throw new Exception("家庭主要成员添加失败！");
                                        }
                                    }
                                    if (!dr["Fa_Name_5"].ToString().IsNullOrEmpty())
                                    {
                                        bool result = FamilyOfPeopleBusiness.AddFamilyOfPeople(people_id, dr["Fa_Name_5"].ToString(), fa_sex_5, fa_birthday_5, dr["Fa_Relationship_5"].ToString(), dr["Fa_Company_5"].ToString(), dr["Fa_Tip_5"].ToString());
                                        if (!result)
                                        {
                                            throw new Exception("家庭主要成员添加失败！");
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
                            errorInfo = "，失败excel序号（包含标题行）为：" + sb_failIndex.ToString();
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
            dt.Columns.Add("Birthday");
            dt.Columns.Add("IdentityNo");
            dt.Columns.Add("ArriveDate");
            dt.Columns.Add("PhoneNo");
            dt.Columns.Add("TWIDNo");
            dt.Columns.Add("ValidityOfTW");
            dt.Columns.Add("OccupationOfTW");
            dt.Columns.Add("CompanyOfTW");
            dt.Columns.Add("PostOfTW");
            dt.Columns.Add("AddressOfTW");
            dt.Columns.Add("Address");
            dt.Columns.Add("TempApplication");
            dt.Columns.Add("HouseType");
            dt.Columns.Add("ComeReason");
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
            dt.Columns.Add("Busi_Scope");
            dt.Columns.Add("Busi_Type");
            dt.Columns.Add("Busi_RegisteredCapital");
            dt.Columns.Add("Busi_RegisteredPlace");
            dt.Columns.Add("Busi_Name");
            dt.Columns.Add("Busi_Address");
            dt.Columns.Add("Busi_PhoneNo");
            dt.Columns.Add("Busi_Postalcode");
            dt.Columns.Add("Stud_School");
            dt.Columns.Add("Stud_Grade");
            dt.Columns.Add("Stud_Major");
            dt.Columns.Add("Stud_Protector");
            dt.Columns.Add("Stud_Relationship");
            dt.Columns.Add("Stud_PhoneNo");
            dt.Columns.Add("Work_Company");
            dt.Columns.Add("Work_Post");
            dt.Columns.Add("Work_Address");
            dt.Columns.Add("Work_Type");
            dt.Columns.Add("Rela_Name");
            dt.Columns.Add("Rela_PhoneNo");
            dt.Columns.Add("Rela_WorkingCondition");
            dt.Columns.Add("Rela_Company");
            dt.Columns.Add("Rela_Relationship");
            dt.Columns.Add("Age_Health");
            dt.Columns.Add("Age_Type");
            dt.Columns.Add("Age_Tendance");
            dt.Columns.Add("Age_Relationship");
            dt.Columns.Add("Age_Phone");
            dt.Columns.Add("Visi_Name");
            dt.Columns.Add("Visi_Relationship");
            dt.Columns.Add("Visi_ArriveTime");
            dt.Columns.Add("Visi_LeaveTime");
            dt.Columns.Add("Visi_Address");
            dt.Columns.Add("Visi_Phone");
            return dt;
        }
        private DataTable GetDataTableForOutput()
        {
            string name = tb_AddName.Text.Trim();
            string sex = ddl_sex.SelectedValue;
            string comereason = ddl_ComeReason.SelectedValue;
            string birth_start = input_birth_start.Value;
            string birth_end = input_birth_end.Value;
            string sh_start = input_sh_start.Value;
            string sh_end = input_sh_end.Value;
            return PeopleBusiness.GetPeoplesAsDataTable(name, sex, comereason, birth_start, birth_end, sh_start, sh_end);
        }

        protected void btn_Output_Click(object sender, EventArgs e)
        {
            DataTable dt = GetDataTableForOutput();
            string TempletFileName = Server.MapPath("~/Template/导出模板（常驻台胞信息）.xls");      //模板文件  
            string ReportFileName = Server.MapPath("~/Template/导出Excel(空).xls");    //导出文件  
            FileStream file = null;
            try
            {
                file = new FileStream(TempletFileName, FileMode.Open, FileAccess.Read);
            }
            catch (Exception)
            {
                Response.Write("<script>alert('模板文件不存在或正在打开，请稍后再试！');</script>");
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
                    //台胞姓名
                    row.CreateCell(0).SetCellValue(dt.Rows[i]["Name"].ToString().Trim());
                    //性别
                    row.CreateCell(1).SetCellValue(Convert.ToBoolean(dt.Rows[i]["Sex"].ToString().Trim()) ? "女" : "男");
                    //出生年月
                    row.CreateCell(2).SetCellValue((Convert.ToDateTime(dt.Rows[i]["Birthday"].ToString().Trim())).ToString("yyyy/MM/dd"));
                    //身份证号
                    row.CreateCell(3).SetCellValue(dt.Rows[i]["IdentityNo"].ToString().Trim());
                    //来沪时间
                    row.CreateCell(4).SetCellValue((Convert.ToDateTime(dt.Rows[i]["ArriveDate"].ToString().Trim())).ToString("yyyy/MM/dd"));
                    //联系电话
                    row.CreateCell(5).SetCellValue(dt.Rows[i]["PhoneNo"].ToString().Trim());
                    //台胞证号
                    row.CreateCell(6).SetCellValue(dt.Rows[i]["TWIDNo"].ToString().Trim());
                    //台胞证有效期
                    row.CreateCell(7).SetCellValue((Convert.ToDateTime(dt.Rows[i]["ValidityOfTW"].ToString().Trim())).ToString("yyyy/MM/dd"));
                    //在台职业
                    row.CreateCell(8).SetCellValue(dt.Rows[i]["OccupationOfTW"].ToString().Trim());
                    //在台单位
                    row.CreateCell(9).SetCellValue(dt.Rows[i]["CompanyOfTW"].ToString().Trim());
                    //职务
                    row.CreateCell(10).SetCellValue(dt.Rows[i]["PostOfTW"].ToString().Trim());
                    //台湾户籍地及地址
                    row.CreateCell(11).SetCellValue(dt.Rows[i]["AddressOfTW"].ToString().Trim());
                    //现住址
                    row.CreateCell(12).SetCellValue(dt.Rows[i]["Address"].ToString().Trim());
                    //临时户口申报地
                    row.CreateCell(13).SetCellValue(dt.Rows[i]["TempApplication"].ToString().Trim());
                    //现住房性质
                    row.CreateCell(14).SetCellValue(GetHouseTypeText(Convert.ToInt32(dt.Rows[i]["HouseType"].ToString().Trim())));
                    //来沪目的
                    row.CreateCell(15).SetCellValue(GetComeReasonText(Convert.ToInt32(dt.Rows[i]["ComeReason"].ToString().Trim())));
                    //备注
                    row.CreateCell(16).SetCellValue(dt.Rows[i]["Tip"].ToString().Trim());

                    //家庭主要成员 17-22 23-28 29-34 35-40 41-46
                    DataTable dt_fa = FamilyOfPeopleBusiness.GetFamilysAsDataTable(Convert.ToInt32(dt.Rows[i]["Id"].ToString()));
                    if (dt_fa != null && dt_fa.Rows.Count > 0)
                    {
                        int start_no = 17;//家庭主要成员第一列index
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

                    switch (Convert.ToInt32(dt.Rows[i]["ComeReason"].ToString()))
                    {
                        case 1:
                            //投资经商 47-54
                            Business bus = Business.FindOne(CK.K["People_Id"] == dt.Rows[i]["Id"].ToString().Trim() && CK.K["IsDelete"] == false);
                            if (bus != null)
                            {
                                int start_no = 47;//投资经商第一列index

                                //经营范围
                                row.CreateCell(start_no + 0).SetCellValue(bus.Scope);
                                //企业性质
                                row.CreateCell(start_no + 1).SetCellValue(bus.Type);
                                //注册资金
                                row.CreateCell(start_no + 2).SetCellValue(bus.RegisteredCapital);
                                //注册地
                                row.CreateCell(start_no + 3).SetCellValue(bus.RegisteredPlace);
                                //企业名称
                                row.CreateCell(start_no + 4).SetCellValue(bus.Name);
                                //企业地址
                                row.CreateCell(start_no + 5).SetCellValue(bus.Address);
                                //企业电话
                                row.CreateCell(start_no + 6).SetCellValue(bus.PhoneNo);
                                //邮政编码
                                row.CreateCell(start_no + 7).SetCellValue(bus.Postalcode);
                            }
                            break;
                        case 2:
                            //就读 55-60
                            Study stu = Study.FindOne(CK.K["People_Id"] == dt.Rows[i]["Id"].ToString().Trim() && CK.K["IsDelete"] == false);
                            if (stu != null)
                            {
                                int start_no = 55;//就读第一列index

                                //就读学校
                                row.CreateCell(start_no + 0).SetCellValue(stu.School);
                                //年级
                                row.CreateCell(start_no + 1).SetCellValue(stu.Grade);
                                //专业
                                row.CreateCell(start_no + 2).SetCellValue(stu.Major);
                                //监护人
                                row.CreateCell(start_no + 3).SetCellValue(stu.Protector);
                                //与监护人关系
                                row.CreateCell(start_no + 4).SetCellValue(stu.Relationship);
                                //监护人联系电话
                                row.CreateCell(start_no + 5).SetCellValue(stu.PhoneNo);
                            }
                            break;
                        case 3:
                            //工作 61-64
                            Work work = Work.FindOne(CK.K["People_Id"] == dt.Rows[i]["Id"].ToString().Trim() && CK.K["IsDelete"] == false);
                            if (work != null)
                            {
                                int start_no = 61;//工作第一列index

                                //受聘（雇）单位
                                row.CreateCell(start_no + 0).SetCellValue(work.Company);
                                //职务
                                row.CreateCell(start_no + 1).SetCellValue(work.Post);
                                //单位地址
                                row.CreateCell(start_no + 2).SetCellValue(work.Address);
                                //单位性质
                                row.CreateCell(start_no + 3).SetCellValue(work.Type);
                            }
                            break;
                        case 4:
                            //依亲 65-69
                            Relative rel = Relative.FindOne(CK.K["People_Id"] == dt.Rows[i]["Id"].ToString().Trim() && CK.K["IsDelete"] == false);
                            if (rel != null)
                            {
                                int start_no = 65;//依亲第一列index

                                //户籍亲人姓名
                                row.CreateCell(start_no + 0).SetCellValue(rel.Name);
                                //联系电话
                                row.CreateCell(start_no + 1).SetCellValue(rel.PhoneNo);
                                //工作状况
                                row.CreateCell(start_no + 2).SetCellValue(rel.WorkingCondition);
                                //工作单位
                                row.CreateCell(start_no + 3).SetCellValue(rel.Company);
                                //与户籍亲人关系
                                row.CreateCell(start_no + 4).SetCellValue(rel.Relationship);
                            }
                            break;
                        case 5:
                            //养老 70-74
                            Aged age = Aged.FindOne(CK.K["People_Id"] == dt.Rows[i]["Id"].ToString().Trim() && CK.K["IsDelete"] == false);
                            if (age != null)
                            {
                                int start_no = 70;//依亲第一列index

                                //身体状况
                                row.CreateCell(start_no + 0).SetCellValue(age.Health);
                                //养老形式
                                row.CreateCell(start_no + 1).SetCellValue(age.Type);
                                //照顾人
                                row.CreateCell(start_no + 2).SetCellValue(age.Tendance);
                                //与养老人关系
                                row.CreateCell(start_no + 3).SetCellValue(age.Relationship);
                                //联系电话
                                row.CreateCell(start_no + 4).SetCellValue(age.Phone);
                            }
                            break;
                        case 6:
                            //探亲 75-80
                            Visit vis = Visit.FindOne(CK.K["People_Id"] == dt.Rows[i]["Id"].ToString().Trim() && CK.K["IsDelete"] == false);
                            if (vis != null)
                            {
                                int start_no = 75;//依亲第一列index

                                //被探亲人
                                row.CreateCell(start_no + 0).SetCellValue(vis.Name);
                                //与探亲人关系
                                row.CreateCell(start_no + 1).SetCellValue(vis.Relationship);
                                //探亲时间（抵）
                                row.CreateCell(start_no + 2).SetCellValue((Convert.ToDateTime(vis.ArriveTime)).ToString("yyyy/MM/dd"));
                                //探亲时间（离）
                                row.CreateCell(start_no + 3).SetCellValue((Convert.ToDateTime(vis.LeaveTime)).ToString("yyyy/MM/dd"));
                                //被探亲人地址
                                row.CreateCell(start_no + 4).SetCellValue(vis.Address);
                                //被探亲人联系电话
                                row.CreateCell(start_no + 5).SetCellValue(vis.Phone);
                            }
                            break;
                        default:
                            //7其他
                            break;
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
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode("常驻台胞数据导出.xls"));
            Response.AddHeader("Content-Length", filet.Length.ToString());
            Response.ContentType = "application/ms-excel";
            Response.WriteFile(filet.FullName);
            Response.End();
        }

        private string GetHouseTypeText(int houseTypeId)
        {
            string text = "";
            switch (houseTypeId)
            {
                case 1:
                    text = "自购产权房";
                    break;
                case 2:
                    text = "租赁房";
                    break;
                case 3:
                    text = "住亲朋家";
                    break;
                case 4:
                    text = "单位（学校）宿舍";
                    break;
                case 5:
                    text = "其它";
                    break;
            }
            return text;
        }

        private string GetComeReasonText(int comeReasonId)
        {
            string text = "";
            switch (comeReasonId)
            {
                case 1:
                    text = "投资经商";
                    break;
                case 2:
                    text = "就读";
                    break;
                case 3:
                    text = "工作";
                    break;
                case 4:
                    text = "依亲";
                    break;
                case 5:
                    text = "养老";
                    break;
                case 6:
                    text = "探亲";
                    break;
                case 7:
                    text = "其它";
                    break;
            }
            return text;
        }
    }
}