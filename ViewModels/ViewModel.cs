using CountriesAPP.Utility;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;

namespace CountriesAPP
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Country> countries;
        public ObservableCollection<Country> Countries
        {
            get
            {
                return countries;
            }
            private set { }
        }

        ICollectionView collectionView;
        public ICollectionView CollectionView
        {
            get { return collectionView; }
            set 
            {
                collectionView = value;
                OnPropertyChanged();

            }
        }

        private ObservableCollection<Country> countriesDB;
        public ObservableCollection<Country> CountriesDB
        {
            get
            {
                return countriesDB;
            }
            private set { }
        }

        ICollectionView collectionViewDB;
        public ICollectionView CollectionViewDB
        {
            get { return collectionViewDB; }
            set
            {
                collectionViewDB = value;
                OnPropertyChanged();

            }
        }


        private ICommand mouseDoubleClickCommand;
        public ICommand MouseDoubleClickCommand
        {
            get
            {
                if (mouseDoubleClickCommand == null)
                {
                    mouseDoubleClickCommand = new RelayCommand<Country>(
                        item =>
                        {
                            ToAdd(item);
                        });
                }

                return mouseDoubleClickCommand;
            }
        }

        public ICommand ButtonCommand { get; private set; }


        void ToAdd(Country country)
        {            
            if (MessageBox.Show("сохранить информацию в базу (MS SQL)?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
            }
            else
            {
                try
                {
                    if (!radioChecked)
                    {
                        
                        new ActionCB().AddCountry(country, GetValuesCB());
                        ViewUpdateCB(GetValuesCB());
                    }
                    new ActionsDB().AddCountry(country);
                    ViewUpdateDB();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        GroupFilter groupFilter;
        #region ПоляФильтра
        string _name = "";
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
                ToFilter();
            }
        }
        string _code = "";
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                OnPropertyChanged();
                ToFilter();
            }
        }
        string _capital = "";
        public string Capital
        {
            get { return _capital; }
            set
            {
                _capital = value;
                OnPropertyChanged();
                ToFilter();
            }
        }
        string _area = "";
        public string Area
        {
            get { return _area; }
            set
            {
                _area = value;
                OnPropertyChanged();
                ToFilter();
            }
        }
        string _population = "";
        public string Population
        {
            get { return _population; }
            set
            {
                _population = value;
                OnPropertyChanged();
                ToFilter();
            }
        }
        string _region="";
        public string Region
        {
            get { return _region; }
            set
            {
                _region = value;
                OnPropertyChanged();
                ToFilter();
            }
        }
        #endregion
        void ToFilter()
        {
            groupFilter = new GroupFilter();
            groupFilter.AddFilter(new Predicate<object>(item => ((Country)item).Name.Contains(_name, StringComparison.OrdinalIgnoreCase)));
            groupFilter.AddFilter(new Predicate<object>(item => ((Country)item).Alpha2Code.Contains(_code, StringComparison.OrdinalIgnoreCase)));
            groupFilter.AddFilter(new Predicate<object>(item => ((Country)item).Capital.Contains(_capital, StringComparison.OrdinalIgnoreCase)));
            groupFilter.AddFilter(new Predicate<object>(item => ((Country)item).Area.ToString().Contains(_area, StringComparison.OrdinalIgnoreCase)));
            groupFilter.AddFilter(new Predicate<object>(item => ((Country)item).Population.ToString().Contains(_population, StringComparison.OrdinalIgnoreCase)));
            groupFilter.AddFilter(new Predicate<object>(item => ((Country)item).Region.Contains(_region, StringComparison.OrdinalIgnoreCase)));

            collectionView.Filter = groupFilter.Filter;
        }

        private bool radioChecked=true;//условия подключения
        public bool RadioChecked
        {
            get { return radioChecked; }
            set
            {
                radioChecked = value;
                OnPropertyChanged();                

            }
        }

        #region ПоляПодключения
        string _serverDB = "";
        public string ServerDB
        {
            get { return _serverDB; }
            set
            {
                _serverDB = value;
                OnPropertyChanged();
            }
        }
        string _loginDB = "";
        public string LoginDB
        {
            get { return _loginDB; }
            set
            {
                _loginDB = value;
                OnPropertyChanged();
            }
        }
        string _passDB = "";
        public string PassDB
        {
            get { return _passDB; }
            set
            {
                _passDB = value;
                OnPropertyChanged();
            }
        }
        string _nameDB = "";
        public string NameDB
        {
            get { return _nameDB; }
            set
            {
                _nameDB = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private Country selectedCountry;
        public Country SelectedCountry
        {           
            set
            {
                selectedCountry = value;
                OnPropertyChanged();                
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string[] GetValuesCB()
        {
            string[] result = { ServerDB, NameDB, LoginDB, PassDB };
            return result;
        }

        public ViewModel()
        {
            UploadCountries uploadCountries = new UploadCountries();
            countries = uploadCountries.GetCountries();
            ViewUpdate();
            ViewUpdateDB();            
        }
     
        void ViewUpdate()
        {
            CollectionViewSource collectionViewSource = new CollectionViewSource() { Source = countries };
            collectionView = collectionViewSource.View;
        }

        void ViewUpdateDB()
        {
            countriesDB = new ActionsDB().GetCountries();
            CollectionViewSource collectionViewSource = new CollectionViewSource() { Source = countriesDB };
            CollectionViewDB = collectionViewSource.View;
            ButtonCommand = new RelayCommand<object>(o=> RefresViewDB());

        }

        void RefresViewDB()
        {
            ViewUpdateCB(GetValuesCB());
        }

        void ViewUpdateCB(string[] values)
        {
            countriesDB = new ActionCB().GetCountries(values);
            CollectionViewSource collectionViewSource = new CollectionViewSource() { Source = countriesDB };
            CollectionViewDB = collectionViewSource.View;

        }


    }
}
