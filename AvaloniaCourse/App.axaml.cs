using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaCourse.ViewModels;
using AvaloniaCourse.Views;

namespace AvaloniaCourse
{
    public partial class App : Application
    {
        private static AppController _controller;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                MainWindowViewModel mainVM = new MainWindowViewModel();
                _controller = new AppController(mainVM);

                desktop.MainWindow = new MainWindow
                {
                    DataContext = _controller.MainWindowViewModel,
                };
                _controller.ChangeCurrentView(CurrentViewTypes.Organizer);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
