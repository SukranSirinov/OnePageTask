using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ExamOne.ExtensionMethods
{
    public static class ExtensionMethod
    {

        public static bool DeleteFile(this string fileName,string path)
        {
            
            if (File.Exists(Path.Combine(path, fileName)))
            {
                File.Delete(Path.Combine(path, fileName));
                return true;
            }
            return false;
        }

        public static bool CheckSize(this IFormFile file,int mb)
        {
            if((file.Length/(1024*1024)) < mb)
            {
                return true;
            } else
            {
                return false;
            }
        }
        

        public static bool CheckType(this IFormFile file)
        {
            if (file.ContentType.Contains("image/"))
            {
                return true;
            } else
            {
                return false;
            }
        }

        public static string SaveImage(this IFormFile file,string folderName)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            
            if (file.CheckSize(3) && file.CheckType())
            {
                using (FileStream fs = new FileStream(Path.Combine(folderName, fileName), FileMode.Create))
                {
                    file.CopyTo(fs);
                }
            }

            return fileName;
        }

    }
}
