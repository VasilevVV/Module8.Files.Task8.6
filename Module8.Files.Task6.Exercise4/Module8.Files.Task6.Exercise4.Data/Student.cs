namespace Module8.Files.Task6.Exercise4.Data;

/// <summary>
/// Студент
/// </summary>
public class Student
{
    /// <summary>
    /// Имя студента
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Группа
    /// </summary>
    public string Group { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Средний балл
    /// </summary>
    public decimal AverageScore { get; set; }

    /// <summary>
    /// Вывод информации о студенте в формате "Имя, дата рождения, средний балл".
    /// </summary>
    /// <returns>Имя, дата рождения, средний балл</returns>
    public string GetStudentToLine()
    {
        string studentInfoToLine = $"{this.Name}, {this.DateOfBirth.ToShortDateString()}, {this.Name}";
        return studentInfoToLine;
    }
}
