using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.Common
{
    public class FileHelper
    {
        /// <summary>
        /// 检查文件名，若重复返回新文件名
        /// </summary>
        /// <param name="uploadDirectory"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetUniqueFileName(string uploadDirectory, string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string name = Path.GetFileNameWithoutExtension(fileName);
            int index = 1;
            string newFileName = name + extension;

            while (File.Exists(Path.Combine(uploadDirectory, newFileName)))
            {
                newFileName = $"{name}_{index++}{extension}";
            }

            return newFileName;
        }
    }
}
