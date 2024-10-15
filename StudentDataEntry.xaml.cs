using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
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
        public List<Student> studentsDataList = new();
        string[] tradeArrey = {"Programozó","Gépész","Pék","Lakatos"};
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
        }

        public void GetStudentData()
        {
            Student student;
            string sName, bPlace, mName, residence, trate, @class, dName;
            DateOnly bDate, eDate;
            bool dormitry;
            try
            {
                sName = tNev.Text;
                bPlace = szHely.Text;
                bDate = DateOnly.Parse(szIdo.Text) ;
                mName = anyjaNeve.Text;
                residence = lakcim.Text;
                eDate = DateOnly.Parse(beiratkozasIdo.Text);
                trate = szakok.SelectedItem.ToString();
                @class = osztaly.Text;
                dormitry = Convert.ToBoolean(kollegium.IsChecked);
                dName = kollegiumNev.Text;


                EmptyTextBox(sName, bPlace, mName, residence, trate, bDate, eDate);

                if (dormitry)
                {
                    student = new(0, "", sName, bPlace, bDate, mName, residence, eDate, trate, @class, dormitry, dName);
                    //StudentRecordSheetNumber(student);
                }
                else
                {
                    student = new(0, "", sName, bPlace, bDate, mName, residence, eDate, trate, @class, dormitry);
                    //StudentRecordSheetNumber(student);
                }

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
            WriteToFiles.WriteToCSV(studentsDataList, "studentsData.json");
        }

        private void StudentRecordSheetNumber(Student student)
        {
            //TODO: Valami jo logikat erre kitalalni

            //var classStudents = studentsDataList.Where(s => s.ClassName == student.ClassName).ToList();

            //if (student.EnrollmentDate.Month < 9)
            //{
            //    classStudents = classStudents.OrderBy(s => s.Name).ToList();
            //}
            //else
            //{
            //    classStudents = classStudents.OrderBy(s => s.EnrollmentDate).ToList();
            //}

            //student.Id = classStudents.Count + 1;
            //student.RecordSheetNumber = $"{student.Id}/{student.EnrollmentDate.Year}";
        }

        private void EmptyTextBox(string sName, string bPlace,string mName,string residence,string trate, DateOnly bDate, DateOnly eDate)
        { 

            //if (string.IsNullOrEmpty(sName) || string.IsNullOrEmpty(bPlace) || string.IsNullOrEmpty(mName) || string.IsNullOrEmpty(residence) || string.IsNullOrEmpty(trate) || string.IsNullOrEmpty(@class) || string.IsNullOrEmpty(dName) || string.IsNullOrEmpty(bDate.ToString()) || string.IsNullOrEmpty(eDate.ToString()))
            //{
            //    throw new NullReferenceException();
            //}
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            AdminPanel admin = new(studentsDataList);
            admin.Show();
        }
    }

    public class WriteToFiles()
    {
        public static void WriteToJSON<T>(T list, string fileName)
        {
            string jsonOut = JsonSerializer.Serialize(list);
            File.WriteAllText(fileName, jsonOut);
        }

        public static void WriteToCSV(List<Student> list, string fileName)
        {
            string csvOut = "";
            foreach (var item in list)
            {
                csvOut += $"{item.Name};{item.BirthPlace};{item.BirthDate};{item.MotherName};{item.Address};{item.EnrollmentDate};{item.ClassName};{item.ClassChar};{item.Dormitory};{item.DormitoryName}\n";
            }

            File.WriteAllText(fileName, csvOut);
        }
    }
}
