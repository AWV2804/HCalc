using System;
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

namespace HexCalc
{
    /// <summary>
    /// Interaction logic for BitVisualizer.xaml
    /// </summary>
    public partial class BitVisualizer : Window
    {
        public BitVisualizer(string binaryValue, string hexValue, string decValue)
        {
            InitializeComponent();
            RenderDisplay();
            DisplayBits(binaryValue);
        }

        private void DisplayBits(string binaryValue)
        {
            binaryValue = binaryValue.PadLeft(32, '0');
            var customBlueColor = new SolidColorBrush(Color.FromRgb(0, 29, 153));
            for (int i = 0; i < 32; i++)
            {
                string bitTextBlockName = "Bit" + i;
                TextBlock bitTextBlock = (TextBlock)FindName(bitTextBlockName);
                if (bitTextBlock != null)
                {
                    bitTextBlock.Text = binaryValue[31 - i].ToString();
                    bitTextBlock.Foreground = binaryValue[31 - i] == '1' ? Brushes.DarkOrange : Brushes.Black;
                    bitTextBlock.FontWeight = binaryValue[31 - i] == '1' ? FontWeights.Bold : FontWeights.Normal;
                }
            }
        }

        private void RenderDisplay()
        {
            int row, col;
            for (int i = 0; i < 37; i++)
            {
                // get current row and col
                row = i >= 16 ? 0 : 1;
                col = i;

                // need textblock for bit name
                // Border for bit rep and bit inside

                TextBlock bitIndex = new TextBlock;
                bitIndex.Margin = 25;
            }
        }
    }
}
