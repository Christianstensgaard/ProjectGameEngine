using NinoGameEngine.ToolBox.Math;
using NinoGameEngine.ToolBox.Utilitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinoGameEngine.ToolBox.Objects
{
    /// <summary>
    /// Create eazy a level with this tool. Guide can be found here: 
    /// https://1drv.ms/w/s!Ai46rLK22WN5gawMIaO2wX5XsHu-9A?e=ge5Fs9
    /// </summary>
    public class LevelDesignTool
    {
        public LevelDesignTool(Sprite[] spriteBundle, int[,] LevelLayout)
        {
            this.spriteBundle = spriteBundle;
            this.levelLayout = LevelLayout;
            height = spriteBundle[0].Scale.y;
            spriteHeight = spriteBundle[0].Scale.y;
            spriteWidth = spriteBundle[0].Scale.x;
            height = NinoBase.WindowSettings.ScreenSize.y - spriteBundle[0].Scale.y*2.65f;
            //> Making Sure its the right render level.
            foreach (Sprite item in spriteBundle)
            {
                item.RenderLayer = 1;
                item.CanCollide = true;
            }
        }

        private Sprite[] spriteBundle;
        private int[,] levelLayout;
        private float horizontalSpacing = -13f;
        private float verticalSpacing;

        private float spriteHeight;
        private float spriteWidth;
        private float width = 0;
        private float height = 0;

        private void OnLoad()
        {
            for (int y = levelLayout.GetUpperBound(0); y >= 0; y--)
            {
                for (int x = 0; x <= levelLayout.GetUpperBound(1); x++)
                {
                    //-> Regel : hvis vores int = 0, betyder det at der intet skal være.
                    //-> Starter med at tjekke efter menneskelige fejl. 
                    if (levelLayout[y, x] != 0 && levelLayout[y, x] < spriteBundle.Length + 1)
                    {
                        // -> Oprette en kopi af vores sprite...
                        Sprite newSprite = new Sprite(spriteBundle[levelLayout[y, x] - 1]);
                        // -> Udregning af vores position
                        newSprite.SetPosition(new Vector2(width + horizontalSpacing, y + height));
                        //-> Udregner vores næste bredde
                        width += newSprite.Scale.x + horizontalSpacing;
                        // -> Visning af vores sprite -> Send til Render
                        newSprite.Show();
                    }
                    else width += spriteWidth;
                }
                width = 0; //Resetting Width
                height -= spriteHeight + verticalSpacing;
            }
        }


        public float GetGround()
        {
            return height;
        }

        /// <summary>
        /// Send all Sprites to Render
        /// </summary>
        /// <returns>Bool, sucsessesrate</returns>
        public bool Load()
        {
            OnLoad();
            return false;
        }

        /// <summary>
        /// Removing sprites from render
        /// </summary>
        /// <returns></returns>
        public bool UnLoad()
        {
            foreach (Sprite sprite in spriteBundle)
            {
                sprite.OnDestroy();
            }
            return true;
        }





    }
}
