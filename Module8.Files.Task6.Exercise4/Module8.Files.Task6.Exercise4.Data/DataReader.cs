namespace Module8.Files.Task6.Exercise4.Data;

/// <summary>
/// Чтение данных
/// </summary>
public static class DataReader
{
    /// <summary>
    /// Чтение данных о студентах из бинарного файла
    /// </summary>
    /// <param name="file">Бинарный файл</param>
    /// <returns>Списов студентов</returns>
    public static List<Student> ReadStudentsDataFromBinFile(FileInfo file)
    {
        List<Student> result = new();
        using FileStream fs = new FileStream(file.FullName, FileMode.Open);
        using StreamReader sr = new StreamReader(fs);
        //Console.WriteLine(sr.ReadToEnd());
        fs.Position = 0;

        BinaryReader br = new BinaryReader(fs);

        while (fs.Position < fs.Length)
        {
            Student student = new Student();
            student.Name = br.ReadString();
            student.Group = br.ReadString();
            long dt = br.ReadInt64();
            student.DateOfBirth = DateTime.FromBinary(dt);
            student.AverageScore = br.ReadDecimal();

            result.Add(student);
        }

        fs.Close();
        return result;
    }

    /// <summary>
    /// Проверка наличия записи о студенте в файле с группой
    /// </summary>
    /// <param name="filePath">Путь до файла</param>
    /// <param name="student">Студент</param>
    /// <returns></returns>
    internal static bool IsStudentExistInFile(string filePath, Student student)
    {
        string studentInfoToLine = student.GetStudentToLine();
        bool isStudentExistInFile = false;
        using StreamReader sr = File.OpenText(filePath);

        string str = "";
        while ((str = sr.ReadLine()) != null)
        {
            if (str == studentInfoToLine)
            {
                isStudentExistInFile = true;
            }
        }
        sr.Close();

        return isStudentExistInFile;

    }
}
