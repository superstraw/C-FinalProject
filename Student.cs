namespace FinalProject;

public class Student
{
    public Student(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public Student()
    {
        FirstName = "Null";
        LastName = "Null";
        Email = "Null";
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName} | {Email}";
    }

    public string FirstLast()
    {
        return $"{FirstName} {LastName}";
    }
}