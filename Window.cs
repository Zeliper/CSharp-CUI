using NStack;
using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace CUI.Controls
{
    class Window : Terminal.Gui.Window
    {
        public event EventHandler Initialize;
        protected virtual void OnInitialize(EventArgs e)
        {
            EventHandler handler = Initialize;
            handler?.Invoke(this, e);
        }
        public void Start()
        {
            Application.Init();
            var top = Application.Top;
            this.X = 0;
            this.Y = 0;
            this.Width = Dim.Fill();
            this.Height = Dim.Fill();
            this.ColorScheme = new ColorScheme()
            {
                Normal = Terminal.Gui.Attribute.Make(Color.White, Color.Black),
                Focus = Terminal.Gui.Attribute.Make(Color.White, Color.DarkGray),
            };
            this.Border = new Border()
            {
                BorderStyle = BorderStyle.None
            };
            Initialize?.Invoke(this,EventArgs.Empty);
            this.Add(new RadioGroup(3, 8, new ustring[] { "ds", "fs" }, 0));
            this.Add(new FrameView(new Rect(15, 5, 30, 10), "Group1"));


            top.Add(this);
            Application.Run();
        }
    }
}
