using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegestrering
{
    public class StudentClass
    {
        
        public int StudentClassId { get; set; }
        public string ClassName { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public virtual List<Student> Students { get; set; }


    }
}
