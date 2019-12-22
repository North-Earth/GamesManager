using System;
using System.Windows.Input;

namespace GamesManager.Client.Helpers.Commands
{
    public class Command : ICommand
    {
        #region Fields

        private readonly Action<object> _execute;

        private readonly Func<object, bool> _canExecute;

        #endregion

        #region Contructors

        public Command(Action<object> execute) : this(execute, null) { }

        public Command(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null) { throw new ArgumentNullException(nameof(execute)); }

            _execute = execute;
            _canExecute = canExecute ?? (x => true);
        }

        #endregion

        #region Methods

        public bool CanExecute(object parameter) => _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Refresh() => CommandManager.InvalidateRequerySuggested();

        #endregion
    }
}
