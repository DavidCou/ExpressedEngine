using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressedEngine.ExpressedEngine
{
    /// <summary>
    /// Sets up a new Vector 2 class
    /// Handles all of the postioning
    /// </summary>
    public class Vector2 
    {
        public float X { get; set; }

        public float Y { get; set; }
        
        public Vector2()
        {
            X = Zero().X;
            Y = Zero().Y;
        }

        /// <summary>
        /// Sets the x and y coordinates
        /// </summary>
        /// <param name="x"> x coordinate </param>
        /// <param name="y"> y coordinate </param>
        public Vector2(float x, float y) 
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// returns X and Y as 0
        /// </summary>
        /// <returns> Vector2(0,0) </returns>
        public static Vector2 Zero()
        {
            return new Vector2(0,0); 
        }
    }
}
