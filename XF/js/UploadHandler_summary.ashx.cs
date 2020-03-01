using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XF.js
{
    /// <summary>
    /// UploadHandler_summary 的摘要说明
    /// </summary>
    public class UploadHandler_summary : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "UTF-8";
            try
            {
                #region 上传
                HttpPostedFile file = context.Request.Files["Filedata"];
                string uploadPath = HttpContext.Current.Server.MapPath("~/upload") + "\\";
                if (file != null)
                {
                    if (!System.IO.Directory.Exists(uploadPath))
                    {
                        System.IO.Directory.CreateDirectory(uploadPath);
                    }
                    //重新给文件命名
                    string time = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();


                    string name = time + file.FileName;

                    file.SaveAs(uploadPath + name);

                    context.Response.Write(name);
                }
                else
                {
                    context.Response.Write("0");
                }
                #endregion
            }
            catch (Exception)
            {
                context.Response.Write("1");
            }
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}