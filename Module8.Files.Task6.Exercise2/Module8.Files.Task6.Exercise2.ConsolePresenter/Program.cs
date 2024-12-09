using System.Diagnostics.Metrics;

namespace Module8.Files.Task6.Exercise2.ConsolePresenter;

public class Program
{  

    public static void GetFilesAndFoldersToConsoleWithSize(DirectoryInfo directoryInfo)
    {
        Console.WriteLine("В директории: {0}\t|\t{1} байт", directoryInfo.FullName, directoryInfo.GetSizeOfFolder());
        foreach (var systemFile in directoryInfo.GetFileSystemInfos())
        {
            if (systemFile is DirectoryInfo directory)
            {
                Console.WriteLine("найдена директория: {0}\t|\t{1} байт", directory.FullName, directory.GetSizeOfFolder());
            }
            else if (systemFile is FileInfo file)
            {
                Console.WriteLine("найден файл: {0}\t|\t{1} байт", file.FullName, file.Length);
            }
        }
        Console.WriteLine();
        foreach (var dir in directoryInfo.GetDirectories())
        {
            if (dir.FullName.Length > 100)
            {
                throw new Exception($"Слишком длиный путь до папки: {dir.FullName}");
            }
            GetFilesAndFoldersToConsoleWithSize(dir);
        }            
    }


    static void Main(string[] args)
    {
        string pathToFolder = Primitives.GetPathToFolderFromConsole();
        var directoryInfo = new DirectoryInfo(pathToFolder);
        GetFilesAndFoldersToConsoleWithSize(directoryInfo);  
        
        Console.ReadKey();
    }
}
