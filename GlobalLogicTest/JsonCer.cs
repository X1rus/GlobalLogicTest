using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GlobalLogicTest
{
    [DataContract]
    public class Folder
    {
        [DataMember]
        public string FolderName { get; set; }
        [DataMember]
        public string DateCreate { get; set; }
       

        public Folder()
        { }
       
        public Folder(string folderName, string dateCreate)
        {
            FolderName = folderName;
            DateCreate = dateCreate;
        }
    }
    [DataContract]
    public class File
    {
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string FileSize { get; set; }
        [DataMember]
        public string PathFile { get; set; }

        public File() { }

        public File(string fileName,string fileSize,string pathFile)
        {
            FileName= fileName;
            FileSize = fileSize;
            PathFile = pathFile;
        }
    }
    
          
        

    
}
