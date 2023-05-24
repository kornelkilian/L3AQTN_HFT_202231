using L3AQTN_HFT_202231.WpfClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace L3AQTN_HFT_202231.WpfClient.Windows
{
    /// <summary>
    /// Interaction logic for OwnerWindow.xaml
    /// </summary>
    public partial class OwnerWindow : Window
    {
        public OwnerWindow()
        {
            InitializeComponent();
            DataContext = new OwnerWindowViewModel();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Regular expression pattern to match only numeric input
            Regex regex = new Regex("[^0-9]+");

            // Check if the input matches the pattern
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
