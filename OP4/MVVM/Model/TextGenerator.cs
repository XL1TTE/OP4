using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ScottPlot;

namespace OP4.MVVM.Model
{
    public class TextGenerator
    {
        private Random random = new Random();
        private Enum _chosenTextCase;
        public Enum ChosenTextCase
        {
            get => _chosenTextCase;
            set
            {
                _chosenTextCase = value;
                
            }
        }
        private int _sentanceCount;
        public int SentanceCount
        {
            get => _sentanceCount;
            set
            {
                _sentanceCount = value;

            }
        }

        private string _generatedText;
        public string Generatedtext
        {
            get => _generatedText;
            set
            {
                _generatedText = value;

            }
        }

        private bool _isTextRandom;
        public bool IsTextRandom
        {
            get => _isTextRandom;
            set
            {
                _isTextRandom = value;

            }
        }
        public ObservableCollection<String> UniqueWords = new ObservableCollection<String>();

        public ObservableCollection<Enum> TextCases = new ObservableCollection<Enum>();

        public Dictionary<int, String> TopOfWords;

        public Dictionary<String, int> FreqOfWords;

        public Dictionary<int, ObservableCollection<String>> DataDictionary;

        public ObservableCollection <String> FirstSentanceCollection = new ObservableCollection<String>()
        {
            "дорогия товарищи"
        };
        public ObservableCollection<String> SecondSentanceCollection = new ObservableCollection<String>()
        {
            "в связи с ситуацией"
        };
        public ObservableCollection<String> ThirdtSentanceCollection = new ObservableCollection<String>()
        {
            "необходимо увеличить"
        };
        public ObservableCollection<String> FourthSentanceCollection = new ObservableCollection<String>()
        {
            "выплаты и поощерения"
        };

        public enum Textregisters
        {
            Верхний,
            Нижний,
            Стандартный,
        }

        public TextGenerator()
        {
            TextCases = new ObservableCollection<Enum>()
            {
                Textregisters.Верхний,
                Textregisters.Нижний,
                Textregisters.Стандартный,
            };

            DataDictionary = new Dictionary<int, ObservableCollection<String>>()
            {
                {1, FirstSentanceCollection },
                {2, SecondSentanceCollection },
                {3, ThirdtSentanceCollection },
                {4, FourthSentanceCollection },
            };


        }
        public string TextGenerate()
        {
            
            string generatedText = "";
            string generatedTempText = "";
            if (IsTextRandom)
            {
                for (int i = 0; i < SentanceCount; i++)
                {
                    generatedTempText += $"{DataDictionary[random.Next(1, DataDictionary.Count)][random.Next(0, DataDictionary[1].Count)]} ";
                    generatedTempText += $"{DataDictionary[random.Next(1, DataDictionary.Count)][random.Next(0, DataDictionary[2].Count)]} ";
                    generatedTempText += $"{DataDictionary[random.Next(1, DataDictionary.Count)][random.Next(0, DataDictionary[3].Count)]} ";
                    generatedTempText += $"{DataDictionary[random.Next(1, DataDictionary.Count)][random.Next(0, DataDictionary[4].Count)]}. ";

                    generatedTempText += "\n";
                    char[] temp = generatedTempText.ToCharArray();
                    temp[0] = Convert.ToChar(temp[0].ToString().ToUpper());
                    generatedTempText = "";
                    generatedText += String.Join(String.Empty, temp);
                }
                

            }
            else
            {
                for (int i = 0; i < SentanceCount; i++)
                {
                    generatedTempText += $"{DataDictionary[1][random.Next(0, DataDictionary[1].Count)]} ";
                    generatedTempText += $"{DataDictionary[2][random.Next(0, DataDictionary[2].Count)]} ";
                    generatedTempText += $"{DataDictionary[3][random.Next(0, DataDictionary[3].Count)]} ";
                    generatedTempText += $"{DataDictionary[4][random.Next(0, DataDictionary[4].Count)]}. ";
                    generatedTempText += "\n";
                    char[] temp = generatedTempText.ToCharArray();
                    temp[0] = Convert.ToChar(temp[0].ToString().ToUpper());
                    generatedTempText = "";
                    generatedText += String.Join(String.Empty, temp);
                }
            }
            
            if (ChosenTextCase == TextCases[0])
            {
                generatedText = generatedText.ToUpper();
            }
            else { if (ChosenTextCase != null & ChosenTextCase != TextCases[2]) { generatedText = generatedText.ToLower(); } }
            Generatedtext = generatedText;
            FindMoreFreqWords(Generatedtext);
            
            return generatedText;

        }
        public PlotData PlotUpdate()
        {
            return new PlotData(TopOfWords, FreqOfWords);
        }
        public class PlotData
        {
            public Dictionary<int, String> TopOfWords;
            public Dictionary<String, int> FreqOfWords;
            public PlotData(Dictionary<int, string> topOfWords, Dictionary<string, int> freqOfWords)
            {
                TopOfWords = topOfWords;
                FreqOfWords = freqOfWords;
            }
        }

        private void FindMoreFreqWords(string _words)
        {
            string[] words = _words.Split();
            Dictionary<String, int> FreqofWords = new Dictionary<String, int>();
            Dictionary<int, String> MoreFreqWords = new Dictionary<int, String>();
            FreqofWords["_"] = _words.Split(" ").Length;
            foreach (string word in words)
            {
                if (word == "")
                {                  
                    continue;
                }
                if (!FreqofWords.ContainsKey(word))
                {
                    FreqofWords[word] = 1;
                }
                else
                {
                    FreqofWords[word] += 1;
                }
            }
            int counter = FreqofWords.Count;
            FreqOfWords = FreqofWords;
            foreach (KeyValuePair<string, int> _word in FreqofWords.OrderBy(key => key.Value))
            {
                if(counter < 6)
                {
                    MoreFreqWords.Add(counter, _word.Key);
                }              
                counter--;
            }
            TopOfWords = MoreFreqWords;
        }

        public string GetSymbCount()
        {
            int SymbCount = Generatedtext.ToCharArray().Length;
            return $"Общее кол-во символов: {SymbCount}";
        }
        public string GetWordCount()
        {
            int WordCount = Generatedtext.Split(" ").Length - 1;
            return $"Общее кол-во слов: {WordCount}";
        }
        public string GetUniqueWordCount()
        {
            int UniqWordCount = FreqOfWords.Count - 1;
            return $"Общее кол-во уникальных слов: {UniqWordCount}";
        }
    }
}
