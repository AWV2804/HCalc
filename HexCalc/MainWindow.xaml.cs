using System.Text;
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
    public partial class MainWindow : Window
    {
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
                int decValue;
                // Hexadecimal input
                if (numInput.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    string hexValue = numInput.Substring(2);
                    decValue = Convert.ToInt32(hexValue, 16);
                }
                // Binary input
                else if (numInput.StartsWith("0b", StringComparison.OrdinalIgnoreCase))
                {
                    string binValue = numInput.Substring(2);
                    decValue = Convert.ToInt32(binValue, 2);
                }
                // Decimal input
                else
                {
                    decValue = Convert.ToInt32(numInput);
                }

                DecTextBox.Text = decValue.ToString();
                HexTextBox.Text = "0x" + decValue.ToString("X");
                BinTextBox.Text = Convert.ToString(decValue, 2);
            }
            catch (Exception)
            {
                DecTextBox.Text = String.Empty;
                HexTextBox.Text = String.Empty;
                BinTextBox.Text = String.Empty;
            }
        }

        private void LaunchBitVisualizerButton_Click(object sender, RoutedEventArgs e)
        {
            string binaryValue = BinTextBox.Text;
            string hexValue = HexTextBox.Text;
            string decValue = DecTextBox.Text;
            BitVisualizer bitVisualizerWindow = new BitVisualizer(binaryValue, hexValue, decValue);
            bitVisualizerWindow.Show();
        }
    }
}