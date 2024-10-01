using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Text.Json;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Elektronikus_ellenőrző
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            CheckData();
        }

        private void CheckData()
        {
            string subjectName = SubjectInput.Text;
            CheckBox[] grades = { NinthGradeCheckBox, TenthGradeCheckBox, EleventhGradeCheckBox, TwelfthGradeCheckBox1, ThirteenthGradeCheckBox };
            string selectedGrades = "";
            string typeOfSubject = CoreSubjectRadioButton.IsChecked == true ? "Közismereti" : "Szakmai";
            TextBox[] lessonsPerWeekByGradeInputs = { LessonsPerWeekNinthGrade, LessonsPerWeekTenthGrade, LessonsPerWeekEleventhGrade, LessonsPerWeekTwelfthGrade, LessonsPerWeekThirteenthGrade };
            string lessonsPerWeekByGrade = "";
            string lessonsPerYearByGrade = "";

            foreach (CheckBox grade in grades)
            {
                if (grade.IsChecked == true)
                    selectedGrades += $"{grade.Content.ToString()} ";
            }

            selectedGrades = selectedGrades.Trim().Replace(' ', ',');

            for (int i = 0; i < lessonsPerWeekByGradeInputs.Length; i++)
            {
                if (lessonsPerWeekByGradeInputs[i].Text != "")
                {
                    lessonsPerWeekByGrade += $"{lessonsPerWeekByGradeInputs[i].Text} ";

                    if (i <= 2)
                        lessonsPerYearByGrade += $"{Convert.ToInt32(lessonsPerWeekByGradeInputs[i].Text) * 36} ";
                    else if (i == 3)
                    {
                        if (typeOfSubject == "Közismereti")
                            lessonsPerYearByGrade += $"{Convert.ToInt32(lessonsPerWeekByGradeInputs[i].Text) * 31} ";
                        else
                            lessonsPerYearByGrade += $"{Convert.ToInt32(lessonsPerWeekByGradeInputs[i].Text) * 36} ";
                    }
                    else
                        lessonsPerYearByGrade += $"{Convert.ToInt32(lessonsPerWeekByGradeInputs[i].Text) * 31} ";
                }
                    
            }

            lessonsPerWeekByGrade = lessonsPerWeekByGrade.Trim().Replace(' ', ',');
            lessonsPerYearByGrade = lessonsPerYearByGrade.Trim().Replace(' ', ',');

            string lineToSave = $"{subjectName};{typeOfSubject};{selectedGrades};{lessonsPerWeekByGrade};{lessonsPerYearByGrade}";
            List<string> jsonToSave = new List<string>();
            jsonToSave.Append(subjectName);
            jsonToSave.Append(typeOfSubject);
            jsonToSave.Append(selectedGrades);
            jsonToSave.Append(lessonsPerWeekByGrade);
            jsonToSave.Append(lessonsPerYearByGrade);
            MessageBox.Show($"{lineToSave}");

            SaveToCSV(lineToSave);
            SaveToJSON(jsonToSave);
        }

        private void SaveToCSV(string lineToSave)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "subjects.csv");

            StreamWriter writer = new StreamWriter(filePath, append: true, Encoding.UTF8);

            if (!File.Exists(filePath))
            {
                writer.WriteLine("Tantárgy;Közismereti/Szakmai;Évfolyamok;Heti óraszám évfolyamonként;Éves óraszám évfolyamonként");
            }

            writer.WriteLine(lineToSave);
            writer.Close();
        }

        private void SaveToJSON(List<string> jsonToSave)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "subjects.json");

            File.WriteAllText(filePath, JsonSerializer.Serialize(jsonToSave));
        }
    }
}