using NinoGameEngine.NinoCore.Objects;
using NinoGameEngine.ToolBox.IO;
using NinoGameEngine.ToolBox.Math;
using NinoGameEngine.ToolBox.Utilitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinoGameEngine.ToolBox.Objects
{
    public class Sprite : GameObject
    {
        public Bitmap GetSprite = null;
        private string filepath;
        private bool sucsess;

        public Sprite(FileManager file ,Vector2 scale, string tag, int renderLayer) : base(tag,renderLayer, scale)
        {
            filepath = file.Filepath;
            sucsess = Load();
        }
        public Sprite(FileManager file, Vector2 position, Vector2 scale, string tag, int renderlayer) : base(position, scale, tag, renderlayer)
        {
            filepath = file.Filepath;

            Console.WriteLine(filepath);
            sucsess = Load();
        }
        public Sprite(Sprite sprite) : base(sprite.GetCurrentPosition(),sprite.Scale,sprite.tag, sprite.RenderLayer)
        {
            filepath = sprite.filepath;
            sucsess = Load();


        }

        

        private bool Load()
        {
            try
            {
                Image tempimg = Image.FromFile(filepath);
                Bitmap sprite = new Bitmap(tempimg, (int)Scale.x, (int)Scale.y);
                GetSprite = sprite;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Render/Show Sprite 
        /// </summary>
        /// <returns></returns>
        public bool Show()
        {
            if (sucsess)
            {
                SpriteUtilitys.AddSprite(this);
                return true;
            }

            else return false;
        }
        /// <summary>
        /// Removeing object from the render.
        /// </summary>
        public override void OnDestroy()
        {
            SpriteUtilitys.RemoveSprite(this);
        }
    }
}
