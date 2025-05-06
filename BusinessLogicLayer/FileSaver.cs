using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.Metadata;

namespace BusinessLogicLayer
{
    public static class FileSaver
    {
        public static void SaveFileToDocuments(string title, string content, string[] tags)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(path, title + ".txt");
            SaveFile(filePath, title, content, tags);
        }

        public static void SaveFileToNewFolderInDocuments(string folderName, string title, string content, string[] tags)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string subFolderPath = Path.Combine(path, folderName);
            Directory.CreateDirectory(subFolderPath);
            string filePath = Path.Combine(subFolderPath, title + ".txt");
            SaveFile(filePath, title, content, tags);
        }

        public static void SaveFileToDesktop(string title, string content, string[] tags)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string filePath = Path.Combine(path, title + ".txt");
            SaveFile(filePath, title, content, tags);
        }


        public static void SaveFile(string filePath, string title, string content, string[] tags)
        {
            StreamWriter writer = new StreamWriter(filePath, false);
            writer.WriteLine(title + "\n");
            writer.WriteLine(content + "\n");
            writer.WriteLine("Етикети: " + string.Join(", ", tags));
            writer.Close();
        }
    }
}
