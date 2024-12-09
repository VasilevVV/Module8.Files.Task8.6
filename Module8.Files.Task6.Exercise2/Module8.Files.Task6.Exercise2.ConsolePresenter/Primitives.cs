namespace Module8.Files.Task6.Exercise2.ConsolePresenter
{
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
        /// Вывод размера папок и файлов в консоль
        /// </summary>
        /// <param name="directoryInfo">Директория или папка</param>
        /// <exception cref="Exception">Слишком длиный путь до папки</exception>
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

    }
}
