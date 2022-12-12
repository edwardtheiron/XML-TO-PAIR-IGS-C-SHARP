using System;
using System.IO;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static int file_counter = 0;
        static string template_path = "C:\\d\\template.xml";
        //
        static int Main(string[] args)
        {

            string dir_path = "C:\\d\\";
            

            if (!Directory.Exists(dir_path)) {
                Print("Directory doesn't exsists" + dir_path);
                return 404;
            }

            if (!File.Exists(template_path)) {
                Print("Template not found at dir - " + template_path);
                return 405;
            }

            if (File.Exists(dir_path))
            {
                // This path is a file
                ProcessFile(dir_path);
            }
            else if (Directory.Exists(dir_path))
            {
                // This path is a directory
                ProcessDirectory(dir_path);
            }
            Console.WriteLine(file_counter);
            return 0;
        }

        public static void ProcessFile(string path)
        {
            if (path.EndsWith(".IGS"))
            {
                string fileName = Path.GetFileName(path);
                string[] cut = fileName.Split(".IGS");
                string fileName_to_create = Path.GetDirectoryName(path) + "\\" + cut[0] + ".xml";
                if (File.Exists(fileName_to_create))
                {
                    Print("File " + fileName_to_create + " already exists");
                }
                else
                {
                    //Print(fileName_to_create);
                    File.Copy(template_path, fileName_to_create);
                    string text = File.ReadAllText(fileName_to_create);
                    text = text.Replace("placeholder", cut[0]);
                    //text = text.Replace("PATH_PLACEHOLDER", cut[0]); add path adapter for BE paths
                    File.WriteAllText(fileName_to_create, text);
                    Program.file_counter++;
                }

            }
        }

        static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                ProcessFile(fileName);
            }
            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }


        static void Print(string text)
        {
            Console.WriteLine(text);
        }


    }
}




