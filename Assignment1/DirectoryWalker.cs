using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;

namespace DirectoryWalker
{
    public class DirectoryWalker
    {
        public static string path = "P:\\Sample Data";
        public static DirectoryInfo outdir = Directory.CreateDirectory("Output");
        public static DirectoryInfo loggingdir = Directory.CreateDirectory("logs");
        public static string output_path = "Output\\Output.csv";
        public static string log_directory = "logs";
        public static string log_path = "logs\\logs.txt";

        public static FileStream logstream = File.Create(log_path);
        public static StreamWriter logfile = new StreamWriter(logstream);
        public static string[] ListDirectories(string root_path)
        {
            System.Collections.Generic.List<string> dirs = new System.Collections.Generic.List<string>();
            foreach (string dir_name in Directory.GetDirectories(root_path))
            {
                dirs.Add(dir_name);
                if (Directory.Exists(dir_name))
                {
                    if (!dir_name.Contains("__MACOSX"))
                    {
                        dirs.AddRange(ListDirectories(dir_name));
                    }
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
            logfile.WriteLine("log created for \"DirectoryWalker.cs\" file.");
            logfile.WriteLine("started logging the information from below this section");
            logfile.WriteLine("at the end of the logs, total valid rows, total invalid rows and total execution time has been logged\n\n");

            var timestamp = System.Diagnostics.Stopwatch.StartNew();
            Directory.CreateDirectory("P:\\Output");
            Directory.CreateDirectory(log_directory);


            System.Collections.Generic.List<string> outrecords = new System.Collections.Generic.List<string>();
            System.Collections.Generic.List<string> directoriesList = new System.Collections.Generic.List<string>();
            directoriesList.Add(path);
            foreach (string dirs in ListDirectories(path))
            {
                directoriesList.Add(dirs);
            }
            string[] directories = directoriesList.ToArray();
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

            int sk_count = 1;
            foreach (string record in invalid_records)
            {
                sk_count++;
            }
            logfile.WriteLine("\n\nTotal Valid rows are " + count);
            logfile.WriteLine("\nTotal skipped rows are " + sk_count);
            logfile.WriteLine("\nTotal execution time is " + totaltime + " miliseconds, that is approximately " + totaltime / 1000 + " seconds.");
            logfile.Close();
        }
    }
}