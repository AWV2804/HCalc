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
        private string paddedHexValue = "";
        private string paddedBinaryValue = "";
        private string paddedDecValue = "";

        public MainWindow()
        {
            this.InitializeComponent();
            RenderDisplay();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
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
            //TODO: call function to update bit visualizer
            DisplayBitVisualizerValues();
        }

        private void NumInputTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            // TODO: prob dont need this function
            DisplayMainWindowValues(false);
            if (e.Key == VirtualKey.Enter)
            {
                // function to update bit visualizer
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

        private void DisplayBitVisualizerValues()
        {
            DisplayBits();
            DisplayHexValues();
            DisplayTextBoxValues();
        }

        private void DisplayBits()
        {
            // First clear all values
            for (int i = 0; i < 32; i++)
            {
                string bitTextBlockName = "Bit" + i;
                TextBlock bitTextBlock = (TextBlock)MainGrid.FindName(bitTextBlockName);
                if (bitTextBlock != null)
                {
                    bitTextBlock.Text = "0";
                    bitTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                    bitTextBlock.FontWeight = FontWeights.Normal;
                }
            }

            // Extract value and fill in bit view
            paddedBinaryValue = MainWindow.Values.binValue.PadLeft(32, '0');
            for (int i = 0; i < 32; i++)
            {
                string bitTextBlockName = "Bit" + i;
                TextBlock bitTextBlock = (TextBlock)MainGrid.FindName(bitTextBlockName);
                if (bitTextBlock != null)
                {
                    bitTextBlock.Text = paddedBinaryValue[31 - i].ToString();
                    bitTextBlock.Foreground = new SolidColorBrush(paddedBinaryValue[31 - i] == '1' ? Colors.DarkOrange : Colors.Black);
                    bitTextBlock.FontWeight = paddedBinaryValue[31 - i] == '1' ? FontWeights.Bold : FontWeights.Normal;
                }
            }
        }

        private void DisplayHexValues()
        {
            // First clear hex values
            for (int i = 0; i < 8; i++)
            {
                string hexValueTextBlockName = $"Hex{i}";
                TextBlock hexValueTextBlock = (TextBlock)MainGrid.FindName(hexValueTextBlockName);
                if (hexValueTextBlock != null)
                {
                    hexValueTextBlock.Text = "0";
                }
            }

            paddedHexValue = MainWindow.Values.hexValue.PadLeft(8, '0');
            for (int i = 0; i < 8; i++)
            {
                string hexValueTextBlockName = $"Hex{i}";
                TextBlock hexValueTextBlock = (TextBlock)MainGrid.FindName(hexValueTextBlockName);
                if (hexValueTextBlock != null)
                {
                    hexValueTextBlock.Text = paddedHexValue[7 - i].ToString();
                }
            }

        }

        private void DisplayTextBoxValues()
        {
            BVBinTextBox.Text = paddedBinaryValue;
            BVDecTextBox.Text = MainWindow.Values.decValue;
            BVHexTextBox.Text = "0x" + paddedHexValue;
        }

        private void UpdateValues(string newBinaryValue)
        {
            uint decValue;

            paddedBinaryValue = newBinaryValue;
            MainWindow.Values.binValue = paddedBinaryValue;


            paddedDecValue = Convert.ToUInt32(paddedBinaryValue, 2).ToString();
            MainWindow.Values.decValue = paddedDecValue;

            decValue = Convert.ToUInt32(paddedDecValue, 10);
            paddedHexValue = decValue.ToString("X");
            MainWindow.Values.hexValue = paddedHexValue;

            DisplayBitVisualizerValues();
            MainWindow.Instance.DisplayMainWindowValues(true);
        }

        private void LeftShiftButton_Click(object sender, RoutedEventArgs e)
        {
            string leftShiftedBinaryValue = paddedBinaryValue.Substring(1);
            leftShiftedBinaryValue = leftShiftedBinaryValue.PadRight(32, '0');
            UpdateValues(leftShiftedBinaryValue);
        }

        private void RightShiftButton_Click(object sender, RoutedEventArgs e)
        {
            string rightShiftedBinaryValue = paddedBinaryValue.Substring(0, paddedBinaryValue.Length - 1);
            rightShiftedBinaryValue = rightShiftedBinaryValue.PadLeft(32, '0');
            UpdateValues(rightShiftedBinaryValue);
        }

        private void RenderDisplay()
        {
            int bitIndex = 31;
            // splitting up into upper 16 bits and lower 16 bits

            // Renders bits 31-16
            for (int col = 0; col < 19; col++)
            {

                // row 0 will be the bit index
                // row 1 will be the bit box
                // row 2 will display the hex value of each set of 4 bits

                if (col == 4 || col == 9 || col == 14)
                {
                    continue;
                }

                TextBlock bitIndexTextBox = new TextBlock
                {
                    Text = bitIndex.ToString(),
                    Margin = new Thickness(5),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(bitIndexTextBox, 0);
                Grid.SetColumn(bitIndexTextBox, col);
                MainGrid.Children.Add(bitIndexTextBox);

                Border bitBoxBorder = new Border
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(2, 0, 2, 0)
                };
                Grid.SetRow(bitBoxBorder, 1);
                Grid.SetColumn(bitBoxBorder, col);
                MainGrid.Children.Add(bitBoxBorder);

                TextBlock bitBoxValueTextBox = new TextBlock
                {
                    Name = $"Bit{bitIndex}",
                    Width = 20,
                    Height = 28,
                    Text = "0",
                    FontSize = 16,
                    TextAlignment = TextAlignment.Center,
                    FontFamily = new FontFamily("Consolas"),
                    //Background = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                bitBoxBorder.Child = bitBoxValueTextBox;


                if ((col - 1) % 5 == 0)
                {
                    TextBlock hexValueTextBlock = new TextBlock
                    {
                        Name = $"Hex{(32 - col) / 4}",
                        Margin = new Thickness(5),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Text = "0"
                    };
                    Grid.SetRow(hexValueTextBlock, 2);
                    Grid.SetColumn(hexValueTextBlock, col);
                    Grid.SetColumnSpan(hexValueTextBlock, 2);

                    //RegisterName(hexValueTextBlock.Name, hexValueTextBlock);
                    MainGrid.Children.Add(hexValueTextBlock);
                }

                //RegisterName(bitBoxValueTextBox.Name, bitBoxValueTextBox);
                bitIndex--;
            }

            bitIndex = 15;
            // Renders bits 15-0
            for (int col = 0; col < 19; col++)
            {

                // row 0 will be the bit index
                // row 1 will be the bit box
                // row 2 will display the hex value of each set of 4 bits

                if (col == 4 || col == 9 || col == 14)
                {
                    continue;
                }

                TextBlock bitIndexTextBox = new TextBlock
                {
                    Text = bitIndex.ToString(),
                    Margin = new Thickness(5),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(bitIndexTextBox, 4);
                Grid.SetColumn(bitIndexTextBox, col);
                MainGrid.Children.Add(bitIndexTextBox);

                Border bitBoxBorder = new Border
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(2, 0, 2, 0)
                };
                Grid.SetRow(bitBoxBorder, 5);
                Grid.SetColumn(bitBoxBorder, col);
                MainGrid.Children.Add(bitBoxBorder);

                TextBlock bitBoxValueTextBox = new TextBlock
                {
                    Name = $"Bit{bitIndex}",
                    Width = 20,
                    Height = 28,
                    Text = "0",
                    FontSize = 16,
                    TextAlignment = TextAlignment.Center,
                    FontFamily = new FontFamily("Consolas"),
                    //Background = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                bitBoxBorder.Child = bitBoxValueTextBox;
                //RegisterName(bitBoxValueTextBox.Name, bitBoxValueTextBox);

                // Renders hex value per 4 bits
                if ((col - 1) % 5 == 0)
                {
                    TextBlock hexValueTextBlock = new TextBlock
                    {
                        Name = $"Hex{(16 - col) / 4}",
                        Margin = new Thickness(5),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Text = "0"
                    };
                    Grid.SetRow(hexValueTextBlock, 6);
                    Grid.SetColumn(hexValueTextBlock, col);
                    Grid.SetColumnSpan(hexValueTextBlock, 2);

                    //RegisterName(hexValueTextBlock.Name, hexValueTextBlock);
                    MainGrid.Children.Add(hexValueTextBlock);

                }



                bitIndex--;
            }
        }

    }

}
