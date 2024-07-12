using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
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
        public MainWindow()
        {
            this.InitializeComponent();
            instance = this;
        }
        public static MainWindow Instance
        {
            get { return instance; }
        }
        public static class Values
        {
            public static string hexValue { get; set; } = "";
            public static string decValue { get; set; } = "";
            public static string binValue { get; set; } = "";
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
        }

        private void LaunchBitVisualizerButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayMainWindowValues(false);
            LaunchBitVisualizer();

        }

        private void NumInputTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            DisplayMainWindowValues(false);
            if (e.Key == VirtualKey.Enter)
            {
                LaunchBitVisualizer();
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
        }

        private void LaunchBitVisualizer()
        {
            BitVisualizer bitVisualizerWindow = new BitVisualizer();
            MainWindow.instance.Content = bitVisualizerWindow;
        }
    }

}
