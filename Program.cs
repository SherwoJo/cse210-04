using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unit04.Game.Casting;
using Unit04.Game.Directing;
using Unit04.Game.Services;


namespace Unit04
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        private static int FRAME_RATE = 12;
        private static int MAX_X = 900;
        private static int MAX_Y = 600;
        public static int CELL_SIZE = 15;
        public static int FONT_SIZE = 15;
        public static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Greed";
        private static Color WHITE = new Color(255, 255, 255);
        public static int ASTERIX_CHAR = 42;
        public static int SQUARE_CHAR = 219;




        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create the cast
            Cast cast = new Cast();

            // create the banner
            Actor banner = new Actor();
            banner.Text = "Score: 0";
            banner.FontSize = FONT_SIZE;
            banner.Color = WHITE;
            //banner.Position = new Point(MAX_X / 2, MAX_Y / 2);
            banner.Position = new Point(CELL_SIZE, 0);
            cast.AddActor("banner", banner);

            // create the robot
            Actor robot = new Actor();
            robot.Text = "#";
            robot.FontSize = FONT_SIZE;
            robot.Color = WHITE;
            robot.Position = new Point(MAX_X / 2, MAX_Y - CELL_SIZE);
            cast.AddActor("robot", robot);

            // create the first artifact
            Random random = new Random();
            int type = random.Next(0, 2);
            string text = "";
            if (type == 0)
            {
                text = ((char)SQUARE_CHAR).ToString();
            }
            else if (type == 1)
            {
                text = ((char)ASTERIX_CHAR).ToString();
            }
            

            int x = random.Next(1, COLS);
            int y = 0;
            Point position = new Point(x, y);
            position = position.Scale(CELL_SIZE);

            Point velocity = new Point(0, CELL_SIZE);

            int r = random.Next(0, 256);
            int g = random.Next(0, 256);
            int b = random.Next(0, 256);
            Color color = new Color(r, g, b);

            Artifact artifact = new Artifact();
            artifact.Text = text;
            artifact.FontSize = FONT_SIZE;
            artifact.Color = color;
            artifact.Position = position;
            artifact.Velocity = velocity;
            artifact.Type = type;
            cast.AddActor("artifacts", artifact);

            // start the game
            KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            VideoService videoService 
                = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director(keyboardService, videoService);
            director.StartGame(cast);
        }
    }
}