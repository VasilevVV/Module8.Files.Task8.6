using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        

    }
}
