using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZookeeperBrowser.Code
{
    public class ViewsHelper
    {
        /// <summary>
        /// 获取员工照片访问地址
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetEmployeePhotosPath(string filename)
        {
            var photosPathroot = ""; //Path.Combine(AppSetting.Setting.ApiUrl, "Upload", "EmployeePhotos");
            return Path.Combine(photosPathroot, filename);
        }
    }
}