using System;
using System.Media;
using System.Windows;
using System.Windows.Input;

namespace PasswordManagerCore.Modules
{
    /// <summary>
    /// Lógica interna para GenericPopupView.xaml
    /// </summary>
    public partial class GenericPopupView : Window
    {
        public GenericPopupView()
        {
            InitializeComponent();
            IsClosing = false;
        }
        public bool IsClosing { get; set; }

        private void RestoreFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            if (e != null && e.NewFocus != this && !IsClosing)
            {
                Keyboard.Focus(this);
                SystemSounds.Beep.Play();
            }

        }

        private void StartClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsClosing = true;
        }
    }
}
