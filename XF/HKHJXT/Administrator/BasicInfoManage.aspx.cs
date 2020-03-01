using BusinessSupply.BusinessOper;
using DataModel.DataModel;
using Lephone.Data;
using Lephone.Extra;
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
    public partial class BasicInfoManage : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshView(0);
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            RefreshView(0);
        }

        private void RefreshView(int NewIndex)
        {
            try
            {
                int size = this.gv_basicInfo.PageSize;
                string name = tb_Name.Text.Trim();
                string phone = tb_phone.Text.Trim();
                string email = tb_email.Text.Trim();
                var ps = BasicInfoBusiness.GetBasicInfos(size, name, phone, email);
                this.gv_basicInfo.PageIndex = NewIndex;
                this.gv_basicInfo.DataSource = new PagedCollection<BasicInfo>(ps, NewIndex);
                this.gv_basicInfo.DataBind();
                this.Label_ResultCount.Text = ps.GetResultCount().ToString();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('数据加载错误！')", true);
            }
        }

        protected void gv_basicInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string command = e.CommandName;
            int id = Convert.ToInt32(e.CommandArgument);
            if (command.Equals("Upd"))//修改
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "btn_Add_jbxx_Click('添加基本信息','./BasicInfoManage_Add.aspx?BasicInfoId=" + id + "')", true);
            }
            else if (command.Equals("Del"))//删除
            {
                bool result = BasicInfoBusiness.DeleteBasicInfo(id);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('删除失败，请重新再试！')", true);
                    return;
                }
                RefreshView(0);
            }
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "btn_Add_jbxx_Click('添加基本信息','./BasicInfoManage_Add.aspx')", true);
        }

        protected void btn_import_Click(object sender, EventArgs e)
        {

        }

        protected void btn_template_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "导入模板（基本信息）.xls";//客户端保存的文件名
                string filePath = Server.MapPath("../../Template/导入模板（基本信息）.xls");//路径

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
                text(ex.ToString());
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "layer.alert('数据加载错误！')", true);
            }
        }

        protected void btn_importEvent_Click(object sender, EventArgs e)
        {
            string fileExtension = null;
            string file = import_upload.FileName.Replace(" ", "");
            string filename = System.IO.Path.GetFileNameWithoutExtension(file) + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(" ", "").Replace("/", "").Replace(":", "").Replace("-", "");
            bool fileOK = false;
            string savepath = Server.MapPath("~/Upload/DepartmentPlace/");
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

                        DataTable dt = ReadExcelToTable(savepath + filename);  //读取Excel文件（.xls和.xlsx格式）
                        int success = 0;
                        int fail = 0;
                        StringBuilder sb_failIndex = new StringBuilder();
                        string errorInfo = "。";
                        for (int i = 1; i < dt.Rows.Count; i++)
                        {
                            string name = dt.Rows[i]["F1"].ToString();
                            string phone = dt.Rows[i]["F2"].ToString();
                            string email = dt.Rows[i]["F3"].ToString();
                            string address = dt.Rows[i]["F4"].ToString();
                            string zipcode = dt.Rows[i]["F5"].ToString();
                            if (name.IsNullOrEmpty() || (phone.IsNullOrEmpty() && email.IsNullOrEmpty()))
                            {
                                fail++;
                                sb_failIndex.Append((i + 1) + "." + name + "、");
                                continue;
                            }

                            if (BasicInfo.FindOne(CK.K["Name"] == name && CK.K["IsDelete"] == false) == null)
                            {
                                if (BasicInfoBusiness.AddBasicInfo(name, phone, email, address, zipcode))
                                {
                                    success++;
                                    continue;
                                }
                                else
                                {
                                    fail++;
                                    sb_failIndex.Append((i + 1) + "." + name + "、");
                                    continue;
                                }
                            }
                            else
                            {
                                fail++;
                                sb_failIndex.Append((i + 1) + "." + name + "、");
                                continue;
                            }
                        }
                        if (sb_failIndex.Length > 0)
                        {
                            sb_failIndex.Remove(sb_failIndex.Length - 1, 1);
                            errorInfo = "，失败excel序号（包含标题行）为：" + sb_failIndex.ToString();
                        }
                        RefreshView(0);
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "提示", "layer.alert('" + "导入完成！总共" + (success + fail) + "条，其中成功导入" + success + "条，失败导入" + fail + "条" + errorInfo + "')", true);
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

    }
}