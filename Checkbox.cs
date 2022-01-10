using System;
using Terminal.Gui;
namespace GUI
{
  public class CheckBox : Label
    {
        public bool Checked { get; set; }
        public string orgText { get; set; }
        private string getText()
        {
            if (Checked)
            {
                return "■  " + orgText;
            }
            else
            {
                return "□  " + orgText;
            }
        }
        private void setText(string t)
        {
            orgText = t;
            this.Text = getText();
        }
        public CheckBox(int x, int y, string text,bool isEnabled, bool isChecked = false)
        {
            this.X = x;
            this.Y = y;
            this.Checked = isChecked;
            this.Enabled = isEnabled;
            this.MouseClick += (MouseEventArgs obj) =>
            {
                if (obj.MouseEvent.Flags.HasFlag(MouseFlags.Button1Released))
                {
                    this.Checked = !this.Checked;
                    this.Text = getText();
                }
            };
            setText(text);
        }
    }
}
