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
        //powinno działać, gdy mniej niż 3 pionki - poza pierwszym ruchem chyba i tylko gdy już po 3 pionki!
        public int RateBoard(Board board)
        {  // lepszą techniką będzie tylko liczyć liczbę pól, na które mamy dostęp - bo często te "możliwości ruchu" poszczególnych pionków się pokrywają i niwelują swoją teoretyczną przewagę
            //TEN PROBLEM ZAŁATWI ALFABETA: słaby punkt: patrz droga twoich testów, taki trochę niesprawiedliwy kill komputera
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

                //if przegrana - kolejność tych ifów z returnami bardzo ważna - mówi o hierarchii
                //podwójny if - żeby nie próbował odpalić tego drugiego warunku?
                if (player.Count() == 3)
                {//one zawsze będą wystarczająco po kolei
                    if (player[0].X - player[1].X == player[1].X - player[2].X &&
                                   player[0].Y - player[1].Y == player[1].Y - player[2].Y)
                    {//warunek linii
                        return -50;
                    }
                }

                //if wygrana
                if (computer.Count() == 3)
                {
                    if (computer[0].X - computer[1].X == computer[1].X - computer[2].X &&
                                   computer[0].Y - computer[1].Y == computer[1].Y - computer[2].Y)
                    {
                        return 50;
                    }
                }

                int anotherResult = 0;

                //podliczanie pustej wartości pojedynczych pól
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
            return -90;//błąd -> podana tablica jest za mała, najlepiej jej nie używać, najniższa ocena i najlepiej wyrzuć gdzieś exception
        }



        public void AlfaBeta(GameTree tree)
        {

            /*dla każdego zrób:
                jeśli wygrana, oceń na 2 i wyczyść "dzieci"
                jeśli ma dzieci, wykonaj dla każdego, potem dobierz swoją ocenę
                jeśli nie ma, oceń boardRatem*/

            int howMany = 0;
            if (!(tree.Children is null))
            {
                howMany = tree.Children.Count();//on jest nulem!!!!! jeśli ostatnie pokolenie to jest nulem!
            }                                   //albo ustawiać na puste, albo sprawdzać czy null
            if (howMany > 0)
            {

                //jeśli jest zwycięzcą, nadaj ocenę i wyczyść dzieci
                if (tree.CurrentDepth % 2 == 1 && RateBoard(tree.BoardState) == 50 && tree.CurrentDepth!=0)
                {//ten kod i tak musiałby być wykonany w znacznej większości
                    tree.Rate = 50;
                    tree.Children.Clear();//ODCIĘCIE
                }

                else if (tree.CurrentDepth % 2 == 0 && RateBoard(tree.BoardState) == -50 && tree.CurrentDepth != 0)
                {//ten kod i tak musiałby być wykonany w znacznej większości
                    tree.Rate = -50;
                    tree.Children.Clear();//ODCIĘCIE
                }


                //jeśli ma dzieci, wykonaj dla każdego, potem dobierz swoją ocenę (wtedy już dzieci miały oceny)
                else
                {



                    for (int c = 0; c < howMany; c++)
                    {

                        AlfaBeta(tree.Children[c]);
                        //Console.WriteLine("\n\n\n\n\n\n\n\nn\n\n\n\n" + c + "\n\n\n\n\n\n\n\n\n\n");
                    }




                    //dobierz swoją ocenę
                    if (tree.CurrentDepth % 2 == 0)
                    {//pierwszy ruch mój, więc wybieram najlepsze dziecko
                        int newRate = -50;//początkowo najniższa ocena



                        for (int c = 0; c < tree.Children.Count(); c++)
                        {
                            if (tree.Children[c].Rate > newRate)
                            {
                                newRate = tree.Children[c].Rate;
                            }
                        }



                        tree.Rate = newRate;
                    }
                    else if (tree.CurrentDepth % 2 == 1)
                    {//pierwszy ruch wroga, więc najgorsze dziecko
                        int newRate = 50;//początkowo najwyższa ocena



                        for (int c = 0; c < tree.Children.Count(); c++)
                        {
                            if (tree.Children[c].Rate < newRate)
                            {
                                newRate = tree.Children[c].Rate;
                            }
                        }



                        tree.Rate = newRate;
                    }
                }

            }
            //jeśli nie ma dzieci, oceń BoardRatem
            else
            {
                tree.Rate = RateBoard(tree.BoardState);
            }
        }
    }
}//https://www.youtube.com/watch?v=wXv2uACAAnM