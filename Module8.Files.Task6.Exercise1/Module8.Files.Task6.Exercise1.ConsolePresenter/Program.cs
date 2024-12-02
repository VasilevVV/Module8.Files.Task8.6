using System.IO;
using System.Linq;
using System.Runtime;

namespace Module8.Files.Task6.Exercise1.ConsolePresenter;

internal class Program
{
    /// <summary>
    /// Интервал времени
    /// </summary>
    const long intervalLifeTimeMinute = 30L;

    static void Main(string[] args)
    {

        Console.WriteLine($"Введите полный путь до папки для очистки от файлов и папок, которые не использвались более {intervalLifeTimeMinute} минут:");
        string? dirName = Console.ReadLine();

#if DEBUG
        if (string.IsNullOrEmpty(dirName))
        {
            dirName = @"G:\TempFolder";
            Console.WriteLine(dirName);
        }
#endif

        var dirInfo = new DirectoryInfo(dirName);

        if (dirInfo.Exists)
        {
            Console.WriteLine($"Папка {dirInfo.FullName} существует, начинаем очистку:");
            Console.WriteLine("--------------------------------------------------------");
            Primitives.GetFilesAndFoldersToConsole(dirInfo);
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("На удаление:");
            Data.FilesAndDirsToDelete filesAndFoldersToDelete = Primitives.GetFilesAndFoldersToDelete(dirInfo, intervalLifeTimeMinute);
            Console.WriteLine("--------------------------------------------------------");
            if (filesAndFoldersToDelete.IsFilesAndDirsToDelete())
            {
                try
                {
                    Primitives.DelFilesAndFolders(filesAndFoldersToDelete, dirInfo);
                    Console.WriteLine("Файлы и папки, которые не использовались более 30 минут, удалены.");
                    Console.WriteLine("--------------------------------------------------------");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла непредвиденная ошибка: {ex.ToString()}");
                    Console.WriteLine("Завершаем работу программы");
                }
            }
            else
            {
                Console.WriteLine("Файлы и папки, которые не использовались более 30 минут, для удаления не найдены.");
                Console.WriteLine("--------------------------------------------------------");
            }
        }
        else 
        {
            Console.WriteLine($"Указанная папка: {dirName} - не существует");
            Console.WriteLine("Завершаем работу программы");
        }

        Console.ReadKey();
    }
}

