using NinoGameEngine.ToolBox.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinoGameEngine.ToolBox.Utilitys
{
    public class SpriteUtilitys
    {
        public static void AddSprite(Sprite sprite)
        {
            NinoBase.AddObject(sprite);
        }
        public static void AddSpriteBundle(Sprite[] sprites)
        {
            NinoBase.AddObjectBundle(sprites);
        }
        public static void RemoveSprite(Sprite sprite)
        {
            NinoBase.RemoveObjectt(sprite);
        }
        public static void GetSprite(string tag)
        {
            NinoBase.GetSprite(tag);
        }
        public static void GetSprite(int id)
        {
            NinoBase.GetSprite(id);
        }
    }
}
