using AvaloniaCourse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvaloniaCourse.ViewModels
{
    public class EventViewModel : ViewModelBase
    {

        private readonly AppController _appController;
        public EventViewModel(AppController appController)
        {
            _appController = appController;
            BackCommand = new RelayCommand(OnBack,CanBack);
            AddEvent = new RelayCommand(OnAdd,CanAdd);
            RemoveEvent = new RelayCommand<Event>(OnRemove, CanRemove);
            EditEvent = new RelayCommand<Event>(OnEdit, CanEdit);
            Currentlist = new ObservableCollection<Event>(_appController.dataContext.EventTable);
            TimeSortCommand = new RelayCommand(OnTimeSort, CanTimeSort);
        }
        public ObservableCollection<Event> Currentlist { get; set; }
        public Event CurrentEvent
        { 
          get => CurrentEvent;
          set
            {
                CurrentEvent = value;
                OnPropertyChanged();
            }
        }
        public DateTime left { get; set; } = DateTime.Now;
        public DateTime right { get; set; } = DateTime.Now.AddHours(23).AddMinutes(59);
        ICommand AddEvent { get; }
        ICommand RemoveEvent { get; }
        ICommand EditEvent{ get; }
        ICommand BackCommand { get; }
        ICommand TimeSortCommand { get; }

        private bool CanTimeSort()
            => true;
        private void OnTimeSort()
        {
            var list = _appController.dataContext.EventTable.Where(p => p.Date >= left && p.Date <= right);
            Currentlist.Clear();
            foreach (var item in list)
                Currentlist.Add(item);
        }
        private bool CanBack()
            => true;
        private void OnBack()
            => _appController.ChangeCurrentView(CurrentViewTypes.Organizer);
        private bool CanAdd()
        => true;

        private void OnAdd()
        {
            _appController.ViewModelBag = new Event(DateTime.Now, "<?>");
            //переходим к редактированию чела
            _appController.ChangeCurrentView(CurrentViewTypes.EventEdit);
        }
        private bool CanRemove(Event parameter)
        => parameter != null;

        private void OnRemove(Event parameter)
        {
            Currentlist.Remove(parameter);
            _appController.dataContext.RemoveEvent(parameter);
        }
        private bool CanEdit(Event parameter)
            => parameter != null;
        private void OnEdit(Event parameter)
        {
            //передаем ссылку на выбранного чела
            _appController.ViewModelBag = parameter;

            //переходим к вьюшке редактирования чела
            _appController.ChangeCurrentView(CurrentViewTypes.EventEdit);
        }
    }
}
