using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinoGameEngine.ToolBox.Math
{
    public class Vector2
    {
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float x { get; set; }
        public float y { get; set; }

        public static Vector2 Zero()
        {
            return new Vector2(0, 0);
        }


    }
}
