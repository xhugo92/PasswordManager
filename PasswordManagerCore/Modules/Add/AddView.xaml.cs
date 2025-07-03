using System.Windows;
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

        private void ValidadePastingInputPasswordSize(object sender, DataObjectPastingEventArgs e)
        {
            if(e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string pastedText = e.DataObject.GetData(DataFormats.Text) as string;

                if (!int.TryParse(pastedText, out int result) || result < 0 || result > 999)
                {
                    e.CancelCommand();
                    System.Media.SystemSounds.Beep.Play();
                }
            }
            else
            {
                e.CancelCommand();
                System.Media.SystemSounds.Beep.Play();
            }
        }
    }
}
