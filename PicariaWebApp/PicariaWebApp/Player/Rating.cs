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
        {  // teoretyczna przewaga leży w ilości możliwych do zajęcia pól możliwie wolnych w najbliższym czasie, lecz liczenie tego to jakieś szaleństwo
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

                //if przegrana 
                if (player.Count() == 3)
                {
                    if (player[0].X - player[1].X == player[1].X - player[2].X &&
                                   player[0].Y - player[1].Y == player[1].Y - player[2].Y)  // warunek przegranej linii, ich kolejność jest zawsze automatycznie poprawna
                    {
                        return -50;
                    }
                }

                //if wygrana
                if (computer.Count() == 3)
                {
                    if (computer[0].X - computer[1].X == computer[1].X - computer[2].X &&
                                   computer[0].Y - computer[1].Y == computer[1].Y - computer[2].Y)  // warunek zwycięskiej linii, ich kolejność jest zawsze automatycznie poprawna
                    {
                        return 50;
                    }
                }

                int anotherResult = 0;

                //podliczanie sumy wartości zajętych pól
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
            return -90;  // błąd -> podana tablica jest za mała. Najniższa ocena - najlepiej nie używać tablicy.
        }


        public void MiniMaks(GameTree tree)
        {
            /*dla każdego:
                jeśli wygrana lub przegrana, od razu oceń i wyczyść "dzieci" - jest to znaczący element
                jeśli ma dzieci, wykonaj dla każdego, potem dobierz swoją ocenę, tudzież usuń zbędne dzieci
                jeśli nie ma, oceń boardRatem*/

            int howMany = 0;
            if (!(tree.Children is null))  // Ostatnie pokolenie - .Children = NULL
            {
                howMany = tree.Children.Count();
            }                                   
            if (howMany > 0)
            {
                // Specjalne odcięcia pozwalają na wyeliminowanie poważnego problemu oprzez usunięcie zbędnych możliwości. Sprawdź: środek, prawy dolny, prawy górny, prawy górny w lewo
                // jeśli jest zwycięzcą, nadaj ocenę i wyczyść dzieci
                if (tree.CurrentDepth % 2 == 1 && RateBoard(tree.BoardState) == 50 && tree.CurrentDepth != 0)
                {
                    tree.Rate = 50;
                    tree.Children.Clear();  // ODCIĘCIE SPECJALNE
                }
                else if (tree.CurrentDepth % 2 == 0 && RateBoard(tree.BoardState) == -50 && tree.CurrentDepth != 0)
                {
                    tree.Rate = -50;
                    tree.Children.Clear();  // ODCIĘCIE SPECJALNE
                } 


                // jeśli ma dzieci, wykonaj dla każdego, potem dobierz swoją ocenę (wtedy już dzieci będą miały swoje oceny)
                else
                {
                    for (int c = 0; c < howMany; c++)
                    {
                        MiniMaks(tree.Children[c]);
                    }

                    // dobierz swoją ocenę
                    if (tree.CurrentDepth % 2 == 0)  // pierwszy ruch mój, więc wybieram najlepsze dziecko
                    {
                        int newRate = -50;  // początkowo najniższa ocena

                        for (int c = 0; c < tree.Children.Count(); c++)
                        {
                            if (tree.Children[c].Rate >= newRate)
                            {
                                newRate = tree.Children[c].Rate;
                            }
                            else  // równych NIE należy usuwać: 1. błąd pustej listy. 2. Mogą się jeszcze przydać np. przy sprawdzaniu, który jest szybszy
                            {
                                tree.Children.Remove(tree.Children[c]);
                                c--;
                            }
                        }
                        tree.Rate = newRate;
                    }
                    else if (tree.CurrentDepth % 2 == 1)  // pierwszy ruch wroga, więc najgorsze dziecko
                    {
                        int newRate = 50;  // początkowo najwyższa ocena

                        for (int c = 0; c < tree.Children.Count(); c++)
                        {
                            if (tree.Children[c].Rate <= newRate)
                            {
                                newRate = tree.Children[c].Rate;
                            }
                            else  // równych NIE należy usuwać: 1. błąd pustej listy. 2. Mogą się jeszcze przydać np. przy sprawdzaniu, który jest szybszy
                            {
                                tree.Children.Remove(tree.Children[c]);
                                c--;
                            }
                        }

                        tree.Rate = newRate;
                    }
                }

            }
            // jeśli nie ma dzieci, oceń BoardRatem
            else
            {
                tree.Rate = RateBoard(tree.BoardState);
            }
        }
    }
}
