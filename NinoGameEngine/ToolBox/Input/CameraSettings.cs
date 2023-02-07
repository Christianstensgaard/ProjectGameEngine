using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinoGameEngine.ToolBox.InputTool
{
    public class CameraSettings
    {
        public float Position { get; set; }
        public float Rotation { get; set; }

        public bool MoveCameraWithPlayer { get; set; } = true;
        public bool PlayerIsCenter { get; set; } = true;
        public float CameraMargin { get; set; } = 0;




    }
}
