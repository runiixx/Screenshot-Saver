using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screenshot_Saver.Utils
{
    class FilesystemManipulation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string CorrectPath(string path)
        {
            int size = 0;
            for (int i = 0; i < path.Length; i++)
            {
                size++;
                if (path[i] == '\\')
                {
                    size++;
                }
            }
            char[] filePathChar = new char[size];
            int j = 0;
            for (int i = 0; i < path.Length; i++)
            {
                filePathChar[j++] = path[i];
                if (path[i] == '\\')
                {
                    filePathChar[j++] = '\\';
                }
            }

            string correctFilePath = new string(filePathChar);
            return correctFilePath;
        }

        /// <summary>
        /// Finds how many files with similar names exist
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <returns>Number of files</returns>
        public static int GetLastCopy(string path, string filename)
        {
            IEnumerable<string> files =
                from file in Directory.GetFiles(path)
                where file.Contains(filename)
                where file.Contains(".png")
                select file;


            return files.Count();

        }
    }
}
