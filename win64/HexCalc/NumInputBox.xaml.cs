using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HexCalc
{
    public sealed partial class NumInputBox : UserControl
    {
        private string stackPanelName = null;
        private StackPanel numInputBoxStackPanel = null;
        private static List<NumInputBox> numInputBoxes = new List<NumInputBox>();
        private Input numInputBox_e = 0;
        public NumInputBox(string stackPanelName, Input numInputBox_e)
        {
            this.InitializeComponent();
            this.stackPanelName = stackPanelName;
            this.numInputBoxStackPanel = (StackPanel)this.FindName(stackPanelName);
            this.DataContext = this;
            numInputBoxes.Add(this);
            AdjustNumInputBoxPositions();
        }

        private void NumInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {


            DisplayMainWindowValues(false);
            //TODO: call function to update bit visualizer
            bitVisualizer1.DisplayBitVisualizerValues();
            //bitVisualizer2.DisplayBitVisualizerValues();
        }

        private void AdjustNumInputBoxPositions()
        {

        }
    }
}
