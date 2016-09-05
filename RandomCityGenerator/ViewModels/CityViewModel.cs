using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace RandomCityGenerator.ViewModels
{
    public class CityViewModel : INotifyPropertyChanged
    {
        #region Fields

        private string _cityId;
        private string _cityName;
        private string _cityPop;
        private string _cityType;
        private string _cityWealth;
        private string _cityHistory;
        private int _randomCity;

        #endregion Fields

        #region Properties

        public string CityId
        {
            get
            {
                return _cityId;
            }
            set
            {
                _cityId = value;
                OnPropertyChanged("CityId");
            }
        }

        public string CityName
        {
            get
            {
                return _cityName;
            }
            set
            {
                _cityName = value;
                OnPropertyChanged("CityName");
            }
        }

        public string CityPop
        {
            get
            {
                return _cityPop;
            }
            set
            {
                _cityPop = value;
                OnPropertyChanged("CityPop");
            }
        }

        public string CityType
        {
            get
            {
                return _cityType;
            }
            set
            {
                _cityType = value;
                OnPropertyChanged("CityType");
            }
        }

        public string CityWealth
        {
            get
            {
                return _cityWealth;
            }
            set
            {
                _cityWealth = value;
                OnPropertyChanged("CityWealth");
            }
        }

        public string CityHistory
        {
            get
            {
                return _cityHistory;
            }
            set
            {
                _cityHistory = value;
                OnPropertyChanged("CityHistory");
            }
        }



        #endregion Properties

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged Members

        #region ICommand Members

        private ICommand _generateCityCommand;
        public ICommand GenerateCityCommand
        {
            get
            {
                if(_generateCityCommand == null)
                {
                    _generateCityCommand = new RelayCommand(param => GenerateCityCommand_CommandExecute(param));
                }
                return _generateCityCommand;
            }
            set
            {
                _generateCityCommand = value;
            }
        }
        
        #endregion ICommand Members

        #region Methods

        private void GenerateRandomCity()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            _randomCity = random.Next(1, 4);
        }

        public void GenerateCityCommand_CommandExecute(object param)
        {
            GenerateRandomCity();
            var cityId = _randomCity.ToString();

            XDocument xDoc = XDocument.Parse(Properties.Resources.Cities);

            IEnumerable<XElement> City = from row in xDoc.Descendants("City")
                                         where (string)row.Attribute("ID") == cityId
                                         select row;
            int maxID = xDoc.Descendants("City").Max(x => (int)x.Attribute("ID"));
            
            if(Convert.ToInt32(cityId) <= maxID)
            {
                foreach(XElement ele in City)
                {
                    CityId = ele.Attribute("ID").Value;
                    CityName = ele.Attribute("Name").Value;
                    CityPop = ele.Attribute("Population").Value;
                    CityHistory = ele.Attribute("History").Value;
                    CityWealth = ele.Attribute("Wealth").Value;

                }
            }
        }
        #endregion Methods
    }
}
