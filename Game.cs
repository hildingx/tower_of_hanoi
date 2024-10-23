// Ansvarar för spelmekaniken, inklusive spelprocess, flytt av diskar och kontroll om spelet är vunnet.

using static System.Console;

namespace TowerOfHanoi
{
    class Game {
        private string playerName;

        // Antal diskar som valts att spela med
        private int numberOfDisks;

        // Torn-objekt som representerar de tre tornen i spelet
        private Tower tower1;
        private Tower tower2;
        private Tower tower3;

        // Antal drag spelaren gjort
        public int userMoves;

        // Referens till highscore-hanteraren för att spara resultat
        private HighscoreManager highscoreManager;

        // Initiera spelet med spelarens namn, antal diskar och highscore-hanteraren
        public Game(string playerName, int numberOfDisks, HighscoreManager highscoreManager)
        {
            this.playerName = playerName;
            this.numberOfDisks = numberOfDisks;
            this.highscoreManager = highscoreManager;
            userMoves = 0; // Sätt antal drag till 0 från start

            // Skapa tornen
            tower1 = new Tower("1");
            tower2 = new Tower("2");
            tower3 = new Tower("3");

            // Lägg till alla diskar i torn 1 vid start
            for (int i = numberOfDisks; i >= 1; i--)
            {
                tower1.AddDisk(new Disk(i));
            }
        }

        // Starta spelet
        public void Play()
        {
            while (!IsGameWon()) // Loop som körs så länge spelet inte är vunnet
            {
                DisplayTowers(); // Visa alla torn
                
                WriteLine("\nAnge vilket torn du vill flytta från och till.");

                // Hantera input för att flytta *från* ett torn
                Tower? from = null;
                while (from == null)
                {
                    Write("Flytta från torn: ");
                    string? fromInput = ReadLine();
                    
                    if (fromInput == "1" || fromInput == "2" || fromInput == "3")
                    {
                        from = GetTowerFromInput(fromInput); // Hämta rätt torn

                        if (from != null && from.IsEmpty()) // Kolla om tornet är tomt
                        {
                            WriteLine("\nDitt valda torn är tomt. Välj ett annat torn att flytta från.");
                            from = null; // Återställ input
                        }
                    }
                    else
                    {
                        WriteLine("\nOgiltig inmatning. Välj torn 1, 2 eller 3. Försök igen.");
                    }
                }

                // Hantera input för att flytta *till* ett torn
                Tower? to = null;
                while (to == null)
                {
                    Write("Flytta till torn: ");
                    string? toInput = ReadLine();

                    if (toInput == "1" || toInput == "2" || toInput == "3")
                    {
                        to = GetTowerFromInput(toInput); // Hämta rätt torn

                        if (to != null && !from.CanMoveDiskTo(to)) // Kolla om flytten är giltig
                        {
                            WriteLine("\nDu kan inte lägga en större disk på en mindre.");
                            to = null; // Återställ to och fråga om inmatning igen
                        }
                        if (from == to) // Kontrollera om spelaren försöker flytta disk till samma torn
                        {
                            WriteLine("\nDu måste flytta en disk från ett torn till ett annat");
                            to = null;
                        }
                    }
                    else
                    {
                        WriteLine("\nOgiltig inmatning. Välj torn 1, 2 eller 3. Försök igen.");
                    }
                }

                // Om flytten är giltig, genomför den
                to.AddDisk(from.RemoveDisk());
                userMoves++;
            }

            // När spelet är över
            SaveHighscore(); // Spara highscore

            // Beräkna optimala antalet drag för att jämföra med spelarens drag
            int optimalMoves = (int)Math.Pow(2, numberOfDisks) - 1;

            if (userMoves == optimalMoves)
            {
                WriteLine($"\nGrattis {playerName}, du har klarat spelet med det optimala antalet drag! ({userMoves} drag)");
            }
            else
            {
                WriteLine($"\nGrattis {playerName}, du har klarat spelet på {userMoves} drag.");
                WriteLine($"Med {numberOfDisks} diskar kan spelet klaras på {optimalMoves} drag.");
            }
        }
        
        // Spara spelarens resultat i highscore-listan
        private void SaveHighscore() {
            highscoreManager.SaveHighscore(playerName, userMoves, numberOfDisks);
        }

        // Kontrollera om spelet är vunnet
        private bool IsGameWon()
        {
            return tower3.GetDiskCount() == numberOfDisks; // Om tredje tornet har alla diskarna har spelaren vunnit
        }

        // Visa alla torn
        private void DisplayTowers()
        {
            Clear();
            tower1.DisplayTower(numberOfDisks);
            tower2.DisplayTower(numberOfDisks);
            tower3.DisplayTower(numberOfDisks);
        }

        // Omvandla spelarens input till rätt torn-objekt
        private Tower? GetTowerFromInput(string input)
        {
            switch (input)
            {
                case "1": return tower1;
                case "2": return tower2;
                case "3": return tower3;
                default: return null; //Ogiltig inmatning
            }
        }
    }
}