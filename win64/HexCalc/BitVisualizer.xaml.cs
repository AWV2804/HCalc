using Microsoft.UI.Text;
using Microsoft.UI;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HexCalc
{
    public sealed partial class BitVisualizer : UserControl
    {
        private string paddedHexValue = "";
        private string paddedBinaryValue = "";
        private string paddedDecValue = "";
        private string gridName = "";
        private Grid bitVisualizerGrid = null;
        public static List<BitVisualizer> bitVisualizers = new List<BitVisualizer>();

        public BitVisualizer(string gridName, Input bitVisualizer_e)
        {

            this.InitializeComponent();

            this.gridName = gridName;
            this.BitVisualizerGrid.Name = gridName;
            this.bitVisualizerGrid = (Grid)this.FindName(gridName);
            this.DataContext = this;
            bitVisualizers.Add(this);
            RenderDisplay();
            AdjustBitVisualizerPositions();
        }

        public void DisplayBitVisualizerValues()
        {
            DisplayBits();
            DisplayHexValues();
        }
        private void DisplayBits()
        {
            /*Grid bitVisualizerGrid = (Grid)this.FindName(gridName);*/

            // First clear all values
            for (int i = 0; i < 32; i++)
            {
                string bitTextBlockName = "Bit" + i;
                TextBlock bitTextBlock = (TextBlock)bitVisualizerGrid.FindName(bitTextBlockName);
                if (bitTextBlock != null)
                {
                    bitTextBlock.Text = "0";
                    bitTextBlock.FontWeight = FontWeights.Normal;
                }
            }

            // Extract value and fill in bit view
            paddedBinaryValue = Values.binValue.PadLeft(32, '0');
            for (int i = 0; i < 32; i++)
            {
                string bitTextBlockName = "Bit" + i;
                TextBlock bitTextBlock = (TextBlock)bitVisualizerGrid.FindName(bitTextBlockName);
                if (bitTextBlock != null)
                {
                    bitTextBlock.Text = paddedBinaryValue[31 - i].ToString();
                    if (MainWindow.isDarkMode())
                    {
                        bitTextBlock.Foreground = new SolidColorBrush(paddedBinaryValue[31 - i] == '1' ? Colors.DarkOrange : Colors.White);
                    }
                    else
                    {
                        bitTextBlock.Foreground = new SolidColorBrush(paddedBinaryValue[31 - i] == '1' ? Colors.Blue : Colors.Black);

                    }
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
                TextBlock hexValueTextBlock = (TextBlock)bitVisualizerGrid.FindName(hexValueTextBlockName);
                if (hexValueTextBlock != null)
                {
                    hexValueTextBlock.Text = "0";
                }
            }

            paddedHexValue = Values.hexValue.PadLeft(8, '0');
            for (int i = 0; i < 8; i++)
            {
                string hexValueTextBlockName = $"Hex{i}";
                TextBlock hexValueTextBlock = (TextBlock)bitVisualizerGrid.FindName(hexValueTextBlockName);
                if (hexValueTextBlock != null)
                {
                    hexValueTextBlock.Text = paddedHexValue[7 - i].ToString();
                }
            }

        }

        private void UpdateValues(string newBinaryValue)
        {
            uint decValue;

            paddedBinaryValue = newBinaryValue;
            Values.binValue = paddedBinaryValue;


            paddedDecValue = Convert.ToUInt32(paddedBinaryValue, 2).ToString();
            Values.decValue = paddedDecValue;

            decValue = Convert.ToUInt32(paddedDecValue, 10);
            paddedHexValue = decValue.ToString("X");
            Values.hexValue = paddedHexValue;

            DisplayBitVisualizerValues();
            //FIXME: MainWindow.Instance.DisplayMainWindowValues(true);
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
                bitVisualizerGrid.Children.Add(bitIndexTextBox);

                Border bitBoxBorder = new Border
                {
                    BorderThickness = new Thickness(2),
                    BorderBrush = new SolidColorBrush(Colors.Gray),
                    Margin = new Thickness(2, 0, 2, 0)
                };
                Grid.SetRow(bitBoxBorder, 1);
                Grid.SetColumn(bitBoxBorder, col);
                bitVisualizerGrid.Children.Add(bitBoxBorder);

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
                        Text = "0",
                    };
                    Grid.SetRow(hexValueTextBlock, 2);
                    Grid.SetColumn(hexValueTextBlock, col);
                    Grid.SetColumnSpan(hexValueTextBlock, 2);

                    //RegisterName(hexValueTextBlock.Name, hexValueTextBlock);
                    bitVisualizerGrid.Children.Add(hexValueTextBlock);
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
                bitVisualizerGrid.Children.Add(bitIndexTextBox);

                Border bitBoxBorder = new Border
                {
                    BorderThickness = new Thickness(2),
                    BorderBrush = new SolidColorBrush(Colors.Gray),
                    Margin = new Thickness(2, 0, 2, 0)
                };
                Grid.SetRow(bitBoxBorder, 5);
                Grid.SetColumn(bitBoxBorder, col);
                bitVisualizerGrid.Children.Add(bitBoxBorder);

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
                    bitVisualizerGrid.Children.Add(hexValueTextBlock);

                }

                bitIndex--;
            }
        }

        private void AdjustBitVisualizerPositions()
        {
            if (bitVisualizers.Count == 3)
            {
                bitVisualizers[0].BitVisualizerGrid.VerticalAlignment = VerticalAlignment.Top;
                bitVisualizers[0].BitVisualizerGrid.Margin = new Thickness(0, 0, 50, 450);
                bitVisualizers[1].BitVisualizerGrid.VerticalAlignment = VerticalAlignment.Center;
                bitVisualizers[2].BitVisualizerGrid.VerticalAlignment = VerticalAlignment.Bottom;
            }
        }
    }
}
