using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace _01LabChumachenko
{
    class HoroscopeViewModel: INotifyPropertyChanged
    {   
        #region Fields

        private string _westHoroscope;
        private string _eastHoroscope;
        private string _age;
        private DateTime? _birthday;
        #endregion
        #region Commands
        private RelayCommand<object> _calculatedCommand;
        private RelayCommand<object> _closeCommand;
        #endregion
        #region Properties
        public string WestHoroscope
        {
            get => _westHoroscope;
            set
            {
                _westHoroscope = value;
                OnPropertyChanged();
            }
        }
        public string EastHoroscope
        {
            get => _eastHoroscope;
            set
            {
                _eastHoroscope = value; 
                OnPropertyChanged();
            }
        }
        public string Age
        {
            get => _age;
            set
            {
                _age = value; 
                OnPropertyChanged();
            }

        }

        public DateTime? Birthday
        {
          
                get => _birthday;
                set
                {
                    _birthday = value;
                    OnPropertyChanged();
                }
            
        }
        #endregion
        #region Commands

        public RelayCommand<object> CalculatedCommand
        {
            get =>
                _calculatedCommand?? (_calculatedCommand = new RelayCommand<object>(
                    HoroscopeImplementation));
        }
        public RelayCommand<Object> CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(o => Environment.Exit(0)));
            }
        }
        private async void HoroscopeImplementation(object o)
        {

            if (_birthday != null)
            {
                DateTime birthday = (DateTime) _birthday; 
                if (!IsDateValid(birthday)) return;

               
                var dayBirth = birthday.Day;
                var monthBirth = birthday.Month;

                LoaderManager.Instance.ShowLoader();

               await Task.Run(() =>
                 {
                     Thread.Sleep(1000);
                     Age = ""+((DateTime.Today.DayOfYear >= birthday.DayOfYear)
                         ? DateTime.Today.Year - birthday.Year
                         : DateTime.Today.Year - birthday.Year - 1);
                     EastHoroscope = Enum.GetName(typeof(EastHoroscopeList), birthday.Year % 12); 
                     WestHoroscope = CreateWestHoroscope(dayBirth, monthBirth);
                    

                 });
                 LoaderManager.Instance.HideLoader();


                if (birthday.Day == DateTime.Today.Day
                    && birthday.Month == DateTime.Today.Month)
                    MessageBox.Show("Happy Birthday To You!");
            }
            else
            {
                MessageBox.Show("Select date, please");
            }

        }



        private bool IsDateValid(DateTime birthday)
        {
            if (DateTime.Compare(birthday, DateTime.Today) > 0)
            {
                MessageBox.Show("Wrong date");
                return false;
            }

            if (DateTime.Today.Year - 135 > birthday.Year)
            {
                MessageBox.Show("Sorry, you cannot be older than 135 years. \n Please, enter correct information about your birthday. " +
                                "\n (If you are vampire, please compute your year of birthday plus twelve while entered information will be correct)");
                return false;
            }

            return true;
        }

        private string CreateWestHoroscope(int dayBirth, int monthBirth)
        {

            if (dayBirth < 18) return Enum.GetName(typeof(WestHoroscopeList), monthBirth);
            if (dayBirth > 23) return Enum.GetName(typeof(WestHoroscopeList), monthBirth);
            switch (monthBirth)
            {
                case 2:
                    return (dayBirth == 18) ? Enum.GetName(typeof(WestHoroscopeList), monthBirth) : Enum.GetName(typeof(WestHoroscopeList), monthBirth + 1);

                case 1:
                case 3:
                case 4:
                    return dayBirth == 20 ? Enum.GetName(typeof(WestHoroscopeList), monthBirth) : Enum.GetName(typeof(WestHoroscopeList), monthBirth + 1);

                case 5:
                case 6:
                case 12:
                    return dayBirth == 21 ? Enum.GetName(typeof(WestHoroscopeList), monthBirth) : Enum.GetName(typeof(WestHoroscopeList), (monthBirth + 1) % 12);

                case 7:
                case 11:
                case 9:
                    return dayBirth == 22 ? Enum.GetName(typeof(WestHoroscopeList), monthBirth) : Enum.GetName(typeof(WestHoroscopeList), monthBirth + 1);

                case 8:
                    return dayBirth == 23 ? Enum.GetName(typeof(WestHoroscopeList), monthBirth) : Enum.GetName(typeof(WestHoroscopeList), monthBirth + 1);
                default:
                    return "Something went wrong";
            }


        }

        internal enum WestHoroscopeList
        {
            Copricorn = 1,
            Aquarius,
            Pisces,
            Aries,
            Taurus,
            Gemini,
            Cancer,
            Leo,
            Virgio,
            Libra,
            Scorpio,
            Sagittarius

        }
        internal enum EastHoroscopeList
        {
            Monkey,
            Cock,
            Dog,
            Pig,
            Rat,
            Bull,
            Tiger,
            Cat,
            Dragon,
            Snake,
            Horse,
            Goat

        }


        #endregion
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
