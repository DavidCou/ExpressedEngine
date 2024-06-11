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
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Directory = "";  // Used for the location of the sprite
        public string Tag = ""; // Used to id the sprite if desired
        public Bitmap Sprite = null;

        /// <summary>
        /// Creates a new sprite
        /// </summary>
        /// <param name="position"> The position of the sprite on screen</param>
        /// <param name="scale">Determines how lagre the sprite is</param>
        /// <param name="directory">Used to track the sprites location</param>
        /// <param name="tag">The sprites lable</param>
        public Sprite2D(Vector2 position, Vector2 scale, string directory, string tag)
        {
            this.Position = position;
            this.Scale = scale;
            this.Directory = directory;
            this.Tag = tag;

            Image temp = Image.FromFile($"Assets/Sprites/{directory}.png");

            // Creates a bitmap and set the sprit to the bit map
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
            if (a.Position.X <= b.Position.X + b.Scale.X && 
                a.Position.X + a.Position.X > b.Position.X &&
                a.Position.Y < b.Position.Y + b.Scale.Y &&
                a.Position.Y + a.Scale.Y > b.Position.Y) 
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Checks if the player is colliding with an abject based on the ojects tag
        /// </summary>
        /// <param name="tag">the selected objects tag</param>
        /// <returns>The sprite being checked</returns>
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
                    if (Position.X <= b.Position.X + b.Scale.X &&
                        Position.X + Scale.X > b.Position.X &&
                        Position.Y < b.Position.Y + b.Scale.Y &&
                        Position.Y + Scale.Y > b.Position.Y)
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
