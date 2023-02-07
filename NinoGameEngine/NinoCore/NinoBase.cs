using NinoGameEngine.NinoCore;
using NinoGameEngine.NinoCore.Function;
using NinoGameEngine.NinoWindow;
using NinoGameEngine.ToolBox.Math;
using NinoGameEngine.ToolBox.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinoGameEngine
{
    public abstract  class NinoBase
    {

        private  Canvas mainWindow = null;
        private  Thread gameLoopThread = null;
        public static  WindowSettings WindowSettings { get; set; } = new WindowSettings();
        public static  Camera camera { get; set; } = new Camera();
        public static bool IsRunning { get; set; }
        /*-------------------------------------------------------------------------*/

        public abstract void Init();
        public  void  Start()
        {
            if(mainWindow == null)
            {
                //Setting up Window
                mainWindow = new Canvas();
                mainWindow.Size = new Size((int)WindowSettings.ScreenSize.x, (int)WindowSettings.ScreenSize.y);
                mainWindow.BackColor = WindowSettings.BackgroundColor;
                mainWindow.Text = WindowSettings.Title;

                //Setting up All Event Listeners -> Part of WindowFormLibary
                mainWindow.Paint += RenderEngine;
                mainWindow.KeyDown += KeyDownEvent;
                mainWindow.KeyUp += KeyUpEvent;
                mainWindow.FormClosing += OnCloseEvent;

                //Starting MainGameLoop
                gameLoopThread = new Thread(GameLoop);
                gameLoopThread.Start();
                gameLoopThread.Priority = ThreadPriority.AboveNormal;

                Application.Run(mainWindow);
            }

        }

        /*-------------------------------------------------------------------------*/
        /*                          Observers-Patten
        /*-------------------------------------------------------------------------*/
        private static  List<CoreInterface> Worker  = new List<CoreInterface>();
        private static  List<KeyInputInterface> WorkerKeyInput = new List<KeyInputInterface>();

        /*Det er nok en bedre ide at bruge C# indbygget Event Listener til dette formål, men tænkte
          at jeg lige selv vil prøve at lave noget som ligner Observer Design-patten.

        internal delegate void OnUpdateCore();
        internal static event OnUpdateCore onUpdateCore;
        */
        



        /*-------------------------------------------------------------------------*/
        //Events  -> Drives from Forms
        private  void OnCloseEvent(object? sender, FormClosingEventArgs e)
        {
            IsRunning = false;
        }
        private  void KeyUpEvent(object? sender, KeyEventArgs e)
        {
            foreach (KeyInputInterface KeyIn in WorkerKeyInput)
            {
                KeyIn.OnKeyUp(e);
            }
        }
        private  void KeyDownEvent(object? sender, KeyEventArgs e)
        {
            foreach (KeyInputInterface KeyIn in WorkerKeyInput)
            {
                KeyIn.OnKeyDown(e);
            }
        }

        /*-------------------------------------------------------------------------*/
        //STATIC
        //: Object Handler: \\
        internal static List<Sprite> RenderSprites = new List<Sprite>();
        internal static void AddObject(Sprite sprite)
        {
            RenderSprites.Add(sprite);
            RenderSprites.Sort((x, y) => x.RenderLayer.CompareTo(y.RenderLayer));
        }
        internal static void AddObjectBundle(Sprite[] sprites)
        {
            foreach (Sprite item in sprites)
            {
                AddObject(item);
            }
            RenderSprites.Sort((x, y) => x.RenderLayer.CompareTo(y.RenderLayer));
        }
        internal static void RemoveObjectt(Sprite sprite)
        {
            RenderSprites.Remove(sprite);
        }
        internal static Sprite? GetSprite(string Tag)
        {
            foreach (Sprite item in RenderSprites)
            {
                if(item.tag == Tag) return item;
            }
            return null;
        }
        internal static Sprite? GetSprite(int id)
        {
            foreach (Sprite item in RenderSprites)
            {
                if (item.id == id) return item;
            }
            return null;
        }
        internal static void ClearRender() { RenderSprites.Clear(); }

                        //: COLLISION :\\
        internal static  bool IsColliding(Vector2 position, Vector2 scale, string objTag)
        {
            if(RenderSprites.Count > 1)
            {
                foreach (Sprite item in RenderSprites)
                {
                    if(item.tag != objTag && item.RenderLayer != 0)
                    {
                        float length;
                        float width;
                        bool DoReturn = false;

                        //Calculating Length
                        if(position.x < item.GetCurrentPosition().x)
                        {
                            length = (item.GetCurrentPosition().x - position.x) + item.Scale.x;
                        }
                        else
                        {
                            length = (position.x - item.GetCurrentPosition().x) + scale.x;
                        }


                        if (length > (item.Scale.x + scale.x)) DoReturn = true;

                        if (!DoReturn)
                        {
                            //Calculation Width
                            if (position.y < item.GetCurrentPosition().y)
                            {
                                width = (item.GetCurrentPosition().y - position.y) + item.Scale.y;
                            }
                            else
                            {
                                width = (position.y - item.GetCurrentPosition().y) + scale.y;
                            }

                            //Calculating Collision
                            if (length < (item.Scale.x + scale.x) && width < (item.Scale.y + scale.y))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        internal static bool IsColliding(Vector2 position, Vector2 scale, int id)
        {
            if (RenderSprites.Count > 1)
            {
                foreach (Sprite item in RenderSprites)
                {
                    if (item.id == id)
                    {
                        float length;
                        float width;
                        bool DoReturn = false;

                        //Calculating Length
                        if (position.x < item.GetCurrentPosition().x)
                        {
                            length = (item.GetCurrentPosition().x - position.x) + item.Scale.x;
                        }
                        else
                        {
                            length = (position.x - item.GetCurrentPosition().x) + scale.x;
                        }

                        //> Bruger den til at stoppe hvis længde er for lang

                        if (length > (item.Scale.x + scale.x)) DoReturn = true; 

                        if (!DoReturn)
                        {

                            //Calculation Width
                            if (position.y < item.GetCurrentPosition().y)
                            {
                                width = (item.GetCurrentPosition().y - position.y) + item.Scale.y;
                            }
                            else
                            {
                                width = (position.y - item.GetCurrentPosition().y) + scale.y;
                            }

                            //Calculating Collision
                            if (length < (item.Scale.x + scale.x) && width < (item.Scale.y + scale.y))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        internal static bool IsCollidingRenderLayer(Vector2 position, Vector2 scale, int Renderlayer)
        {
            if (RenderSprites.Count > 1)
            {
                foreach (Sprite item in RenderSprites)
                {
                    if (item.RenderLayer == Renderlayer)
                    {
                        float length;
                        float width;
                        bool DoReturn = false;

                        //Calculating Length
                        if (position.x < item.GetCurrentPosition().x)
                        {
                            length = (item.GetCurrentPosition().x - position.x) + item.Scale.x;
                        }
                        else
                        {
                            length = (position.x - item.GetCurrentPosition().x) + scale.x;
                        }

                        //> Bruger den til at stoppe hvis længde er for lang

                        if (length > (item.Scale.x + scale.x)) DoReturn = true;

                        if (!DoReturn)
                        {

                            //Calculation Width
                            if (position.y < item.GetCurrentPosition().y)
                            {
                                width = (item.GetCurrentPosition().y - position.y) + item.Scale.y;
                            }
                            else
                            {
                                width = (position.y - item.GetCurrentPosition().y) + scale.y;
                            }

                            //Calculating Collision
                            if (length < (item.Scale.x + scale.x) && width < (item.Scale.y + scale.y))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        internal static Sprite? SpriteCollision(Vector2 position, Vector2 scale, int id)
        {
            if (RenderSprites.Count > 1)
            {
                foreach (Sprite item in RenderSprites)
                {
                    if (item.id == id && item.RenderLayer != 0)
                    {
                        float length;
                        float width;
                        bool DoReturn = false;

                        //Calculating Length
                        if (position.x < item.GetCurrentPosition().x)
                        {
                            length = (item.GetCurrentPosition().x - position.x) + item.Scale.x;
                        }
                        else
                        {
                            length = (position.x - item.GetCurrentPosition().x) + scale.x;
                        }

                        if (length > (item.Scale.x + scale.x)) DoReturn = true;

                        if (!DoReturn)
                        {
                            //Calculation Width
                            if (position.y < item.GetCurrentPosition().y)
                            {
                                width = (item.GetCurrentPosition().y - position.y) + item.Scale.y;
                            }
                            else
                            {
                                width = (position.y - item.GetCurrentPosition().y) + scale.y;
                            }

                            //Calculating Collision
                            if (length < (item.Scale.x + scale.x) && width < (item.Scale.y + scale.y))
                            {
                                return item;
                            }
                        }
                    }
                }
            }
            return null;
        }
        internal static Sprite? SpriteCollision(Vector2 position, Vector2 scale, string objTag)
        {
            if (RenderSprites.Count > 1)
            {
                foreach (Sprite item in RenderSprites)
                {
                    if (item.tag == objTag && item.RenderLayer != 0)
                    {
                        float length;
                        float width;
                        bool DoReturn = false;

                        //Calculating Length
                        if (position.x < item.GetCurrentPosition().x)
                        {
                            length = (item.GetCurrentPosition().x - position.x) + item.Scale.x;
                        }
                        else
                        {
                            length = (position.x - item.GetCurrentPosition().x) + scale.x;
                        }

                        if (length > (item.Scale.x + scale.x)) DoReturn = true;

                        if (!DoReturn)
                        {
                            //Calculation Width
                            if (position.y < item.GetCurrentPosition().y)
                            {
                                width = (item.GetCurrentPosition().y - position.y) + item.Scale.y;
                            }
                            else
                            {
                                width = (position.y - item.GetCurrentPosition().y) + scale.y;
                            }

                            //Calculating Collision
                            if (length < (item.Scale.x + scale.x) && width < (item.Scale.y + scale.y))
                            {
                                return item;
                            }
                        }
                    }
                }
            }
            return null; 
        }

        //: Small talk with gameloop :\\ -> Observer Design-patten
        internal static void AddTokGameLoop(CoreInterface gameInterface)
        {
            Worker.Add(gameInterface);
        }
        internal static void AddToKeyListener(KeyInputInterface keybordInputInterface)
        {
            WorkerKeyInput.Add(keybordInputInterface);
        }
        internal static bool RemoveFromGameLoop(CoreInterface gameInterface)
        {
            return Worker.Remove(gameInterface);
        }
        internal static bool RemoveFromKeyListener(KeyInputInterface keybordInputInterface)
        {
            return WorkerKeyInput.Remove(keybordInputInterface);
        }


        float LoopSleep = 14f; // -> Target Total Time for the loop, to hit 60-90 fps-ish.
        /// <summary>
        /// Main GameLoop
        /// </summary>
        private void GameLoop()
        {
            Init(); //> Runs As The first!
            IsRunning = true;
            while (IsRunning)
            {
                double timestart = System.DateTime.Now.Millisecond;
                try
                {
                    mainWindow.Invoke((MethodInvoker)delegate { mainWindow.Refresh(); });
                    foreach (CoreInterface worker in Worker)
                    {
                        worker.OnUpdate();
                    }
                    Physics();
                }
                catch (Exception e)
                {
                    IsRunning = false;
                    Console.WriteLine(e);
                }
                finally
                {
                    UpdateScreenSize();
                }

                double timeEnd = System.DateTime.Now.Millisecond;
                int sleep = (int)LoopSleep - ((int)timeEnd - (int)timestart);
                if (sleep < 1 || sleep > LoopSleep)
                {
                    Thread.Sleep((int)LoopSleep);           /* Denne del kan laves lidt bedre endnu. Hvis det tager længere tid end det det som er udregnet */
                }                                           /* Så skal det bare trækkes fra det næste også, så vi heletiden tager tiden der blev brugt før  */
                else                                        /* ind i vores udregning.                                                                       */
                {
                    Thread.Sleep(sleep);
                }
            }
            //Running alll Oberservers OnClose();
            foreach (CoreInterface worker in Worker)
            {
                worker.OnClose();
            }
        }
        private void UpdateScreenSize()
        {
            
            WindowSettings.ScreenSize.x = mainWindow.Width;
            WindowSettings.ScreenSize.y = mainWindow.Height;
        }
        private void RenderEngine(object? sender, PaintEventArgs e)
        {
            // Creating drawing tool
            Graphics graphics = e.Graphics;
            graphics.Clear(WindowSettings.BackgroundColor);

            //Setting up the camera Position, Rotation
            graphics.TranslateTransform(camera.position.x, camera.position.y);
            graphics.RotateTransform(camera.rotation);

            //-> Har haft problmer med at listen blev modifiseret under dette, derfor har jeg været tvunget til den If() statement
            if (IsRunning)
            {
                foreach (Sprite sprite in RenderSprites)
                {
                    if (sprite.GetSprite != null && RenderSprites.Count>0)
                    {
                        //Drawing sprite
                        graphics.DrawImage
                            (
                                sprite.GetSprite,
                                sprite.GetCurrentPosition().x,
                                sprite.GetCurrentPosition().y,
                                sprite.Scale.x,
                                sprite.Scale.y
                            );
                    }
                }
            }
            //Drawing Sprite to Screen -> having problems with this list gets change while this is running! 
        }
        private void Physics()
        {
            foreach (Sprite item in RenderSprites)
            {
                if(item.RenderLayer != 1 && item.RenderLayer != 0)
                {
                    if (!IsColliding(new Vector2(item.GetCurrentPosition().x, item.GetCurrentPosition().y + 1), item.Scale, item.tag))
                    {
                        item.physics.IsGrounded = false;
                        item.AddToPosition(0, item.physics.GetFallspeed());
                        item.AddToPosition(0, .2f);
                    }
                    else
                    {
                        item.AddToPosition(0, .2f);
                        item.physics.ResetCurretSpeed();
                    }
                }
            }
        }
    }
}
