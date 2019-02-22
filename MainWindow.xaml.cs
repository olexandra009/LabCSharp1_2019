using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace _01LabChumachenko
{

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

    }
}
