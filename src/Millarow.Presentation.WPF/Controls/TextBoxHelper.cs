using System.Windows.Controls;

namespace Millarow.Presentation.WPF.Controls
{
    public static class TextBoxHelper
    {
        public static bool AddText(this TextBox self, string text)
        {
            if (self.IsReadOnly || !self.IsEnabled)
                return false;

            try
            {
                if (self.CaretIndex == self.Text.Length)
                {
                    self.AppendText(text);
                    self.CaretIndex = self.Text.Length;
                }
                else
                {
                    var index = self.CaretIndex;
                    var newText = self.Text.Remove(self.SelectionStart, self.SelectionLength).Insert(index, text);

                    self.SetCurrentValue(TextBox.TextProperty, newText);
                    self.CaretIndex = index + text.Length;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
