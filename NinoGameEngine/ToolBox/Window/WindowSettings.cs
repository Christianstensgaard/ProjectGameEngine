using NinoGameEngine.ToolBox.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinoGameEngine.NinoWindow
{
    public class WindowSettings
    {
        public Vector2 ScreenSize { get; set; }
        public string Title { get; set; }
        public Color BackgroundColor { get; set; }



        public bool CanResize { get; set; } = true;





        public WindowSettings(Vector2 screenSize, string title, Color backgroundColor)
        {
            ScreenSize = screenSize;
            Title = title;
            BackgroundColor = backgroundColor;
        }
        public WindowSettings()
        {
            this.ScreenSize = new Vector2(525, 525);
            this.Title = "New Game";
            this.BackgroundColor = Color.Black;
        }

    }
}
