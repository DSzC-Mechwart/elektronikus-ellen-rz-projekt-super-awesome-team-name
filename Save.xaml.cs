using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ElektronikusEllenorzo
{
    /// <summary>
    /// Interaction logic for Save.xaml
    /// </summary>
    public partial class Save : Window
    {
        public Save()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(fileName.Text))
            {
                MessageBox.Show("Nincs megadva fájlnév");
                return;
            }

            if (!File.Exists("savedFiles.txt"))
            {
                var file = File.Create("savedFiles.txt");
                file.Close();
            }
            var save = File.ReadAllText("savedFiles.txt");

            if (!save.Contains(fileName.Text))
            {
                save += $"{fileName.Text};";
                File.WriteAllText("savedFiles.txt", save);
            }

            WriteToFiles.WriteToCSV(StudentDataEntry.studentsDataList, $"{fileName.Text}.csv");
            WriteToFiles.WriteToJSON(StudentDataEntry.studentsDataList, $"{fileName.Text}.json");

            AdminPanel a = new();
            a.Show();
            Close();
        }

        private void Vissza_Click(object sender, RoutedEventArgs e)
        {
            AdminPanel a = new();
            a.Show();
            Close();
        }
    }
}
