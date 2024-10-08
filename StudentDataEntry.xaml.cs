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
        string[] tradeArrey = {"Programozó","Gépész","Pék","Lakatos"};
        public StudentDataEntry()
        {
            InitializeComponent();

            foreach (var item in tradeArrey)
            {
                szakok.Items.Add(item);
            }
            
        }

        private void kuldes_Click(object sender, RoutedEventArgs e)
        {
            GetStudentData();
        }



        public void GetStudentData()
        {
            //test
            //TODO: Ha hibás adat ne dögöljön meg az alkalmazás:)))
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
                trate = szakok.SelectedItem.ToString() ?? "Problema";
                @class = osztaly.Text;
                dormitry = Convert.ToBoolean(kollegium.IsChecked);
                dName = kollegiumNev.Text;


                
                studentsDataList.Add(dormitry ? new(1, sName, bPlace, bDate, mName, residence, eDate, trate, @class, dormitry, dName) : new(1, sName, bPlace, bDate, mName, residence, eDate, trate, @class, dormitry, dName));
            }
            catch (Exception ex)
            {
                if (ex is FormatException || ex is NullReferenceException)
                {
                    MessageBox.Show("Hibás adat");
                    return;
                }

                return;

            }

            JsonOut.WriteToJSON(studentsDataList, "studentsData.json");

        }

        //public void GetStudentDataFromJSON()
        //{
        //    string jsonPath = "studentsData.json";
        //    Student myDeserializedClass = JsonSerializer.Deserialize<Student>(jsonPath);

        //}


       
    }
    public class JsonOut()
    {
        public static void WriteToJSON<T>(T list, string fileName)
        {
            string jsonOut = JsonSerializer.Serialize(list);
            File.WriteAllText(fileName, jsonOut);
        }
    }
}
