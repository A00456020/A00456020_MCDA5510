using System;
using System.IO;

namespace DirectoryWalker
{
    public class DirectoryWalker
    {
        public static string[] ListDirectories(string root_path)
        {
            System.Collections.Generic.List<string> dirs = new System.Collections.Generic.List<string>();
            foreach (string dir_name in Directory.GetDirectories(root_path))
            {
                dirs.Add(dir_name);
                if (Directory.Exists(dir_name))
                {
                    dirs.AddRange(ListDirectories(dir_name));
                }
            }
            //try
            //{
            //    dirs = Directory.GetDirectories(root_path);
            //}
            //catch (System.UnauthorizedAccessException)
            //{
            //    dirs = new string[] {""};
            //}
            return dirs.ToArray();
        }
        public static string[] ListCsvFiles(string[] list_directories)
        {
            System.Collections.Generic.List<string> fileslist = new System.Collections.Generic.List<string>();
            foreach (string dir_name in list_directories)
            {
                foreach(string file in Directory.GetFiles(dir_name))
                {
                    if (file.Contains(".csv"))
                    {
                        fileslist.Add(file);
                    }
                }                
            }
            //try
            //{
            //    dirs = Directory.GetDirectories(root_path);
            //}
            //catch (System.UnauthorizedAccessException)
            //{
            //    dirs = new string[] {""};
            //}
            return fileslist.ToArray();
        }
        public static void Main(string[] args)
        {
            string path = "P:\\Sample Data";
            string[] dirnames = ListDirectories(path);
            string[] files = ListCsvFiles(dirnames);
            foreach (string file_name in files)
            {
                Console.WriteLine(file_name);
                //string[] new_dirs = ListDirectories(dir_name);
                //foreach (string new_dir in new_dirs)
                //{
                //    Console.WriteLine(" " + new_dir);
                //}
            }
            Console.WriteLine(files.Length);

        }
    }
}