using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathTooLongRemover
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                var path = @"D:\workspace\MyFuckLongFolder";
                var basePathToMoveLongDirAndFiles = @"D:\workspace\"; 
             */
            var path = args[0];
            var basePathToMoveLongDirAndFiles = args[1];

            Rename(path, basePathToMoveLongDirAndFiles);
            Remove(path);
        }

        private static void Rename(string path, string basePath)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                var fileI = new FileInfo(file);
                fileI.MoveTo(Path.Combine(basePath, RandomString(6)));
            }

            var dirs = Directory.GetDirectories(path);
            foreach (var dir in dirs)
            {
                var dirI = new DirectoryInfo(dir);
                if (dirI.Parent.FullName != basePath)
                    dirI.MoveTo(Path.Combine(basePath, RandomString(6)));
                Rename(dirI.FullName, basePath);
            }

        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private static void Remove(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                File.Delete(file);
            }

            var dirs = Directory.GetDirectories(path);
            foreach (var dir in dirs)
            {

                Remove(dir);
                Directory.Delete(dir);
            }
        }
    }
}
