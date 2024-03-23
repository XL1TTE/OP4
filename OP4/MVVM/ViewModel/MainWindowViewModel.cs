using OP4.Core;
using OP4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace OP4.MVVM.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private InavigationService _navigation;

        public InavigationService Navigation
        {
            get => _navigation;

            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        

        public MainWindowViewModel(InavigationService navigationService)
        {
            Navigation = navigationService;

            Navigation.NavigateTo<TextGeneratorViewModel>();
        }
    }
}
