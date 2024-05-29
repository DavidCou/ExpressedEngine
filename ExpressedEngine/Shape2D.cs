using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressedEngine.ExpressedEngine
{
    /// <summary>
    /// Used to draw primitive 2D shapes 
    /// </summary>
    public class Shape2D
    {
        public Vector2 Postion = null;
        public Vector2 Scale = null;
        public string Tag = ""; // Used to id the shape if desired 

        public Shape2D(Vector2 postion, Vector2 scale, string tag)
        {
            this.Postion = postion;
            this.Scale = scale;
            this.Tag = tag;

            Log.Info($"[SHAPE2D]({tag}) - Has been regestered!");
            
            // Automatically registers the newly created shape
            ExpressedEngine.RegisterShape(this);
        }

        /// <summary>
        /// Deletes itself (said 2D shape) from the game
        /// Note: this currently does not actually remove the instance from memory
        /// it just removes the shape from the shape list
        /// </summary>
        public void DestroySelf() 
        {
            Log.Info($"[SHAPE2D]({Tag}) - Has been detroyed!");
            ExpressedEngine.UnregisterShape(this);
        }
    }
}
