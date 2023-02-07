using NinoGameEngine.ToolBox.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoGame
{
    public static class Data
    {
        //PLayer == Sprite.png
        //Ground == 512x512.png


       public static FileManager playerFile = new(@"C:\SharedOneDrive\OneDrive\Software\C#\ProjectNano\DemoGame\Assets\Sprite.png");
       public static FileManager groundFile = new(@"C:\SharedOneDrive\OneDrive\Software\C#\ProjectNano\DemoGame\Assets\512x512.png");
    }
}
