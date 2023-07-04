﻿using System;
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
using TaskOrganiser.Views;

namespace TaskOrganiser.Views
{
    public partial class EditTaskWindow : Window
    {
        Model.TDL toDoList;
        Model.Task task;
        int index;
        public EditTaskWindow(Model.TDL toDoList, Model.Task task, int index)
        {
            InitializeComponent();
            this.toDoList = toDoList;
            this.task = task;
            this.index = index;
            NameTextBox.Text = task.Name;
            DescriptionTextBox.Text = task.Description;
            /*if(task.Status == true)
            {
                StatusCheckBox.IsChecked = true;
            }
            else
            {
                StatusCheckBox.IsChecked = false;
            }
            */
            PriorityComboBox.Text = task.Priority.ToString();
            TypeComboBox.Text = task.Type.ToString();
            DeadlineDatePicker.Text = task.Deadline.ToString();
            //DateOfFinishDatePicker.Text = task.DateOfFinish.ToString();
            CategoryComboBox.Text = task.Category.ToString();
        }

        private void EditTaskButtonClicked(object sender, RoutedEventArgs e)
        {   
            string name = NameTextBox.Text;
            string description = DescriptionTextBox.Text;
            string type = TypeComboBox.Text;
            Model.Type desiredType = Model.Type.Minor;
            if (type == "Minor")
            {
                desiredType = Model.Type.Minor;
            }
            else if (type == "Major")
            {
                desiredType = Model.Type.Major;
            }
            string category = CategoryComboBox.Text;
            Model.Category desiredCategory = Model.Category.Home;
            if (category == "Home")
            {
                desiredCategory = Model.Category.Home;
            }
            if (category == "Work")
            {
                desiredCategory = Model.Category.Work;
            }
            if (category == "Music Production")
            {
                desiredCategory = Model.Category.MusicProduction;
            }
            if (category == "Shopping")
            {
                desiredCategory = Model.Category.Shopping;
            }
            string priority = PriorityComboBox.Text;
            Model.Priority desiredPriority = Model.Priority.Low;
            if (priority == "Low")
            {
                desiredPriority = Model.Priority.Low;
            }
            if (priority == "Medium")
            {
                desiredPriority = Model.Priority.Medium;
            }
            if (priority == "High")
            {
                desiredPriority = Model.Priority.High;
            }
            /*bool status;
            if (StatusCheckBox.IsChecked == false)
            {
                status = false;
            }
            else
            {
                status = true;
            }*/
            DateTime deadline = DeadlineDatePicker.SelectedDate ?? DateTime.MinValue;
            //DateTime dateOfFinish = DateOfFinishDatePicker.SelectedDate ?? DateTime.MinValue;
            Model.Task actualTask = new Model.Task();
            actualTask.Name = name;
            actualTask.Description = description;
            actualTask.Type = desiredType;
            actualTask.Category = desiredCategory;
            actualTask.Priority = desiredPriority;
            actualTask.Status = false;
            actualTask.Deadline = deadline;
            //actualTask.DateOfFinish = dateOfFinish;
            toDoList.Tasks[index] = actualTask;
            this.Close();
        }
    }
}
