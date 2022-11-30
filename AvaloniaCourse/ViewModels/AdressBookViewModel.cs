using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaCourse.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace AvaloniaCourse.ViewModels
{
    public class AdressBookViewModel : ViewModelBase
    {
        private readonly AppController _appController;
        public AdressBookViewModel(AppController appController)
        {
            _appController = appController ?? throw new ArgumentNullException(nameof(appController));
            AddPeople = new RelayCommand(OnAdd, CanAdd);
            RemovePeople = new RelayCommand<People>(OnRemove, CanRemove);
            EditPeople = new RelayCommand<People>(OnEdit, CanEdit);
            SearchPeople = new RelayCommand(OnSearch, CanSearch);
            BackCommand = new RelayCommand(OnBack,CanBack);
            Pepl = new ObservableCollection<People>(_appController.dataContext.PeopleTable);
        }

        public ObservableCollection<People> Pepl { get; set; } = new ObservableCollection<People>();
        public People SelectedPepl { get; set; }
        ICommand AddPeople { get; }
        ICommand RemovePeople { get; }
        ICommand EditPeople { get; }
        ICommand SearchPeople { get; }
        ICommand BackCommand { get; }

        private bool CanBack()
            => true;
        private void OnBack()
            => _appController.ChangeCurrentView(CurrentViewTypes.Organizer);
        private bool CanSearch()
            => Pepl.Count != 0;
        private void OnSearch()
            => _appController.ChangeCurrentView(CurrentViewTypes.Search);
        private bool CanAdd()
        => true;
        
        private void OnAdd()
        {
            _appController.ViewModelBag = new People("<?>", "<?>", "<?>", "<?>", "<?>", "<?>", "<?>");
            //переходим к редактированию чела
            _appController.ChangeCurrentView(CurrentViewTypes.AdressEdit);
        }

        private bool CanRemove(People parameter)
         => parameter != null;

        private void OnRemove(People parameter)
        {
            Pepl.Remove(parameter);
            _appController.dataContext.RemovePerson(parameter); 
        }

        private bool CanEdit(People parameter)
            => parameter != null;
        private void OnEdit(People parameter)
        {
            //передаем ссылку на выбранного чела
            _appController.ViewModelBag = parameter;

            //переходим к вьюшке редактирования чела
            _appController.ChangeCurrentView(CurrentViewTypes.AdressEdit);
        }

        
    }
}
