using System.IO;
using System.Linq;
using System.Runtime;

namespace Module8.Files.Task6.Exercise1.ConsolePresenter;

internal class Program
{
    const long intervalLifeTimeMinute = 30L;


    public static bool IsFileSystemOld(FileSystemInfo fileSystemInfo)
    {
        long fileLifeTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - ((DateTimeOffset)fileSystemInfo.LastWriteTimeUtc).ToUnixTimeSeconds();
        if (fileLifeTime > intervalLifeTimeMinute * 60L)
        {
            return true;
        }
        else
        {
            return false;
        }            
    }

    public static void GetFilesAndFoldersToConsole(DirectoryInfo directoryInfo)
    {
        Console.WriteLine($"В директории: {directoryInfo.FullName}");
        foreach (var systemFile in directoryInfo.GetFileSystemInfos())
        {
            if (systemFile is DirectoryInfo directory)
            {
                Console.WriteLine($"директория: {systemFile.FullName}\t|\t{IsFileSystemOld(directory)}");
            }
            else if (systemFile is FileInfo file)
            {
                Console.WriteLine($"файл: {systemFile.FullName}\t|\t{IsFileSystemOld(file)}");
            }
        }
        Console.WriteLine();
        foreach (var dir in directoryInfo.GetDirectories())
        {
            if(dir.FullName.Length > 100)
            {
                throw new Exception("Слишком длиный путь до папки");
            }
            GetFilesAndFoldersToConsole(dir);
        }
    }

    public static Data.FilesAndDirsToDelete GetFilesAndFoldersToDelete(DirectoryInfo directoryInfo)
    {
        Data.FilesAndDirsToDelete filesAndDirsToDelete = new Data.FilesAndDirsToDelete();

        foreach (var file in directoryInfo.GetFiles())
        {
            bool isFileOld = IsFileSystemOld((FileSystemInfo)file);
            if (isFileOld)
            {
                Console.WriteLine($"файл: {file.FullName}\t|\t{isFileOld}");
                filesAndDirsToDelete.Add((file.FullName, isFileOld));                
                //file.Delete();
            }
        }
        foreach (var dir in directoryInfo.GetDirectories())
        {
            GetFilesAndFoldersToDelete(dir);
            bool isDirOld = IsFileSystemOld((FileSystemInfo)dir);
            if (isDirOld)
            {
                Console.WriteLine($"папка: {dir.FullName}\t|\t{isDirOld}");
                filesAndDirsToDelete.Add((dir.FullName, isDirOld));                
                //dir.Delete(false);
            }
        }

        return filesAndDirsToDelete;
    }


    


    static void Main(string[] args)
    {
        Console.WriteLine("Введите полный путь до папки для очистки от файлов и папок, которые не использвались более 30 минут:");
        string? dirName = Console.ReadLine();

        if (string.IsNullOrEmpty(dirName))
        {
            dirName = @"G:\TempFolder";
            Console.WriteLine(dirName);
        }

        var dirInfo = new DirectoryInfo(dirName);
                
        if (dirInfo.Exists)
        {
            Console.WriteLine("--------------------------------------------------------");
            GetFilesAndFoldersToConsole(dirInfo);
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("На удаление:");
            Data.FilesAndDirsToDelete filesAndFoldersToDelete = GetFilesAndFoldersToDelete(dirInfo);
            Console.WriteLine("--------------------------------------------------------");
            GetFilesAndFoldersToConsole(dirInfo);
            Console.WriteLine("--------------------------------------------------------");
            

            FileSystemInfo[] systemFiles = dirInfo.GetFileSystemInfos();

            foreach (FileSystemInfo systemFile in systemFiles)
            {
                if (systemFile is DirectoryInfo directory)
                {
                    Console.WriteLine($"Директория: {systemFile.FullName}");
                }
                else if (systemFile is FileInfo file)
                {
                    Console.WriteLine($"Файл: {systemFile.FullName}");
                }

                long fileLifeTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - ((DateTimeOffset)systemFile.LastWriteTimeUtc).ToUnixTimeSeconds();
                Console.WriteLine($"{systemFile.FullName}\t|\t{systemFile.LastWriteTimeUtc}\t|\t{fileLifeTime}");
            }
            Console.WriteLine();

            foreach (FileSystemInfo file in systemFiles)
            {
                long fileLifeTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - ((DateTimeOffset)file.LastWriteTimeUtc).ToUnixTimeSeconds();

                if (fileLifeTime > intervalLifeTimeMinute * 60L)
                {
                    //Console.WriteLine($"{file.FullName}\t|\t{file.LastWriteTimeUtc}\t|\t{fileLifeTime}");

                    try
                    {
                        //file.Delete(); // Удаление со всем содержимым
                        Console.WriteLine("Каталог удален:");
                        Console.WriteLine($"{file.FullName}\t|\t{file.LastWriteTimeUtc}\t|\t{fileLifeTime}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }




        Console.ReadKey();
    }
}

