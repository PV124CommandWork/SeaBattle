using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ShipsClass
{
    class Ship
    {
        public bool IsHorisontal;
        public List<Deck> Decks;
        public Ship(List<Deck> decks, bool isHorisontal)
        {
            IsHorisontal = isHorisontal;
            Decks = decks;
        }
    }

    class Deck
    {
        public Coords<int> Coords;
        public bool IsDamaged;
        public Deck(int x, int y)
        {
            IsDamaged = false;
            Coords.X = x;
            Coords.Y = y;
        }
    }

    public struct Coords<T>
    {
        public T X;
        public T Y;
    }
}
