using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ExpressedEngine.ExpressedEngine
{
    public class Sprite2D
    {
        public Vector2 Postion = null;
        public Vector2 Scale = null;
        public string Directory = "";  // Used for the location of the sprite
        public string Tag = ""; // Used to id the sprite if desired
        public Bitmap Sprite = null;

        public Sprite2D(Vector2 postion, Vector2 scale, string directory, string tag)
        {
            this.Postion = postion;
            this.Scale = scale;
            this.Directory = directory;
            this.Tag = tag;

            Image temp = Image.FromFile($"Assets/Sprites/{directory}.png");

            // Look more into what this does
            //Sets up the sprite
            Bitmap sprite = new Bitmap(temp);
            Sprite = sprite;

            Log.Info($"[SPRITE2D]({tag}) - Has been regestered!");

            // Automatically registers the newly created sprite
            ExpressedEngine.RegisterSprite(this);
        }

        /// <summary>
        /// Check if the two selected sprites are colliding 
        /// </summary>
        /// <param name="a"> The first sprite</param>
        /// <param name="b">the second sprite </param>
        /// <returns>true if the two sprites are collibing otherwise false</returns>
        public bool isColliding(Sprite2D a, Sprite2D b)
        {
            if (a.Postion.X <= b.Postion.X + b.Scale.X && 
                a.Postion.X + a.Postion.X > b.Postion.X &&
                a.Postion.Y < b.Postion.Y + b.Scale.Y &&
                a.Postion.Y + a.Scale.Y > b.Postion.Y) 
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Checks if the player is colliding with an abject based on the ojects tag
        /// </summary>
        /// <param name="tag">the selected objects tag</param>
        /// <returns></returns>
        public Sprite2D isColliding(string tag)
        {
            /*if (a.Postion.X <= b.Postion.X + b.Scale.X &&
                a.Postion.X + a.Postion.X > b.Postion.X &&
                a.Postion.Y < b.Postion.Y + b.Scale.Y &&
                a.Postion.Y + a.Scale.Y > b.Postion.Y)
            {
                return true;
            }*/
            foreach (Sprite2D b in ExpressedEngine.AllSprites)
            {
                if (b.Tag == tag)
                {
                    if (Postion.X <= b.Postion.X + b.Scale.X &&
                        Postion.X + Scale.X > b.Postion.X &&
                        Postion.Y < b.Postion.Y + b.Scale.Y &&
                        Postion.Y + Scale.Y > b.Postion.Y)
                        {
                            return b;
                        }
                }
            }

            return null;
        }

        /// <summary>
        /// Deletes itself (said 2D shape) from the game
        /// Note: this currently does not actually remove the instance from memory
        /// it just removes the shape from the shape list
        /// </summary>
        public void DestroySelf()
        {
            ExpressedEngine.UnregisterShape(this);
        }
    }
}
