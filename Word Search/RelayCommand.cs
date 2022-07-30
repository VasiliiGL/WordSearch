using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Word_Search
{
    class RelayCommand
    {
        private Action<object> searchParameters;
        private Func<object, bool> canSearch;

        public event EventHandler CanSearchChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> searchParameters, Func<object, bool> canSearch = null)
        {
            this.searchParameters = searchParameters;
            this.canSearch = canSearch;
        }

        public bool CanExecute(object parameter)
        {
            return this.canSearch == null || this.canSearch(parameter);
        }

        public void Search(object parameter)
        {
            this.searchParameters(parameter);
        }
    }
}

