using NinoGameEngine.NinoCore.Function;
using NinoGameEngine.ToolBox.IO;
using NinoGameEngine.ToolBox.Math;
using NinoGameEngine.ToolBox.Objects;
using NinoGameEngine.ToolBox.Utilitys;
using NinoGameEngine.ToolBox.Utilitys.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoGame.DemoGame
{
    public class Player : CoreKeyInput
    {
        FileManager filepath = Data.playerFile;
        /*Vores spil har også brug for en spiller og jeg har lavet en Super-Klasse som hurtig hælper med at 
          komme i gang Superklassen CoreKeyInput, Bruger begge vores interfaces til at samle funktionerne.   */
        public override void OnClose()
        {
            //* Her kan vi gemmer vores spillers posiotion, hvis det ønskes.
        }

        /* Player Sprite */
        //FileManager filepath = new FileManager(@"C:\SharedOneDrive\OneDrive\Software\C#\ProjectNano\DemoGame\Assets\Sprite.png");
        Vector2 scale = new Vector2(50, 50);
        Vector2 position = new Vector2(1, 800-115);
        Sprite player;
        /* Spilleren skal kun 3 ting. Frem, Tilbage og sprint. */
        float speed = 3f;
        float sprint;

        public Player()
        {
            //* Her Opretter vi vores Sprite. 
            sprint = speed * 2;
            player = new Sprite(filepath, position, scale, "Player", 3);

            //* Opsætning af Collision m.m. 
            player.CanCollide = true;
            player.physics.mass = 120;
            player.Show();
        }



        Vector2 Direction = Vector2.Zero();


        //* Indlæsning af vores Keydown 
        public override void OnKeyDown(KeyEventArgs key)
        {
            switch (key.KeyCode)
            {
                case Keys.A: Direction.x = -1; break;
                case Keys.Left: Direction.x = -1; break;

                case Keys.D: Direction.x = 1; break;
                case Keys.Right: Direction.x = 1; break;
            }


        }



        public override void OnKeyUp(KeyEventArgs key)
        {
            if (key.KeyCode == Keys.A || key.KeyCode == Keys.D) Direction.x = 0;
            if (key.KeyCode == Keys.Left || key.KeyCode == Keys.Right) Direction.x = 0;




        }

        public override void OnUpdate()
        {
            /*Der tages ingen højde for at du rammer ud af banen eller andre småting, men det kan hurtig laves.*/

            /*Hvis du Rammer enden af banen så har du vundet*/
            if (player.GetCurrentPosition().x >= 900) Console.WriteLine("Du Har Vundet");


            //* Laver collision ud fra spillerens nuværende Direction. kan nok laves lidt smarter. 
            bool DoReset = false;
            if (Direction.x > 0)  DoReset = Collision.IsCollidingRenderLayer(new Vector2(player.GetCurrentPosition().x + speed, player.GetCurrentPosition().y), player.Scale, 4);
            if (Direction.x < 0)  DoReset = Collision.IsCollidingRenderLayer(new Vector2(player.GetCurrentPosition().x - speed, player.GetCurrentPosition().y), player.Scale, 4);
            if (Direction.x == 0) DoReset = Collision.IsCollidingRenderLayer(new Vector2(player.GetCurrentPosition().x,         player.GetCurrentPosition().y), player.Scale, 4);
            
            
            /*Spillets regler er at hvis vi rammer en pillar har du tabt. så det tjekker vi om inden vi laver en handling*/
            if (DoReset) player.SetPosition(Vector2.Zero());


            switch (Direction.x)
            {
                case 1: player.AddToPosition(speed, 0); break;
                case -1: player.AddToPosition(-speed, 0); break;
            }


        }
    }
}
