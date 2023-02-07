using NinoGameEngine.ToolBox.IO;
using NinoGameEngine.ToolBox.Math;
using NinoGameEngine.ToolBox.Objects;
using NinoGameEngine.ToolBox.Utilitys.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoGame.DemoGame
{
    public class Level
    {
        FileManager file = Data.groundFile;
        /* Ved hjælp af superklassen Core kan vi få fat i vores game loops funktioner */

        // OnClose(); -> Køres 1x gang når vinduet bliver lukket.
        // OnUpdate(); -> køres efter der er blevet opdateret et frame. 

        //-------------------------------------------------------------

        /* Jeg har forsøgt at gøre hele dette biblotek så nemt at bruge som muligt, derfor har jeg lavet nogle hjælpe klasse som arbejder sammen
         * med Enginen. Alle værktøjerne kan findes under Namespace: ToolBox */

        //-------------------------------------------------------------
        
        // -> En del af vores Toobox - til at placere objekter. 
        //FileManager file = new FileManager(@"C:\SharedOneDrive\OneDrive\Software\C#\ProjectNano\DemoGame\Assets\512x512.png");
        Sprite groundSprite;
        LevelDesignTool designTool;

        Sprite g1;
        Sprite g2;
        Sprite g3;
        Sprite g4;
        Sprite g5;


        public Level()
        {
            /*---------------------------------------------------------------------------------------------------------------------------------------
                                                                OPRETTELSE AF VORES LEVEL GROUND
            -----------------------------------------------------------------------------------------------------------------------------------------*/
            /* Der er forskellige måder levelet kan opbygges på. Det er muligt hvis at placere dem enkle vis, men jeg har lavet et designtool, som gør
               hele processen meget nemmere.*/

            //> Step 1 Opret Et Sprite
            groundSprite = new Sprite(file, new Vector2(100, 25), "Ground", 1);
            /* Tag: er bare et Tag, som bruges i forskellige sammenhæng, til Collision m.m. RenderLayer er i hvilken rækkefølge deet bliver renderet af enginen.
               Når der bruges LevelDesignTool, så vil renderlayer automatisk blive ændret til 1 som er den fortrækkende layer*/

            // Det skal siges at alle Objekter som er i layer 1 vil automatisk blive en del af Collision gruppen. hvis der ønskes at andre skal være det kan det kaldes direkte inde
            // i  groundSprite.CanCollide 

            //> Step 2 Opret level Layout
            int[,] level = new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0}, /* Vores level skal laves som et 2d array, her er det vigtig at kende sine sprites, når jeg skriver 1, betyder det faktisk at   */
                {0,0,0,0,0,0,0,0,0,0,0,0}, /* det er vores index 0, der skrives 1 fordi 0 bliver brugt som empty space. */
                {0,0,0,0,0,0,0,0,0,0,0,0},
                {1,1,1,1,1,1,1,1,1,1,1,1}
            };

            //> Step 3 Opret Sprite Array
            Sprite[] sprites = new Sprite[]
            {
                groundSprite
            };


            designTool = new LevelDesignTool(sprites, level);
            designTool.Load();

            /* Nu har vi lavet et level hvor bunden består af vores GroundSprite. En rimelig simple måde at oprette større levels på. */
            //NOTE: ønskes levelet fjernet kan funktionen Unload() kaldes.
            //NOTE: På nuværende tid kan man ikke klade de enkle sprites i gennem klassen.


            /*---------------------------------------------------------------------------------------------------------------------------------------
                                                                OPRETTELSE AF Andre OBJEKTS
            -----------------------------------------------------------------------------------------------------------------------------------------*/
            int spacing = 80;
            int renderlayer = 4;
            //->
            g1 = new Sprite(file, new Vector2(150, start), new Vector2(50, 50), "obj1", renderlayer);
            g2 = new Sprite(file, new Vector2(150+spacing*2+spacing, start), new Vector2(50, 50), "obj1", renderlayer);
            g3 = new Sprite(file, new Vector2(150+(spacing *4+ spacing), start), new Vector2(50, 50), "obj1", renderlayer);
            g4 = new Sprite(file, new Vector2(150+spacing*6+ spacing, start), new Vector2(50, 50), "obj1", renderlayer);
            g5 = new Sprite(file, new Vector2(150* spacing*8+ spacing, start), new Vector2(50, 50), "obj1", renderlayer);

            Pillar p1 = new Pillar(g1, start, end, speed);
            Pillar p2 = new Pillar(g2, start, end+10, speed);
            Pillar p3 = new Pillar(g3, start, end+50, speed);
            Pillar p4 = new Pillar(g4, start, end+25, speed);
            Pillar p5 = new Pillar(g5, start, end+10, speed);

            //->

            List<Pillar> pillars = new List<Pillar>();
            pillars.Add(p1);
            pillars.Add(p2);
            pillars.Add(p3);
            pillars.Add(p4);
            pillars.Add(p5);


            foreach (var item in pillars)
            {
                item.sprite.Show();
            }

        }
        float start = 500;
        float end = 750;
        float speed = 1.5f;

    }
}
