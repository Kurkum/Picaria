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
        private int RateBoard(List<Position> boardState)//boardstate to teraz cała klasa
        { //powinno działać, gdy mniej niż 3 pionki - poza pierwszym ruchem chyba i tylko gdy już po 3 pionki!
            if (boardState.Count() == 9)
            {
                List<Position> computer = new List<Position>();
                List<Position> player = new List<Position>();

                for (int c = 0; c < boardState.Count(); c++)
                {
                    if (boardState[c].Status == Status.PlayerTwo)
                    {
                        computer.Add(boardState[c]);
                    }
                    if (boardState[c].Status == Status.PlayerOne)
                    {
                        player.Add(boardState[c]);
                    }
                }

                //if przegrana - kolejność tych ifów z returnami bardzo ważna - mówi o hierarchii
                //podwójny if - żeby nie próbował odpalić tego drugiego warunku?
                if (player.Count() == 3)
                {
                    if (player[0].X - player[1].X == player[1].X - player[2].X &&
                                   player[0].Y - player[1].Y == player[1].Y - player[2].Y)
                    {//warunek linii
                        return -2;
                    }
                }

                //if wygrana
                if (computer.Count() == 3)
                {
                    if (computer[0].X - computer[1].X == computer[1].X - computer[2].X &&
                                   computer[0].Y - computer[1].Y == computer[1].Y - computer[2].Y)
                    {
                        return 2;
                    }
                }

                //if środek
                if (computer.Count > 0)
                {
                    if (boardState[4].Status == Status.PlayerTwo) return 1;
                }
            }
            return -1;//absolutnie każdy inny przypadek, np. brak środka
        }

        private void RateLast(GameTree tree, int whichFloorRated/*4*/)
        {
            if (whichFloorRated == tree.CurrentDepth)
            {
                tree.Rate = RateBoard(tree.BoardState.Positions);
            }
            else
            {
                int HowMany = tree.Children.Count();
                if (HowMany > 0)
                {
                    for (int c = 0; c < HowMany; c++)
                    {
                        RateLast(tree.Children[c], whichFloorRated);
                    }
                }
            }
        }

        private void RateSecond(GameTree tree, int whichFloorRated/*2*/)
        {//użyj PO RateLast i po podliczeniu ocen poszczególnych elementów
            if (whichFloorRated == tree.CurrentDepth)
            {
                List<Position> BoardState = tree.BoardState.Positions;

                if (BoardState.Count() == 9)
                {
                    List<Position> computer = new List<Position>();

                    for (int c = 0; c < BoardState.Count(); c++)
                    {
                        if (BoardState[c].Status == Status.PlayerTwo) computer.Add(BoardState[c]);
                    }

                    //if wygrana
                    if (computer.Count() == 3)
                    {
                        if (computer[0].X - computer[1].X == computer[1].X - computer[2].X &&
                                   computer[0].Y - computer[1].Y == computer[1].Y - computer[2].Y)
                        {
                            tree.Rate = 2;
                        }
                    }
                }
            }
            else
            {
                int HowMany = tree.Children.Count();
                if (HowMany > 0)
                {
                    for (int c = 0; c < HowMany; c++)
                    {
                        RateLast(tree.Children[c], whichFloorRated);
                    }
                }
            }
        }



        public void RateAll(GameTree tree)
        {//oceń piętro 2 i 4, koniecznie osobne algorytmy
            RateLast(tree, tree.MaximumDepth);

            //TO DO
            //alfabeta dla przeniesienia ocen
            //funkcja zwracająca najlepszy ruch - jako List<Position> Positions

            RateSecond(tree, 2);//dla drugiego piętra
        }

    }
}
