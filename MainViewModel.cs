// MainViewModel.cs
using System.ComponentModel;
using System.Windows.Input;
using WpfApp1;
using GalaSoft.MvvmLight.Command;

namespace WpfApp1
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand SwitchToView1Command { get; }
        public ICommand SwitchToView2Command { get; }

        public ICommand SwitchToView3Command { get; }

        public ICommand SwitchToView4Command { get; }

        public MainViewModel()
        {
            SwitchToView1Command = new RelayCommand(SwitchToHomeView);
            SwitchToView2Command = new RelayCommand(SwitchToAnotherView);
            SwitchToView3Command = new RelayCommand(SwitchToThirdView);
            SwitchToView4Command = new RelayCommand(SwitchToFourthView);

            
            SwitchToHomeView();
        }

        private void SwitchToHomeView()
        {
            CurrentView = new HomeView();
        }

        private void SwitchToAnotherView()
        {
            CurrentView = new AttendencePage();
        }

        private void SwitchToThirdView()
        {
            CurrentView = new filterpage();
        }

        private void SwitchToFourthView()
        {
            CurrentView = new detailsfilter();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
