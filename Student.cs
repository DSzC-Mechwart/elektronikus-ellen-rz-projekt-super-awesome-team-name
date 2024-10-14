using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronikusEllenorzo
{
    public class Student
    {
        public int Id { get; set; }
        public string RecordSheetNumber { get; set; }
        public string Name { get; set; }
        public string BirthPlace { get; set; }
        public DateOnly BirthDate { get; set; }
        public string MotherName { get; set; }
        public string Address { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public string ClassName { get; set; }
        public string ClassChar { get; set; }
        public bool? Dormitory { get; set; }
        public string DormitoryName { get; set; }
        public Student(int id, string recordSheetNumber, string name, string birthPlace, DateOnly birthDate, string motherName, string address, DateOnly enrollmentDate, string className, string classChar, bool? dormitory, string dormitoryName)
        {
            Id = id;
            RecordSheetNumber = recordSheetNumber;
            Name = name;
            BirthPlace = birthPlace;
            BirthDate = birthDate;
            MotherName = motherName;
            Address = address;
            EnrollmentDate = enrollmentDate;
            ClassName = className;
            ClassChar = classChar;
            Dormitory = dormitory;
            DormitoryName = dormitoryName;   
        }

        public Student(int id, string recordSheetNumber, string name, string birthPlace, DateOnly birthDate, string motherName, string address,DateOnly enrollmentDate, string className, string classChar, bool? dormitory)
        {
            Id = id;
            RecordSheetNumber = recordSheetNumber;
            Name = name;
            BirthPlace = birthPlace;
            BirthDate = birthDate;
            MotherName = motherName;
            Address = address;
            EnrollmentDate = enrollmentDate;
            ClassName = className;
            ClassChar = classChar;
            Dormitory = dormitory;
            DormitoryName = "";
        }
    }
}
