using AvaloniaCourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvaloniaCourse.ViewModels
{
    public class EventEditViewModel : ViewModelBase
    {
        public EventEditViewModel(AppController appController)
        {
            _appController = appController ?? throw new ArgumentNullException(nameof(appController));

            BackCommand = new RelayCommand(OnBack, CanBack);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            if (_appController.ViewModelBag == null
                || (CurrentEvent = (_appController.ViewModelBag as Event)) == null)
            {
                throw new ArgumentException(nameof(_appController.ViewModelBag));
            }
            else
            {
                CurrentEvent = _appController.ViewModelBag as Event;
                _appController.ViewModelBag = null;
            }
        }
        #region Fields

        private readonly AppController _appController;

        private Event _CurrentEvent;

        #endregion

        #region Properties

        public Event CurrentEvent
        {
            get => _CurrentEvent;
            set
            {
                _CurrentEvent = value;
                OnPropertyChanged();
            }
        }

        #endregion
        ICommand SaveCommand { get; }

        ICommand BackCommand { get; }
        private bool CanBack()
        {
            return true;
        }
        private void OnBack()
        {
            _appController.ChangeCurrentView(CurrentViewTypes.Event);
        }
        private bool CanSave()
        {
            return true;
        }
        private async void OnSave()
        {
            if (CurrentEvent.Id == -1)
            {
                _appController.dataContext.AddEvent(CurrentEvent);
            }
            else
            {
                _appController.dataContext.UpdateEvent(CurrentEvent);
            }

            //возвращаемся назад
            _appController.ChangeCurrentView(CurrentViewTypes.Event);
        }
    }
}
