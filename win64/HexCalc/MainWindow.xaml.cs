using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HexCalc
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private static MainWindow instance;
        public static NumInputBox numInputBox0;
        public static NumInputBox numInputBox1;
        public static NumInputBox numInputBoxR;
        public static BitVisualizer bitVisualizer0;
        public static BitVisualizer bitVisualizer1;
        public static BitVisualizer bitVisualizerR;

        public MainWindow()
        {
            this.InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            instance = this;
            // Initialize NumInputBox1
            numInputBox0 = new NumInputBox("NumInputBox0", Input.NUM_INPUT_BOX_0);
            NumInputBoxContainer.Children.Add(numInputBox0);
            // Initialize BitVisualizer1
            bitVisualizer0 = new BitVisualizer("BitVisualizerGrid0", Input.BIT_VISUALIZER_0);
            BitVisualizerContainer.Children.Add(bitVisualizer0);
        }

        public static MainWindow Instance
        {
            get { return instance; }
        }

        /*public static class Values
        {
            public static string hexValue { get; set; } = "";
            public static string decValue { get; set; } = "";
            public static string binValue { get; set; } = "";
        }*/

        public static bool isDarkMode()
        {
            return Application.Current.RequestedTheme == ApplicationTheme.Dark;
        }

/*----------------------------------------------------------------------------
Value Entry Methods
----------------------------------------------------------------------------*/

        /*private void NumInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DisplayMainWindowValues(false);
            //TODO: call function to update bit visualizer
            bitVisualizer1.DisplayBitVisualizerValues();
            //bitVisualizer2.DisplayBitVisualizerValues();
        }

        private void NumInputTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                DisplayMainWindowValues(false);
                bitVisualizer1.DisplayBitVisualizerValues();
            }
        }

        public void DisplayMainWindowValues(bool shiftedValues)
        {
            try
            {
                uint decValue;
                if (!shiftedValues)
                {
                    string numInput = NumInputTextBox.Text.Trim();

                    if (numInput.Length == 0)
                    {
                        DecTextBox.Text = String.Empty;
                        HexTextBox.Text = String.Empty;
                        BinTextBox.Text = String.Empty;
                        return;
                    }

                    // Hexadecimal input
                    if (numInput.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    {
                        if (numInput.Length > 2)
                        {
                            Values.hexValue = numInput.Substring(2);
                            decValue = Convert.ToUInt32(Values.hexValue, 16);
                        }
                        else
                        {
                            return;
                        }

                    }
                    // Binary input
                    else if (numInput.StartsWith("0b", StringComparison.OrdinalIgnoreCase))
                    {
                        if (numInput.Length > 2)
                        {
                            Values.binValue = numInput.Substring(2);
                            decValue = Convert.ToUInt32(Values.binValue, 2);
                        }
                        else
                        {
                            return;
                        }
                    }
                    // Decimal input
                    else
                    {
                        decValue = Convert.ToUInt32(numInput);
                    }
                }
                else
                {
                    decValue = Convert.ToUInt32(Values.decValue);
                }

                Values.hexValue = decValue.ToString("X");
                Values.binValue = Convert.ToString(decValue, 2);
                Values.decValue = decValue.ToString();

                DecTextBox.Text = Values.decValue;
                HexTextBox.Text = "0x" + Values.hexValue;
                BinTextBox.Text = Values.binValue;
            }
            catch (Exception exception)
            {
                DecTextBox.Text = String.Empty;
                HexTextBox.Text = String.Empty;
                BinTextBox.Text = String.Empty;
                ShowErrorMessage("There was an error: " + exception.Message, "Error");
            }
        }*/

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            // Hide add button
            AddButton.Visibility = Visibility.Collapsed;

            bitVisualizer1 = new BitVisualizer("BitVisualizerGrid1", Input.BIT_VISUALIZER_1);
            BitVisualizerContainer.Children.Add(bitVisualizer1);
            bitVisualizerR = new BitVisualizer("BitVisualizerGridR", Input.BIT_VISUALIZER_R);
            BitVisualizerContainer.Children.Add(bitVisualizerR);
            numInputBox1 = new NumInputBox("NumInputBox1", Input.NUM_INPUT_BOX_1);
            NumInputBoxContainer.Children.Add(numInputBox1);
            numInputBoxR = new NumInputBox("NumInputBoxR", Input.NUM_INPUT_BOX_R);
            NumInputBoxContainer.Children.Add(numInputBoxR);
        }


        /*----------------------------------------------------------------------------
        Bit Visualizer Methods
        ----------------------------------------------------------------------------*/







    }

}
