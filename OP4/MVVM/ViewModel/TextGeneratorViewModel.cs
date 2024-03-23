using OP4.Components;
using OP4.Core;
using OP4.MVVM.Model;
using OP4.Services;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace OP4.MVVM.ViewModel
{
    public class TextGeneratorViewModel : ViewModelBase
    {
        private InavigationService _navigation;
        private TextGenerator _Textgenerator;
        public InavigationService Navigation
        {
            get => _navigation;

            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Enum> _textCases;
        public ObservableCollection<Enum> TextCases
        {
            get => _textCases;
            set
            {
                _textCases = value;
                OnPropertyChanged();
            }
        }

        private Control _menuView;
        public Control MenuView
        {
            get => _menuView;
            set
            {
                _menuView = value;
                OnPropertyChanged();
            }
        }
        private string _generatedText;
        public string GeneratedText
        {
            get => _generatedText;
            set
            {
                _generatedText = value;
                OnPropertyChanged();
            }
        }
        private Enum _chosenTextCase;
        public Enum ChosenTextCase
        {
            get => _chosenTextCase;
            set
            {
                _chosenTextCase = value;
                OnPropertyChanged();
            }
        }
        private int _sentanceCount;
        public int SentanceCount
        {
            get => _sentanceCount;
            set
            {
                _sentanceCount = value;
                OnPropertyChanged();
            }
        }


        private bool _isTextRandom;
        public bool IsTextRandom
        {
            get => _isTextRandom;
            set
            {
                _isTextRandom = value;
                OnPropertyChanged();
            }
        }
        private string _symbCount;
        public string SymbCount
        {
            get => _symbCount;
            set
            {
                _symbCount = value;
                OnPropertyChanged();
            }
        }
        private string _wordCount;
        public string WordCount
        {
            get => _wordCount;
            set
            {
                _wordCount = value;
                OnPropertyChanged();
            }
        }
        private string _uniqWordCounts;
        public string UniqWordCounts
        {
            get => _uniqWordCounts;
            set
            {
                _uniqWordCounts = value;
                OnPropertyChanged();
            }
        }

        private TextGenerator.PlotData _plotData ;
        public TextGenerator.PlotData PlotData
        {
            get => _plotData;
            set
            {
                _plotData = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SwitchToTextGenerationMenu { get; set; }
        public RelayCommand SwitchToTextInfoMenu { get; set; }
        public RelayCommand NavigateToDataDictionaryView { get; set; }

        public RelayCommand GenerateSomeText { get; set; }
        public TextGeneratorViewModel(InavigationService navigation, TextGenerator textGenerator)
        {
            MenuView = new TextGenerationMenu();
            _Textgenerator = textGenerator;

            Navigation = navigation;
            TextCases = textGenerator.TextCases;
            
            SwitchToTextGenerationMenu = new RelayCommand(o => MenuView = new TextGenerationMenu(), o => true);
            SwitchToTextInfoMenu = new RelayCommand(o => MenuView = new TextInformationMenu(PlotData), o => true);

            NavigateToDataDictionaryView = new RelayCommand(o => Navigation.NavigateTo<DataDictionaryViewModel>(), o => true);

            GenerateSomeText = new RelayCommand(o => GenerateText(), o => true);

        }

        private void GenerateText()
        {
            _Textgenerator.IsTextRandom = IsTextRandom;
            _Textgenerator.SentanceCount = SentanceCount;
            _Textgenerator.ChosenTextCase = ChosenTextCase;
            GeneratedText =  _Textgenerator.TextGenerate();
            GetTextStats();
            PlotData = _Textgenerator.PlotUpdate();
        }

        private void GetTextStats()
        {
            SymbCount = _Textgenerator.GetSymbCount();
            WordCount = _Textgenerator.GetWordCount();
            UniqWordCounts = _Textgenerator.GetUniqueWordCount();
        }
    }
}
