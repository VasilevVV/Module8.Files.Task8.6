namespace Module8.Files.Task6.Exercise3.ConsolePresenter;

public static class Primitives
{
    /// <summary>
    /// Получить путь до папки из консоли
    /// </summary>
    /// <returns>Путь до папки</returns>
    static public string GetPathToFolderFromConsole()
    {
        bool flag;
        string? dirName = null;
        do
        {
            flag = true;                
            dirName = Console.ReadLine();
#if DEBUG
            if (string.IsNullOrEmpty(dirName))
            {
                dirName = @"G:\TempFolder";
                Console.WriteLine(dirName);
            }
#endif
            if (!string.IsNullOrEmpty(dirName) && Directory.Exists(dirName))
            {
                flag = false;
            }
            else
            {
                Console.WriteLine($"Полный путь до папки введен неправильно. Попробуйте снова.");
            }
        }
        while (flag);
        return dirName;
    }

    /// <summary>
    /// Определение файла или папки, которая не использовалась заданный интервал времени
    /// </summary>
    /// <param name="fileSystemInfo">Файл или папка</param>
    /// <param name="intervalLifeTimeMinute">Заданный интервал времени в минутах, за который файл или папка не использовалась</param>
    /// <returns>True если файл или папка не использовалась в заданный интервал времени, false - если использовалась</returns>
    private static bool IsFileSystemOld(FileSystemInfo fileSystemInfo, long intervalLifeTimeMinute)
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

    /// <summary>
    /// Подготовка файлов и папок на удаление, которые не использовались заданный интервал времени
    /// </summary>
    /// <param name="directoryInfo">Путь до папки для очистки</param>
    /// <param name="intervalLifeTimeMinute">Заданный интервал времени в минутах, за который файл или папка не использовалась</param>
    /// <returns>Файлы и папки на удаление, которые не использовались заданный интервал времени</returns>
    /// <exception cref="Exception">Слишком длиный путь до папки</exception>
    public static Data.FilesAndDirsToDelete GetFilesAndFoldersToDelete(DirectoryInfo directoryInfo, long intervalLifeTimeMinute)
    {
        var filesAndDirsToDelete = new Data.FilesAndDirsToDelete();

        foreach (var file in directoryInfo.GetFiles())
        {
            bool isFileOld = IsFileSystemOld((FileSystemInfo)file, intervalLifeTimeMinute);
            if (isFileOld)
            {
                //Console.WriteLine($"Удаляем файл: {file.FullName}");
                filesAndDirsToDelete.Add((file.FullName, isFileOld));
            }
        }
        foreach (var dir in directoryInfo.GetDirectories())
        {
            if (dir.FullName.Length > 100)
            {
                throw new Exception($"Слишком длиный путь до папки: {dir.FullName}");
            }
            filesAndDirsToDelete.Add(GetFilesAndFoldersToDelete(dir, intervalLifeTimeMinute));
            bool isDirOld = IsFileSystemOld((FileSystemInfo)dir, intervalLifeTimeMinute);
            if (isDirOld)
            {
                //Console.WriteLine($"Удаляем папку: {dir.FullName}");
                filesAndDirsToDelete.Add((dir.FullName, isDirOld));
            }
        }
        return filesAndDirsToDelete;
    }


    /// <summary>
    /// Удалить отобранные файлы и папки
    /// </summary>
    /// <param name="filesAndDirsToDelete">Файлы и папки на удаление, которые не использовались заданный интервал времени</param>
    /// <param name="directoryInfo">Путь до папки для очистки</param>
    /// <exception cref="Exception">Слишком длиный путь до папки</exception>
    public static void DelFilesAndFolders(Data.FilesAndDirsToDelete filesAndDirsToDelete, DirectoryInfo directoryInfo)
    {
        foreach (var file in directoryInfo.GetFiles())
        {
            bool isFileToDelete = filesAndDirsToDelete.IsPathToFileToDelete(file.FullName);
            if (isFileToDelete)
            {
                try
                {
                    //Console.WriteLine($"Файл удалён: {file.FullName}");
                    file.Delete();
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"Нет доступа к файлу: {file.FullName}. Обратитесь к владельцу файла.");
                }
            }
        }
        foreach (var dir in directoryInfo.GetDirectories())
        {
            if (dir.FullName.Length > 100)
            {
                throw new Exception($"Слишком длиный путь до папки: {dir.FullName}");
            }
            DelFilesAndFolders(filesAndDirsToDelete, dir);
            bool isDirToDelete = filesAndDirsToDelete.IsPathToFileToDelete(dir.FullName);
            if (isDirToDelete)
            {
                if (Directory.GetFiles(dir.FullName).IsNullOrEmptyArr()
                    && Directory.GetDirectories(dir.FullName).IsNullOrEmptyArr())
                {
                    try
                    {
                        //Console.WriteLine($"Папка удалена: {dir.FullName}");
                        dir.Delete(false);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Нет доступа к папке: {dir.FullName}. Обратитесь к владельцу папки.");
                    }
                }
            }
        }
    }

}
