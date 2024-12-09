namespace Module8.Files.Task6.Exercise3.ConsolePresenter;

internal static class DirectoryInfoExtention
{
    internal static long GetSizeOfFolder(this DirectoryInfo directoryInfo)
    {
        long fullSize = 0L;
        foreach (var file in directoryInfo.GetFiles())
        {
            fullSize += file.Length;
        }
        foreach (var dir in directoryInfo.GetDirectories())
        {
            if (dir.FullName.Length > 100)
            {
                throw new Exception($"Слишком длиный путь до папки: {dir.FullName}");
            }
            fullSize += GetSizeOfFolder(dir);
        }
        return fullSize;
    }
}
