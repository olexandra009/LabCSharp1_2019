namespace _01LabChumachenko
{
    /// <summary>
    /// Логика взаимодействия для HoroscopeControl.xaml
    /// </summary>
    public partial class HoroscopeControl
    {
        public HoroscopeControl()
        {
            InitializeComponent();
            DataContext = new HoroscopeViewModel();
        }

      
    }
}
