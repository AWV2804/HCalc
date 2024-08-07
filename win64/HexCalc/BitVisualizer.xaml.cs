﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static HexCalc.MainWindow;

namespace HexCalc
{
    /// <summary>
    /// Interaction logic for BitVisualizer.xaml
    /// </summary>
    public partial class BitVisualizer : Window
    {
        private string paddedHexValue = "";
        private string paddedBinaryValue = "";
        private string paddedDecValue = "";

        public BitVisualizer()
        {
            this.paddedDecValue = MainWindow.Values.decValue;
            InitializeComponent();
            RenderDisplay();
            DisplayBitVisualizerValues();
        }

        private void DisplayBitVisualizerValues()
        {
            DisplayBits();
            DisplayHexValues();
            DisplayTextBoxValues();
        }

        private void DisplayBits()
        {
            paddedBinaryValue = MainWindow.Values.binValue.PadLeft(32, '0');
            for (int i = 0; i < 32; i++)
            {
                string bitTextBlockName = "Bit" + i;
                TextBlock bitTextBlock = (TextBlock)FindName(bitTextBlockName);
                if (bitTextBlock != null)
                {
                    bitTextBlock.Text = paddedBinaryValue[31 - i].ToString();
                    bitTextBlock.Foreground = paddedBinaryValue[31 - i] == '1' ? Brushes.DarkOrange : Brushes.Black;
                    bitTextBlock.FontWeight = paddedBinaryValue[31 - i] == '1' ? FontWeights.Bold : FontWeights.Normal;
                }
            }
        }

        private void DisplayHexValues()
        {
            paddedHexValue = MainWindow.Values.hexValue.PadLeft(8, '0');
            for (int i = 0;i < 8; i++)
            {
                string hexValueTextBlockName = $"Hex{i}";
                TextBlock hexValueTextBlock = (TextBlock)FindName(hexValueTextBlockName);
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
                    BorderBrush = Brushes.Black,
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
                    Background = Brushes.White,
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

                    RegisterName(hexValueTextBlock.Name, hexValueTextBlock);
                    MainGrid.Children.Add(hexValueTextBlock);
                }

                RegisterName(bitBoxValueTextBox.Name, bitBoxValueTextBox);
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
                    BorderBrush = Brushes.Black,
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
                    Background = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                bitBoxBorder.Child = bitBoxValueTextBox;
                RegisterName(bitBoxValueTextBox.Name, bitBoxValueTextBox);

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

                    RegisterName(hexValueTextBlock.Name, hexValueTextBlock);
                    MainGrid.Children.Add(hexValueTextBlock);

                }



                bitIndex--;
            }
        }
    }
}
