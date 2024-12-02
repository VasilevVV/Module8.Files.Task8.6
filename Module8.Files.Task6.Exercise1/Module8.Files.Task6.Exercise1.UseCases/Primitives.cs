namespace Module8.Files.Task6.Exercise1.UseCases;

public static class Primitives
{



    public static bool IsFileSystemOld(FileSystemInfo fileSystemInfo, long intervalLifeTimeMinute)
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


    public static void GetFilesAndFoldersToConsole(DirectoryInfo directoryInfo, long intervalLifeTimeMinute)
    {
        Console.WriteLine($"В директории: {directoryInfo.FullName}");
        foreach (var systemFile in directoryInfo.GetFileSystemInfos())
        {
            if (systemFile is DirectoryInfo directory)
            {
                Console.WriteLine($"директория: {systemFile.FullName}\t|\t{IsFileSystemOld(directory, intervalLifeTimeMinute)}");
            }
            else if (systemFile is FileInfo file)
            {
                Console.WriteLine($"файл: {systemFile.FullName}\t|\t{IsFileSystemOld(file, intervalLifeTimeMinute)}");
            }
        }
        Console.WriteLine();
        foreach (var dir in directoryInfo.GetDirectories())
        {
            if (dir.FullName.Length > 100)
            {
                throw new Exception("Слишком длиный путь до папки");
            }
            GetFilesAndFoldersToConsole(dir, intervalLifeTimeMinute);
        }
    }


    public static Data.FilesAndDirsToDelete GetFilesAndFoldersToDelete(DirectoryInfo directoryInfo, long intervalLifeTimeMinute)
    {
        Data.FilesAndDirsToDelete filesAndDirsToDelete = new Data.FilesAndDirsToDelete();

        foreach (var file in directoryInfo.GetFiles())
        {
            bool isFileOld = IsFileSystemOld((FileSystemInfo)file, intervalLifeTimeMinute);
            if (isFileOld)
            {
                Console.WriteLine($"файл: {file.FullName}\t|\t{isFileOld}");
                filesAndDirsToDelete.Add((file.FullName, isFileOld));
                //file.Delete();
            }
        }
        foreach (var dir in directoryInfo.GetDirectories())
        {
            GetFilesAndFoldersToDelete(dir, intervalLifeTimeMinute);
            bool isDirOld = IsFileSystemOld((FileSystemInfo)dir, intervalLifeTimeMinute);
            if (isDirOld)
            {
                Console.WriteLine($"папка: {dir.FullName}\t|\t{isDirOld}");
                filesAndDirsToDelete.Add((dir.FullName, isDirOld));
                //dir.Delete(false);
            }
        }

        return filesAndDirsToDelete;
    }
}

