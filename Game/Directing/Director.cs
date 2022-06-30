using System;
using System.Collections.Generic;
using Unit04;
using Unit04.Game.Casting;
using Unit04.Game.Services;


namespace Unit04.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        public int Score { get; set; } = 0;
        private static int CELL_SIZE = 15;
        private static int FONT_SIZE = 15;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static int ASTERIX_CHAR = 42;
        private static int SQUARE_CHAR = 219;
        
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                MakeArtifacts(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor robot = cast.GetFirstActor("robot");
            Point velocity = keyboardService.GetDirection();
            robot.Velocity = velocity;
        }

        /// <summary>
        /// Updates the robot's position and resolves any collisions with artifacts.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");
            Actor robot = cast.GetFirstActor("robot");
            List<Actor> artifacts = cast.GetActors("artifacts");

            banner.Text = $"Score: {Score}";
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            robot.MoveNext(maxX, maxY);

            foreach (Actor actor in artifacts)
            {
                actor.MoveNext(maxX, maxY);
                if (robot.Position.Equals(actor.Position))
                {
                    Artifact artifact = (Artifact) actor;
                    Score = Score + artifact.GetScore();
                    cast.RemoveActor("artifacts", actor);
                    // string message = artifact.GetMessage();
                    // banner.SetText(message);
                }

            } 
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();
        }

        public void MakeArtifacts(Cast cast)
        {
            List<Actor> artifacts = cast.GetActors("artifacts");

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
        }
    }
}