﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HexCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///


    public partial class MainWindow : Window
    {
        //private string hexValue = "";
        public MainWindow()
        {
            InitializeComponent();
        }



        private void NumInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string numInput = NumInputTextBox.Text.Trim();
            if (numInput.Length == 0)
            {
                DecTextBox.Text = String.Empty;
                HexTextBox.Text = String.Empty;
                BinTextBox.Text = String.Empty;
                return;
            }

            try
            {
                uint decValue;
                // Hexadecimal input
                if (numInput.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    Values.hexValue = numInput.Substring(2);
                    decValue = Convert.ToUInt32(Values.hexValue, 16);
                }
                // Binary input
                else if (numInput.StartsWith("0b", StringComparison.OrdinalIgnoreCase))
                {
                    Values.binValue = numInput.Substring(2);
                    decValue = Convert.ToUInt32(Values.binValue, 2);
                }
                // Decimal input
                else
                {
                    decValue = Convert.ToUInt32(numInput);
                }
                Values.hexValue = Values.decValue.ToString("X");
                Values.binValue = Convert.ToString(decValue, 2);
                Values.decValue = decValue.ToString();

                DecTextBox.Text = Values.decValue;
                HexTextBox.Text = "0x" + Values.hexValue;
                BinTextBox.Text = Values.binValue;
            }
            catch (Exception)
            {
                DecTextBox.Text = String.Empty;
                HexTextBox.Text = String.Empty;
                BinTextBox.Text = String.Empty;
            }
        }

        private void NumInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LaunchBitVisualizer();
            }
        }

        private void LaunchBitVisualizerButton_Click(object sender, RoutedEventArgs e)
        {
            LaunchBitVisualizer();
        }
        private void LaunchBitVisualizer()
        {
            string binaryValue = BinTextBox.Text;
            //string hexValue = HexTextBox.Text;
            string decValue = DecTextBox.Text;
            BitVisualizer bitVisualizerWindow = new BitVisualizer();
            bitVisualizerWindow.Show();
        }

        public static class Values
        {
            public static string hexValue { get; set; } = "";
            public static string decValue { get; set; } = "";
            public static string binValue { get; set; } = "";
        }
    }
}