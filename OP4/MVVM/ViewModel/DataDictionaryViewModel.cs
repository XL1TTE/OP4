using OP4.Core;
using OP4.MVVM.Model;
using OP4.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace OP4.MVVM.ViewModel
{
    public class DataDictionaryViewModel: ViewModelBase
    {
        private readonly TextGenerator _textGenerator; 
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

        private string _firstDataColumn;
        public string FirstDataColumn
        {
            get => _firstDataColumn;

            set
            {
                _firstDataColumn = value;
                OnPropertyChanged();
            }
        }
        private string _secondDataColumn;
        public string SecondDataColumn
        {
            get => _secondDataColumn;

            set
            {
                _secondDataColumn = value;
                OnPropertyChanged();
            }
        }
        private string _thirdDataColumn;
        public string ThirdDataColumn
        {
            get => _thirdDataColumn;

            set
            {
                _thirdDataColumn = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<String> _firstDataColumnList;
        public ObservableCollection<String> FirstDataColumnList
        {
            get => _firstDataColumnList;

            set
            {
                _firstDataColumnList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<String> _secondDataColumnList;
        public ObservableCollection<String> SecondDataColumnList
        {
            get => _secondDataColumnList;

            set
            {
                _secondDataColumnList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<String> _thirdDataColumnList;
        public ObservableCollection<String> ThirdDataColumnList
        {
            get => _thirdDataColumnList;

            set
            {
                _thirdDataColumnList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<String> _fourthDataColumnList;
        public ObservableCollection<String> FourthDataColumnList
        {
            get => _fourthDataColumnList;

            set
            {
                _fourthDataColumnList = value;
                OnPropertyChanged();
            }
        }

        private string _fourthDataColumn;
        public string FourthDataColumn
        {
            get => _fourthDataColumn;

            set
            {
                _fourthDataColumn = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddInFirstDataColumn { get; set; }
        public RelayCommand AddInSecondsDataColumn { get; set; }
        public RelayCommand AddInThirdDataColumn { get; set; }
        public RelayCommand AddInFourthDataColumn { get; set; }
        public RelayCommand RemovefromFirstDataColumn { get; set; }
        public RelayCommand RemovefromSecondsDataColumn { get; set; }
        public RelayCommand RemovefromThirdDataColumn { get; set; }
        public RelayCommand RemovefromFourthDataColumn { get; set; }
        public RelayCommand NavigateToTextGenerator { get; set; }
        public RelayCommand LoadData {  get; set; }
        public RelayCommand SaveData { get; set; }
        public DataDictionaryViewModel(InavigationService navigations, TextGenerator textGenerator)
        {
            Navigation = navigations;
            _textGenerator = textGenerator;

            DataListUpdate();

            AddInFirstDataColumn = new RelayCommand(o => AddinFirstDataColumn(FirstDataColumn), o => FirstDataColumn != null && FirstDataColumn.Length >= 1);
            AddInSecondsDataColumn = new RelayCommand(o => AddinSecondDataColumn(SecondDataColumn), o => SecondDataColumn != null && SecondDataColumn.Length >= 1);
            AddInThirdDataColumn = new RelayCommand(o => AddinThirdDataColumn(ThirdDataColumn), o => ThirdDataColumn != null && ThirdDataColumn.Length >= 1);
            AddInFourthDataColumn = new RelayCommand(o => AddinFourthDataColumn(FourthDataColumn), o => FourthDataColumn != null && FourthDataColumn.Length >= 1);

            RemovefromFirstDataColumn = new RelayCommand(o => RemoveFromFirstDataColumn(FirstDataColumn), o=> FirstDataColumn != null && FirstDataColumn.Length >= 1);
            RemovefromSecondsDataColumn = new RelayCommand(o => RemoveFromSecondDataColumn(SecondDataColumn), o => SecondDataColumn != null && SecondDataColumn.Length >= 1);
            RemovefromThirdDataColumn = new RelayCommand(o => RemoveFromThirdDataColumn(ThirdDataColumn), o => ThirdDataColumn != null && ThirdDataColumn.Length >= 1);
            RemovefromFourthDataColumn = new RelayCommand(o => RemoveFromFourthDataColumn(FourthDataColumn), o => FourthDataColumn != null && FourthDataColumn.Length >= 1);


            LoadData = new RelayCommand(o => LoadDataInView(), o=> true);
            SaveData = new RelayCommand(o => _textGenerator.SaveDataFunc(), o => true);

            NavigateToTextGenerator = new RelayCommand(o => Navigation.NavigateTo<TextGeneratorViewModel>(), o => true);
        }

        private void AddinFirstDataColumn(string Word)
        {
            Word = Word.ToLower();
            if(!_textGenerator.FirstSentanceCollection.Contains(Word))
                _textGenerator.FirstSentanceCollection.Add(Word);
            DataListUpdate();
        }
        private void AddinSecondDataColumn(string Word)
        {
            Word = Word.ToLower();
            if (!_textGenerator.SecondSentanceCollection.Contains(Word))
                _textGenerator.SecondSentanceCollection.Add(Word);
            DataListUpdate();
        }
        private void AddinThirdDataColumn(string Word)
        {
            Word = Word.ToLower();
            if (!_textGenerator.ThirdtSentanceCollection.Contains(Word))
                _textGenerator.ThirdtSentanceCollection.Add(Word);
            DataListUpdate();
        }
        private void AddinFourthDataColumn(string Word)
        {
            Word = Word.ToLower();
            if (!_textGenerator.FourthSentanceCollection.Contains(Word))
                _textGenerator.FourthSentanceCollection.Add(Word);
            DataListUpdate();
        }

        private void RemoveFromFirstDataColumn(string Word)
        {
            Word = Word.ToLower();
            if (_textGenerator.FirstSentanceCollection.Contains(Word))
                _textGenerator.FirstSentanceCollection.Remove(Word);
            DataListUpdate();
        }
        private void RemoveFromSecondDataColumn(string Word)
        {
            Word = Word.ToLower();
            if (_textGenerator.SecondSentanceCollection.Contains(Word))
                _textGenerator.SecondSentanceCollection.Remove(Word);
            DataListUpdate();
        }
        private void RemoveFromThirdDataColumn(string Word)
        {
            Word = Word.ToLower();
            if (_textGenerator.ThirdtSentanceCollection.Contains(Word))
                _textGenerator.ThirdtSentanceCollection.Remove(Word);
            DataListUpdate();
        }
        private void RemoveFromFourthDataColumn(string Word)
        {
            Word = Word.ToLower();
            if (_textGenerator.FourthSentanceCollection.Contains(Word))
                _textGenerator.FourthSentanceCollection.Remove(Word);
            DataListUpdate();
        }

        private void DataListUpdate()
        {
            FirstDataColumnList = _textGenerator.FirstSentanceCollection;
            SecondDataColumnList = _textGenerator.SecondSentanceCollection;
            ThirdDataColumnList = _textGenerator.ThirdtSentanceCollection;
            FourthDataColumnList = _textGenerator.FourthSentanceCollection;
        }

        private void LoadDataInView()
        {
            _textGenerator.LoadDataFunc();
            DataListUpdate();
        }
    }
}
