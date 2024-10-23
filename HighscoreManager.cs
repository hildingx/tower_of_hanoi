// Hanterar lagring, sortering och visning av highscore-listor, samt sparar dessa till en JSON-fil.


using System.Text.Json;
using static System.Console;

namespace TowerOfHanoi
{
    public class HighscoreManager
    {
        // Dictionary för att lagra highscore-listor per antal diskar
        private Dictionary<int, List<HighscoreEntry>> HighscoresByDisks;

        private string filePath = "highscore.json";

        // Ladda highscores från fil om fil existerar
        public HighscoreManager()
        {
            HighscoresByDisks = []; // Skapa ny instans av dictionary

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath); // Läs filens innehåll
                HighscoresByDisks = JsonSerializer.Deserialize<Dictionary<int, List<HighscoreEntry>>>(json) ?? []; // Deserialisera JSON-strängen till dictionary
            }
        }

        // Spara nytt highscore-resultat (om det är bland de fem bästa i listan)
        public void SaveHighscore(string playerName, int moves, int numberOfDisks)
        {
            var entry = new HighscoreEntry(playerName, moves); // Skapa nytt highscore-objekt

            // Om det inte finns en lista för det antal diskar, skapa en
            if (!HighscoresByDisks.ContainsKey(numberOfDisks))
            {
                HighscoresByDisks[numberOfDisks] = [];
            }

            // Lägg till resultatet för det specifika antal diskar
            HighscoresByDisks[numberOfDisks].Add(entry);

            // Sortera listan efter Moves och begränsa till de 5 bästa
            HighscoresByDisks[numberOfDisks] = HighscoresByDisks[numberOfDisks]
                .OrderBy(h => h.Moves)
                .Take(5)
                .ToList();
            
            Marshal(); // Spara uppdaterad lista till fil
        }

        // Visa highscore-lista för specifikt antal diskar
        public void ShowHighscores(int numberOfDisks)
        {
            WriteLine($"\nTopp 5 rankinglista för {numberOfDisks} diskar.");

            // Kontrollera om det finns resultat för angivet antal diskar
            if (HighscoresByDisks.ContainsKey(numberOfDisks) && HighscoresByDisks[numberOfDisks].Count > 0)
            {
                var filteredHighscores = HighscoresByDisks[numberOfDisks]; // Hämta lista för specifikt antal diskar

                // Skriv ut varje resultat i listan
                for (int i = 0; i < filteredHighscores.Count; i++)
                {
                    var entry = filteredHighscores[i];
                    WriteLine($"{i + 1}. {entry.PlayerName}: {entry.Moves} drag");
                }
            }
            else
            {
                WriteLine($"Ingen har klarat spelet med {numberOfDisks} diskar.");
            }
        }

        // Serialisera och spara listan till fil i JSON-format
        private void Marshal()
        {
            var jsonString = JsonSerializer.Serialize(HighscoresByDisks, new JsonSerializerOptions { WriteIndented = true }); // Serialisera listan till JSON-sträng, som indenteras med WriteIndented 
            File.WriteAllText(filePath, jsonString); // Skriv JSON-strängen till fil
        }

        // Hämta alla unika antal diskar som har highscore
        public List<int> GetUniqueDiskCounts()
        {
            return HighscoresByDisks.Keys.OrderBy(d => d).ToList();
        }
    }
}