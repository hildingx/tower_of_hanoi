// Representerar en enskild disk i spelet, lagrar dess storlek och ansvarar för dess visuella representation.

namespace TowerOfHanoi
{
    class Disk
    {
        public int Size { get; } // Diskens storlek

        //Konstruktor för att skapa en disk med angiven storlek
        public Disk(int size)
        {
            Size = size;
        }

        //Returnera en visuell presentation av disken
        public override string ToString()
        {
            return new string('*', Size);
        }
    }
}