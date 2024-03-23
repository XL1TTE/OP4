using OP4.MVVM.Model;
using ScottPlot;
using ScottPlot.Drawing.Colormaps;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static OP4.MVVM.Model.TextGenerator;
using static ScottPlot.Plottable.PopulationPlot;

namespace OP4.Components
{
    /// <summary>
    /// Interaction logic for TextInformationMenu.xaml
    /// </summary>
    public partial class TextInformationMenu : UserControl
    {
        private TextGenerator.PlotData _plotData;
        public TextInformationMenu(TextGenerator.PlotData plotData)
        {
            _plotData = plotData;
            InitializeComponent();
            PlotUpdate();
            
        }

        public void PlotUpdate()
        {

            double[] countsOfWords = new double[5];
            double[] positions = { 0, 1, 2, 3, 4 };
            string[] labels = new string[5];
            if(_plotData != null)
            {
                foreach (var el in _plotData.TopOfWords.Keys)
                {
                    labels[el - 1] = _plotData.TopOfWords[el];
                    countsOfWords[el - 1] = _plotData.FreqOfWords[_plotData.TopOfWords[el]];
                }
                TextInfo.Plot.AddBar(countsOfWords, positions);
                TextInfo.Plot.XTicks(positions, labels);
                TextInfo.Plot.SetAxisLimits(yMin: 0);
                TextInfo.Refresh();
            }
           
        }
    }
}
