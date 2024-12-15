namespace Module8.Files.Task6.Exercise4.Data;

/// <summary>
/// Запись данных
/// </summary>
public static class DataWriter
{
    /// <summary>
    /// Запись данных о студентах в файла
    /// </summary>
    /// <param name="students">Данные о студентах</param>
    /// <param name="fileName">Путь до файла для записи</param>
    public static void WriteStudentsToBinFile(List<Student> students, string fileName)
    {
        using FileStream fs = new FileStream(fileName, FileMode.Create);
        using BinaryWriter bw = new BinaryWriter(fs);

        foreach (Student student in students)
        {
            bw.Write(student.Name);
            bw.Write(student.Group);
            bw.Write(student.DateOfBirth.ToBinary());
            bw.Write(student.AverageScore);
        }
        bw.Flush();
        bw.Close();
        fs.Close();

    }

    /// <summary>
    /// Запись данных о студентах по группа на рабочий стол в папку Students
    /// </summary>
    /// <param name="students">Данные о студентах для записи</param>
    public static void WriteStudentsIntoGroupsToDesktop(List<Student> students)
    {
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string pathToStudents = desktop + @"\Students";
        DirectoryInfo dirInfo = new DirectoryInfo(desktop);
        if (!Directory.Exists(pathToStudents))
        {
            Directory.CreateDirectory(pathToStudents);
        }
        foreach (Student student in students)
        {
            string filePath = pathToStudents + @"\" + student.Group + @".txt";
            if (!File.Exists(filePath))
            {
                using StreamWriter sw = File.CreateText(filePath);
                sw.WriteLine(student.GetStudentToLine());
                sw.Close();
            }
            else if (File.Exists(filePath)) 
            {
                if (!DataReader.IsStudentExistInFile(filePath, student))
                {
                    using StreamWriter sw = new StreamWriter(filePath, true, System.Text.Encoding.UTF8);
                    sw.WriteLine(student.GetStudentToLine());
                    sw.Close();
                }
            }            
        }
    }


    
}
