using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace System.Prog_Дз_7_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Candidate> candidates = new List<Candidate>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddAllResumeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = @"C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                DirectoryInfo directory = new DirectoryInfo(dialog.FileName);
                FileInfo[] files = directory.GetFiles("*.txt");
                string[] filesName = new string[files.Length];
                for (int i = 0; i < files.Length; i++)
                {
                    filesName[i] = files[i].FullName;
                }
                Task.Run(() =>
                {
                    Parallel.ForEach(filesName, AddCandidate);
                });
            }
        }
        void AddCandidate(String fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                Candidate newCandidate = new Candidate();
                newCandidate.Name = sr.ReadLine();
                newCandidate.YearsOfExperience = int.Parse(sr.ReadLine());
                newCandidate.City = sr.ReadLine();
                newCandidate.SalaryRequirements = float.Parse(sr.ReadLine());
                candidates.Add(newCandidate);
            }
        }

        private void AddOneResumeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"D:\Visual studio\System.Prog_Дз_7\System.Prog_Дз_7_4";
            if (openFileDialog.ShowDialog() == true)
            {
                Task.Run(() =>
                {
                    Parallel.ForEach(openFileDialog.FileNames, AddCandidate);
                });
            }
        }

        private void ShowAllMenuItem_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            for (int i = 0; i < candidates.Count; i++)
            {
                listBox.Items.Add(candidates[i]);
            }
        }

        private void MostExperiencedCandidate_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();

            listBox.Items.Add(candidates.AsParallel().AsOrdered().OrderByDescending(c => c.YearsOfExperience).First().ToString());
        }

        private void MostInexperiencedCandidate_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();

            listBox.Items.Add(candidates.AsParallel().AsOrdered().OrderBy(c => c.YearsOfExperience).First().ToString());
        }

        private void TheCandidateWithTheLowestSalaryRequirements_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();

            listBox.Items.Add(candidates.AsParallel().AsOrdered().OrderBy(c => c.SalaryRequirements).First().ToString());
        }

        private void TheCandidateWithTheHighestSalaryRequirements_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();

            listBox.Items.Add(candidates.AsParallel().AsOrdered().OrderByDescending(c => c.SalaryRequirements).First().ToString());
        }

        private void ApplicantsFromTheSameCity_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(CityNameTextBox.Text))
            {
                listBox.Items.Clear();
                string nameCity = CityNameTextBox.Text;
                var cands = candidates.AsParallel().AsOrdered().Where(c => c.City == nameCity);
                List<Candidate> temp = cands.ToList();
                foreach (var item in temp)
                {
                    listBox.Items.Add(item);
                }
            }
        }
    }
}
