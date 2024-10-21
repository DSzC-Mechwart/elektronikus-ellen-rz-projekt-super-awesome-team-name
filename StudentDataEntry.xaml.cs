using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ElektronikusEllenorzo
{
    /// <summary>
    /// Interaction logic for StudentDataEntry.xaml
    /// </summary>
    public partial class StudentDataEntry : Window
    {
        public static List<Student> studentsDataList = new();
        string[] tradeArrey = ["Programozó", "Gépész", "Pék", "Lakatos"];
        public StudentDataEntry()
        {
            InitializeComponent();

            foreach (var item in tradeArrey)
            {
                szakok.Items.Add(item);
            }
            
        }

        private void Kuldes_Click(object sender, RoutedEventArgs e)
        {
            GetStudentData();
            StudentRecordSheetNumber(studentsDataList);
        }

        public void GetStudentData()
        {
            Student student;
            string sName, bPlace, mName, residence, trade, @class, dName, bDate, eDate;
            bool dormitry;
            try
            {
                sName = tNev.Text;
                bPlace = szHely.Text;
                bDate = szIdo.Text;
                mName = anyjaNeve.Text;
                residence = lakcim.Text;
                eDate = beiratkozasIdo.Text;
                trade = szakok.SelectedItem.ToString() ?? throw new FormatException();
                @class = osztaly.Text;
                dormitry = Convert.ToBoolean(kollegium.IsChecked);
                dName = kollegiumNev.Text;


                

                if (dormitry)
                {
                    EmptyTextBox(sName, bPlace, mName, residence, trade, bDate, eDate);
                    student = new Student(0, "", sName, bPlace, bDate, mName, residence, eDate, trade, @class, dormitry, dName);
                }
                else
                {
                    EmptyTextBox(sName, bPlace, mName, residence, trade, bDate, eDate, dName);
                    student = new Student(0, "", sName, bPlace, bDate, mName, residence, eDate, trade, @class, dormitry);
                }

                StudentRecordSheetNumber(student);

                studentsDataList.Add(student);
            }
            catch (FormatException)
            {
                MessageBox.Show("Hibás adat");
                return;

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Az összes mezőt ki kell tölteni!");
            }

            WriteToFiles.WriteToJSON(studentsDataList, "studentsData.json");
            WriteToFiles.WriteToCSV(studentsDataList, "studentsData.csv");
        }

        //public void GetStudentData()
        //{
            
        //}

        private void StudentRecordSheetNumber(Student student)
        {
            var studentClass = studentsDataList.Where(x => x.Trade == student.Trade).ToList();
            var preSeptStudents = studentClass.Where(x => x.GetEnrollmentDate().Month < 9).OrderBy(x => x.Name).ToList();
            var postSeptStudents = studentClass.Where(x => x.GetEnrollmentDate().Month <= 9).OrderBy(x => x.EnrollmentDate).ToList();

            if (student.GetEnrollmentDate().Month < 9)
            {
                preSeptStudents.Add(student);
                preSeptStudents = preSeptStudents.OrderBy(x => x.Name).ToList();
            }
            else
            {
                postSeptStudents.Add(student);
                postSeptStudents = postSeptStudents.OrderBy(x => x.GetEnrollmentDate()).ToList();
            }

            preSeptStudents = preSeptStudents.Concat(postSeptStudents).ToList();

            var index = 0;
            foreach (var item in preSeptStudents)
            {
                index++;
                item.Id = index;
                item.RecordSheetNumber = $"{item.Id}/{item.GetEnrollmentDate().Year}";
            }

        }
        public static void StudentRecordSheetNumber(List<Student> list)
        {
            var classes = list.GroupBy(x => x.Trade).ToList();

            foreach (var item in classes)
            {
                var index = 0;
                foreach (var student in item)
                {
                    index++;
                    student.Id = index;
                    student.RecordSheetNumber = $"{student.Id}/{student.GetEnrollmentDate().Year}";
                }
            }
        }

        private static void EmptyTextBox(string sName, string bPlace,string mName,string residence,string trade, string bDate, string eDate)
        {

            if (string.IsNullOrEmpty(sName) || string.IsNullOrEmpty(bPlace) || string.IsNullOrEmpty(mName) || string.IsNullOrEmpty(residence) || string.IsNullOrEmpty(trade) || string.IsNullOrEmpty(bDate.ToString()) || string.IsNullOrEmpty(eDate.ToString()))
            {
                throw new NullReferenceException();
            }
        }
        private static void EmptyTextBox(string sName, string bPlace, string mName, string residence, string trade, string bDate, string eDate, string dName)
        {

            if (string.IsNullOrEmpty(sName) || string.IsNullOrEmpty(bPlace) || string.IsNullOrEmpty(mName) || string.IsNullOrEmpty(residence) || string.IsNullOrEmpty(trade) ||  string.IsNullOrEmpty(dName) || string.IsNullOrEmpty(bDate.ToString()) || string.IsNullOrEmpty(eDate.ToString()))
            {
                throw new NullReferenceException();
            }
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            AdminPanel admin = new();
            admin.Show();
            Close();
        }

        private void Vissza_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new();
            main.Show();
            Close();
        }
    }

    public class WriteToFiles()
    {
        public static void WriteToJSON<T>(T list, string fileName)
        {
            var jsonOut = JsonSerializer.Serialize(list);
            File.WriteAllText(fileName, jsonOut);
        }

        public static void WriteToCSV(List<Student> list, string fileName)
        {
            var csvOut = list.Aggregate("", (current, item) => current + $"{item.Name};{item.BirthPlace};{item.BirthDate};{item.MotherName};{item.Address};{item.EnrollmentDate};{item.Trade};{item.ClassChar};{item.Dormitory};{item.DormitoryName}\n");

            File.WriteAllText(fileName, csvOut);
        }
    }

    public class ReadFromFiles()
    {
        public static void ReadFromJSON(string path)
        {
            StudentDataEntry.studentsDataList.Clear();

            string jsonString = File.ReadAllText(path);
            var json = JsonSerializer.Deserialize<ObservableCollection<Student>>(jsonString);
            foreach (var student in json)
            {
                StudentDataEntry.studentsDataList.Add(student);
            }
        }

        public static void ReadFromCSV(string fileName)
        {
            StudentDataEntry.studentsDataList.Clear();

            foreach (var item in File.ReadAllLines($"{fileName}.csv"))
            {
                var parts = item.Split(';');
                var id = int.Parse(parts[0]);
                var record = parts[1];
                var name = parts[2];
                var birthP  = parts[3];
                var birthD = parts[4];
                var motherN = parts[5];
                var address = parts[6];
                var enrollmentD = parts[7];
                var trade = parts[8];
                var classChar = parts[9];
                var dormitory = bool.Parse(parts[10]);
                var dormitoryN = parts[11];

                Student student = new Student(id, record, name, birthP, birthD, motherN, address, enrollmentD, trade, classChar, dormitory, dormitoryN);
                StudentDataEntry.studentsDataList.Add(student);
            }
        }
    }
}
