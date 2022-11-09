using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ShipsClass
{
    public abstract class Cell
    {
        public Coords<int> Coords;
    }
    public class Shoot
        : Cell
    {
        public int ReturnedValue;
        Shoot(int x, int y)
        {
            Coords.X = x;
            Coords.Y = y;
        }
        /// <summary>
        /// Returns -1 when missed, 0 when damaged but still alive, 1 when successfully destroyed.
        /// </summary>
        public int Damage(ref List<Ship> ships)
        {
            for (int i = 0; i < ships.Count; i++)
            {
                for (int j = 0; j < ships[i].DecksCount; j++)
                {
                    if (ships[i].Decks[j].Coords.X == Coords.X && ships[i].Decks[j].Coords.Y == Coords.Y)
                    {
                        ships[i].Decks[j].IsDamaged = true;
                        foreach (var deck in ships[i].Decks)
                        {
                            if (!deck.IsDamaged)
                            {
                                ReturnedValue = 0;
                                return 0;//If damaged but NOT detsroyed
                            }
                        }
                        ReturnedValue = 1;
                        return 1;//If destroyed
                    }
                }
            }
            ReturnedValue = -1;
            return -1;//If miss
        }
    }
    public class Ship
    {
        public List<Deck> Decks;
        public int DecksCount { get { return Decks.Count; } }
        public bool IsHorisontal;
        public Ship() { }
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
        : Cell
    {
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
