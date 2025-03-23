using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegestrering
{
    class StudentManagment
    {
        public int input;
        private ProgramDb db;
        private Start start;
        public StudentManagment(Start startInstance)
        {
            db = new ProgramDb();
            start = startInstance;
        }

        public void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("Du ska nu registrera en student, följ händvisningarna nedan!");
            Console.Write("\nVad är studentens namn: "); string name = Console.ReadLine();
            Console.Write("\nVad är studentens efternamn: "); string lastNamn = Console.ReadLine();
            Console.Write("\nVilken stad bor studenten i: "); string city = Console.ReadLine();
            Console.WriteLine("\n\nStudenten är nästanskapad, Välj en av de befintliga klasserna nedan för att placera studenten i önskad klass");
            Console.WriteLine("\nExisterande klasser\n----------------------------------------\n");

            foreach(var classes in db.StudentClasses)
            {
                Console.WriteLine($"Class Id: {classes.StudentClassId} Class namn: {classes.ClassName} ");
            }
            Console.WriteLine("\n----------------------------------------");
            Console.Write("\nAnge classens Id för att placera studenten i classen: "); CheckInput();                     

            var student = new Student(name, lastNamn, city, input);
            db.Add(student);
            db.SaveChanges();
            Console.WriteLine("\nStudenten registrerades!");
            ContinueMessage();
        }

        public void ListaAllaStudenter()
        {
            Console.Clear();

            Console.WriteLine("Listan av alla registrerade studenter\n");

            if (db.Students.Any())
            {
                foreach (var students in db.Students.Include(s => s.StudentClass))
                {
                    Console.WriteLine($"Student Id: {students.StudentId} - {students.FirstName} {students.LastName} {students.City} {students.StudentClass.ClassName}");
                }
                Console.Write("\nTryck \"1\" för att radera en student eller tryck \"2\" för att återgå till meny: "); CheckInput();

                if (input == 1)
                {
                    Console.Write("\nDu kommer nu att radera en student, var vänlig att mata in studentens ID: "); CheckInput();

                    var removeStudent = db.Students.First(s => s.StudentId == input);
                    db.Students.Remove(removeStudent);
                    db.SaveChanges();
                    Console.WriteLine("Studenten raderas!");
                    ContinueMessage();
                }
                else if (input == 2)
                {
                    start.RunStart();
                }
                else Console.WriteLine("\nDu angav inte en tillgänglig kommando"); ContinueMessage();              
            }

            else Console.WriteLine("Whoops det finns inga registrerade studenter var vänlig att regestrera en student först!"); ContinueMessage();
        }


        public void ChangeStudent()
        {
            Console.Clear();
            Console.WriteLine("För att ändra en student följ händvisningarna nedan!");
            Console.Write("\nSök på studentens förnamn, efternamn eller stad för att hitta studenten: ");
            string input1 = Console.ReadLine();

            var matchingStudents = db.Students.Where(n => n.FirstName.Contains(input1) || n.LastName.Contains(input1) || n.City.Contains(input1));

            if (!matchingStudents.Any())
            {
                Console.WriteLine("\nIngen student matchade din sökning");
                ContinueMessage();
            }

            Console.WriteLine($"\nAlla studenter med innehåll: {input1} \n------------------------------------\n");
            foreach (var students in matchingStudents)
            {
                Console.WriteLine($" Student Id {students.StudentId}: {students.FirstName} {students.LastName} {students.City}");
            }

            Console.Write("\n--------------------------------\n\nFör att välja en student, ange studentens ID: ");
            CheckInput();
            var selectedStudent = db.Students.Find(input);

            if (selectedStudent == null) ContinueMessage();

            Console.WriteLine($"\nDu har valt student {selectedStudent.StudentId}\nTryck enter för att lämna kategorin orörd\n--------------------------------------");

            Console.Write($"\nnuvarande Namn: {selectedStudent.FirstName} - Nytt namn: "); string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                selectedStudent.FirstName = newName;
            }

            Console.Write($"Nuvarande Efternamn: {selectedStudent.LastName} - Nytt efternamn: "); string newLastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newLastName))
            {
                selectedStudent.LastName = newLastName;
            }

            Console.Write($"Nuvarande Stad: {selectedStudent.City} - Ny stad: "); string newCity = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newCity))
            {
                selectedStudent.City = newCity;
            }
            db.SaveChanges();
            Console.WriteLine("\nStudenten är uppdaterad!");
            ContinueMessage();
        } 

        public void ContinueMessage()
        {
            Console.Write("\n----------------------------------------------\n \nTryck \"enter\" för att fortsätta: ");
            Console.ReadLine();
            start.RunStart();
        }

        public void CheckInput()
        {
            if (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("\nOtillåten inmatning, snälla försök igen!");
            }
        }

    }
}
