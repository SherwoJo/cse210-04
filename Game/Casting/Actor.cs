using System;


namespace Unit04.Game.Casting
{
    /// <summary>
    /// <para>A thing that participates in the game.</para>
    /// <para>
    /// The responsibility of Actor is to keep track of its appearance, position and velocity in 2d 
    /// space.
    /// </para>
    /// </summary>
    public class Actor
    {
        public string Text { get; set; } = "";
        public int FontSize { get; set; } = 15;
        public Color Color { get; set; } = new Color(255, 255, 255); // white
        public Point Position { get; set; } = new Point(0, 0);
        public Point Velocity { get; set; } = new Point(0, 0);

        /// <summary>
        /// Constructs a new instance of Actor.
        /// </summary>
        public Actor()
        {
        }

        /// <summary>
        /// Moves the actor to its next position according to its velocity. Will wrap the position 
        /// from one side of the screen to the other when it reaches the maximum x and y 
        /// values.
        /// </summary>
        /// <param name="maxX">The maximum x value.</param>
        /// <param name="maxY">The maximum y value.</param>
        public void MoveNext(int maxX, int maxY)
        {
            int x = ((Position.GetX() + Velocity.GetX()) + maxX) % maxX;
            int y = ((Position.GetY() + Velocity.GetY()));
            Position = new Point(x, y);
        }

    }
}