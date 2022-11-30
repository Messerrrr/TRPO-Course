using AvaloniaCourse.ViewModels;
using System;
using AvaloniaCourse.Views;
using AvaloniaCourse.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaCourse
{
    public enum CurrentViewTypes
    {
        Start,
        Edit,
        Organizer,
        AdressBook,
        AdressEdit,
        Search,
        EventEdit,
        Event
    }
    public class AppController
    {
        public AppController(MainWindowViewModel mainWindowVM)
        {
            MainWindowViewModel = mainWindowVM ?? throw new ArgumentNullException(nameof(mainWindowVM));   
            
            dataContext = new DataContext();
        }

        public DataContext dataContext { get; set; }

        public MainWindowViewModel MainWindowViewModel { get; }

        public object ViewModelBag { get; set; }


        public void ChangeCurrentView(CurrentViewTypes viewType)
        {
            switch (viewType)
            {
                case CurrentViewTypes.Event:
                    EventViewModel eVM = new EventViewModel(this);
                    EventView eV = new EventView();
                    eV.DataContext = eVM;
                    MainWindowViewModel.CurrentView = eV;
                    break;
                case CurrentViewTypes.EventEdit:
                    EventEditViewModel eeVM = new EventEditViewModel(this);
                    EventEditView eeV = new EventEditView();
                    eeV.DataContext = eeVM;
                    MainWindowViewModel.CurrentView = eeV;
                    break;
                case CurrentViewTypes.Organizer:
                    OrganizerViewModel oVM = new OrganizerViewModel(this);
                    OrganizerView oV = new OrganizerView();
                    oV.DataContext = oVM;
                    MainWindowViewModel.CurrentView = oV;
                    break;
                case CurrentViewTypes.AdressBook:
                    AdressBookViewModel aVM = new AdressBookViewModel(this);
                    AdressBookView aV = new AdressBookView();
                    aV.DataContext = aVM;
                    MainWindowViewModel.CurrentView= aV;
                    break;
                case CurrentViewTypes.AdressEdit:
                    AdressEditViewModel aeVM = new AdressEditViewModel(this);
                    AdressEditView aeV = new AdressEditView();
                    aeV.DataContext = aeVM;
                    MainWindowViewModel.CurrentView= aeV;
                    break;
                case CurrentViewTypes.Search:
                    SearchViewModel srVM = new SearchViewModel(this);
                    SearchView srV = new SearchView();
                    srV.DataContext = srVM;
                    MainWindowViewModel.CurrentView= srV;
                    break;
                case CurrentViewTypes.Start:
                    StartWindowViewModel sVM = new StartWindowViewModel(this);
                    StartView sV = new StartView();
                    sV.DataContext = sVM;
                    MainWindowViewModel.CurrentView = sV;
                    break;
                case CurrentViewTypes.Edit:
                    EditWindowViewModel esVM = new EditWindowViewModel(this);
                    EditView esV = new EditView();
                    esV.DataContext = esVM;
                    MainWindowViewModel.CurrentView = esV;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType));
            }
        }
    }
}
