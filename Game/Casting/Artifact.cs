namespace Unit04.Game.Casting
{
    /// <summary>
    /// <para>An item of cultural or historical interest.</para>
    /// <para>
    /// The responsibility of an Artifact is to provide a message about itself.
    /// </para>
    /// </summary>
    public class Artifact : Actor
    {
        public int Type { get; set; }

        public int GetScore()
        {
            int score = 0;
            if (Type == 0)
            {
                score = -1; 
            }
            else if (Type == 1)
            {
                score = 1;
            }

            return score;
        }
    }
}