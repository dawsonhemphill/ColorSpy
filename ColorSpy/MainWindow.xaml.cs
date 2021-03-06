﻿using System.Windows;
using System.Windows.Input;

namespace ColorSpy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Left = SystemParameters.PrimaryScreenWidth - (Width + 50);
            Top = 50;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            Mediator.Mediator.Instance.NotifyColleagues(Mediator.Message.MouseEnteredWindow, null);
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            Mediator.Mediator.Instance.NotifyColleagues(Mediator.Message.MouseLeftWindow, null);
        }
    }
}