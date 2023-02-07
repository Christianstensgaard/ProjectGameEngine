using NinoGameEngine.ToolBox.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinoGameEngine.NinoWindow
{
    public class Camera
    {
        public Camera()
        {
            position = Vector2.Zero();
            rotation = 0;
        }

        public Vector2 position { get; set; }
        public float rotation { get; set; }

    }
}
