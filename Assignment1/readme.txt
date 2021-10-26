Hello, I am Parth Vaidya, and this is my directory for Assignment 1 of MCDA 5510 course.

This is the readme file for DirectoryWalker Assignment 1 of MCDA 5510.
The path variable in the Main function and logs path in the class DirectoryWalker needs to be changed as per the execution time and environment.
The assumption for a skipped row is that if any value of the attribute (at least one) is an Empty string, then the row is counted as skipped row.
The program creates only some directories and files if they do not exist at the given path, rest needs to be present.
Logging is implemented and logs contain skipped rows from processing of the files, Total number of invalid records, total number of valid rcords 
and the total execution time of the program. For the sake of simplicity, "_MACOSX" file has been ignored to scan for ".csv" files as it contains 
characters not readable by windows system.

Whenever the program needs to be executed, the paths for the root of the "Sample Data" folder or any other source of directories for csv files needs
to be updated in the "path" variable at the beginning of the class DirectoryWalker. The program automatically creates Output and logs directories
wherever it gets executed and will create output.csv file and logs.txt files in these respective directories as well.

The program first walks through all the directories and subdirectories possible from the root path, including the root directory. Then is walks
thorugh all the possible files from the list of all the directories. Then the program processess all the files one at a time and logs tha appropriate
information and then at the end, creates an output file called Output.csv and wrtites logs in logs.txt. at the end of the logs file, total valid rows,
total invalid rows, and total execution time is also written.

Thank you,
Parth Vaidya(A00456020).
