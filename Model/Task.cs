using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOrganiser.Model
{
    public enum Type { Minor, Major }
    public enum Priority { Low, Medium, High }
    public enum Category { Work, MusicProduction, Home, Shopping }
    public class Task : BaseVM
    {
        private string name;
        private Type type;
        private bool status;
        private string description;
        private Priority priority;
        private DateTime deadline;
        private DateTime dateOfFinish;
        private Category category;

        public string Name
        {
            get { return name; }
            set 
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
         public Type Type
        {
            get { return type; }
            set 
            { 
                type = value;
                OnPropertyChanged("Type");
            }
        }
        public bool Status
        {
            get { return status; }
            set 
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }
        public Priority Priority
        {
            get { return priority; }
            set 
            {
                priority = value;
                OnPropertyChanged("Priority"); 
            }
        }
        
        public DateTime Deadline
        {
            get { return deadline; }
            set 
            {
                deadline = value;
                OnPropertyChanged("Deadline");
            }
        }
        
        public DateTime DateOfFinish
        {
            get { return dateOfFinish; }
            set
            {
                dateOfFinish = value;
                OnPropertyChanged("DateOfFinish");
            }
        }

        public Category Category
        {
            get { return category; }
            set 
            { 
                category = value;
                OnPropertyChanged("Category");
            }
        }

        
    }
}
