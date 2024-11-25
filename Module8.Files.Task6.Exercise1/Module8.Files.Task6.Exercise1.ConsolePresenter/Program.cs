namespace Module8.Files.Task8.Exercise1.ConsolePresenter;

internal class Program
{
    static void Main(string[] args)
    {
        const long intervalLifeTimeMinute = 30L;

        Console.WriteLine("Введите полный путь до папки для очистки от файлов и папок, которые не использвались более 30 минут:");
        string? dirName = Console.ReadLine();

        #if DEBUG
        if (String.IsNullOrEmpty(dirName))
        {
            dirName = @"G:\TempFolder";
            Console.WriteLine(dirName);
        }
        #endif

        DirectoryInfo dirInfo = new DirectoryInfo(dirName);

        if (dirInfo.Exists)
        {
            FileSystemInfo[] files = dirInfo.GetFileSystemInfos();

            #if DEBUG
            foreach (FileSystemInfo file in files)
            {
                long fileLifeTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - ((DateTimeOffset)file.LastWriteTimeUtc).ToUnixTimeSeconds();
                Console.WriteLine($"{file.FullName}\t|\t{file.LastWriteTimeUtc}\t|\t{fileLifeTime}");
            }
            #endif

            foreach (FileSystemInfo file in files)
            {
                long fileLifeTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - ((DateTimeOffset)file.LastWriteTimeUtc).ToUnixTimeSeconds();

                if (fileLifeTime > (intervalLifeTimeMinute * 60L))
                {
                    Console.WriteLine($"{file.FullName}\t|\t{file.LastWriteTimeUtc}\t|\t{fileLifeTime}");
                    try
                    {
                        file.Delete(); // Удаление со всем содержимым
                        Console.WriteLine("Каталог удален");
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

