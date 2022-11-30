using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AvaloniaCourse.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaCourse.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private readonly AppController _appController;
        public string SearchSurname { get; set; }
        public string SearchVakancy { get; set; }
        public string SearchJob { get; set; }
        public ObservableCollection<People> Pepl { get; set; } 
        public ObservableCollection<People> SortPepl { get; set; }
        public SearchViewModel(AppController appController)
        {
            _appController = appController;
            Pepl = new ObservableCollection<People>(_appController.dataContext.PeopleTable.ToList());
            SortPepl = Pepl;
            BackCommand = new RelayCommand(OnBack, CanBack);
            VakancySort = new RelayCommand(OnVakancy, CanVakancy);
            JobSort = new RelayCommand(OnJob,CanJob);
            SurnameSort = new RelayCommand(OnSurname,CanSurname);
        }
        ICommand BackCommand { get; }
        ICommand VakancySort { get; }
        ICommand JobSort { get; }
        ICommand SurnameSort { get; }
        private bool CanBack()
        {
            return true;
        }
        private void OnBack()
        {
            _appController.ChangeCurrentView(CurrentViewTypes.AdressBook);
        }
        private bool CanVakancy()
            => Pepl.Count > 0;
        private void OnVakancy()
        {
            var list = _appController.dataContext.PeopleTable.Where(p => EF.Functions.Like(p.Function, $"%{SearchVakancy}%")).ToList();
            SortPepl.Clear();
            foreach (var item in list)
                SortPepl.Add(item);
        }
        private bool CanJob()
            => Pepl.Count > 0;
        private void OnJob()
        {
            var list = _appController.dataContext.PeopleTable.Where(p => EF.Functions.Like(p.Job, $"%{SearchJob}%")).ToList();
            SortPepl.Clear();
            foreach (var item in list)
                SortPepl.Add(item);
        }
        private bool CanSurname()
            => Pepl.Count > 0;
        private void OnSurname()
        {
            var list = _appController.dataContext.PeopleTable.Where(p => EF.Functions.Like(p.Surname, $"%{SearchSurname}%")).ToList();
            SortPepl.Clear();
            foreach (var item in list)
                SortPepl.Add(item);
        }

    }
}
