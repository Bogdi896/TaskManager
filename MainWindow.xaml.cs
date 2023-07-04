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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskOrganiser.Model;
using TaskOrganiser.ViewModel;
using TaskOrganiser.Views;

namespace TaskOrganiser
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AboutButtonClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Gargu Bogdan \n10LF312 \nbogdan.gargu@student.unitbv.ro \n","Info student", MessageBoxButton.OK);
        }

        private void AddButtonClicked(object sender, RoutedEventArgs e)
        {   TDL tdl = TDLTreeView.SelectedItem as TDL;
            if (tdl != null)
            {
                AddTaskWindow newWindow = new AddTaskWindow(tdl);
                newWindow.Show();
            }
            else
            {
                MessageBox.Show("You did not select a To Do List!", "Could not add task", MessageBoxButton.OK);
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(e.NewValue is TDL SelectedTDL)
            {
                TaskListView.ItemsSource = SelectedTDL.Tasks;
            }
        }

        private void EditButtonClicked(object sender, RoutedEventArgs e)
        {
            TDL desiredItem = TDLTreeView.SelectedItem as TDL;
            Model.Task desiredTask = TaskListView.SelectedItem as Model.Task;
            int index = desiredItem.Tasks.IndexOf(desiredTask);
            EditTaskWindow newWindow = new EditTaskWindow(desiredItem, desiredTask, index);
            newWindow.Show();
        }

        /*private void DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            TDL desiredItem = TDLTreeView.SelectedItem as TDL;
            Model.Task desiredTask = TaskListView.SelectedItem as Model.Task;
            desiredItem.Tasks.Remove(desiredTask);
        }*/

        private void SetDoneButtonClicked(object sender, RoutedEventArgs e)
        {
            Model.Task desiredTask = TaskListView.SelectedItem as Model.Task;
            desiredTask.Status = true;
            ListViewItem desiredItem = TaskListView.ItemContainerGenerator.ContainerFromItem(desiredTask) as ListViewItem;
        }

        private void MoveUpButtonClicked(object sender, RoutedEventArgs e)
        {
            TDL selectedItem = TDLTreeView.SelectedItem as TDL;
            Model.Task desiredTask = TaskListView.SelectedItem as Model.Task; 
            int index = selectedItem.Tasks.IndexOf(desiredTask);
            if(index > 0)
            {
                Model.Task auxTask = selectedItem.Tasks[index - 1];
                ListViewItem listViewItem1 = TaskListView.ItemContainerGenerator.ContainerFromItem(selectedItem.Tasks[index]) as ListViewItem; 
                ListViewItem listViewItem2 = TaskListView.ItemContainerGenerator.ContainerFromItem(selectedItem.Tasks[index - 1]) as ListViewItem;
                selectedItem.Tasks[index - 1] = desiredTask;
                Brush paint = listViewItem1.Background;
                listViewItem1.Background = listViewItem2.Background;
                listViewItem2.Background = paint;
                selectedItem.Tasks[index] = auxTask;
            }
            else
            {
                MessageBox.Show("Can't move up!", "Error", MessageBoxButton.OK);
            }
        }

        private void MoveDownButtonClicked(object sender, RoutedEventArgs e)
        {
            TDL selectedItem = TDLTreeView.SelectedItem as TDL;
            Model.Task desiredTask = TaskListView.SelectedItem as Model.Task;
            int index = selectedItem.Tasks.IndexOf(desiredTask);
            if(index < selectedItem.Tasks.Count - 1)
            {
                Model.Task auxTask = selectedItem.Tasks[index + 1];
                ListViewItem listViewItem1 = TaskListView.ItemContainerGenerator.ContainerFromItem(selectedItem.Tasks[index]) as ListViewItem;
                ListViewItem listViewItem2 = TaskListView.ItemContainerGenerator.ContainerFromItem(selectedItem.Tasks[index + 1]) as ListViewItem;
                selectedItem.Tasks[index + 1] = desiredTask;
                Brush paint = listViewItem1.Background;
                listViewItem1.Background = listViewItem2.Background;
                listViewItem2.Background = paint;
                selectedItem.Tasks[index] = auxTask;
            }
            else
            {
                MessageBox.Show("Can't move down!", "Error", MessageBoxButton.OK);
            }
        }

        private void FindTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            ViewModel.TreeViewModel view = this.DataContext as ViewModel.TreeViewModel;
            ObservableCollection<TDL> ToDoLists = view.TDLs;
            TDL selectedRoot = TDLTreeView.SelectedItem as TDL;
            if (selectedRoot != null)
            {
                FindTaskWindow newWindow = new FindTaskWindow(ToDoLists, selectedRoot, view.tuples);
                newWindow.Show();
            }
            else
            {
                MessageBox.Show("You didn't select a view!", "Error", MessageBoxButton.OK);
            }
        }
        private void SortByDeadline(object sender, RoutedEventArgs e)
        {
            TreeViewModel treeViewViewModel = this.DataContext as TreeViewModel;
            ObservableCollection<TDL> ToDoLists = treeViewViewModel.TDLs;
            foreach (TDL tdl in ToDoLists)
            {
                tdl.Tasks = new ObservableCollection<Model.Task>(tdl.Tasks.OrderBy(t => t.Deadline));
            }
            treeViewViewModel.TDLs = ToDoLists;
            if (TDLTreeView.SelectedItem is TDL SelectedTDL)
            {
                TaskListView.ItemsSource = SelectedTDL.Tasks;
            }
        }

        private void SortByPriority(object sender, RoutedEventArgs e)
        {
            TreeViewModel treeViewViewModel = this.DataContext as TreeViewModel;
            foreach (TDL tdl in treeViewViewModel.TDLs)
            {
                ObservableCollection<Model.Task> lowPriorityTask = new ObservableCollection<Model.Task>();
                ObservableCollection<Model.Task> mediumPriorityTask = new ObservableCollection<Model.Task>();
                ObservableCollection<Model.Task> highPriorityTask = new ObservableCollection<Model.Task>();
                foreach (Model.Task task in tdl.Tasks)
                {
                    if (task.Priority == Model.Priority.Low)
                    {
                        lowPriorityTask.Add(task);
                    }
                    else if (task.Priority == Model.Priority.Medium)
                    {
                        mediumPriorityTask.Add(task);
                    }
                    else
                    {
                        highPriorityTask.Add(task);
                    }
                }
                tdl.Tasks.Clear();
                foreach (Model.Task it in lowPriorityTask.Concat(mediumPriorityTask).Concat(highPriorityTask))
                {
                    tdl.Tasks.Add(it);
                }
            }
            if (TDLTreeView.SelectedItem is TDL SelectedTDL)
            {
                TaskListView.ItemsSource = SelectedTDL.Tasks;
            }
        }
        private void DisplayFinishedTasks(object sender, RoutedEventArgs e)
        {
            TDL selectedTDL = TDLTreeView.SelectedItem as TDL;
            if (selectedTDL != null)
            {
                var finishedTasks = new ObservableCollection<Model.Task>(selectedTDL.Tasks.Where(t => t.Status));
                TaskListView.ItemsSource = finishedTasks;
            }
        }
        private void DisplayUnfinishedTasks(object sender, RoutedEventArgs e)
        {
            TDL selectedTDL = TDLTreeView.SelectedItem as TDL;
            if (selectedTDL != null)
            {
                var unfinishedTasks = new ObservableCollection<Model.Task>(selectedTDL.Tasks.Where(t => !t.Status));
                TaskListView.ItemsSource = unfinishedTasks;
            }
        }

        private void DisplayAllTasks(object sender, RoutedEventArgs e)
        {
            TDL selectedTDL = TDLTreeView.SelectedItem as TDL;
            if (selectedTDL != null)
            {
                TaskListView.ItemsSource = selectedTDL.Tasks;
            }
        }


        private void StatisticsButtonClicked(object sender, RoutedEventArgs e)
        {
            int tasksDueToday = 0;
            int tasksDueTomorrow = 0;
            int tasksOverdue = 0;
            int tasksDone = 0;
            int tasksToBeDone = 0;

            TreeViewModel treeView = this.DataContext as TreeViewModel;

            foreach (TDL tdl in treeView.TDLs)
            {
                foreach (Model.Task task in tdl.Tasks)
                {
                    if (task.Status)
                    {
                        tasksDone++;
                    }
                    else
                    {
                        if (task.Deadline.Date < DateTime.Now.Date)
                        {
                            tasksOverdue++;
                            tasksToBeDone++;
                        }
                        else if (task.Deadline.Date == DateTime.Now.Date)
                        {
                            tasksDueToday++;
                            tasksToBeDone++;
                        }
                        else if (task.Deadline.Date == DateTime.Now.Date.AddDays(1))
                        {
                            tasksDueTomorrow++;
                            tasksToBeDone++;
                        }
                        else
                        {
                            tasksToBeDone++;
                        }
                    }
                }
            }

            TasksDueTodayBlock.Text = tasksDueToday.ToString();
            TasksDueTomorrowBlock.Text = tasksDueTomorrow.ToString();
            TasksOverdueBlock.Text = tasksOverdue.ToString();
            TasksDoneBlock.Text = tasksDone.ToString();
            TasksToBeDoneBlock.Text = tasksToBeDone.ToString();
        }

        private void TaskListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TreeViewModel treeView = this.DataContext as TreeViewModel;
            if(treeView != null)
            {
                Model.Task selectedTask = TaskListView.SelectedItem as Model.Task;
                treeView.selectedTask = selectedTask;
                TDL selectedList = TDLTreeView.SelectedItem as TDL;
                treeView.selectedTDL = selectedList;
            }
        }
    }
}
