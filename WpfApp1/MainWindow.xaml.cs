using System;
using System.Linq;
using System.Windows;
using System.Collections.Specialized;



using DataLib;
using System.Windows.Data;
using System.Windows.Controls;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_Sample
{

    public partial class MainWindow : Window
    {
        TeamObservable team;

        Researcher temp_researcher ;

        public MainWindow()
        {
            InitializeComponent();

            team = new TeamObservable("Test Team");

            team.CollectionChanged += Data_Changed_Handler;

            temp_researcher = new Researcher();

            DataContext = team;

            inp_grid.DataContext = temp_researcher;

            research.ItemsSource = team.RT;
            research.SelectedIndex = 0;

        }

        public void Data_Changed_Handler(object source, NotifyCollectionChangedEventArgs args)
        {
            ListBox_Researchers.ItemsSource = team.Get_Researchers;

            team.NotifyPropertyChanged("researchers_percent");
            //Update_Content();  
        }

        private void Save_ToFile()
        {            
            if (team.IfChanged)
            {
                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                var result = MessageBox.Show("Save changes? Data may be lost.", "Message", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (sfd.ShowDialog() == true)
                    {
                        TeamObservable.Save(sfd.FileName, team);
                        team.IfChanged = false;
                        //TextBlock_IfChanged.Text = team.IfChanged.ToString();
                    }

                }
                else
                    MessageBox.Show("Changes canceled.", "Message");
            }
        }


        private void Update_Content()
        {
            DataContext = team;

            team.CollectionChanged += Data_Changed_Handler;

            //ListBox_All.ItemsSource = team;
            //ListBox_Researchers.ItemsSource = team.Get_Researchers;

            //TextBlock_IfChanged.Text = team.IfChanged.ToString();
            //TextBlock_ResearchersPercent.Text = team.researchers_percent.ToString();
            //TextBox_GroupName.Text = team.group_name;
        }


        private void window_Closed(object sender, EventArgs e)
        {
            Save_ToFile();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox_All.ItemsSource = team;
            ListBox_Researchers.ItemsSource = team.Get_Researchers; //спорно

            //TextBlock_IfChanged.Text = team.IfChanged.ToString();
            //TextBlock_ResearchersPercent.Text = team.researchers_percent.ToString();
            //TextBox_GroupName.Text = team.group_name;
        }

        private void FileMenu_Open_Clicked(object sender, RoutedEventArgs e)
        {
            Save_ToFile();
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                TeamObservable.Load(ofd.FileName,  ref team);
                Update_Content();
                DataContext = team;
            }

        }

        private void FileMenu_New_Clicked(object sender, RoutedEventArgs e)
        {
            Save_ToFile();
            team = new  TeamObservable("Test Team");
            Update_Content();
            DataContext = team;
        }

        private void FileMenu_Save_Clicked(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            if (sfd.ShowDialog() == true)
            {
                TeamObservable.Save(sfd.FileName, team);
            }

        }

        private void EditMenu_AddDefaultResearcher(object sender, RoutedEventArgs e)
        {
            team.AddDefaultResearcher();
        }

        private void EditMenu_AddDefaultProgrammer(object sender, RoutedEventArgs e)
        {
            team.AddDefaultProgrammer();
        }

        private void EditMenu_AddDefaults(object sender, RoutedEventArgs e)
        {
            team.AddDefaults();
        }

        private void EditMenu_Remove(object sender, RoutedEventArgs e)
        {
            team.RemovePersonAt(ListBox_All.SelectedIndex);
        }

        private void EditMenu_AddCustomResearcher(object sender, RoutedEventArgs e)
        {
            team.AddPerson(temp_researcher);
        }

        private void ListBox_All_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RBClicked(object sender, RoutedEventArgs e)
        {
            RadioButton RButton = sender as RadioButton;
            DataTemplate template = (DataTemplate)TryFindResource("MyTemplate_All");
            if (RButton.Name == "DisableTemplate")
            {
                if (RButton.IsChecked == true)
                {
                    ListBox_All.ItemTemplate = null;
                }
                else
                {
                    ListBox_All.ItemTemplate = template;
                }
            }
            else if (RButton.Name == "EnableTemplate")
            {
                if (RButton.IsChecked == true)
                {
                    ListBox_All.ItemTemplate = template;
                }
                else
                {
                    ListBox_All.ItemTemplate = null;
                }
            }
        }
    }
}