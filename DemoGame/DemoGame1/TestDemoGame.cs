using NinoGameEngine;
using NinoGameEngine.NinoWindow;
using NinoGameEngine.ToolBox.Math;
using NinoGameEngine.ToolBox.Utilitys.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoGame.DemoGame
{
    internal class TestDemoGame: NinoBase
    {


       // Opsætning af alle Window
       public TestDemoGame()
       {

            /*  For at åbne vores windue så skal vi  vi kalde vores start metode, som er en del
             *  af NinoBase super klassen. Iden vi gør det har vi mulighed for at ændre på nogle
                af indstillingerne. */
            WindowSettings.ScreenSize = new Vector2(900, 800);
            WindowSettings.Title = "Hej Flemming";
            //Der er flere, men ikke rigtig nødvendig.

            Start(); //> Klader Start, Som starter vores Loop og andre Engine ting.
       }



        /*Her har vi vores Init, denne funktion bliver kørt inden noget andet. Det er her vi skal
          fortælle vores engine hvilke objecter vi bruger i spillet */
        public override void Init()
        {
            /*Dette Demo spil har kun brug for en spiller og et level*/
            Level level = new Level();
            Player player = new Player();



        }
    }
}
