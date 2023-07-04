using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TaskOrganiser.Model;

namespace TaskOrganiser.Views
{
    
    public partial class FindTaskWindow : Window
    {
        ObservableCollection<TDL> ToDoLists = new ObservableCollection<TDL>();
        ObservableCollection<string> taskTitles { get; set; }
        ObservableCollection<TDL> ToDoListLocations { get; set; }
        ObservableCollection<TDL> auxiliaryToDoList { get; set; }
        TDL selectedRoot { get; set; }
        ObservableCollection<Tuple<string,TDL>> tuples { get; set; }
        
        public FindTaskWindow(ObservableCollection<TDL> ToDoLists, TDL selectedRoot, ObservableCollection<Tuple<string, TDL>> tuples)
        {   
            InitializeComponent();
            this.ToDoLists = ToDoLists;
            this.selectedRoot = selectedRoot;
            taskTitles = new ObservableCollection<string>();
            ToDoListLocations = new ObservableCollection<TDL>();
            this.tuples = tuples;
        }

        private void CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FindButtonClicked(object sender, RoutedEventArgs e)
        {
            
            int count = 0;
            string desiredTaskName = SearchByNameTextBox.Text;
            if(viewCheckBox.IsChecked == false)
            {
                foreach(TDL tdl in ToDoLists)
                {
                    if(tdl.ToDoLists != null)
                    {
                        foreach(TDL tdl2 in tdl.ToDoLists)
                        {
                            foreach(Model.Task task in tdl2.Tasks)
                            {
                                if (task.Name == desiredTaskName)
                                {
                                    taskTitles.Add(task.Name);
                                    ToDoListLocations.Add(tdl2);
                                    tuples.Add(new Tuple<string, TDL>(desiredTaskName, tdl2));
                                }
                            }
                        }
                    }
                    foreach(Model.Task task in tdl.Tasks)
                    {
                        if(task.Name == desiredTaskName)
                        {
                            taskTitles.Add(task.Name);
                            ToDoListLocations.Add(tdl);
                            tuples.Add(new Tuple<string, TDL>(desiredTaskName, tdl));
                        }
                    }
                }
            }
            else
            {
                foreach(Model.Task task in selectedRoot.Tasks)
                {
                    if(task.Name == desiredTaskName)
                    {
                        taskTitles.Add(desiredTaskName);
                        ToDoListLocations.Add(selectedRoot);
                        tuples.Add(new Tuple<string, TDL>(desiredTaskName, selectedRoot));
                    }
                }
            }
            count = tuples.Count;
            string help = count.ToString();
            help += " task/(s) found";
            FoundItemsTextBlock.Text = help;
            FoundTasksListView.ItemsSource = tuples;
            taskTitles.Clear();
            ToDoListLocations.Clear();
        }

        
    }
}
