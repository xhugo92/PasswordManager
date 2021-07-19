using System.Windows;

namespace PasswordManagerCore.Modules
{
    /// <summary>
    /// Lógica interna para HelpView.xaml
    /// </summary>
    public partial class HelpView : Window
    {
        public HelpView()
        {
            InitializeComponent();
            Current = this;
        }

        public static HelpView Current { get; set; }
    }
}
