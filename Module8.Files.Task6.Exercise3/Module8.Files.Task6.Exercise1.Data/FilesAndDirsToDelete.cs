namespace Module8.Files.Task6.Exercise3.Data;


/// <summary>
/// Класс для хранения отобранных файлов и папок на удаление
/// </summary>
public class FilesAndDirsToDelete
{
    /// <summary>
    /// Приватное поле для хранения массива отобранных файлов и папок на удаление
    /// </summary>
    private (string pathToFile, bool toDelete)[] arrFileSystemInfoToDelete;

    /// <summary>
    /// Массив отобранных файлов и папок для удаления
    /// </summary>
    public (string pathToFile, bool toDelete)[] ArrFileSystemInfoToDelete
    {
        get => arrFileSystemInfoToDelete;
    }

    /// <summary>
    /// Файл или папка для удаления
    /// </summary>
    /// <param name="index">Индекс в масиве</param>
    /// <returns>Файл или папка для удаления</returns>
    public (string pathToFile, bool toDelete) this[int index]
    {
        get => arrFileSystemInfoToDelete[index];
        set => arrFileSystemInfoToDelete[index] = value;
    }

    /// <summary>
    /// Отобранные файлы и папки для удаления
    /// </summary>
    /// <param name="arrFileSystemInfoToDelete">Отобранные файлы и папки для удаления</param>
    public FilesAndDirsToDelete((string pathToFile, bool toDelete)[] arrFileSystemInfoToDelete)
    {
        this.arrFileSystemInfoToDelete = arrFileSystemInfoToDelete;
    }

    /// <summary>
    /// Отобранные файлы и папки для удаления
    /// </summary>
    public FilesAndDirsToDelete()
    {
        this.arrFileSystemInfoToDelete = Array.Empty<(string pathToFile, bool toDelete)>();
    }

    /// <summary>
    /// Добавить файл или папку для удаления
    /// </summary>
    /// <param name="fileSystemInfoToDelete">Файл или папка для удаления</param>
    /// <exception cref="Exception">Файл или каталог не найден</exception>
    public void Add((string pathToFile, bool toDelete) fileSystemInfoToDelete)
    {
        if (Directory.Exists(fileSystemInfoToDelete.pathToFile) || File.Exists(fileSystemInfoToDelete.pathToFile))
        {
            Array.Resize(ref arrFileSystemInfoToDelete, (int)arrFileSystemInfoToDelete.Length + 1);
            arrFileSystemInfoToDelete[((int)arrFileSystemInfoToDelete.Length - 1)] = fileSystemInfoToDelete;
        }
        else
        {
            throw new Exception("Файл или каталог не найден.");
        }        
    }

    /// <summary>
    /// Добавить файлы и папки для удаления 
    /// </summary>
    /// <param name="filesAndDirsToDelete">Файлы и папки на удаление</param>
    public void Add(FilesAndDirsToDelete filesAndDirsToDelete)
    {
        foreach (var file in filesAndDirsToDelete.ArrFileSystemInfoToDelete)
        {
            this.Add((file.pathToFile, file.toDelete));
        }
    }

    /// <summary>
    /// Проверка наличия полного пути до файла или до папки в массиве отобранных на удаления
    /// </summary>
    /// <param name="pathToFileToCheck">Полный путь до папки</param>
    /// <returns>True если указанный полный путь до папки есть в массиве на удаления, false - если в массиве такого нет</returns>
    public bool IsPathToFileToDelete(string pathToFileToCheck)
    {
        if (Directory.Exists(pathToFileToCheck) || File.Exists(pathToFileToCheck))
        {
            if(this.arrFileSystemInfoToDelete == null || this.arrFileSystemInfoToDelete.Length == 0)
            {
                return false;
            }
            else
            {
                foreach (var file in this.arrFileSystemInfoToDelete)
                {
                    if (file.pathToFile == pathToFileToCheck && file.toDelete == true)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }


    /// <summary>
    /// Проверка наличия файлов или папок на удаление
    /// </summary>
    /// <returns>True если файлы или папки есть в массиве на удаления, false - если для удаления ничего нет</returns>
    public bool IsFilesAndDirsToDelete()
    {
        foreach (var file in this.arrFileSystemInfoToDelete)
        {
            if (file.toDelete == true)
            {
                return true;
            }
        }
        return false;
    }


}
