using System;
using System.IO;
namespace automontronic.search.files
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the path to search:");
            string path = Console.ReadLine();
            Console.WriteLine("Enter the term search:");
            string term = Console.ReadLine();
            Console.WriteLine("Enter the file extension to search (* for all):");
            string ext = Console.ReadLine();
            if (File.Exists(path))
            {
                // This path is a file
                ProcessFile(path, term, ext);
            }
            else if (Directory.Exists(path))
            {
                // This path is a directory
                ProcessDirectory(path, term, ext);
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", path);
            }
            Console.ReadKey();
        }
        // Process all files in the directory passed in, recurse on any directories  
        // that are found, and process the files they contain. 
        public static void ProcessDirectory(string targetDirectory, string term, string ext)
        {
            // Process the list of files found in the directory. 
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName, term, ext);

            // Recurse into subdirectories of this directory. 
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory, term, ext);
        }

        // Insert logic for processing found files here. 
        public static void ProcessFile(string path, string term, string ext)
        {
            try
            {
                if (path.EndsWith(ext) || ext == "*")
                {
                    int counter = 0;
                    string line;
                    System.IO.StreamReader file = new System.IO.StreamReader(path);
                    while ((line = file.ReadLine()) != null)
                    {
                        counter++;
                        if (line.Contains(term))
                        {
                            Console.WriteLine(path + " - Line number: {0}", counter);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
