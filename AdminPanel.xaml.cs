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
                var sorted = StudentDataEntry.studentsDataList.OrderBy(keySelector: x => x.Trade);
                dataGrid.ItemsSource = sorted;
                UpdateStatistics();
            }
            
        }

        private void UpdateStatistics()
        {
            var dormitoryCount = StudentDataEntry.studentsDataList.Count(predicate: x => x.Dormitory);
            var debCount = StudentDataEntry.studentsDataList.Count(predicate: x => x.Address.Contains(value: "Debrecen"));
            var notDebCount = StudentDataEntry.studentsDataList.Count(predicate: x => !x.Address.Contains(value: "Debrecen") && !x.Dormitory);

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
                MessageBox.Show(messageBoxText: "Válassz ki egy tanulót");
                return;
            }

            MessageBoxResult messageBox = MessageBox.Show(messageBoxText: "Biztos szeretné törölni a tanulót?", caption: "", button: MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                StudentDataEntry.studentsDataList.Remove(item: selectedStudent);
                dataGrid.ItemsSource = null;
                StudentDataEntry.StudentRecordSheetNumber(list: StudentDataEntry.studentsDataList);
                var sorted = StudentDataEntry.studentsDataList.OrderBy(keySelector: x => x.Trade);
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
