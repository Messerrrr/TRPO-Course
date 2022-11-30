using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using AvaloniaCourse.Models;

namespace AvaloniaCourse.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private object _CurrentView;
        public object CurrentView
        {
            get => _CurrentView;
            set
            {
                _CurrentView = value;
                OnPropertyChanged();
            }
        }
    }
}
