//Based on https://github.com/migueldeicaza/gui.cs
using System;
using Terminal.Gui;

namespace GUI
{
  public class TextBox : TextField
    {
        public int Length { get; set; }
        public TextBox(int x, int y, int w, int l, bool secret = false)
        {
            this.Secret = secret;
            this.X = x;
            this.Y = y;
            this.Width = w+1;
            this.Length = l;
            this.KeyPress += (View.KeyEventEventArgs obj) =>
            {
                bool check(KeyEvent keyEvent)
                {
                    bool handled = true;
                    if (this.Text.Length >= this.Length && this.SelectedStart == -1)
                        handled = true;
                    else
                    {
                        handled = false;
                    }
                    if (keyEvent.Key.HasFlag(Key.CtrlMask) || keyEvent.Key.HasFlag(Key.ShiftMask) || keyEvent.Key.HasFlag(Key.CursorDown) || keyEvent.Key.HasFlag(Key.CursorLeft) || keyEvent.Key.HasFlag(Key.CursorRight) || keyEvent.Key.HasFlag(Key.CursorUp) || keyEvent.Key.HasFlag(Key.Backspace))
                    {
                        handled = false;
                    }
                    if (keyEvent.Key.HasFlag(Key.A) && keyEvent.Key.HasFlag(Key.CtrlMask))
                    {
                        handled = true;
                        this.SelectedStart = 0;
                        this.CursorPosition = this.Text.Length;
                    }
                    return handled;
                }
                obj.Handled = check(obj.KeyEvent);
            };
        }
    }
}
