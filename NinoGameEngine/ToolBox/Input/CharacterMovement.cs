using NinoGameEngine.ToolBox.Math;
using NinoGameEngine.ToolBox.Objects;
using NinoGameEngine.ToolBox.Utilitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinoGameEngine.ToolBox.InputTool
{
    public abstract class CharacterMovement
    {
        /*NOTE Denne klasse virker ikke lige nu, pga. ændring i Game-Engine Kernen. Måske vil jeg tage nogle ting fra denne klasse
          til at bygge noget nyt. Denne klasse bliver nok kasseret.*/
        public bool NewPlayer(float speed, Sprite sprite)
        {
            
            if(sprite != null) return true;
            return false;
        }

        protected enum DoDirection { Left, Right, Up, Down }
        protected enum DoAction { Do, Not }
        protected void Move(DoDirection direction, DoAction action)
        {
            switch (action)
            {
                case DoAction.Do:
                    switch (direction)
                    {
                        case DoDirection.Left:
                            Direction.x = -1;
                            break;
                        case DoDirection.Right:
                            Direction.x = 1;
                            break;
                        case DoDirection.Up:
                            Direction.y = -1;
                            break;
                        case DoDirection.Down:
                            Direction.y = +1;
                            break;
                    }
                    break;

                case DoAction.Not:
                    switch (direction)
                    {
                        case DoDirection.Left:
                            Direction.x = 0;
                            break;
                        case DoDirection.Right:
                            Direction.x = 0;
                            break;
                        case DoDirection.Up:
                            Direction.y = 0;
                            break;
                        case DoDirection.Down:
                            Direction.y = 0;
                            break;
                    }
                    break;
            }
        }
        private Vector2 Direction { get; set; } = Vector2.Zero();
        public Sprite sprite { get; set; }
        protected CameraSettings cameraSettings {get; set;} = new CameraSettings();
        public float JumpHeight = 50;
        public float Speed { get; set; }
        bool Isjumping = false;
        //Used for timing, with the GameLoop OnUpdate().
        private void UpdateClock()
        {
            if (sprite != null)
            {
               
                if (Isjumping)
                {

                }

                //> Moving the Player obj
                if (Direction.x == 1 ) sprite.AddToPosition( Speed, 0 );
                if (Direction.x == -1) sprite.AddToPosition( -Speed, 0 );
                if (Direction.y == 1 ) sprite.AddToPosition(0, Speed);
                if (Direction.y == -1) sprite.AddToPosition(0, -Speed);


                //> Moving the camera as commanded
                if (cameraSettings.MoveCameraWithPlayer)
                {
                    if (cameraSettings.PlayerIsCenter)
                    {
                        NinoBase.camera.position = new Vector2( ((NinoBase.WindowSettings.ScreenSize.x/2 )-(sprite.GetCurrentPosition().x + (sprite.Scale.x /2)))+ cameraSettings.CameraMargin, 0);
                    }
                    else NinoBase.camera.position = new Vector2(-sprite.GetCurrentPosition().x + cameraSettings.CameraMargin, 0);
                }

                //> Passive Downforce, to make sure the player is at the ground.
                if (!sprite.physics.IsGrounded) sprite.AddToPosition(0, 1.15f);
            }
        }
    }
}
