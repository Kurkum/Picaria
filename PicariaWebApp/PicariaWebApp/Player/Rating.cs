using PicariaWebApp.Game;
using PicariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicariaWebApp.Player {
    public class Rating {
        //powinno działać, gdy mniej niż 3 pionki - poza pierwszym ruchem chyba i tylko gdy już po 3 pionki!
        public int RateBoard(List<Position> positions) {
            if (positions.Count() == 9) {
                List<Position> computer = new List<Position>();
                List<Position> player = new List<Position>();

                for (int c = 0; c < positions.Count(); c++) {
                    if (positions[c].Status == Status.PlayerTwo) {
                        computer.Add(positions[c]);
                    }
                    if (positions[c].Status == Status.PlayerOne) {
                        player.Add(positions[c]);
                    }
                }

                //if przegrana - kolejność tych ifów z returnami bardzo ważna - mówi o hierarchii
                //podwójny if - żeby nie próbował odpalić tego drugiego warunku?
                if (player.Count() == 3) {
                    if (player[0].X - player[1].X == player[1].X - player[2].X &&
                                   player[0].Y - player[1].Y == player[1].Y - player[2].Y) {//warunek linii
                        return -2;
                    }
                }

                //if wygrana
                if (computer.Count() == 3) {
                    if (computer[0].X - computer[1].X == computer[1].X - computer[2].X &&
                                   computer[0].Y - computer[1].Y == computer[1].Y - computer[2].Y) {
                        return 2;
                    }
                }

                //if środek
                if (positions[4].Status == Status.PlayerTwo) {
                    return 1;
                }

            }
            return -1;//absolutnie każdy inny przypadek, w założeniu: brak środka
        }



        public void AlfaBeta(GameTree tree) {

            /*dla każdego zrób:
                jeśli wygrana, oceń na 2 i wyczyść "dzieci"
                jeśli ma dzieci, wykonaj dla każdego, potem dobierz swoją ocenę
                jeśli nie ma, oceń boardRatem*/

            int howMany = tree.Children.Count();
            if (howMany > 0) {

                //jeśli jest zwycięzcą, nadaj ocenę i wyczyść dzieci
                if (tree.CurrentDepth%2==1 && RateBoard(tree.BoardState.Positions) == 2) {//ten kod i tak musiałby być wykonany w znacznej większości
                    tree.Rate = 2;
                    tree.Children.Clear();//ODCIĘCIE
                }

                //jeśli ma dzieci, wykonaj dla każdego, potem dobierz swoją ocenę (wtedy już dzieci miały oceny)
                else {
                    for (int c = 0; c < howMany; c++) {
                        AlfaBeta(tree.Children[c]);
                    }

                    //dobierz swoją ocenę
                    if (tree.CurrentDepth % 2 == 0) {//pierwszy ruch mój, więc wybieram najlepsze dziecko
                        int newRate = -2;//początkowo najniższa ocena
                        for (int c = 0; c < tree.Children.Count(); c++) {
                            if (tree.Children[c].Rate > newRate) {
                                newRate = tree.Children[c].Rate;
                            }
                        }
                        tree.Rate = newRate;
                    }
                    else if (tree.CurrentDepth % 2 == 1) {//pierwszy ruch wroga, więc najgorsze dziecko
                        int newRate = 2;//początkowo najwyższa ocena
                        for (int c = 0; c < tree.Children.Count(); c++) {
                            if (tree.Children[c].Rate < newRate) {
                                newRate = tree.Children[c].Rate;
                            }
                        }
                        tree.Rate = newRate;
                    }
                }

            }
            //jeśli nie ma dzieci, oceń BoardRatem
            else {
                tree.Rate = RateBoard(tree.BoardState.Positions);
            }
        }
    }
}