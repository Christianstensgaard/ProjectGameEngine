using NinoGameEngine.ToolBox.Objects;
using NinoGameEngine.ToolBox.Utilitys.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoGame.DemoGame
{
    public class Pillar : Core
    {
        public Sprite sprite { get; set; }
        public Pillar(Sprite sprite , float start, float end, float speed)
        {
            this.Start = start;
            this.End = end;
            this.Speed = speed;
            this.sprite = sprite;

        }


        private void setup()
        {
            sprite.GetCurrentPosition().y = Start;
        }


        public bool GoUp { get; set; } = false;
        public float Start { get; set; }
        public float End { get; set; }
        public float Speed { get; set; }

        public override void OnClose()
        {
        }


        public override void OnUpdate()
        {
            if (GoUp)
            {
                sprite.AddToPosition(0, -Speed);
                if(sprite.GetCurrentPosition().y < Start) GoUp = false;
            }
            else
            {
                sprite.AddToPosition(0, Speed);
                if (sprite.GetCurrentPosition().y > End) GoUp = true;
            }
        }
    }
}
