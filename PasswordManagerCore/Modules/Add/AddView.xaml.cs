using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace PasswordManagerCore.Modules
{
    /// <summary>
    /// Interação lógica para AddView.xam
    /// </summary>
    public partial class AddView : UserControl
    {
        public AddView()
        {
            InitializeComponent();
        }

        private void PreviewTextInputPasswordSize(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(int.TryParse(e.Text, out int value) && value >= 0 && value <= 999);
        }
    }
}
