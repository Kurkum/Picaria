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
        public int RateBoard(Board board)
        {  
            List<Position> positions = board.Positions;
            if (positions.Count() == 9)
            {
                List<Position> computer = new List<Position>();
                List<Position> player = new List<Position>();

                for (int c = 0; c < positions.Count(); c++)
                {
                    if (positions[c].Status == Status.PlayerTwo)
                    {
                        computer.Add(positions[c]);
                    }
                    if (positions[c].Status == Status.PlayerOne)
                    {
                        player.Add(positions[c]);
                    }
                }

                if (player.Count() == 3)
                {
                    if (player[0].X - player[1].X == player[1].X - player[2].X &&
                                   player[0].Y - player[1].Y == player[1].Y - player[2].Y)  
                    {
                        return -50;
                    }
                }

                if (computer.Count() == 3)
                {
                    if (computer[0].X - computer[1].X == computer[1].X - computer[2].X &&
                                   computer[0].Y - computer[1].Y == computer[1].Y - computer[2].Y)  
                    {
                        return 50;
                    }
                }

                int anotherResult = 0;

                for (int c = 0; c < positions.Count(); c++)
                {
                    if (c == 0 || c == 2 || c == 6 || c == 8)
                    {
                        if (positions[c].Status == Status.PlayerTwo)
                        {
                            anotherResult += 3;
                        }
                        if (positions[c].Status == Status.PlayerOne)
                        {
                            anotherResult -= 3;
                        }
                    }
                    if (c == 1 || c == 3 || c == 5 || c == 7)
                    {
                        if (positions[c].Status == Status.PlayerTwo)
                        {
                            anotherResult += 5;
                        }
                        if (positions[c].Status == Status.PlayerOne)
                        {
                            anotherResult -= 5;
                        }
                    }
                    if (c == 4)
                    {
                        if (positions[c].Status == Status.PlayerTwo)
                        {
                            anotherResult += 8;
                        }
                        if (positions[c].Status == Status.PlayerOne)
                        {
                            anotherResult -= 8;
                        }
                    }
                }
                return anotherResult;

            }
            return -90;  // błąd -> podana tablica jest za mała. Najniższa ocena - należy unikać używania takiej tablicy.
        }


        public void MiniMaks(GameTree tree)
        {
            int howMany = 0;
            if (!(tree.Children is null)) 
            {
                howMany = tree.Children.Count();
            }                                   
            if (howMany > 0)
            {
                if (tree.CurrentDepth % 2 == 1 && RateBoard(tree.BoardState) == 50 && tree.CurrentDepth != 0)
                {
                    tree.Rate = 50;
                    tree.Children.Clear();  
                }
                else if (tree.CurrentDepth % 2 == 0 && RateBoard(tree.BoardState) == -50 && tree.CurrentDepth != 0)
                {
                    tree.Rate = -50;
                    tree.Children.Clear(); 
                } 

                else
                {
                    for (int c = 0; c < howMany; c++)
                    {
                        MiniMaks(tree.Children[c]);
                    }

                    if (tree.CurrentDepth % 2 == 0)
                    {
                        int newRate = -50;

                        for (int c = 0; c < tree.Children.Count(); c++)
                        {
                            if (tree.Children[c].Rate >= newRate)
                            {
                                newRate = tree.Children[c].Rate;
                            }
                            else
                            {
                                tree.Children.Remove(tree.Children[c]);
                                c--;
                            }
                        }
                        tree.Rate = newRate;
                    }
                    else if (tree.CurrentDepth % 2 == 1)
                    {
                        int newRate = 50;

                        for (int c = 0; c < tree.Children.Count(); c++)
                        {
                            if (tree.Children[c].Rate <= newRate)
                            {
                                newRate = tree.Children[c].Rate;
                            }
                            else
                            {
                                tree.Children.Remove(tree.Children[c]);
                                c--;
                            }
                        }

                        tree.Rate = newRate;
                    }
                }

            }
            else
            {
                tree.Rate = RateBoard(tree.BoardState);
            }
        }
    }
}
