using OP4.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OP4.Services
{
    public interface InavigationService
    {
        public ViewModelBase CurrentViewModel { get; }
        public void NavigateTo<ViewModelType>() where ViewModelType : ViewModelBase;
    }

    public class NavigationService : ObservableObject, InavigationService
    {
        private Func<Type, ViewModelBase> _fabric;
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public NavigationService(Func<Type, ViewModelBase> fabric)
        {
            _fabric = fabric;
        }

        public void NavigateTo<ViewModelType>() where ViewModelType : ViewModelBase
        {
            ViewModelBase ViewModel = _fabric.Invoke(typeof(ViewModelType));
            CurrentViewModel = ViewModel;
        }
    }
}
