using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using AvaloniaCourse.Models;

namespace AvaloniaCourse.ViewModels
{
    public class StartWindowViewModel : ViewModelBase
    {
        public StartWindowViewModel(AppController appController)
        {
            _appController = appController ?? throw new ArgumentNullException(nameof(appController));         
            CreateCommand = new RelayCommand(OnCreate, CanCreate);
            DeleteBook = new RelayCommand<Book>(RemoveBook, CanRemoveBook);
            EditCommand = new RelayCommand<Book>(OnEdit,CanEdit);
            Shelf = new ObservableCollection<Book>(_appController.dataContext.BookTable);
            RecalculateValues();
            ShopsTable();
            CalculateCommand = new RelayCommand<double>(OnCalculate, CanCalculate);
            PriceDelete = new RelayCommand<double>(OnPriceDelete, CanPriceDelete);
            DecreasePrice = new RelayCommand(OnDecreasePrice, CanDecreasePrice);
            BackCommand = new RelayCommand(OnBack, CanBack);
        }

        #region Fields

        private readonly AppController _appController;

        private Book _SelectedBook;

        private int _SoldinShop;

        private int _PriceAllRemain;

        private int _AveragePrice;

        #endregion

        #region Properties

        public int AveragePrice
        {
            get => _AveragePrice;
            set
            {
                _AveragePrice = value;
                OnPropertyChanged();
            }
        }
        public int PriceAllRemain
        {
            get => _PriceAllRemain;
            set
            {
                _PriceAllRemain = value;
                OnPropertyChanged();
            }
        }
        public int SoldinShop
        {
            get => _SoldinShop;
            set
            {
                _SoldinShop = value;
                OnPropertyChanged();
            }
        }
        public Book SelectedBook
        {
            get => _SelectedBook;
            set
            {
                _SelectedBook = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Book> Shelf { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<Shop> Shops { get; set; } = new ObservableCollection<Shop>();
        #endregion

        #region Commands

        ICommand CreateCommand { get; }
        ICommand EditCommand { get; }
        ICommand DeleteBook { get; }
        ICommand CalculateCommand { get; }
        ICommand PriceDelete { get; }
        ICommand DecreasePrice { get; }
        ICommand BackCommand { get; }

        #endregion

        #region Methods

        private bool CanBack()
        {
            return true;
        }
        private void OnBack()
        {
            _appController.ChangeCurrentView(CurrentViewTypes.Organizer);
        }
        private bool CanCalculate(double parameter)
            => Shelf.Count != 0;

        private void OnCalculate(double parameter)
        { 
            int j = 0;
            for (int i = 0; i < Shelf.Count; i++)
                if (Shelf[i].ShopNum == parameter)
                    j += Shelf[i].Sold;
            SoldinShop = j;
        }
        private bool CanEdit(Book book)
        {
            return book != null;
        }
        private void OnEdit(Book book)
        {
            //передаем ссылку на выбранного чела
            _appController.ViewModelBag = book;

            //переходим к вьюшке редактирования чела
            _appController.ChangeCurrentView(CurrentViewTypes.Edit);
        }
        private void RemoveBook(Book parameter)
        {
            Shelf.Remove(parameter);
            _appController.dataContext.RemoveBook(parameter);
            _appController.dataContext.RecalculateValues();
            RecalculateValues();
            ShopsTable();
        }
        private bool CanRemoveBook(Book parameter)
                 => parameter != null;
        
   
        private void RecalculateValues()
        {
            for (int i = 0; i < Shelf.Count; i++)
                PriceAllRemain += Shelf[i].Cost * Shelf[i].Remain;
            int allprice = 0;
            for (int i = 0; i < Shelf.Count; i++)
            {
                allprice += Shelf[i].Cost;
            }
            if (Shelf.Count > 0)
                AveragePrice = allprice / Shelf.Count;
            else AveragePrice = 0;
            
            
        }
        private bool CanCreate()
        {
            return true;
        }
        private void OnCreate()
        {
            //создаем ссылку на нового чела
            _appController.ViewModelBag = new Book
            {
                Id = -1,
                Namebook = "<?>",
                Surname = "<?>",
                ShopNum = 0,
                Cost = 0,
                Sold = 0,
                Remain = 0,
            };
            //переходим к редактированию чела
            _appController.ChangeCurrentView(CurrentViewTypes.Edit);
        }

        private void ShopsTable()
        {
            Shops.Clear();
            List<int> Nums = new List<int>();
            for (int i = 0; i < Shelf.Count; i++)
                if (Nums.Contains(Shelf[i].ShopNum))
                    continue;
                else Nums.Add(Shelf[i].ShopNum);

            for (int i = 0; i < Nums.Count; i++)
            {
                int money = 0;
                for (int j = 0; j < Shelf.Count; j++)
                {
                    if (Nums[i] == Shelf[j].ShopNum)
                        money += Shelf[j].Cost * Shelf[j].Sold;
                }
                Shops.Add(new Shop(Nums[i], money));
            }
        }

        private bool CanPriceDelete(double parameter)
        => parameter > 0;
        private void OnPriceDelete(double parameter)
        {
            for (int i = 0; i < Shelf.Count; i++)
                if (Shelf[i].Cost < parameter)
                {
                    _appController.dataContext.RemoveBook(Shelf[i]);
                    Shelf.Remove(Shelf[i]);
                }
            _appController.dataContext.RecalculateValues();
            RecalculateValues();
            ShopsTable();
        }
        private bool CanDecreasePrice()
        => Shelf.Count != 0;
        private void OnDecreasePrice()
        {
            var tmp = _appController.dataContext.BookTable.ToList<Book>();
            for (int i = 0; i < Shelf.Count; i++)
            {
                if (Shelf[i].Remain > Shelf[i].Sold * 2)
                {
                    Shelf[i].Cost /= 2;
                    tmp[i].Cost = Shelf[i].Cost;
                }
            }
        }

        #endregion
    }
}
