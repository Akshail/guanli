using Aspose.Words;
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
    public partial class CheckAffairList_tzjlgz_Add : PageBase
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_basic();
                if (id != 0)//修改  
                {
                    Button3.Text = "确认修改";
                    Button4.Enabled = true;
                    try
                    {
                        GroupCommunicationWork gcw = GroupCommunicationWork.FindById(id);
                        ddl_CommunicationType.SelectedValue = gcw.CommunicationTypeId.ToString();
                        tb_GroupArea.Text = gcw.GroupArea;
                        tb_GroupName.Text = gcw.GroupName;
                        tb_GroupMonitor.Text = gcw.GroupMonitor;
                        tb_GroupMembers.Value = gcw.GroupMembers;
                        tb_CommunicationGenerate.Value = gcw.CommunicationGenerate;
                        tb_CommunicationSummarize.Value = gcw.CommunicationSummarize;
                        tb_CommunicationReport.Value = gcw.CommunicationReport;
                        tb_CommunicationPhotos.Value = gcw.CommunicationPhotos;
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
            DataTable dt_communicationType = CommunicationType.Find(CK.K["IsDelete"] == false).ToDataTable();
            ddl_CommunicationType.DataSource = dt_communicationType;
            ddl_CommunicationType.DataValueField = "CommunicationTypeId";
            ddl_CommunicationType.DataTextField = "CommunicationTypeText";
            ddl_CommunicationType.DataBind();
            ddl_CommunicationType.Items.Insert(0, new ListItem("请选择", "0"));
            ddl_CommunicationType.SelectedValue = "0";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                int communicationTypeId = Convert.ToInt32(ddl_CommunicationType.SelectedValue);
                string groupArea = tb_GroupArea.Text;
                string groupName = tb_GroupName.Text;
                string groupMonitor = tb_GroupMonitor.Text;
                string groupMembers = tb_GroupMembers.Value;
                string communicationGenerate = tb_CommunicationGenerate.Value;
                string communicationSummarize = tb_CommunicationSummarize.Value;
                string communicationReport = tb_CommunicationReport.Value;
                string communicationPhotos = tb_CommunicationPhotos.Value;
                if (communicationTypeId == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('请选择交流形式！')", true);
                    return;
                }
                if (id != 0) //修改
                {
                    GroupCommunicationWorkBusiness.UpdateGroupCommunication(id, communicationTypeId, groupArea, groupName, groupMonitor, groupMembers, communicationGenerate, communicationSummarize, communicationReport, communicationPhotos);
                    LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 4, "团组交流工作信息", log_ip, log_account);//修改添加日志 操作类型->4
                }
                else
                {
                    GroupCommunicationWorkBusiness.AddGroupCommunication(communicationTypeId, groupArea, groupName, groupMonitor, groupMembers, communicationGenerate, communicationSummarize, communicationReport, communicationPhotos);
                    LogInfoBusiness.AddLogInfo(Convert.ToInt32(log_userid), DateTime.Now, Convert.ToInt32(log_usertype), log_username, 2, "团组交流工作信息", log_ip, log_account);//新增添加日志 操作类型->2
                }
                Response.Redirect("./CheckAffairList_tzjlgz.aspx");
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
            Response.Redirect("./CheckAffairList_tzjlgz.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Output_Word_zdts();
        }

        private string GenerateHtml(string content)
        {
            if (!System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("../../upload/word_tzjlgz/temp"))) 
            {
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("../../upload/word_tzjlgz/temp"));
            }  
            string filePath = System.Web.HttpContext.Current.Server.MapPath("../../upload/word_tzjlgz/temp/contentToHtml-" + Guid.NewGuid().ToString() + ".html");
            StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default);
            sw.Write("<html><head></head><body>");//temp.html中没有完整的html文件标记不行，没有的话会在word中显示html tag而不是样式，预先写入模版也行 
            sw.Write(content.Replace("src=\"/", "src=\"../../../../"));
            sw.Write("</body></html>");
            sw.Close();
            return filePath;

        }

        public void Output_Word_zdts()
        {
            try
            {
                if (id == 0)
                {
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
                    return;
                }
                GroupCommunicationWork gcw = GroupCommunicationWork.FindById(id);

                string basePath = Server.MapPath("../../");
                object missing = Type.Missing;
                object filePath = basePath + "Template/团组交流工作情况表.doc";
                var docApp = new Microsoft.Office.Interop.Word.Application();
                docApp.Visible = false;
                //打开Word文件  
                var doc = docApp.Documents.Open(ref filePath,
                    false, false, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing);
                object oFalse = false;
                object oTrue = true;
                object oMissing = System.Reflection.Missing.Value;

                //交流形式
                doc.Bookmarks["CommunicationTypeText"].Range.Text = CommunicationType.FindOne(CK.K["CommunicationTypeId"] == gcw.CommunicationTypeId).CommunicationTypeText;
                //组团地区
                doc.Bookmarks["GroupArea"].Range.Text = gcw.GroupArea == null ? "" : gcw.GroupArea;
                //交流团组名称
                doc.Bookmarks["GroupName"].Range.Text = gcw.GroupName == null ? "" : gcw.GroupName;
                //交流团团长
                doc.Bookmarks["GroupMonitor"].Range.Text = gcw.GroupMonitor == null ? "" : gcw.GroupMonitor;
                //团组成员名称
                if (gcw.GroupMembers != null)
                {
                    doc.Bookmarks["GroupMembers"].Range.InsertFile(GenerateHtml(gcw.GroupMembers), ref oMissing, ref oFalse, ref oTrue, ref oFalse);
                }
                //交流形成
                if (gcw.CommunicationGenerate != null)
                {
                    doc.Bookmarks["CommunicationGenerate"].Range.InsertFile(GenerateHtml(gcw.CommunicationGenerate), ref oMissing, ref oFalse, ref oTrue, ref oFalse);
                }
                //交流工作总结
                if (gcw.CommunicationSummarize != null)
                {
                    doc.Bookmarks["CommunicationSummarize"].Range.InsertFile(GenerateHtml(gcw.CommunicationSummarize), ref oMissing, ref oFalse, ref oTrue, ref oFalse);
                }
                //专项工作报告
                if (gcw.CommunicationReport != null)
                {
                    doc.Bookmarks["CommunicationReport"].Range.InsertFile(GenerateHtml(gcw.CommunicationReport), ref oMissing, ref oFalse, ref oTrue, ref oFalse);
                }
                //交流照片
                if (gcw.CommunicationPhotos != null)
                {
                    doc.Bookmarks["CommunicationPhotos"].Range.InsertFile(GenerateHtml(gcw.CommunicationPhotos), ref oMissing, ref oFalse, ref oTrue, ref oFalse);
                }
                string fileNameWithOutExtention = Guid.NewGuid().ToString();
                doc.SaveAs(basePath + "upload/word_tzjlgz/" + fileNameWithOutExtention + ".pdf", Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                object f = false;
                doc.Close(ref f, ref missing, ref missing);
                FileStream fs = new FileStream(basePath + "upload/word_tzjlgz/" + fileNameWithOutExtention + ".pdf", FileMode.Open);
                byte[] file = new byte[fs.Length];
                fs.Read(file, 0, file.Length);
                fs.Close();
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("团组交流工作情况表-" + gcw.GroupName + ".pdf", System.Text.Encoding.UTF8));
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(file);
                Response.End();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
                text(ex.ToString());
                //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载失败，请重新再试！')", true);
                return;
            }
        }

        //public void Output_Word_zdts()
        //{
        //    try
        //    {
        //        if (id == 0)
        //        {
        //            //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
        //            return;
        //        }
        //        GroupCommunicationWork gcw = GroupCommunicationWork.FindById(id);
        //        string basePath = Server.MapPath("../../");
        //        Document doc = new Document(basePath + "Template/团组交流工作情况表.doc");
        //        //交流形式
        //        Bookmark bookmark = doc.Range.Bookmarks["CommunicationTypeText"];
        //        bookmark.Text = CommunicationType.FindOne(CK.K["CommunicationTypeId"] == gcw.CommunicationTypeId).CommunicationTypeText;
        //        //组团地区
        //        bookmark = doc.Range.Bookmarks["GroupArea"];
        //        bookmark.Text = gcw.GroupArea == null ? "" : gcw.GroupArea;
        //        //交流团组名称
        //        bookmark = doc.Range.Bookmarks["GroupName"];
        //        bookmark.Text = gcw.GroupName == null ? "" : gcw.GroupName;
        //        //交流团团长
        //        bookmark = doc.Range.Bookmarks["GroupMonitor"];
        //        bookmark.Text = gcw.GroupMonitor == null ? "" : gcw.GroupMonitor;
        //        //团组成员名称
        //        bookmark = doc.Range.Bookmarks["GroupMembers"];
        //        bookmark.Text = gcw.GroupMembers == null ? "" : gcw.GroupMembers;
        //        //交流形成
        //        bookmark = doc.Range.Bookmarks["CommunicationGenerate"];
        //        bookmark.Text = gcw.CommunicationGenerate == null ? "" : gcw.CommunicationGenerate;
        //        //交流工作总结
        //        bookmark = doc.Range.Bookmarks["CommunicationSummarize"];
        //        bookmark.Text = gcw.CommunicationSummarize == null ? "" : gcw.CommunicationSummarize;
        //        //专项工作报告
        //        bookmark = doc.Range.Bookmarks["CommunicationReport"];
        //        bookmark.Text = gcw.CommunicationReport == null ? "" : gcw.CommunicationReport;
        //        //交流照片
        //        bookmark = doc.Range.Bookmarks["CommunicationPhotos"];
        //        bookmark.Text = gcw.CommunicationPhotos == null ? "" : gcw.CommunicationPhotos;

        //        string fileNameWithOutExtention = Guid.NewGuid().ToString();
        //        doc.Save(basePath + "upload/word_tzjlgz/" + fileNameWithOutExtention + ".pdf", SaveFormat.Pdf);
        //        FileStream fs = new FileStream(basePath + "upload/word_tzjlgz/" + fileNameWithOutExtention + ".pdf", FileMode.Open);
        //        byte[] file = new byte[fs.Length];
        //        fs.Read(file, 0, file.Length);
        //        fs.Close();
        //        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("团组交流工作情况表-" + gcw.GroupName + ".pdf", System.Text.Encoding.UTF8));
        //        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        //        Response.ContentType = "application/octet-stream";
        //        Response.BinaryWrite(file);
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
        //        //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载失败，请重新再试！')", true);
        //        return;
        //    }
        //}

    }
}