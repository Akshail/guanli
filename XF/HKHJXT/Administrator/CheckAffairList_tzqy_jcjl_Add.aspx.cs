using BusinessSupply.BusinessOper;
using DataModel.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XF.HKHJXT.Administrator
{
    public partial class CheckAffairList_tzqy_jcjl_Add : PageBase
    {
        private int enterpriseId//只有新增才传此值
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString["enterpriseId"]);
                }
                catch
                {
                    return 0;
                }
            }
        }
        private int idInspect
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString["idInspect"]);
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
                if (idInspect != 0)//修改
                {
                    btn_Add.Text = "确认修改";
                    //回填
                    try
                    {
                        Inspect ins = Inspect.FindById(idInspect);
                        input_RegisterDate.Value = DateTime.Compare((DateTime)ins.Time, new DateTime(1900, 1, 1)) > 0 ? ((DateTime)ins.Time).ToString("yyyy/MM/dd") : "";
                        TextBox1.Text = ins.Unit;
                        tb_PublishPeriod.Text = ins.Result;
                        if (!ins.PhotoName.IsNullOrEmpty())
                        {
                            hide_PhotoName.Value = ins.PhotoName;
                            Photo.ImageUrl = "../../upload/photos/" + ins.PhotoName;
                        }
                    }
                    catch (Exception ex)
                    {
                        text(ex.ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('数据加载错误，请重新再试！')", true);
                    }
                }
            }
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            DateTime time = new DateTime(1900, 1, 1);
            try
            {
                time = input_RegisterDate.Value.Equals("") ? time : Convert.ToDateTime(input_RegisterDate.Value);
            }
            catch (Exception ex)
            {
                text(ex.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('时间输入格式不规范，请重新输入！')", true);
                return;
            }
            string unit = TextBox1.Text;
            string result = tb_PublishPeriod.Text;
            string photoName = hide_PhotoName.Value;
            bool res = false;
            if (idInspect != 0)//修改
            {
                res = InspectBusiness.UpdateInspect(idInspect, time, unit, result, photoName);
                if (!res)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('修改失败，请重新再试！')", true);
                    return;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('修改成功', 2, -1);close();", true);
            }
            else//新增
            {
                res = InspectBusiness.AddInspect(enterpriseId, time, unit, result, photoName);
                if (!res)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('新增失败，请重新再试！')", true);
                    return;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('新增成功', 2, -1);close();", true);
            }

        }

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            string fileExtension = null;
            string file = FileUpload_Photo.FileName;
            string filename = "TZQY-AQSCZD-" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss").Replace(" ", "").Replace("/", "").Replace(":", "").Replace("-", "");
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
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('上传照片不能大于5M！', 2, -1);", true);
                            //ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "alert('上传照片不能大于5M！')", true);
                            return;
                        }
                        if (!Directory.Exists(savepath))
                        {
                            Directory.CreateDirectory(savepath);
                        }
                        FileUpload_Photo.PostedFile.SaveAs(savepath + filename);

                        string photoUrl = "../../upload/photos/" + filename;
                        Photo.ImageUrl = "../../upload/photos/" + filename;
                        //照片名称
                        hide_PhotoName.Value = filename;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('上传成功！', 2, -1);", true);
                        //ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "alert('上传成功！')", true);
                    }
                    catch (Exception ex)
                    {
                        text(ex.ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('无法上传！', 2, -1);", true);
                        //ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "alert('无法上传！')", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('未识别的文件类型！', 2, -1);", true);
                    //ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "alert('未识别的文件类型！')", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alter", "parent.layer.msg('请选择文件！', 2, -1);", true);
                //ScriptManager.RegisterStartupScript(UpdatePanel_basic, this.GetType(), "alter", "alert('请选择文件！')", true);
            }
        }
    }
}