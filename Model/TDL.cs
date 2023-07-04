using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TaskOrganiser.Model
{
    public class TDL : BaseVM
    {
        private string name;
        private ObservableCollection<TDL> tDLs;
        private ObservableCollection<Task> tasks;
        public string Name 
        {   
            get { return name; }
            set 
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public ObservableCollection<TDL> ToDoLists
        {
            get { return tDLs; }
            set 
            { 
                tDLs = value;
                OnPropertyChanged("TDLs");
            }
        }
        public ObservableCollection<Task> Tasks
        {
            get { return tasks; }
            set 
            { 
                tasks = value;
                OnPropertyChanged("Tasks");
            }
        }

        
        
    }
}
