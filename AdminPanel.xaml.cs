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

namespace ElektronikusEllenorzo
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        private List<Student> studentsDataList;
        public AdminPanel(List<Student> student)
        {
            InitializeComponent();
            this.studentsDataList = student;
            dataGrid.ItemsSource = student;
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            int dormitryCount = studentsDataList.Count(x => x.Dormitory);
            int debCount = studentsDataList.Count(x => x.Address.Contains("Debrecen"));
            int notDebCount = studentsDataList.Count(x => !x.Address.Contains("Debrecen") && !x.Dormitory);

            dormCount.Content = $"Kollégisták: {dormitryCount}";
            deb.Content = $"Debreceni: {debCount}";
            notDeb.Content = $"Bejáros: {notDebCount}";
            studentsCount.Content = $"Felvett tanulók: {studentsDataList.Count}";
        }

        private void delStudent_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudent = (Student)dataGrid.SelectedItem;
            MessageBoxResult messageBox = MessageBox.Show("Biztos szeretné törölni a tanulót?", "", MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {   studentsDataList.Remove(selectedStudent);
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = studentsDataList;
            }
        }
    }
}
