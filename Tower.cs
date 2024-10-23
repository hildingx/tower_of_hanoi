// Representerar ett torn i spelet, ansvarar för att lagra diskar och hantera flytten av diskar mellan torn.

using static System.Console;

namespace TowerOfHanoi
{
    class Tower
    {
        private Stack<Disk> disks; // Lagra diskarna i tornet
        public string Name { get; } // Tornets namn

        // Initierar tornets namn och skapar tom stack för diskar
        public Tower(string name)
        {
            Name = name;
            disks = new Stack<Disk>();
        }

        // Lägg till en disk på högst upp på tornet
        public void AddDisk(Disk disk)
        {
            //Kontrollera om tornet har diskar och om den översta disken är mindre än den nya disken
            if(disks.Count > 0 && disks.Peek().Size < disk.Size)
            {
                throw new InvalidOperationException("Du kan inte placera en större disk på en mindre.");
            }
            disks.Push(disk); // Lägg till disken på toppen av stacken
        }

        // Ta bort och returnera den översta disken från tornet
        public Disk RemoveDisk()
        {
            // Kontrollera om tornet är tomt
            if (disks.Count == 0)
            {
                throw new InvalidOperationException("Det finns ingen disk att flytta.");
            }
            return disks.Pop(); // Ta bort den översta disken
        }

        // Kontrollera om det är giltigt att flytta en disk till ett annat torn
        public bool CanMoveDiskTo(Tower destination)
        {
            if (destination.IsEmpty()) // Om destinationstorn är tomt är flytt giltig
            {
                return true;
            }

            // Om destinationstornets översta disk är större än disk som ska flyttas är flytt giltig
            if (destination.disks.Peek().Size > disks.Peek().Size)
            {
                return true;
            }

            return false; // Annars är flytt inte giltig
        }

        // Visa diskarna i tornet
        public void DisplayTower(int numberOfDisks)
        {
            // Tornets namn
            WriteLine($"Torn {Name}:\n");

            int numberOfEmptySpaces = numberOfDisks - disks.Count;

            // Visa de tomma platserna överst i tornet
            for (int i = 0; i < numberOfEmptySpaces; i++)
            {
                WriteLine(new string(' ', numberOfDisks) + "|");  // Centrerad tom plats
            }

            // Visa diskarna i tornet med dubbla asterisker
            foreach (var disk in disks)
            {
                // Beräkna hur många mellanslag som behövs för att centrera disken
                int padding = numberOfDisks - disk.Size;
                string spaces = new string(' ', padding);  // Skapar mellanslag baserat på diskens storlek

                // Dubbla antal asterisker
                string doubledAsterisks = new string('*', disk.Size * 2);

                // Lägg till mellanslag för centrering
                string centeredDisk = spaces + doubledAsterisks;

                WriteLine(centeredDisk);
            }

            // Baslinje som justeras efter antalet diskar
            string baseLine = new string('¨', numberOfDisks * 2 + 2);
            WriteLine(baseLine);
        }

        // Metod för att kolla om ett torn är tomt
        public bool IsEmpty()
        {
            return disks.Count == 0;
        }

        // Returnera antalet diskar i tornet
        public int GetDiskCount()
        {
            return disks.Count;
        }
    }
}