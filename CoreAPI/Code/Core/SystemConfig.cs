using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreAPI.Code.Core
{
    public class SystemConfig
    {
        /// <summary>
        /// 学生照片上传路径
        /// </summary>
        /// <returns></returns>
        public static string photosPath()
        {
            var photosPath = Path.Combine(uploadPath(), "StudentPhotos");
            if (!Directory.Exists(photosPath))
            {
                Directory.CreateDirectory(photosPath);
            }

            return photosPath;
        }

        private static string uploadPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Upload");
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="formFile">文件</param>
        /// <param name="uploadPath">保存路径</param>
        /// <param name="guidname">使用guid文件名</param>
        /// <param name="fileExt">后缀名</param>
        /// <param name="cancellationToken">取消token</param>
        /// <returns></returns>
        public static async Task<string> UploadSave(IFormFile formFile, string uploadPath, bool guidname = false,
            FileExt fileExt = FileExt.jpg, CancellationToken cancellationToken = default)
        {
            var date = DateTime.Now;
            var fileroot = Path.Combine(date.ToString("yyyy"), date.ToString("MM"), date.ToString("dd"));

            var name = formFile.FileName;
            var size = formFile.Length;
            var path = Path.Combine(uploadPath, fileroot);
            if (guidname)
            {
                name = $"{Guid.NewGuid().ToString().Replace("-", "")}.{fileExt.ToString()}";
            }
            else
            {
                int index = name.LastIndexOf(".");
                if (index == -1)
                {
                    name = $"{name}.{fileExt.ToString()}";
                }
                else
                {
                    name = $"{name.Substring(0, index)}.{fileExt.ToString()}";
                }
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //写入
            var savePath = Path.Combine(path, name);
            using (var stream = new FileStream(savePath, FileMode.OpenOrCreate))
            {
                await formFile.CopyToAsync(stream, cancellationToken);
            }

            return Path.Combine(fileroot, name);
        }

        public enum FileExt
        {
            jpg,
            bmp,
            png,
            gif,
            zip,
            rar,
            iso,
            txt,
            doc,
            docx,
            xlsx,
            xls,
            xml,
            pdf
        }
    }
}