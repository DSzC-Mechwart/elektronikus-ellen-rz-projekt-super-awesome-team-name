using System;
using System.Collections.Generic;
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
using System.Diagnostics;

namespace ElektronikusEllenorzo
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        
        public AdminPanel()
        {
            Debug.Write(StudentDataEntry.studentsDataList);
            InitializeComponent();
            dataGrid.ItemsSource = null;
            if (StudentDataEntry.studentsDataList != null)
            {
                var sorted = StudentDataEntry.studentsDataList.OrderBy( x => x.Trade);
                dataGrid.ItemsSource = sorted;
                UpdateStatistics();
            }
            
        }

        private void UpdateStatistics()
        {
            var dormitoryCount = StudentDataEntry.studentsDataList.Count( x => x.Dormitory);
            var debCount = StudentDataEntry.studentsDataList.Count( x => x.Address.Contains("Debrecen"));
            var notDebCount = StudentDataEntry.studentsDataList.Count(x => !x.Address.Contains("Debrecen") && !x.Dormitory);

            dormCount.Content = $"Kollégisták: {dormitoryCount}";
            deb.Content = $"Debreceni: {debCount}";
            notDeb.Content = $"Bejáros: {notDebCount}";
            studentsCount.Content = $"Felvett tanulók: {StudentDataEntry.studentsDataList.Count}";
        }

        private void delStudent_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudent = (Student)dataGrid.SelectedItem;
            if (selectedStudent == null)
            {
                MessageBox.Show( "Válassz ki egy tanulót");
                return;
            }

            MessageBoxResult messageBox = MessageBox.Show( "Biztos szeretné törölni a tanulót?","", MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                StudentDataEntry.studentsDataList.Remove(selectedStudent);
                dataGrid.ItemsSource = null;
                StudentDataEntry.StudentRecordSheetNumber(StudentDataEntry.studentsDataList);
                var sorted = StudentDataEntry.studentsDataList.OrderBy(x => x.Trade);
                dataGrid.ItemsSource = sorted;
            }
            UpdateStatistics();
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            Load load = new();
            load.Show();
            Close();
        }

        private void Vissza_Click(object sender, RoutedEventArgs e)
        {
            StudentDataEntry s = new();
            s.Show();
            Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Save s = new();
            s.Show();
            Close();
        }
    }
}
