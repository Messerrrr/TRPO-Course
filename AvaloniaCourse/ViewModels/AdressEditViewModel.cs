using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AvaloniaCourse.Models;

namespace AvaloniaCourse.ViewModels
{
    public class AdressEditViewModel : ViewModelBase
    {
        public AdressEditViewModel(AppController appController)
        {
            _appController = appController ?? throw new ArgumentNullException(nameof(appController));
            // AddBook = new ICommand();

            BackCommand = new RelayCommand(OnBack, CanBack);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            if (_appController.ViewModelBag == null
                || (CurrentPeople = (_appController.ViewModelBag as People)) == null)
            {
                throw new ArgumentException(nameof(_appController.ViewModelBag));
            }
            else
            {
                CurrentPeople = _appController.ViewModelBag as People;
                _appController.ViewModelBag = null;
            }
        }

        #region Fields

        private readonly AppController _appController;

        private People _CurrentPeople;

        #endregion

        #region Properties

        public People CurrentPeople
        {
            get => _CurrentPeople;
            set
            {
                _CurrentPeople = value;
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
            _appController.ChangeCurrentView(CurrentViewTypes.AdressBook);
        }
        private bool CanSave()
        {
            return true;
        }
        private async void OnSave()
        {
            if (CurrentPeople.Id == -1)
            {
                _appController.dataContext.AddPerson(CurrentPeople);
            }
            else
            {
                _appController.dataContext.UpdatePerson(CurrentPeople);
            }

            //возвращаемся назад
            _appController.ChangeCurrentView(CurrentViewTypes.AdressBook);
            #endregion
        }
    }
}
