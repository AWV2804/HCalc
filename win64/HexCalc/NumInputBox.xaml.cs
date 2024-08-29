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
using static HexCalc.MainWindow;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HexCalc
{
    public sealed partial class NumInputBox : UserControl
    {
        private string stackPanelName = null;
        private StackPanel numInputBoxStackPanel = null;
        public static List<NumInputBox> numInputBoxes = new List<NumInputBox>();
        private Input numInputBox_e = 0;
        public NumInputBox(string stackPanelName, Input numInputBox_e)
        {
            this.InitializeComponent();
            this.stackPanelName = stackPanelName;
            this.NumInputBox_S.Name = stackPanelName;
            this.numInputBoxStackPanel = (StackPanel)this.FindName(stackPanelName);
            this.DataContext = this;
            numInputBoxes.Add(this);
            AdjustNumInputBoxPositions();
        }

        private async void ShowErrorMessage(string message, string title)
        {
            ContentDialog dialog = new ContentDialog
            {
                XamlRoot = this.Content.XamlRoot,
                Title = title,
                Content = message,
                CloseButtonText = "OK"
            };

            await dialog.ShowAsync();
        }

        private void NumInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DisplayMainWindowValues(false);
            //TODO: call function to update bit visualizer
            switch(numInputBox_e)
            {
                case Input.BIT_VISUALIZER_0:
                    MainWindow.bitVisualizer0.DisplayBitVisualizerValues();
                    break;
                case Input.BIT_VISUALIZER_1:
                    MainWindow.bitVisualizer1.DisplayBitVisualizerValues();
                    break;
                case Input.BIT_VISUALIZER_R:
                    MainWindow.bitVisualizerR.DisplayBitVisualizerValues();
                    break;
                default:
                    ShowErrorMessage("Something went wrong. Please try again.", "Invalid BitVisualizer Call");
                    break;
            }
            if (Values.binValue.Length == 32)
            {
                numInputBoxes[0].InstructionFormatComboBox.Visibility = Visibility.Visible;
            }
            else
            {
                numInputBoxes[0].InstructionFormatComboBox.Visibility = Visibility.Collapsed;

            }
        }

        public void DisplayInstructionFormat()
        {
            Console.WriteLine("Would display instruction format");

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
        }

        private void AdjustNumInputBoxPositions()
        {
            if (numInputBoxes.Count == 3)
            {
                numInputBoxes[0].numInputBoxStackPanel.VerticalAlignment = VerticalAlignment.Top;
                numInputBoxes[1].numInputBoxStackPanel.VerticalAlignment = VerticalAlignment.Center;
                numInputBoxes[2].numInputBoxStackPanel.VerticalAlignment = VerticalAlignment.Bottom;
            }
        }

        private void NumInputTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                numInputBoxes[0].DisplayMainWindowValues(false);
                if (Values.binValue.Length == 32)
                {
                    numInputBoxes[0].InstructionFormatComboBox.Visibility = Visibility.Visible;
                }
                else
                {
                    numInputBoxes[0].InstructionFormatComboBox.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void InstructionFormatComboBox_Changed(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
