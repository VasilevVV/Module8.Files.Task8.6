namespace Module8.Files.Task6.Exercise2.ConsolePresenter;

public class Program
{  

    static void Main(string[] args)
    {
        Console.WriteLine($"Введите полный путь до папки для расчета размера");
        string pathToFolder = Primitives.GetPathToFolderFromConsole();
        var directoryInfo = new DirectoryInfo(pathToFolder);
        try
        {
            Primitives.GetFilesAndFoldersToConsoleWithSize(directoryInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.ToString()}");
            Console.WriteLine("Завершаем работу программы");
        }
          
        
        Console.ReadKey();
    }
}
