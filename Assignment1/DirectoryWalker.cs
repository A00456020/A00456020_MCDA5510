using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;

namespace DirectoryWalker
{
    public class DirectoryWalker
    {
        public static string log_directory = "P:\\logs";
        public static string log_path = "P:\\logs\\logs.txt";
        public static FileStream logstream = File.OpenWrite(log_path);
        public static StreamWriter logfile = new StreamWriter(logstream);
        public static void LogInfo(string log_dir, string[] info)
        {
            if (!Directory.Exists(log_dir))
            {
                Directory.CreateDirectory(log_dir);
            }
            string log_path = log_dir + "\\" + "logs.txt";
            FileStream logstream = File.OpenWrite(log_path);

            StreamWriter logfile = new StreamWriter(logstream);
            foreach (string line in info)
            {
                logfile.WriteLine(line);
            }
            logfile.Close();
        }
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
            return dirs.ToArray();
        }
        public static string[] ListCsvFiles(string[] list_directories)
        {
            System.Collections.Generic.List<string> fileslist = new System.Collections.Generic.List<string>();
            foreach (string dir_name in list_directories)
            {
                foreach (string file in Directory.GetFiles(dir_name))
                {
                    if (file.Contains(".csv"))
                    {
                        fileslist.Add(file);
                    }
                }
            }
            return fileslist.ToArray();
        }
        public static string[][] ProcessFiles(string[] files)
        {
            string[] delimiters = new string[] { "," };
            System.Collections.Generic.List<string> ValidRecords = new System.Collections.Generic.List<string>();
            System.Collections.Generic.List<string> InValidRecords = new System.Collections.Generic.List<string>();
            bool isHeaded = false;
            foreach (string filename in files)
            {
                using (TextFieldParser fileReader = new TextFieldParser(filename))
                {
                    fileReader.TextFieldType = FieldType.Delimited;
                    fileReader.SetDelimiters(delimiters);
                    if (!isHeaded)
                    {
                        string tmp_str = string.Empty;
                        string[] headers = fileReader.ReadFields();
                        foreach (string field in headers)
                        {
                            tmp_str += field + ",";
                        }
                        ValidRecords.Add(tmp_str);
                        InValidRecords.Add(tmp_str);
                        tmp_str = string.Empty;
                        isHeaded = true;
                    }
                    while (!fileReader.EndOfData)
                    {
                        string[] fields = fileReader.ReadFields();
                        string record = string.Empty;
                        //Console.WriteLine(fields[0]);
                        if (fields[0].Contains("First Name"))
                        {
                            continue;
                        }
                        bool isValid = true;
                        foreach (string field in fields)
                        {
                            record += field + ",";
                            if (string.IsNullOrEmpty(field))
                            {
                                isValid = false;
                            }
                        }
                        if (isValid)
                        {
                            ValidRecords.Add(record);
                            record = string.Empty;
                        }
                        else
                        {
                            InValidRecords.Add(record);
                            logfile.WriteLine("Skipped Row at \"" + record + "\"");
                            record = string.Empty;
                        }

                    }


                }
            }
            string[][] records = new string[][] { ValidRecords.ToArray(), InValidRecords.ToArray() };
            return records;
        }
        public static void Main(string[] args)
        {

            var timestamp = System.Diagnostics.Stopwatch.StartNew();
            string path = "P:\\Sample Data";
            Directory.CreateDirectory("P:\\Output");
            string output_path = "P:\\Output\\Output.csv";
            string log_path = "P:\\logs";

            System.Collections.Generic.List<string> outrecords = new System.Collections.Generic.List<string>();
            //string skipped_path = "P:\\Output\\SkippedOutput.csv";

            string[] directories = ListDirectories(path);
            string[] files = ListCsvFiles(directories);
            string[][] total_records = ProcessFiles(files);
            string[] invalid_records = total_records[1];
            string[] records = total_records[0];
            if (File.Exists(output_path))
            {
                File.Delete(output_path);
            }
            FileStream outstream = File.Create(output_path);
            StreamWriter outfile = new StreamWriter(outstream);
            int count = 1;
            foreach (string record in records)
            {
                count++;
                outrecords.Add(record);
                outfile.WriteLine(record);
            }
            outfile.Close();
            timestamp.Stop();
            long totaltime = timestamp.ElapsedMilliseconds;
            logfile.WriteLine("\nTotal execution time is " + totaltime + " miliseconds, that is " + totaltime / 1000 + " seconds.");

            //if (File.Exists(skipped_path))
            //{
            //    File.Delete(skipped_path);
            //}
            //FileStream outstream2 = File.Create(skipped_path);
            //StreamWriter outfile2 = new StreamWriter(outstream2);
            //int sk_count = 1;
            //foreach (string record in invalid_records)
            //{
            //    sk_count++;
            //    //outrecords.Add(record);
            //    outfile2.WriteLine(record);
            //}
            //outfile2.Close();
            logfile.Close();
        }
    }
}