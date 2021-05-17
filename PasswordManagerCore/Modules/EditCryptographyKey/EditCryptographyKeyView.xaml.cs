using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PasswordManagerCore.Modules
{
    /// <summary>
    /// Lógica interna para EditCryptographyKeyView.xaml
    /// </summary>
    public partial class EditCryptographyKeyView : Window
    {
        public EditCryptographyKeyView()
        {
            InitializeComponent();
        }

        private bool isClosing = false;

        private void RestoreFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            if (isClosing)
            {
                return;
            }
            var VisualChildren = FindVisualChildren<FrameworkElement>(this);
            var ChildHasFocus = VisualChildren.FirstOrDefault(Visual => Visual == e.NewFocus);
            if (ChildHasFocus == null && e != null && e.NewFocus != this)
            {
                Keyboard.Focus(this);
                SystemSounds.Beep.Play();
            }
        }

        private void ClosingEvent(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isClosing = true;
        }

        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
