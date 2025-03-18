using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LinearListApp.ViewModels;

namespace LinearListApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new LinearListViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
