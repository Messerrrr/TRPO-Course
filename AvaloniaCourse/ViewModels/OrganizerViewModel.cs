using AvaloniaCourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvaloniaCourse.ViewModels
{
    public class OrganizerViewModel
    {
        private readonly AppController _appController;
        
        public OrganizerViewModel(AppController appController)
        {
            _appController = appController;
            AdressCommand = new RelayCommand(OnAdress, CanAdress);
            ToStart = new RelayCommand(OnStart,CanStart);
            ToEvent = new RelayCommand(OnEvent, CanEvent);
            CurrentEvents = new List<Event>(_appController.dataContext.EventTable.Where(p => p.Date >= left && p.Date <= right));
        }
        public List<Event> CurrentEvents { get; set; }
        public DateTime left { get; set; } = DateTime.Today;
        public DateTime right { get; set; } = DateTime.Today.AddHours(23).AddMinutes(59);
        ICommand AdressCommand { get; }
        ICommand ToEvent { get; }
        ICommand ToStart { get; }

        private bool CanEvent()
            => true;
        private void OnEvent()
            => _appController.ChangeCurrentView(CurrentViewTypes.Event);
        private bool CanAdress()
           => true;
        private void OnAdress()
            => _appController.ChangeCurrentView(CurrentViewTypes.AdressBook);
        private bool CanStart()
            => true;
        private void OnStart()
            => _appController.ChangeCurrentView(CurrentViewTypes.Start);
    }
}
