/*
Konsolapplikation - Tornen i Hanoi
Av: Alexander Hilding
*/

// Hanterar spelets huvudflöde, inklusive menyval, spelstart och visning av highscore-listor.

using static System.Console;

namespace TowerOfHanoi
{
    class Program
    {
        static void Main()
        {
            // Visa huvudmenyn och hantera spelarens val
            ShowMainMenu();
        }
        
        static void ShowMainMenu()
        {
            // Rensar konsolen och visar huvudmenyn för spelet
            Clear();
            WriteLine("\n          Välkommen till spelet");
            WriteLine(@"
  _______                             _    _    _                   _ 
 |__   __|                           (_)  | |  | |                 (_)
    | | ___  _ __ _ __   ___ _ __     _   | |__| | __ _ _ __   ___  _ 
    | |/ _ \| '__| '_ \ / _ \ '_ \   | |  |  __  |/ _` | '_ \ / _ \| |
    | | (_) | |  | | | |  __/ | | |  | |  | |  | | (_| | | | | (_) | |
    |_|\___/|_|  |_| |_|\___|_| |_|  |_|  |_|  |_|\__,_|_| |_|\___/|_|
                                                                      
            ");

            // Visa menyalternativ och få inmatning från användare
            WriteLine("1. Starta spelet");
            WriteLine("2. Visa rankinglistorna");
            WriteLine("3. Avsluta");
            Write("Vänligen välj ditt alternativ: ");

            string? choice = ReadLine();

            // Verifiera att användaren valt giltigt alternativ
            while(choice != "1" && choice != "2" && choice != "3")
            {
                Write("Ogiltigt val. Försök igen: ");
                choice = ReadLine();
            }

            // Hantera valet och anropa motsvarande metod
            switch (choice)
            {
                case "1":
                    StartGame(); // Starta spelet
                    break;
                case "2":
                    ShowHighScoreMenu(); // Visa rankinglistor
                    break;
                case "3":
                    WriteLine("Spelet avslutas...");
                    Environment.Exit(0); // Avsluta programmet
                    break;
            }
        }

        static void StartGame()
        {
            // Rensa konsol och ta emot spelarnamn
            Clear();
            Write("Ange ditt namn: ");
            string? playerName = ReadLine();

            // Verifiera att namnet inte är tomt
            while (string.IsNullOrWhiteSpace(playerName))
            {
                WriteLine("Ogiltig inmatning. Ange ett giltigt namn: ");
                playerName = ReadLine();
            }

            // Ta emot antal diskar
            Write("Ange antal diskar (minimum 3, maximum 10): ");
            string? input = ReadLine();
            int numberOfDisks;

            // Parsa antal diskar till int, verifiera antal diskar (min 3, max 10)
            while (!int.TryParse(input, out numberOfDisks) || numberOfDisks < 3 || numberOfDisks > 10)
            {
                Write("Ogiltig inmatning. Ange ett giltigt antal skivor (minimum 3, maximum 10): ");
                input = ReadLine();
            }

            //Visa rankinglista för valt antal diskar
            HighscoreManager highscoreManager = new HighscoreManager();
            highscoreManager.ShowHighscores(numberOfDisks);

            WriteLine("\nTryck på valfri tangent för att fortsätta...");
            ReadKey();

            // Initiera spelet
            Game game = new Game(playerName, numberOfDisks, highscoreManager);
            game.Play();

            // När spel är klart, återvänd till huvudmeny
            WriteLine("\nTryck på valfri tangent för att återvända till huvudmenyn");
            ReadKey();
            ShowMainMenu();
        }

        static void ShowHighScoreMenu()
        {
            Clear();
            // Skapa ny instans av HighscoreManager för att visa highscore-listor
            HighscoreManager highscoreManager = new HighscoreManager();

            // Hämta unika antal diskar med sparade resultat
            List<int> uniqueDiskCounts = highscoreManager.GetUniqueDiskCounts();

            if (uniqueDiskCounts.Count == 0)
            {
                WriteLine("Det finns inga highscore-listor att visa");
            }
            else
            {
                // Visa rankinglistor för varje diskantal
                foreach (int diskCount in uniqueDiskCounts)
                {
                    highscoreManager.ShowHighscores(diskCount);
                }
            }

            WriteLine("\nTryck på valfri tangent för att återvända till huvudmenyn");
            ReadKey();
            ShowMainMenu();
        }
    }
}