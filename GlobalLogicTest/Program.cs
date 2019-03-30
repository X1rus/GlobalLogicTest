/*  Решение должно позволить выбрать любую папку из файловой системы.

Чем выбранная папка должна быть преобразована в JSON.

Файл JSON должен содержать имя и дату создания выбранной папки.
Если папка имеет вложенные папки, то для каждой из подпапок должна присутствовать одна и та же информация.
Если папка имеет файлы внутри, то должно присутствовать имя файла, размер и полный путь для каждого из подфайлов:

Input example : "D:\Projects"
 
Output example: 
 
{
 
    "Name": "Projects",
 
    "DateCreated": "10-Jun-18 5:59 PM",
 
    "Files": [
 
      {
 
        "Name": "Test.txt",
 
        "Size": "27 B",
 
        "Path": "D:\\Projects\\Test.txt"
 
      },
 
      ...
 
    ],
 
    "Children": [
 
                   {
 
            "Name": "SubProjects",
 
            "DateCreated": "10-Jun-18 5:59 PM",
 
            "Files": [
 
                {
 
                  "Name": "SubTest.txt",
 
                  "Size": "2 B",
 
                  "Path": "D:\\Projects\\SubProjects\\SubTest.txt"
 
                },
 
                ...
 
            ],
 
            "Children": [ 
 
                              ....
 
                          ]
 
        },
 
        {
 
            "Name": "SubProjects3",
 
            "DateCreated": "10-Jun-18 5:59 PM",
 
            "Files": [],
 
            "Children": []
 
        },
 
        ...
 
    ]
 
}

*/

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
            string dirName = Console.ReadLine();


            Folder folder = new Folder();
            File file = new File();
            List<Folder> listFolder = new List<Folder>();
            List<File> listFile = new List<File>();

            if (Directory.Exists(dirName))
            {

                string[] dirs = Directory.GetDirectories(dirName);
                DirectoryInfo dirInfo = new DirectoryInfo(dirName);

                listFolder.Add(new Folder(dirInfo.Name, Convert.ToString(dirInfo.CreationTime)));

                Console.WriteLine("Folders:");
               
                foreach (string s in dirs)
                {
                    dirInfo = new DirectoryInfo(s);
                    Console.WriteLine($"Folder Name: {dirInfo.Name}   Creation Time: {dirInfo.CreationTime}      Path: {dirInfo.FullName}");
                    folder.FolderName = dirInfo.Name;
                    folder.DateCreate = Convert.ToString(dirInfo.CreationTime);

                  listFolder.Add(new Folder(folder.FolderName, folder.DateCreate));

                  

                }
                Console.WriteLine();
                Console.WriteLine("Files:");
                string[] files = Directory.GetFiles(dirName);
                FileInfo fileinfo = new FileInfo(dirName);
                foreach (string s in files)
                {
                    fileinfo = new FileInfo(s);
                    Console.WriteLine($"File Name: {fileinfo.Name}  |   Creation Time: {fileinfo.Length} B |   Path: {fileinfo.FullName} ");
                    file.FileName = fileinfo.Name;
                    file.FileSize = Convert.ToString(fileinfo.Length)+"B";
                    file.PathFile = fileinfo.FullName;

                    listFile.Add(new File(file.FileName, file.FileSize, file.PathFile));

                }

                


            }

            DataContractJsonSerializer jsonFormatterFolder = new DataContractJsonSerializer(typeof(List<Folder>));
            DataContractJsonSerializer jsonFormatterFile = new DataContractJsonSerializer(typeof(List<File>));
          

            using (FileStream fs = new FileStream("FoldersAndFiles.json", FileMode.Create))
            {
                jsonFormatterFolder.WriteObject(fs, listFolder);
                jsonFormatterFile.WriteObject(fs,listFile);
            }


            Console.ReadKey();
        }
    }
}
