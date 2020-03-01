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
    public partial class CheckAffairList_zxgz_Add : PageBase
    {
        private int communicationId
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

        private string IsNew
        {
            get
            {
                try
                {
                    return Request.QueryString["IsNew"].ToString();
                }
                catch
                {
                    return null;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (communicationId == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
                    return;
                }
                Bind_basic();
                if (IsNew == null)//修改
                {
                    Button4.Enabled = true;
                    btn_submit.Text = "确认修改";
                    //回填
                    try
                    {
                        SpecialWork com = SpecialWork.FindById(communicationId);
                        tb_Exchange.Text = com.Exchange;
                        ddl_SpecialWorkType.SelectedValue = com.SpecialWorkTypeId.ToString();
                        if (com.WorkStartDate != null)
                        {
                            tb_workStartDate.Value = ((DateTime)com.WorkStartDate).ToString("yyyy/MM/dd");
                        }
                        if (com.WorkEndDate != null)
                        {
                            tb_workEndDate.Value = ((DateTime)com.WorkEndDate).ToString("yyyy/MM/dd");
                        }
                        tb_Type.Text = com.Type;
                        tb_Name.Text = com.Name;
                        if ((bool)com.Sex)// true女性 false男性
                        {
                            Radio_woman.Checked = true;
                        }
                        else
                        {
                            Radio_man.Checked = true;
                        }
                        if (com.Birthday != null)
                        {
                            tb_birth.Value = ((DateTime)com.Birthday).ToString("yyyy/MM/dd");
                        }

                        tb_company.Text = com.Company;
                        tb_address.Text = com.Address;
                        tb_phoneNo.Text = com.PhoneNo;
                        tb_ballot.Text = com.Ballot.ToString();
                        if ((bool)com.IsReelect)// 是否连任
                        {
                            Radio_IsReselect_Yes.Checked = true;
                        }
                        else
                        {
                            Radio_IsReselect_No.Checked = true;
                        }
                        tb_Background.Text = com.Background;
                        tb_Situation.Text = com.Situation;
                        if (!com.PhotoName.IsNullOrEmpty())//照片
                        {
                            Photo.ImageUrl = "../../upload/photos/" + com.PhotoName;
                        }

                        Bind_Situation(0);
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
            DataTable dt_SpecialWorkType = SpecialWorkType.Find(CK.K["IsDelete"] == false).ToDataTable();
            ddl_SpecialWorkType.DataSource = dt_SpecialWorkType;
            ddl_SpecialWorkType.DataValueField = "SpecialWorkTypeId";
            ddl_SpecialWorkType.DataTextField = "SpecialWorkTypeText";
            ddl_SpecialWorkType.DataBind();
            ddl_SpecialWorkType.Items.Insert(0, new ListItem("请选择", "0"));

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

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CheckAffairList_zxgz.aspx");
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                //所有表单数据
                string exchange = tb_Exchange.Text;
                string type = tb_Type.Text;
                string name = tb_Name.Text;
                bool sex = Radio_woman.Checked;//sex  true女  false男
                DateTime birth = new DateTime(1900, 1, 1);
                int specialWorkTypeId = Convert.ToInt32(ddl_SpecialWorkType.SelectedValue);
                DateTime workStartDate = new DateTime(1900, 1, 1);
                DateTime workEndDate = new DateTime(1900, 1, 1);
                if (specialWorkTypeId == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('请选择类别！')", true);
                    return;
                }
                try
                {
                    birth = tb_birth.Value.Equals("") ? birth : Convert.ToDateTime(tb_birth.Value);
                    workStartDate = tb_workStartDate.Value.Equals("") ? workStartDate : Convert.ToDateTime(tb_workStartDate.Value);
                    workEndDate = tb_workEndDate.Value.Equals("") ? workEndDate : Convert.ToDateTime(tb_workEndDate.Value);
                }
                catch (Exception ex)
                {
                    text(ex.ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('日期输入格式不规范，请重新输入！')", true);
                    return;
                }
                string company = tb_company.Text;
                string address = tb_address.Text;
                string phoneNo = tb_phoneNo.Text;
                double ballot = 0;
                try
                {
                    ballot = tb_ballot.Text.ToString().IsNullOrEmpty() ? 0 : Convert.ToDouble(tb_ballot.Text);
                }
                catch (Exception ex)
                {
                    text(ex.ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('当选得票数率输入格式不规范，请重新输入！')", true);
                    return;
                }
                bool isReselect = Radio_IsReselect_Yes.Checked;
                string background = tb_Background.Text;
                string situation = tb_Situation.Text;
                if (communicationId != 0)
                {
                    bool result = false;
                    if (IsNew == null)//修改
                    {
                        result = SpecialWorkBusiness.UpdateCommunication(communicationId, exchange, specialWorkTypeId, workStartDate, workEndDate, type, name, sex, birth, company, address, phoneNo, ballot, isReselect, background, situation);
                        LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 4, "专项工作信息", log_ip, log_account);//修改添加日志 操作类型->4
                    }
                    else//新增
                    {
                        result = SpecialWorkBusiness.UpdateCommunicationForNew(communicationId, exchange, specialWorkTypeId, workStartDate, workEndDate, type, name, sex, birth, company, address, phoneNo, ballot, isReselect, background, situation);
                        LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 2, "专项工作信息", log_ip, log_account);//新增添加日志 操作类型->2
                    }
                    if (!result)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('保存失败，请重新再试！')", true);
                        return;
                    }
                    Response.Redirect("CheckAffairList_zxgz.aspx");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                text(ex.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
            }
        }

        //照片上传
        protected void btn_upload_Click(object sender, EventArgs e)
        {
            if (communicationId == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
                return;
            }
            string fileExtension = null;
            string file = FileUpload_Photo.FileName;
            string filename = "ZXGZ-" + communicationId + "-" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss").Replace(" ", "").Replace("/", "").Replace(":", "").Replace("-", "");
            bool fileOK = false;
            string savepath = Server.MapPath("~/upload/photos/");
            //判断是否已选择上传文件
            if (FileUpload_Photo.HasFile)
            {
                //获得文件后缀
                fileExtension = System.IO.Path.GetExtension(FileUpload_Photo.FileName).ToLower();
                //允许上传文件后缀
                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
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
                        long strLen = FileUpload_Photo.PostedFile.ContentLength;
                        if (strLen >= 5 * 1024 * 1024)
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "alert('上传照片不能大于5M！')", true);
                            return;
                        }
                        if (!Directory.Exists(savepath))
                        {
                            Directory.CreateDirectory(savepath);
                        }
                        FileUpload_Photo.PostedFile.SaveAs(savepath + filename);

                        string photoUrl = "../../upload/photos/" + filename;
                        Photo.ImageUrl = "../../upload/photos/" + filename;
                        //照片
                        SpecialWork com = SpecialWork.FindById(communicationId);
                        com.PhotoName = filename;
                        com.Save();
                        ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "alert('上传成功！')", true);
                    }
                    catch (Exception ex)
                    {
                        text(ex.ToString());
                        ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "alert('无法上传！')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "alert('未识别的文件类型！')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "alert('请选择文件！')", true);
            }
        }

        protected void btn_Refresh_Click(object sender, EventArgs e)
        {
            Bind_Situation(0);
        }

        public void Bind_Situation(int NewIndex)
        {
            //DataTable dt = Situation.Find(CK.K["Communication_Id"] == communicationId && CK.K["IsDelete"] == false).ToDataTable();
            //gv_Situation.DataSource = dt;
            //gv_Situation.DataBind();
            try
            {
                int size = this.gv_Situation.PageSize;
                var ps = SituationToSpecialWorkBusiness.GetSituations(communicationId, size);
                this.gv_Situation.PageIndex = NewIndex;
                this.gv_Situation.DataSource = new PagedCollection<SituationToSpecialWork>(ps, NewIndex);
                this.gv_Situation.DataBind();
                this.label_ResultCount.Text = ps.GetResultCount().ToString();
            }
            catch (Exception ex)
            {
                text(ex.ToString());
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);
            }
        }

        protected void btn_Add_jlqk_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "btn_Add_jlqk_Click('添加交流情况记录','./CheckAffairList_zxgz_jlqk_Add.aspx?communicationId=" + communicationId + "')", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "btn_Add_jlqk_Click('添加交流情况记录')", true);
        }

        protected void gv_Situation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string command = e.CommandName;
            int id = Convert.ToInt32(e.CommandArgument);
            if (command.Equals("Upd"))
            {
                ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "btn_Add_jlqk_Click('添加交流情况记录','./CheckAffairList_zxgz_jlqk_Add.aspx?situationId=" + id + "')", true);
            }
            else if (command.Equals("Del"))
            {
                bool result = SituationBusiness.DeleteSituation(id);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('删除失败，请重新再试！')", true);
                    return;
                }
                Bind_Situation(0);
            }
        }

        protected void gv_Situation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[1].Text = Convert.ToDateTime(e.Row.Cells[1].Text).ToString("yyyy/MM/dd");
                }
            }
            catch (Exception ex)
            {
                text(ex.ToString());
                ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "alert('时间格式错误！')", true);
            }
        }

        protected void gv_Situation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Bind_Situation(e.NewPageIndex);
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            Output_Word_zdts();
        }
        public void Output_Word_zdts()
        {
            try
            {
                if (communicationId == 0)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
                    return;
                }
                int selectedYear = Convert.ToInt32(ddl_year.SelectedValue);

                SpecialWork com = SpecialWork.FindById(communicationId);
                string basePath = Server.MapPath("../../");
                Document doc = new Document(basePath + "Template/专项工作情况表.doc");
                //对口交流地区
                Bookmark bookmark = doc.Range.Bookmarks["Exchange"];
                bookmark.Text = com.Exchange == null ? "" : com.Exchange;
                //交流对象类别
                bookmark = doc.Range.Bookmarks["Type"];
                bookmark.Text = com.Type == null ? "" : com.Type;
                //姓名
                bookmark = doc.Range.Bookmarks["Name"];
                bookmark.Text = com.Name == null ? "" : com.Name;
                //性别
                bookmark = doc.Range.Bookmarks["Sex"];
                bookmark.Text = com.Sex == null ? "" : (bool)com.Sex ? "女" : "男";
                //出生年月
                bookmark = doc.Range.Bookmarks["Birthday"];
                bookmark.Text = com.Birthday == null ? "" : ((DateTime)com.Birthday).ToString("yyyy.MM.dd");
                //工作单位
                bookmark = doc.Range.Bookmarks["Company"];
                bookmark.Text = com.Company == null ? "" : com.Company;
                //联系电话
                bookmark = doc.Range.Bookmarks["PhoneNo"];
                bookmark.Text = com.PhoneNo == null ? "" : com.PhoneNo;
                //类别
                bookmark = doc.Range.Bookmarks["SpecialWorkTypeText"];
                bookmark.Text = SpecialWorkType.FindOne(CK.K["SpecialWorkTypeId"] == com.SpecialWorkTypeId && CK.K["IsDelete"] == false).SpecialWorkTypeText;
                //任期开始
                bookmark = doc.Range.Bookmarks["WorkStartDate"];
                bookmark.Text = com.WorkStartDate == null ? "" : ((DateTime)com.WorkStartDate).ToString("yyyy.MM.dd");
                //任期结束
                bookmark = doc.Range.Bookmarks["WorkEndDate"];
                bookmark.Text = com.WorkEndDate == null ? "" : ((DateTime)com.WorkEndDate).ToString("yyyy.MM.dd");
                //联系地址
                bookmark = doc.Range.Bookmarks["Address"];
                bookmark.Text = com.Address == null ? "" : com.Address;
                //得票数
                bookmark = doc.Range.Bookmarks["Ballot"];
                bookmark.Text = com.Ballot == null ? "" : com.Ballot.ToString();
                //是否连任
                bookmark = doc.Range.Bookmarks["IsReelect"];
                bookmark.Text = com.IsReelect == null ? "" : (bool)com.IsReelect ? "是" : "否";
                //社会关系及人脉
                bookmark = doc.Range.Bookmarks["Background"];
                bookmark.Text = com.Background == null ? "" : com.Background;
                //工作单位情况
                bookmark = doc.Range.Bookmarks["Situation"];
                bookmark.Text = com.Situation == null ? "" : com.Situation;
                //交流情况表
                DataTable dt_fa = SituationToSpecialWork.Find(CK.K["Communication_Id"] == communicationId && CK.K["IsDelete"] == false && CK.K["Time"] >= (new DateTime(selectedYear, 1, 1)) && CK.K["Time"] <= (new DateTime(selectedYear, 12, 31))).ToDataTable();
                if (dt_fa.Rows.Count > 0)
                {
                    int fa_count = dt_fa.Rows.Count <= 5 ? dt_fa.Rows.Count : 5;
                    for (int i = 0; i < fa_count; i++)
                    {
                        //时间
                        bookmark = doc.Range.Bookmarks["Si_" + i + "_Time"];
                        bookmark.Text = dt_fa.Rows[i]["Time"].ToString().IsNullOrEmpty() ? "" : Convert.ToDateTime(dt_fa.Rows[i]["Time"].ToString()).ToString("yyyy.MM.dd");
                        //地点
                        bookmark = doc.Range.Bookmarks["Si_" + i + "_Lo"];
                        bookmark.Text = dt_fa.Rows[i]["Location"].ToString();
                        //交流情况
                        bookmark = doc.Range.Bookmarks["Si_" + i + "_Si"];
                        bookmark.Text = dt_fa.Rows[i]["SituationName"].ToString();
                    }
                }


                ////插入图片   
                if (!com.PhotoName.IsNullOrEmpty())
                {
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    builder.MoveToBookmark("Photo");
                    var img = builder.InsertImage(basePath + "upload/photos/" + com.PhotoName);
                    img.Width = 115;
                    img.Height = 150;
                    img.HorizontalAlignment = Aspose.Words.Drawing.HorizontalAlignment.Center;
                }

                string fileNameWithOutExtention = Guid.NewGuid().ToString();
                doc.Save(basePath + "upload/word_zxgz/" + fileNameWithOutExtention + ".pdf", SaveFormat.Pdf);
                FileStream fs = new FileStream(basePath + "upload/word_zxgz/" + fileNameWithOutExtention + ".pdf", FileMode.Open);
                byte[] file = new byte[fs.Length];
                fs.Read(file, 0, file.Length);
                fs.Close();
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("专项工作情况表-" + ddl_year.SelectedItem.Text + "-" + com.Name + ".pdf", System.Text.Encoding.UTF8));
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