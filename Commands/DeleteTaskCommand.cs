using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskOrganiser.Model;
using TaskOrganiser.ViewModel;

namespace TaskOrganiser.Commands
{
    public class DeleteTaskCommand : ICommand
    {
        private TreeViewModel tree;
       
        public event EventHandler CanExecuteChanged;

        public DeleteTaskCommand(TreeViewModel tree)
        {
            this.tree = tree;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DeleteTask();
        }

        public void DeleteTask()
        {
            TDL selectedTDL = tree.selectedTDL;
            if(tree.selectedTask != null)
            {
                selectedTDL.Tasks.Remove(tree.selectedTask);
            }
            else
            {
                MessageBox.Show("You didn't select a task!", "Error", MessageBoxButton.OK);
            }
        }
    }

}
