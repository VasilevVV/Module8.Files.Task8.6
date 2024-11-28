namespace Module8.Files.Task6.Exercise1.Data;


/// <summary>
/// Класс для хранения отобранных файлов и папок на удаление
/// </summary>
public class FilesAndDirsToDelete
{
    /// <summary>
    /// Приватное поле для хранения массива отобранных файлов и папок на удаление
    /// </summary>
    private (string fileSystemInfo, bool ToDelete)[] arrFileSystemInfoToDelete;

    /// <summary>
    /// Массив отобранных файлов и папок для удаления
    /// </summary>
    public (string fileSystemInfo, bool ToDelete)[] ArrFileSystemInfoToDelete
    {
        get => arrFileSystemInfoToDelete;
    }

    /// <summary>
    /// Файл или папка для удаления
    /// </summary>
    /// <param name="index">Индекс в масиве</param>
    /// <returns>Файл или папка для удаления</returns>
    public (string fileSystemInfo, bool ToDelete) this[int index]
    {
        get => arrFileSystemInfoToDelete[index];
        set => arrFileSystemInfoToDelete[index] = value;
    }

    /// <summary>
    /// Отобранные файлы и папки для удаления
    /// </summary>
    /// <param name="arrFileSystemInfoToDelete">Отобранные файлы и папки для удаления</param>
    public FilesAndDirsToDelete((string fileSystemInfo, bool ToDelete)[] arrFileSystemInfoToDelete)
    {
        this.arrFileSystemInfoToDelete = arrFileSystemInfoToDelete;
    }

    /// <summary>
    /// Отобранные файлы и папки для удаления
    /// </summary>
    public FilesAndDirsToDelete()
    {
        this.arrFileSystemInfoToDelete = Array.Empty<(string fileSystemInfo, bool ToDelete)>();
    }

    /// <summary>
    /// Добавить файл или папку для удаления
    /// </summary>
    /// <param name="fileSystemInfoToDelete">Файл или папка для удаления</param>
    /// <exception cref="Exception">Файл или каталог не найден</exception>
    public void Add((string fileSystemInfo, bool ToDelete) fileSystemInfoToDelete)
    {
        if (Directory.Exists(fileSystemInfoToDelete.fileSystemInfo) || File.Exists(fileSystemInfoToDelete.fileSystemInfo))
        {
            Array.Resize(ref arrFileSystemInfoToDelete, (int)arrFileSystemInfoToDelete.Length + 1);
            arrFileSystemInfoToDelete[((int)arrFileSystemInfoToDelete.Length - 1)] = fileSystemInfoToDelete;
        }
        else
        {
            throw new Exception("Файл или каталог не найден.");
        }        
    }
}
