namespace Module8.Files.Task6.Exercise4.ConsolePresenter;

/// <summary>
/// Используемые методы для консоли
/// </summary>
public class Primitives
{
    /// <summary>
    /// Получить путь до файла из консоли
    /// </summary>
    /// <returns>Путь до файла</returns>
    public static string GetPathToFileFromConsole()
    {
        bool flag;
        string? fileName = null;
        do
        {
            flag = true;
            fileName = Console.ReadLine();
#if DEBUG
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = @"G:\TempFolder\students.dat";
                Console.WriteLine(fileName);
            }
#endif
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                flag = false;
            }
            else
            {
                Console.WriteLine($"Полный путь до файла введен неправильно. Попробуйте снова:");
            }
        }
        while (flag);
        return fileName;
    }
}
