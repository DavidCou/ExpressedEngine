using ExpressedEngine.ExpressedEngine;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExpressedEngine
{
    /// <summary>
    /// Test game to test out game engine
    /// </summary>
    class DemoGame : ExpressedEngine.ExpressedEngine
    {
        //Shape2D player;
        Sprite2D player;
        //Sprite2D player2;
        bool left;
        bool right;
        bool up;
        bool down;

        Vector2 lastPos = Vector2.Zero();
        string[,] Map =
        {
            {"g", "g", "g", "g", "g", "g", "g"},
            {"g", ".", ".", ".", ".", ".", "g"},
            {"g", "c", "c", "c", "g", "c", "g"},
            {"g", "c", "g", "g", "g", "c", "g"},
            {"g", "c", "g", "c", "g", "c", "g"},
            {"g", "c", "g", "c", "c", "c", "g"},
            {"g", "g", "g", "g", "g", "g", "g"},
        }; 
        public DemoGame() : base(new Vector2(615, 515), "Expressed Engine Demo") { }

        public override void OnLoad()
        {
            backgroundColour = Color.Black;
            //player = new Shape2D(new Vector2(10, 10), new Vector2(10, 10), "Test");

            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {
                    if (Map[j, i] == "g")
                    {
                        new Sprite2D(new Vector2(i * 50, j * 50), new Vector2(50, 50), "Tiles/Blue tiles/tileBlue_03", "Ground");
                    }
                    
                    if (Map[j, i] == "c")
                    {
                        new Sprite2D(new Vector2(i * 50 + 15, j * 50 + 15), new Vector2(25, 25), "Items/yellowCrystal", "Coin");
                    }
                }
            }

            player = new Sprite2D(new Vector2(100, 100), new Vector2(30, 40), "Players/Player Green/playerGreen_walk1", "Player");
            //player2 = new Sprite2D(new Vector2(90, 30), new Vector2(30, 40), "Players/Player Green/playerGreen_walk1", "Player2");
        }

        public override void OnDraw()
        {

        }

        int times = 0;
        //int time = 0;
        //int frame = 0;
        //float x = 0.5f; 
        public override void OnUpdate()
        {
            //CameraPostion.X += .1f;
            //CameraPostion.Y += .1f;
            //CameraAngle += 2f;

            // Easiest work arount to fully remove the shape
            //if (time > 400)
            //{
            //    if (player != null)
            //    {
            //        player.DestroySelf();
            //        player = null;
            //    }
            //}
            //time++;

            //player.Postion.X += x;


            //Console.WriteLine($"frame Count: {frame}");
            //player.Postion.X += x;
            //frame++;

            if (up)
            {
                player.Position.Y -= 2f;
            }

            if (down)
            {
                player.Position.Y += 2f;
            }

            if (left)
            {
                player.Position.X -= 2f;
            }

            if (right)
            {
                player.Position.X += 2f;
            }


            Sprite2D coin = player.isColliding("Coin");
            if (coin != null)
            {
                coin.DestroySelf(); 
            }

                if (player.isColliding("Ground") != null)
            {
                //Log.Info($"COLLIDING! {times}");
                //times++;
                player.Position.X = lastPos.X;
                player.Position.Y = lastPos.Y;
            }
            else
            {
                lastPos.X = player.Position.X;
                lastPos.Y = player.Position.Y;
            }
        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W){ up = true; }
            if (e.KeyCode == Keys.S) { down = true; }
            if (e.KeyCode == Keys.A) { left = true; }
            if (e.KeyCode == Keys.D) { right = true; }
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { up = false; }
            if (e.KeyCode == Keys.S) { down = false; }
            if (e.KeyCode == Keys.A) { left = false; }
            if (e.KeyCode == Keys.D) { right = false; }
        }
    }
}
