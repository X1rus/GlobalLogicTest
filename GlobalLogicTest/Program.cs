

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace GlobalLogicTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path");
            string dirPath;

            do
            {
                dirPath = Console.ReadLine(); //enter path
                if (!string.IsNullOrWhiteSpace(dirPath)) break;
                Console.WriteLine("Enter path");
            } while (true);
          


            Folder folder = new Folder();
            File file = new File();
            List<Folder> listFolder = new List<Folder>();// create folders list
            List<File> listFile = new List<File>();//create files list

            if (Directory.Exists(dirPath))// checked directory
            {

                string[] dirs = Directory.GetDirectories(dirPath);
                DirectoryInfo dirInfo = new DirectoryInfo(dirPath);

                listFolder.Add(new Folder(dirInfo.Name, Convert.ToString(dirInfo.CreationTime)));// add first path element 

                Console.WriteLine("Folders:");
               
                foreach (string s in dirs)//initial folders list 
                {
                    dirInfo = new DirectoryInfo(s);
                    Console.WriteLine($"Folder Name: {dirInfo.Name}    |   Creation Time: {dirInfo.CreationTime}   |   Path: {dirInfo.FullName}");
                    folder.FolderName = dirInfo.Name;
                    folder.DateCreate = Convert.ToString(dirInfo.CreationTime);

                  listFolder.Add(new Folder(folder.FolderName, folder.DateCreate));

                  

                }
                Console.WriteLine();
                Console.WriteLine("Files:");
                string[] files = Directory.GetFiles(dirPath);
                FileInfo fileinfo = new FileInfo(dirPath);
                foreach (string s in files)// initial files list
                {
                    fileinfo = new FileInfo(s);
                    Console.WriteLine($"File Name: {fileinfo.Name}  |   Creation Time: {fileinfo.Length} B |   Path: {fileinfo.FullName} ");
                    file.FileName = fileinfo.Name;
                    file.FileSize = Convert.ToString(fileinfo.Length)+"B";
                    file.PathFile = fileinfo.FullName;

                    listFile.Add(new File(file.FileName, file.FileSize, file.PathFile));

                }

                


            }

            DataContractJsonSerializer jsonFormatterFolder = new DataContractJsonSerializer(typeof(List<Folder>));//json contract for folders
            DataContractJsonSerializer jsonFormatterFile = new DataContractJsonSerializer(typeof(List<File>));//json contract for files
          

            using (FileStream fs = new FileStream("FoldersAndFiles.json", FileMode.Create)) // add stream in json files
            {
                jsonFormatterFolder.WriteObject(fs, listFolder);
                jsonFormatterFile.WriteObject(fs,listFile);
            }


            Console.ReadKey();
        }
    }
}
