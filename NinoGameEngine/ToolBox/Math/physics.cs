using NinoGameEngine.ToolBox.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinoGameEngine.ToolBox.Math
{
    /// <summary>
    /// Need more work...
    /// </summary>
    public class physics
    {
        public bool Use { get; set; } = true;
        public float mass { get; set; }
        public float currentSpeed { get; set; } = 0f;
        public bool IsJumping { get; set; }
        public float Gravity { get; set; } = 9.38f;
        public bool IsGrounded { get; set; }
        private float magicNumber = 1.14f;
        private float constDrag = .2f;


        public void ResetCurretSpeed()
        {
            currentSpeed = 0f;
        }

        public float GetFallspeed()
        {
        
            if (currentSpeed > ((mass / Gravity) / magicNumber))
            {
               return (mass / Gravity) / magicNumber;
            }
            else
            {
               currentSpeed += constDrag;
            }
            return currentSpeed;
        }



    }
}
