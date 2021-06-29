using PasswordManagerCore.Resources;
using System.Windows;

namespace PasswordManagerCore.Modules
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
            var desktopWorkingArea = SystemParameters.WorkArea;
            Left = desktopWorkingArea.Right - Width;
            Top = desktopWorkingArea.Bottom - Height;
            DataContext = new MainWindowViewModel();
            Current = this;
            InstanceVariables = new ConfigurationVariables();
        }

        public ConfigurationVariables InstanceVariables { get; set; }

        public static MainWindowView Current { get; set; }
    }
}
