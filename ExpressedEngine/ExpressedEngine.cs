using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace ExpressedEngine.ExpressedEngine
{
    class Canvas : Form 
    {
        /// <summary>
        /// Prevents flickering when redrawing the game window
        /// </summary>
        public Canvas() 
        {
            // Not enabled by default
            this.DoubleBuffered = true;
        }
    }
    /// <summary>
    /// Sets up the game engine
    /// NOTE 1: By defualt windows.Drawing does not continuously refresh
    /// it only redraws when something major changes 
    /// e.g when the spacebar is pressed, then the entire window is redrawn
    /// NOTE 2: Windows forms is not designed to render games, 
    /// large amounts of game objects (ie 2000+) will cause performance issues
    /// </summary>
    public abstract class ExpressedEngine
    {
        // Sets up the default screen size and tile
        private Vector2 ScreenSize = new Vector2(512, 512);
        private string Title = "New Game";
        private Canvas Window = null;
        private Thread GameLoopThread = null;

        // Generates a new list of 2D shapes
       public static List<Shape2D> AllShapes = new List<Shape2D>();
        
        //Generates a new list of 2D sprites
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();

        public Color backgroundColour = Color.White;

        public Vector2 CameraPostion = Vector2.Zero();
        public float CameraAngle = 0f;

        /// <summary>
        /// Sets up the constructor 
        /// </summary>
        /// <param name="screenSize"> Size of the screen </param>
        /// <param name="title"> Tile of the game </param>
        public ExpressedEngine(Vector2 screenSize, string title) 
        {
            Log.Info("Game is starting...");
            this.ScreenSize = screenSize;
            this.Title = title;

            // Sets up the game window
            Window = new Canvas();
            // Size requires ints therefor must cast to int first
            Window.Size = new Size((int)this.ScreenSize.X, (int)this.ScreenSize.Y);
            Window.Text = this.Title;
            Window.Paint += Renderer; // By default it is called Windows_Paint, was renamed
            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUp;
            Window.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Window.FormClosing += Window_FormClosing;
            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }

        /// <summary>
        /// This is an attempt to fix the memory leak issue
        ///  it did not fix it :(
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameLoopThread.Abort();
        }

        /// <summary>
        /// Key up Event handler - for key releases
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }


        /// <summary>
        /// Key down event handler - for key presses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        /// <summary>
        /// Adds the newly created 2D shape to the list of 2D shapes
        /// </summary>
        /// <param name="shape"> The newly created 2D oject </param>
        public static void RegisterShape(Shape2D shape)
        {
            AllShapes.Add(shape);
        }

        /// <summary>
        /// Adds the newly created 2D sprite to the list of 2D sprites
        /// </summary>
        /// <param name="sprite"> The newly created 2D sprite </param>
        public static void RegisterSprite(Sprite2D sprite)
        {
            AllSprites.Add(sprite);
        }

        /// <summary>
        /// Removes the selected 2D shape from the list of 2D shapes
        /// </summary>
        /// <param name="shape"> The 2D object </param>
        public static void UnregisterShape(Shape2D shape)
        {
            AllShapes.Remove(shape);
        }

        /// <summary>
        /// Removes the selected 2D sprite from the list of 2D sprites
        /// </summary>
        /// <param name="sprite"> The 2D sprite </param>
        public static void UnregisterShape(Sprite2D sprite)
        {
            AllSprites.Remove(sprite);
        }

        /// <summary>
        /// Forces the game window to continuously refresh
        /// Note: An abstract Onclose method with a GameLoop.Abort function can be used to stop the refresh
        /// Useful for exiting the game - prevents the game loop from continuing 
        /// to run after the game window is closed, avoiding memory leaks 
        /// </summary>
        void GameLoop() 
        {
            OnLoad();
            while (GameLoopThread.IsAlive)
            {
                // Ensures the window is loaded in before running the game loop
                // (hackey way of doing it)
                try
                {
                    OnDraw();

                    // Because the GameLoopThread cannot be called in another thread
                    // (by default you cannot refresh the window from a different thread)
                    // BeginInvoke must be called to override windows defualt thread handling
                    // and force the window to be refreshed
                    // - forcing windows to refresh something it does not want to refresh
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();

                    // Adds a delay to allow time for the window to refresh
                    Thread.Sleep(1);
                }
                catch
                {
                    Log.Error("Game window has not been found...");
                }
            }
        }

        /// <summary>
        /// Sets up the skybox/background
        /// As well as renders in all 2D shapes and sprites to the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// NOTE: without the GameLoopThread, the renderer will only redraw itself when it needs to
        /// but can be forced to redraw if the game window is moved off screen (not ideal)
        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(backgroundColour); // The colour that is used to clear the screen aka the background colour

            // Sets up the renderer so it is inline with the camera
            g.TranslateTransform(CameraPostion.X, CameraPostion.Y);
            g.RotateTransform(CameraAngle);

            try
            {
                // Iterates through each shape and sprite and draws them in
                foreach (Shape2D  shape in AllShapes)
                {
                    g.FillRectangle(new SolidBrush(Color.Red), shape.Postion.X, shape.Postion.Y, shape.Scale.X, shape.Scale.Y);
                }

                foreach(Sprite2D sprite in AllSprites) 
                {
                    g.DrawImage(sprite.Sprite, sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y);
                }
            }
            catch (Exception)
            {

               
            }
            
        }

        // Loads game assets 
        public abstract void OnLoad();

        // Updates physics as well as movement
        public abstract void OnUpdate();
        
        // Adds/removes/updates assests (ie a rock, an explosion ect)
        public abstract void OnDraw();

        // Checks for key presses
        public abstract void GetKeyDown(KeyEventArgs e);
        
        // Checks for Key releases
        public abstract void GetKeyUp(KeyEventArgs e);
    }
}
