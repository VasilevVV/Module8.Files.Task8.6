using Module8.Files.Task6.Exercise4.Data;

namespace Module8.Files.Task6.Exercise4.ConsolePresenter;

public class Program
{
    public static void Main(string[] args)
    {

        Console.WriteLine($"Введите полный путь до бинарного файла для чтения данных о студентах:");
        string pathToFile = Primitives.GetPathToFileFromConsole();
        var fileInfo = new FileInfo(pathToFile);

        List<Student> studentsToRead = new List<Student>();

        try
        {
            studentsToRead = DataReader.ReadStudentsDataFromBinFile(fileInfo);
        }        
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при чтении файла: {ex.ToString()}");
            Console.WriteLine("Неправильный формат файла. Завершаем работу программы.");
        }

#if DEBUG
        foreach (Student studentProp in studentsToRead)
        {
            Console.WriteLine(studentProp.Name + " " + studentProp.Group + " " + studentProp.DateOfBirth.ToShortDateString() + " " + studentProp.AverageScore);
        }
#endif
        try
        {
            DataWriter.WriteStudentsIntoGroupsToDesktop(studentsToRead);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при записи файла: {ex.ToString()}");
            Console.WriteLine("Завершаем работу программы.");
        }       
        Console.WriteLine("Запись студентов по группам на рабочий стол завершена.");

        Console.ReadKey();

    }
}
