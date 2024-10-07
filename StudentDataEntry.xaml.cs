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
using System.Xml.Serialization;

namespace ElektronikusEllenorzo
{
    /// <summary>
    /// Interaction logic for StudentDataEntry.xaml
    /// </summary>
    public partial class StudentDataEntry : Window
    {
        List<Student> studentsDataList = new();
        public StudentDataEntry()
        {
            InitializeComponent();
            
        }

        private void kuldes_Click(object sender, RoutedEventArgs e)
        {
            GetStudentData();
        }



        public void GetStudentData()
        {
            //test
            //TODO: Ha hibás adat ne dögöljön meg az alkalmazás:)))
            string sName, bPlace, any, szulhely, szak, oszt, kolNev;
            DateTime bT, beir;
            bool kol;
            try
            {
                sName = tNev.Text;
                bPlace = szHely.Text;
                bT = Convert.ToDateTime(szIdo.Text);
                any = anyjaNeve.Text;
                szulhely = "Valami";
                beir = new(2002, 3, 15);
                szak = "Programozo";
                oszt = "9.F";
                kol = Convert.ToBoolean(kollegium.IsChecked);
                kolNev = kollegiumNev.Text;

                
                studentsDataList.Add(kol ? new(1, sName, bPlace, bT, any, szulhely, beir, szak, oszt, kol, kolNev) : new(1, sName, bPlace, bT, any, szulhely, beir, szak, oszt, kol, kolNev));
            }
            catch (FormatException)
            {
                MessageBox.Show("Hibás adat");
                throw;
                
            }

            JsonOut.WriteToJSON(studentsDataList, "studentsData.json");
        }

        //public void GetStudentDataFromJSON()
        //{
        //    string jsonPath = "studentsData.json";
        //    Student myDeserializedClass = JsonSerializer.Deserialize<Student>(jsonPath);

        //}


        public class JsonOut()
        {
            public static void WriteToJSON<T>(T list, string fileName)
            {
                string jsonOut = JsonSerializer.Serialize(list);
                File.WriteAllText(fileName, jsonOut);
            }
        }
    }
}
