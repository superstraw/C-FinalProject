using System.Net.Mail;
using Microsoft.VisualBasic.FileIO;

namespace FinalProject;

internal static class Program
{
    private static void Main(string[] args)
    {
        var studentList = new List<Student>();
        Start();

        void Start()
        {
            while (true)
            {
                Console.WriteLine("Student Club Management");
                Console.WriteLine("#######################");
                Console.WriteLine("1. Add Student\n2. Delete Student\n3. Edit Student\n4. List Students\n5. Exit");
                Console.Write("\nSelect an option: ");
                var menuSel = Console.ReadKey().KeyChar;
                Console.Clear();

                switch (menuSel)
                {
                    case '1':
                        string? firstName;

                        do
                        {
                            Console.Write("Enter first name: ");
                            firstName = Console.ReadLine();
                            NameCheck(firstName, "First");
                        } while (!NameCheck(firstName, "First"));

                        firstName = char.ToUpper(firstName[0]) + firstName[1..].ToLower();

                        string? lastName;

                        do
                        {
                            Console.Write("Enter last name: ");
                            lastName = Console.ReadLine();
                            NameCheck(lastName, "Last");
                        } while (!NameCheck(lastName, "Last"));

                        lastName = char.ToUpper(lastName[0]) + lastName[1..].ToLower();

                        string? email;

                        do
                        {
                            Console.Write("Enter email: ");
                            email = Console.ReadLine();
                            if (IsEmail(email)) continue;
                            Console.Clear(); 
                            Console.Write("Invalid email address.\n");
                        } while (!IsEmail(email));

                        studentList.Add(new Student(firstName, lastName, email));

                        Console.Write("\nStudent added successfully\nPress any key to continue. ");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case '2':
                        if (studentList.Count <= 0)
                            EmptyList();
                        else
                        {
                            Console.Clear();
                            var delete = 0;
                            GetList();
                            do
                            {
                                Console.Write("\nEnter # to delete: ");

                                var input = Console.ReadLine();

                                if (!int.TryParse(input, out delete) || delete <= 0 || delete > studentList.Count)
                                    Console.Write("Invalid entry.");
                            } while (delete <= 0 || delete > studentList.Count);

                            delete -= 1;

                            Console.Write($"\n{studentList[delete].FirstLast()} has been deleted successfully.\nPress any key to continue. ");
                            Console.ReadKey();
                            studentList.RemoveAt(delete);
                            Console.Clear();
                        }

                        break;
                    case '3':
                        if (studentList.Count <= 0)
                            EmptyList();
                        else
                        {
                            Console.Clear();
                            var edit = 0;
                            GetList();
                            do
                            {
                                Console.Write("\nEnter # to edit: ");

                                var input = Console.ReadLine();

                                if (!int.TryParse(input, out edit) || edit <= 0 || edit > studentList.Count)
                                    Console.Write("Invalid entry.");
                            } while (edit <= 0 || edit > studentList.Count);

                            edit -= 1;
                            
                            string? editFirstName;
                            do
                            {
                                Console.Write("Enter new first name: ");
                                editFirstName = Console.ReadLine();
                                NameCheck(editFirstName, "First");
                            } while (!NameCheck(editFirstName, "First"));

                            editFirstName = char.ToUpper(editFirstName[0]) + editFirstName[1..].ToLower();

                            string? editLastName;

                            do
                            {
                                Console.Write("Enter new last name: ");
                                editLastName = Console.ReadLine();
                                NameCheck(editLastName, "Last");
                            } while (!NameCheck(editLastName, "Last"));

                            editLastName = char.ToUpper(editLastName[0]) + editLastName[1..].ToLower();

                            string? editEmail;

                            do
                            {
                                Console.Write("Enter email: ");
                                editEmail = Console.ReadLine();
                                if (IsEmail(editEmail)) continue;
                                Console.Clear(); 
                                Console.Write("Invalid email address.\n");
                            } while (!IsEmail(editEmail));

                            
                            Console.Write($"\n{studentList[edit].FirstLast()} has been edited successfully.\nPress any key to continue. ");
                            Console.ReadKey();
                            studentList[edit] = new Student(editFirstName, editLastName, editEmail);
                            Console.Clear();
                            
                        }

                        break;
                    case '4':
                        if (studentList.Count <= 0)
                        {
                            EmptyList();
                        }
                        else
                        {
                            Console.Clear();
                            GetList();
                            Console.Write("\nPress any key to continue. ");
                            Console.ReadKey();
                            Console.Clear();
                        }

                        break;
                    case '5':
                        return;
                    default:
                        Console.Clear();
                        Console.Write("Invalid Entry.\nPress any key to continue. ");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        void EmptyList()
        {
            Console.Clear();
            Console.Write("The list is empty.\nPress any key to continue. ");
            Console.ReadKey();
            Console.Clear();
            Start();
        }

        void GetList()
        {
            for (var i = 0; i < studentList.Count; i++)
                Console.WriteLine($"{i + 1}. {studentList[i].ToString()}");
        }

        bool IsEmail(string email)
        {
            try
            {
                var address = new MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

        bool NameCheck(string? name, string location)
        {
            if (name == null || name.Any(char.IsDigit))
            {
                Console.Clear();
                Console.Write("Invalid Entry.\n");
                return false;
            }

            if (name is not {Length: < 2} || name.Any(char.IsDigit)) return true;
            Console.Clear();
            Console.Write($"{location} name must be at least 2 characters long.\n");
            return false;

        }
    }
}