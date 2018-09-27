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

            var path = @"D:\workspace\Laboratorio\AngularJS_1.x_legacy\seed_oldd";
            var basePathToMoveLongDirAndFiles = @"D:\Remover";

            //var path = args[0];
            //var basePathToMoveLongDirAndFiles = args[1];

            if (!Directory.Exists(basePathToMoveLongDirAndFiles))
                Directory.CreateDirectory(basePathToMoveLongDirAndFiles);

            Rename(path, basePathToMoveLongDirAndFiles);
            Remove(basePathToMoveLongDirAndFiles);
        }

        private static void Rename(string path, string basePath)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                var fileI = new FileInfo(file);
                Console.WriteLine(fileI);
                fileI.MoveTo(Path.Combine(basePath, RandomString(15)));
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
                Console.WriteLine(file);
                File.Delete(file);
            }

            var dirs = Directory.GetDirectories(path);
            foreach (var dir in dirs)
            {

                Remove(dir);
                Console.WriteLine(dir);
                Directory.Delete(dir);
            }
        }
    }
}
