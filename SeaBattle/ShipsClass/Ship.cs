using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ShipsClass
{
    public class Ship
    {
        public List<Deck> Decks;
        public int DecksCount { get { return Decks.Count; } }
        public bool IsHorisontal;
        public Ship(List<Deck> decks)
        {
            Decks = decks;
            if (DecksCount >= 2)
            {
                IsHorisontal = (Decks[0].Coords.X == Decks[1].Coords.X - 1);
            }
            else IsHorisontal = true;
        }
        public Ship(Deck deck, int palubes = 1, bool isHorisontal = true)
        {
            IsHorisontal = isHorisontal;
            Decks = new List<Deck>();
            Decks.Add(deck);
            for (int i = 1; i < palubes; i++)
            {
                Decks.Add(
                    new Deck((isHorisontal == false ? 
                    deck.Coords.X : deck.Coords.X + i), 
                    (isHorisontal == true ? 
                    deck.Coords.Y : deck.Coords.Y + i))
                    );
            }
        }
    }

    public class Deck
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
