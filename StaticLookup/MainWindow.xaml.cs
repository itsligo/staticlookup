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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            lbxCities.ItemsSource = DataStore.Cities;
        }

        private void btnAddCity_Click(object sender, RoutedEventArgs e)
        {
            // read from textbox for data in
            string _city = tbkCity.Text;
            City _newCity = new City { Name = _city, Pop = 400000 };
            // add to the collection if not already present
            DataStore.AddIfNotThere(_newCity);
        }
    }   // end class

    public class DataStore
    {
        // collection of cities
        private static ObservableCollection<City> cities = new ObservableCollection<City>()
        { new City { Name="Paris", Pop=7000000 }, new City {Name="London", Pop=12000000 } };
        public static ObservableCollection<City> Cities { get { return cities; } set { cities = value; } }

        public static void AddIfNotThere(City _city)
        {
            // check if _city alredy here in datastore
            foreach (City cty in cities)
            {
                if (_city.Name == cty.Name) cities.Add(_city);
            }
            //if (!cities.Contains(_city))
            //{
            //    cities.Add(_city);
            //}
        }
    }

    public class City
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }
        private long _pop;
        public long Pop { get { return _pop; } set { _pop= value; } }
    }
}      // ns
