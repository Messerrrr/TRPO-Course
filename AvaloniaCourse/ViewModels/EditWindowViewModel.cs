using System;
using System.Collections.Generic;
using System.Linq;
using AvaloniaCourse.Models;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvaloniaCourse.ViewModels
{
    public class EditWindowViewModel : ViewModelBase
    {
        public EditWindowViewModel(AppController appController)
        {
            _appController = appController ?? throw new ArgumentNullException(nameof(appController));
            // AddBook = new ICommand();
            
            BackCommand = new RelayCommand(OnBack, CanBack);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            if (_appController.ViewModelBag == null
                || (CurrentBook = (_appController.ViewModelBag as Book)) == null)
            {
                throw new ArgumentException(nameof(_appController.ViewModelBag));
            }
            else
            {
                CurrentBook = _appController.ViewModelBag as Book;
                _appController.ViewModelBag = null;
            }
        }

        #region Fields

        private readonly AppController _appController;

        private Book _CurrentBook;

        #endregion

        #region Properties

        public Book CurrentBook
        {
            get => _CurrentBook;
            set
            {
                _CurrentBook = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands      
        ICommand SaveCommand { get; }

        ICommand BackCommand { get; }

        #endregion

        #region Methods

        private bool CanBack()
        {
            return true;
        }
        private void OnBack()
        {
            _appController.ChangeCurrentView(CurrentViewTypes.Start);
        }
        private bool CanSave()
        {
            return true;
        }
        private async void OnSave()
        {
            if (CurrentBook.Id == -1)
            {
                _appController.dataContext.AddBook(CurrentBook);
                _appController.dataContext.RecalculateValues();
            }
            else
            {
                _appController.dataContext.UpdateBook(CurrentBook);
                _appController.dataContext.RecalculateValues();
            }

            //возвращаемся назад
            _appController.ChangeCurrentView(CurrentViewTypes.Start);
        }
        #endregion
    }
}
