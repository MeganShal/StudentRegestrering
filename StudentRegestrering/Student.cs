using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegestrering
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public int StudentClassId { get; set; }
        public StudentClass StudentClass { get; set; }

        public Student() { }

        public Student (string firstName, string lastName, string city, int studentClassId)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            StudentClassId = studentClassId;
        }
    }
}
