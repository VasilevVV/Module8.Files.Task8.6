using System.Diagnostics.Metrics;

namespace Module8.Files.Task6.Exercise2.ConsolePresenter
{
    internal class Program
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
                Console.WriteLine($"Введите полный путь до папки для расчета размера");
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



        public static long GetFilesAndFoldersToConsoleWithSize(DirectoryInfo directoryInfo)
        {
            long fullSize = 0;
            Console.WriteLine($"В директории: {directoryInfo.FullName}");
            foreach (var systemFile in directoryInfo.GetFileSystemInfos())
            {
                if (systemFile is DirectoryInfo directory)
                {
                    Console.WriteLine($"найдена директория: {directory.FullName}");
                }
                else if (systemFile is FileInfo file)
                {
                    fullSize += file.Length;
                    Console.WriteLine("найден файл:: {0}\t|\t{1} байт", file.FullName, file.Length);
                }
            }
            Console.WriteLine();
            foreach (var dir in directoryInfo.GetDirectories())
            {
                if (dir.FullName.Length > 100)
                {
                    throw new Exception($"Слишком длиный путь до папки: {dir.FullName}");
                }
                fullSize += GetFilesAndFoldersToConsoleWithSize(dir);
            }
            return fullSize;
        }


        static void Main(string[] args)
        {

            var directoryInfo = new DirectoryInfo(GetPathToFolderFromConsole());


            var fullSize = GetFilesAndFoldersToConsoleWithSize(directoryInfo);

           
            Console.WriteLine("Размер папки: {0}\t|\t{1} байт", directoryInfo.FullName, fullSize);
            //Console.WriteLine($"Размер: {directoryInfo.FullName}\t|\t{fullSize / 1024} Килобайт");



            Console.ReadKey();
        }
    }
}
