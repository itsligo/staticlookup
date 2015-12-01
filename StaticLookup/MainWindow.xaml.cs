using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StaticLookup
{
    public partial class MainWindow : Window
    {
        Random rnd = new Random(System.Environment.TickCount);
        public MainWindow()
        {
            InitializeComponent();
            lbxCities.ItemsSource = DataStore.Cities;   // wire up the listbox
            cbxCities.ItemsSource = DataStore.Cities;
            cbxCities.SelectedIndex = (DataStore.Cities.Count>0)?1:0;
        }

        private void btnAddCity_Click(object sender, RoutedEventArgs e)
        {
            string _city = tbkCity.Text;    // read from textbox for data in
            City _newCity = new City { Name = _city, Pop = rnd.Next(10000000) };
            // add to the collection if not already present
            DataStore.AddIfNotThere(_newCity);
        }

        private void cbxCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            City selCity = (City)((ComboBox)sender).SelectedItem;
            //City selCity = (City)cbxCities.SelectedItem;
            tbkPop.Text = selCity.Pop.ToString("N0");
        }
    }   // end class

    public class DataStore
    {
        // collection of cities with property initialisation
        private static ObservableCollection<City> cities = new ObservableCollection<City>()
        { new City { Name="Paris", Pop=7000000 }, new City {Name="London", Pop=12000000 } };
        // public property to permit access to the 'database' of cities
        public static ObservableCollection<City> Cities { get { return cities; } set { cities = value; } }

        // public method to add to the database if not already there
        public static void AddIfNotThere(City _city)
        {
            //containsCheck(_city);   // won't work (easily) aside from String (see below)
            //simplestCheck(_city);   // simple but cumbersome

            #region BEST SOLUTION - use LINQ to iterate through collection detecting if there or not
            if (!(cities.Any(cty => cty.Name == _city.Name))) cities.Add(_city);
            #endregion
        }

        #region Contains won't work as you would expect when comparing City objects. By default it will compare the object memory address rather than the name and/or pop of the City. You can implement extra code (i.e. City:IEquatable<City>) to handle this but it's just easier to use LINQ above. I'll leave Contains here for now so you can see that it doesn't work.            
        private static void containsCheck(City _city)
        {
            if (!cities.Contains(_city))
            {
                cities.Add(_city);
            }
        }
        #endregion

        #region SIMPLEST SOLUTION - check if _city alredy here in datastore by iterating through collection with foreach and setting bool if found. Adding City is determined by that bool
        private static void simplestCheck(City _city)
        {
            bool foundCity = false;
            foreach (City cty in cities)
            {
                if (_city.Name == cty.Name)
                {
                    foundCity = true;
                    break;
                }
            }
            if (!foundCity) cities.Add(_city);
        }
        #endregion
    }

    public class City
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }
        private long _pop;
        public long Pop { get { return _pop; } set { _pop= value; } }
        public override string ToString()
        {
            return String.Format("{0} [pop: {1:N0}]",_name,_pop);
        }
    }
}      // end namespace
