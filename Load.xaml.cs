using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Path = System.IO.Path;

namespace ElektronikusEllenorzo
{
    /// <summary>
    /// Interaction logic for Load.xaml
    /// </summary>
    public partial class Load : Window
    {
        public Load()
        {
            InitializeComponent();
            LoadSaveFiles();
        }

        private void LoadSaveFiles()
        {
            var line = File.ReadAllText("savedFiles.txt");
            
            var split = line.Split(";");
            saveFiles.ItemsSource = split;
        }

        private void LoadFile()
        {
            MessageBoxResult messageBox = MessageBox.Show("Biztos szeretné betölteni az adatokat?", "", MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                if (saveFiles.SelectedItems == null)
                {
                    MessageBox.Show("Nincs file kiválasztva");
                    return;
                }

                foreach (var item in saveFiles.SelectedItems)
                {
                    //ReadFromFiles.ReadFromCSV(item.ToString());
                    ReadFromFiles.ReadFromJSON($"{item.ToString()}.json");
                }

                AdminPanel a = new AdminPanel();
                a.Show();
                Close();
            }
            
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            LoadFile();
        }

        private void Vissza_Click(object sender, RoutedEventArgs e)
        {
            AdminPanel a = new();
            a.Show();
            Close();
        }
    }
}
