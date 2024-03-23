using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OP4.Core
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute ;
        private readonly Predicate<object> _canExecute ;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove {  CommandManager.RequerySuggested -= value;}
        }

        public RelayCommand(Action<object> Execute, Predicate<object> CanExecute)
        {
            _execute = Execute;
            _canExecute = CanExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}
