using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TaskOrganiser.Commands;
using TaskOrganiser.Model;

namespace TaskOrganiser.ViewModel
{
    public class TreeViewModel : Model.BaseVM
    {
        private DeleteTaskCommand deleteTask;
        public ICommand DeleteTaskCommand
        {
            get { return deleteTask; }
        }
        public ObservableCollection<Tuple<string, TDL>> tuples { get; set; }
        private TDL tdl;
        public TDL Tdl
        {
            get { return tdl; }
            set
            {
                tdl = value;
                OnPropertyChanged("Tdl");
            }

        }

        public ObservableCollection<TDL> TDLs { get; set; }
        public TreeViewModel()
        {
            selectedTDL = new TDL();
            selectedTask = new Model.Task();
            deleteTask = new DeleteTaskCommand(this);
            tuples = new ObservableCollection<Tuple<string,TDL>>();
            TDLs = new ObservableCollection<TDL>();
            TDLs.Add(new TDL
            {
                Name = "Home",

                Tasks = new ObservableCollection<Model.Task>()
                {
                    new Model.Task()
                    {
                        Name = "Clean the dishes",
                        Description = "The sink is full",
                        Category = Category.Home,
                        Deadline = new DateTime(2023 , 4 , 25),
                        Priority = Priority.High,
                        Status = false,
                        Type = Model.Type.Minor
                    }
                }
            });
            TDLs.Add(new TDL
            {
                Name = "School",
                Tasks = new ObservableCollection<Model.Task>()
                {
                    new Model.Task(){
                    Name = "Proiect MVP",
                    Description = "To do list APP using MVVM",
                    Category = Category.Home,
                    Deadline = new DateTime(2023, 4, 28),
                    Priority = Priority.High,
                    Status = false,
                    Type = Model.Type.Major
                    },
                    new Model.Task()
                    {
                        Name = "Tema IA",
                        Description = "Tema 4",
                        Category = Category.Home,
                        Deadline = new DateTime(2023 , 5 , 3),
                        Priority = Priority.High,
                        Status = false,
                        Type = Model.Type.Major
                    },
                                        new Model.Task()
                    {
                        Name = "Tema RC",
                        Description = "Tema 3",
                        Category = Category.Home,
                        Deadline = new DateTime(2023 , 5 , 3),
                        Priority = Priority.Medium,
                        Status = false,
                        Type = Model.Type.Minor
                    }
                }
            });
        }
        public TDL selectedTDL;
        public Model.Task selectedTask;
        
    }
}
