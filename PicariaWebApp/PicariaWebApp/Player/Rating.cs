using PicariaWebApp.Game;
using PicariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicariaWebApp.Player
{
    public class Rating
    {
        private int RateBoard(List<Position> BoardState)
        { //tylko dla liści! powinno działać, gdy mniej niż 3 pionki - poza pierwszym ruchem chyba i tylko gdy już po 3 pionki!
            if (BoardState.Count() == 9)
            {
                List<Position> computer = new List<Position>();
                List<Position> player = new List<Position>();

                for (int c = 0; c < BoardState.Count(); c++)
                {
                    if (BoardState[c].Status == Status.PlayerTwo) computer.Add(BoardState[c]);
                    if (BoardState[c].Status == Status.PlayerOne) player.Add(BoardState[c]);
                }

                //if przegrana - kolejność tych ifów z returnami bardzo ważna - mówi o hierarchii
                //podwójny if - żeby nie próbował odpalić tego drugiego warunku?
                if (player.Count() == 3) if (player[0].X - player[1].X == player[1].X - player[2].X &&
                                   player[0].Y - player[1].Y == player[1].Y - player[2].Y)
                    {
                        return -2;
                    }

                //if wygrana
                if (computer.Count() == 3) if (computer[0].X - computer[1].X == computer[1].X - computer[2].X &&
                                   computer[0].Y - computer[1].Y == computer[1].Y - computer[2].Y)
                    {
                        return 2;
                    }

                //if środek
                if (computer.Count > 0) if (BoardState[4].Status == Status.PlayerTwo) return 1;
            }
            return -1;//absolutnie każdy inny przypadek, np. brak środka
        }

        private void RateLast(GameTree Tree, int WhichFloorRated/*4*/)
        {
            if (WhichFloorRated == Tree.CurrentDepth)
            {
                //Tree.Rating = RateBoard(Tree.BoardState);//////////////////////////////////////////////
            }
            else
            {
                int HowMany = Tree.Children.Count();
                if (HowMany > 0)
                {
                    for (int c = 0; c < HowMany; c++)
                    {
                        RateLast(Tree.Children[c], WhichFloorRated);
                    }
                }
            }
        }

        private void RateSecond(GameTree Tree, int WhichFloorRated/*2*/)
        {//użyj PO RateLast i po podliczeniu ocen poszczególnych elementów
            if (WhichFloorRated == Tree.CurrentDepth)
            {
                List<Position> BoardState = Tree.BoardState;

                if (BoardState.Count() == 9)
                {
                    List<Position> computer = new List<Position>();


                    for (int c = 0; c < BoardState.Count(); c++)
                    {
                        if (BoardState[c].Status == Status.PlayerTwo) computer.Add(BoardState[c]);
                    }

                    //if wygrana
                    if (computer.Count() == 3) if (computer[0].X - computer[1].X == computer[1].X - computer[2].X &&
                                   computer[0].Y - computer[1].Y == computer[1].Y - computer[2].Y)
                        {
                            //Tree.Rating = 2;//////////////////////////////////////////////
                        }
                }
            }
            else
            {
                int HowMany = Tree.Children.Count();
                if (HowMany > 0)
                {
                    for (int c = 0; c < HowMany; c++)
                    {
                        RateLast(Tree.Children[c], WhichFloorRated);
                    }
                }
            }
        }

        private void RateAll(GameTree Tree)
        {       //oceń piętro 2 i 4, koniecznie osobne algorytmy
            RateLast(Tree, Tree.MaximumDepth);

            //TO DO
            //alfabeta dla przeniesienia ocen

            RateSecond(Tree, 2);//dla drugiego piętra
        }

    }
}
