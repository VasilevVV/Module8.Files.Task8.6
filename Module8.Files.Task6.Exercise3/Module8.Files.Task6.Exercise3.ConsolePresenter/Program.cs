namespace Module8.Files.Task6.Exercise3.ConsolePresenter;

public class Program
{
    /// <summary>
    /// Интервал времени
    /// </summary>
    const long intervalLifeTimeMinute = 30L;

    
    public static void Main(string[] args)
    {
        Console.WriteLine($"Введите полный путь до папки для очистки от файлов и папок, которые не использвались более {intervalLifeTimeMinute} минут:");
        string dirName = Primitives.GetPathToFolderFromConsole();

        var dirInfo = new DirectoryInfo(dirName);        
        Console.WriteLine($"Исходный размер папки: {dirInfo.GetSizeOfFolder()} байт");
        Console.WriteLine($"Начинаю очистку.");
        Console.WriteLine("--------------------------------------------------------");
        try
        {
            Data.FilesAndDirsToDelete filesAndFoldersToDelete = Primitives.GetFilesAndFoldersToDelete(dirInfo, intervalLifeTimeMinute);
            if (filesAndFoldersToDelete.IsFilesAndDirsToDelete())
            {
                long sizeBefore = dirInfo.GetSizeOfFolder();
                Primitives.DelFilesAndFolders(filesAndFoldersToDelete, dirInfo);
                long sizeAfter = dirInfo.GetSizeOfFolder();
                Console.WriteLine("Файлы и папки, которые не использовались более 30 минут, удалены.");
                Console.WriteLine("--------------------------------------------------------");                
                Console.WriteLine($"Освобождено: {sizeBefore - sizeAfter} байт");
                Console.WriteLine($"Текущий размер папки: {sizeAfter}  байт");
            }
            else
            {
                Console.WriteLine("Файлы и папки, которые не использовались более 30 минут, для удаления не найдены.");
                Console.WriteLine("--------------------------------------------------------");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.ToString()}");
            Console.WriteLine("Завершаем работу программы");
        }        

        Console.ReadKey();
    }
}
