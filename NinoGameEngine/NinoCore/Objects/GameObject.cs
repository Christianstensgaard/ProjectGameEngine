using NinoGameEngine.ToolBox.Math;
using NinoGameEngine.ToolBox.Objects;
using NinoGameEngine.ToolBox.Utilitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinoGameEngine.NinoCore.Objects
{
    public abstract class GameObject
    {
        protected GameObject(string tag, int renderLayer, Vector2 scale)
        {
            this.tag = tag;
            RenderLayer = renderLayer;
            CurrentPosition = Vector2.Zero();
            Scale = scale;
        }
        protected GameObject(Vector2 position, Vector2 scale, string tag, int renderlayer)
        {
            CurrentPosition = position;
            Scale = scale;
            this.tag = tag;
            RenderLayer = renderlayer;
        }

        public bool CanCollide { get; set; } = false;
        public physics physics { get; set; } = new physics();


        public int id { get; set; }
        public string tag { get; set; }
        public int RenderLayer { get; set; }


        private Vector2 CurrentPosition;
        public Vector2 Scale { get;}



        public void SetPosition(Vector2 position)
        {
            CurrentPosition = position;
        }
        public void SetPosition(float x, float y)
        {
            
            CurrentPosition.x = x;
            CurrentPosition.y = y;
        }
        public void AddToPosition(Vector2 position)
        {
            if (CanCollide)
            {
                if (!CalculateCollision(position))
                {
                    CurrentPosition.x += position.x;
                    CurrentPosition.y += position.y;
                }
            }
            else
            {
                CurrentPosition.x += position.x;
                CurrentPosition.y += position.y;
            }
            
        }
        public void AddToPosition(float x, float y)
        {
            if (CanCollide)
            {
                if(!CalculateCollision(new Vector2(CurrentPosition.x + x, CurrentPosition.y + y)))
                {
                    CurrentPosition.x += x;
                    CurrentPosition.y += y;
                }
            }
            else
            {
                CurrentPosition.x += x;
                CurrentPosition.y += y;
            }
        }


        /// <summary>
        /// Get the current position of the object
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCurrentPosition()
        { 
            return CurrentPosition; 
        }
        public abstract void OnDestroy();

        private bool CalculateCollision(Vector2 position)
        {
           return  Collision.IsColliding(position, Scale, tag);
        }
    }
}
