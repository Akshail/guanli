using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSupply
{
    public class Helper
    {
        /// <summary>
        /// 根据日期不同情况,更改显示效果,结果显示为”yyyy年mm月dd日 hh:mm:ss“
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ChangeTimeDisplay(string time)
        {
            string temp = time;
            DateTime dt_temp = Convert.ToDateTime(time);
            //根据日期不同情况截取
            time = temp.Substring(0, 4) + "年";

            if (dt_temp.Month < 10)
            {
                time += temp.Substring(5, 1) + "月";

                if (dt_temp.Day < 10)
                {
                    time += temp.Substring(7, 1) + "日";
                }
                else
                {
                    time += temp.Substring(7, 2) + "日";
                }
            }
            else
            {
                time += temp.Substring(5, 2) + "月";
                if (dt_temp.Day < 10)
                {
                    time += temp.Substring(8, 1) + "日";
                }
                else
                {
                    time += temp.Substring(8, 2) + "日";
                }
            }
            //添加时分秒
            int length = temp.Trim().Length;
            if (dt_temp.Hour < 10)
            {
                time += " " + temp.Substring(length - 7, 7);
            }
            else
            {
                time += " " + temp.Substring(length - 8, 8);
            }

            return time;
        }




        /// <summary>
        /// 更改日期格式，输出为补全十位上略去0的”yyyy/mm/dd hh:mm:ss“
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ConvertTime(string time)
        {
            //string temp = time;
            //string[] temp = time.Split('/');
            //time=temp[0]

            //先转换为DateTime,在根据日期各部分判断转换
            DateTime dt_temp = Convert.ToDateTime(time);
            //年月日
            time = dt_temp.Year + "/";
            if (dt_temp.Month < 10)
            {
                time += "0";
            }
            time += dt_temp.Month + "/";
            if (dt_temp.Day < 10)
            {
                time += "0";
            }
            time += dt_temp.Day;

            //时分秒
            time += " ";
            if (dt_temp.Hour < 10)
            {
                time += "0";
            }
            time += dt_temp.Hour + ":";
            if (dt_temp.Minute < 10)
            {
                time += "0";
            }
            time += dt_temp.Minute + ":";
            if (dt_temp.Second < 10)
            {
                time += "0";
            }
            time += dt_temp.Second;

            return time;
        }

    }
}
