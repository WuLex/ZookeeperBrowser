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
        /// 获取学生照片访问地址
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetStudentPhotosPath(string filename)
        {
            var photosPathroot = ""; //Path.Combine(AppSetting.Setting.ApiUrl, "Upload", "StudentPhotos");
            return Path.Combine(photosPathroot, filename);
        }
    }
}