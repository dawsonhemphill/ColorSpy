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
            this.Loaded += SelectorView_Loaded;
        }

        private void SelectorView_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Closing += Window_Closing;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.Unsubscibe();
            Loaded -= SelectorView_Loaded;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Subscibe();
        }
    }
}