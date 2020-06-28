using System.Windows;
using System.Windows.Controls;

namespace ColorSpy.Views
{
    /// <summary>
    /// Interaction logic for SelectorView.xaml
    /// </summary>
    public partial class SelectorView : UserControl
    {
        public SelectorView()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.Unsubscibe();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SpyStarted = true;
        }
    }
}