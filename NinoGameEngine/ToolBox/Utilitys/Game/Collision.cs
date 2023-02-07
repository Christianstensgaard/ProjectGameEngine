using NinoGameEngine.ToolBox.Math;
using NinoGameEngine.ToolBox.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinoGameEngine.ToolBox.Utilitys
{
    public static class Collision
    {
        public static bool IsColliding(Vector2 position, Vector2 scale, string objTag)
        {
            return NinoBase.IsColliding(position, scale, objTag);
        }
        public static bool IsColliding(Vector2 position, Vector2 scale, int id)
        {
            return NinoBase.IsColliding(position,scale,id);
        }
        public static bool IsCollidingRenderLayer(Vector2 position, Vector2 scale, int Renderlayer)
        {
            return NinoBase.IsCollidingRenderLayer(position,scale,Renderlayer);
        }
        public static Sprite? SpriteCollision(Vector2 position, Vector2 scale, int id)
        {
            return NinoBase.SpriteCollision(position,scale,id);
        }
        public static Sprite? SpriteCollision(Vector2 position, Vector2 scale, string objTag)
        {
            return NinoBase.SpriteCollision(position, scale,objTag);
        }
    }
}
