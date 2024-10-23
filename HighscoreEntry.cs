// Representerar en enskild highscore-post med spelarens namn och antal drag

namespace TowerOfHanoi
{
    public class HighscoreEntry
    {
        public string PlayerName { get; set; } // Spelarens namn
        public int Moves { get; set; } // Antal drag spelaren använt

        // Konstruktor som tar spelarens namn och antal drag och skapar en highscore-post
        public HighscoreEntry(string PlayerName, int Moves)
        {
            this.PlayerName = PlayerName;
            this.Moves = Moves;
        }
    }
}