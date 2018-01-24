using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            DriveInfo[] drivesInfo = DriveInfo.GetDrives();
            /*
            foreach (DriveInfo driveInfo in drivesInfo)
            {
                Console.WriteLine("Drive {0}", driveInfo.Name);
                Console.WriteLine("  File type: {0}", driveInfo.DriveType);

                if (driveInfo.IsReady == true)
                {
                    Console.WriteLine("  Volume label: {0}", driveInfo.VolumeLabel);
                    Console.WriteLine("  File system: {0}", driveInfo.DriveFormat);
                    Console.WriteLine("  Available space to current user:{0, 15} bytes", 
                        driveInfo.AvailableFreeSpace);
                    Console.WriteLine("  Total available space: {0, 15} bytes",
                        driveInfo.TotalFreeSpace);
                    Console.WriteLine("  Total size of drive: {0, 15} bytes ",
                        driveInfo.TotalSize);
                }
            }*/

            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\GitRepository\IBAtasks\task1\task1");
            Console.WriteLine("Directories:");
            foreach (DirectoryInfo item in directoryInfo.EnumerateDirectories())
            {
                Console.WriteLine("   " + item.Name);
            }

            Console.WriteLine("Files:");
            int i = 0;
            foreach (FileInfo item in directoryInfo.EnumerateFiles())
            {
                Console.WriteLine("  {0} - " + item.Name, i);
                i++;
            }

            Console.Write("Введите номер файла который хотите прочесть - ");
            i = Convert.ToInt32(Console.ReadLine());
            FileInfo fileInfo = directoryInfo.EnumerateFiles().ElementAt(i);

            using (StreamReader streamWriter = File.OpenText(fileInfo.DirectoryName+"/"+fileInfo.Name))
            {
                Console.WriteLine(streamWriter.ReadToEnd()); // Displays: MyValue
            }


            Console.ReadKey();
        }
    }
}
