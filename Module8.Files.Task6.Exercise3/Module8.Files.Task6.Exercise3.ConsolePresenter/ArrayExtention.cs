namespace Module8.Files.Task6.Exercise3.ConsolePresenter;

/// <summary>
/// Расширение для массива
/// </summary>
internal static class ArrayExtention
{
    /// <summary>
    /// Проверка массива на равенство нулю или пустым значениям
    /// </summary>
    /// <param name="array">Массив</param>
    /// <returns>True если массив равен null или пустой, false - если нет</returns>
    internal static bool IsNullOrEmptyArr(this Array array)
    {
        return (array == null || array.Length == 0);
    }
}
