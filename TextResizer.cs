using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CityTransport
{
    internal class TextResizer
    {
        Window window;

        public TextResizer(Window window)
        {
            this.window = window;
            window.SizeChanged += Window_SizeChanged;

            var NewTextSize = window.Height / 100.0 + window.Width / 40.0;
            if (NewTextSize > 48) NewTextSize = 48;

            resize(NewTextSize);
        }

        private void resize(double NewTextSize)
        {
            var buttons = FindVisualChildren<Button>(window);
            var textboxes = FindVisualChildren<TextBox>(window);
            var textBlocks = FindVisualChildren<TextBlock>(window);
            var passwordBoxes = FindVisualChildren<PasswordBox>(window);
            var richTextBoxes = FindVisualChildren<RichTextBox>(window);

            foreach (var button in buttons)
            {
                button.FontSize = NewTextSize;
            }

            foreach (var textbox in textboxes)
            {
                textbox.FontSize = NewTextSize / 1.5;
            }

            foreach (var textblock in textBlocks)
            {
                textblock.FontSize = NewTextSize / 2;
            }

            foreach (var passwordBox in passwordBoxes)
            {
                passwordBox.FontSize = NewTextSize / 1.5;
            }

            foreach (var richTextBox in richTextBoxes)
            {
                richTextBox.FontSize = NewTextSize;
            }
        }

        public void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {     
            var NewTextSize = e.NewSize.Height / 50.0 + e.NewSize.Width / 30.0;
            if (NewTextSize > 48) NewTextSize = 48;

            resize(NewTextSize);
        }

        private IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T nestedChild in FindVisualChildren<T>(child))
                    {
                        yield return nestedChild;
                    }
                }
            }
        }


    }
}
