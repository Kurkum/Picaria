using PicariaWebApp.Models;
using PicariaWebApp.Player;
using System;
using UnitTests.Builders;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class RatingTests
    {
        [Fact]
        public void Test01()
        {
            List<Position> Positions = new List<Position> {
                new Position(0, 0, Status.PlayerOne),
                new Position(0, 1, Status.PlayerOne),
                new Position(0, 2, Status.PlayerOne),
                new Position(1, 0, Status.FreeToCapture),
                new Position(1, 1, Status.PlayerTwo),
                new Position(1, 2, Status.FreeToCapture),
                new Position(2, 0, Status.PlayerTwo),
                new Position(2, 1, Status.FreeToCapture),
                new Position(2, 2, Status.PlayerTwo)
            };

            var board = new BoardBuilder()  // BoardBuilder ma za zadanie tworzyæ pust¹ planszê, ustawiæ swoje pola musisz manualnie
                .WithPositions()
                .Build();

            board.Positions.Clear();
            Rating rating = new Rating();
            Console.WriteLine(rating.RateBoard(board));

            Assert.True((rating.RateBoard(board) == -90));
        }

        [Fact]
        public void Test02()
        {
            List<Position> Positions = new List<Position> {
                new Position(0, 0, Status.PlayerOne),
                new Position(0, 1, Status.PlayerOne),
                new Position(0, 2, Status.PlayerOne),
                new Position(1, 0, Status.FreeToCapture),
                new Position(1, 1, Status.PlayerTwo),
                new Position(1, 2, Status.FreeToCapture),
                new Position(2, 0, Status.PlayerTwo),
                new Position(2, 1, Status.FreeToCapture),
                new Position(2, 2, Status.PlayerTwo)
            };

            var board = new BoardBuilder()  // BoardBuilder ma za zadanie tworzyæ pust¹ planszê, ustawiæ swoje pola musisz manualnie
                .WithPositions()
                .Build();

            for (int c = 0; c < 9; c++)
            {
                board.Positions[c] = Positions[c];
            }
            Rating rating = new Rating();
            Console.WriteLine(rating.RateBoard(board));

            Assert.True((rating.RateBoard(board) == -50));  // PC: Jestem numerem 2
        }

        [Fact]
        public void Test03()
        {
            List<Position> Positions = new List<Position> {
                new Position(0, 0, Status.PlayerTwo),  // 0
                new Position(0, 1, Status.PlayerOne),  // 1
                new Position(0, 2, Status.PlayerOne),  // 2
                new Position(1, 0, Status.FreeToCapture),  // 3
                new Position(1, 1, Status.PlayerTwo),  // 4
                new Position(1, 2, Status.FreeToCapture),  // 5
                new Position(2, 0, Status.PlayerOne),  // 6
                new Position(2, 1, Status.FreeToCapture),  // 7
                new Position(2, 2, Status.PlayerTwo)  // 8
            };

            var board = new BoardBuilder()  // BoardBuilder ma za zadanie tworzyæ pust¹ planszê, ustawiæ swoje pola musisz manualnie
                .WithPositions()
                .Build();

            for (int c = 0; c < 9; c++)
            {
                board.Positions[c] = Positions[c];
            }
            Rating rating = new Rating();
            Console.WriteLine(rating.RateBoard(board));

            Assert.True((rating.RateBoard(board) == 50));  // PC: Jestem numerem 2
        }

        [Fact]
        public void Test04()
        {
            List<Position> Positions = new List<Position> {
                new Position(0, 0, Status.FreeToCapture),  // 0 skos
                new Position(0, 1, Status.FreeToCapture),  // 1 bok
                new Position(0, 2, Status.FreeToCapture),  // 2 skos
                new Position(1, 0, Status.FreeToCapture),  // 3 bok
                new Position(1, 1, Status.FreeToCapture),  // 4 œrodek
                new Position(1, 2, Status.FreeToCapture),  // 5 bok
                new Position(2, 0, Status.FreeToCapture),  // 6 skos
                new Position(2, 1, Status.FreeToCapture),  // 7 bok
                new Position(2, 2, Status.FreeToCapture)  // 8 skos
            };

            var board = new BoardBuilder()  // BoardBuilder ma za zadanie tworzyæ pust¹ planszê, ustawiæ swoje pola musisz manualnie
                .WithPositions()
                .Build();

            for (int c = 0; c < 9; c++)
            {
                board.Positions[c] = Positions[c];
            }

            Rating rating = new Rating();

            Assert.True((rating.RateBoard(board) == 0));
        }

        [Fact]
        public void Test05()
        {
            List<Position> Positions = new List<Position> {
                new Position(0, 0, Status.FreeToCapture),  // 0 skos
                new Position(0, 1, Status.FreeToCapture),  // 1 bok
                new Position(0, 2, Status.FreeToCapture),  // 2 skos
                new Position(1, 0, Status.FreeToCapture),  // 3 bok
                new Position(1, 1, Status.PlayerOne),  // 4 œrodek
                new Position(1, 2, Status.FreeToCapture),  // 5 bok
                new Position(2, 0, Status.FreeToCapture),  // 6 skos
                new Position(2, 1, Status.FreeToCapture),  // 7 bok
                new Position(2, 2, Status.FreeToCapture)  // 8 skos
            };

            var board = new BoardBuilder()  // BoardBuilder ma za zadanie tworzyæ pust¹ planszê, ustawiæ swoje pola musisz manualnie
                .WithPositions()
                .Build();

            for (int c = 0; c < 9; c++)
            {
                board.Positions[c] = Positions[c];
            }

            Rating rating = new Rating();
            Console.WriteLine(rating.RateBoard(board));

            Assert.True((rating.RateBoard(board) == -8));
        }

        [Fact]
        public void Test06()
        {
            List<Position> Positions = new List<Position> {
                new Position(0, 0, Status.FreeToCapture),  // 0 skos
                new Position(0, 1, Status.PlayerTwo),  // 1 bok
                new Position(0, 2, Status.FreeToCapture),  // 2 skos
                new Position(1, 0, Status.FreeToCapture),  // 3 bok
                new Position(1, 1, Status.PlayerOne),  // 4 œrodek
                new Position(1, 2, Status.FreeToCapture),  // 5 bok
                new Position(2, 0, Status.FreeToCapture),  // 6 skos
                new Position(2, 1, Status.FreeToCapture),  // 7 bok
                new Position(2, 2, Status.FreeToCapture)  // 8 skos
            };

            var board = new BoardBuilder()  // BoardBuilder ma za zadanie tworzyæ pust¹ planszê, ustawiæ swoje pola musisz manualnie
                .WithPositions()
                .Build();

            for (int c = 0; c < 9; c++)
            {
                board.Positions[c] = Positions[c];
            }

            Rating rating = new Rating();
            Console.WriteLine(rating.RateBoard(board));

            Assert.True((rating.RateBoard(board) == -3));
        }

        [Fact]
        public void Test07()
        {
            List<Position> Positions = new List<Position> {
                new Position(0, 0, Status.FreeToCapture),  // 0 skos
                new Position(0, 1, Status.PlayerTwo),  // 1 bok
                new Position(0, 2, Status.FreeToCapture),  // 2 skos
                new Position(1, 0, Status.FreeToCapture),  // 3 bok
                new Position(1, 1, Status.PlayerOne),  // 4 œrodek
                new Position(1, 2, Status.FreeToCapture),  // 5 bok
                new Position(2, 0, Status.PlayerOne),  // 6 skos
                new Position(2, 1, Status.FreeToCapture),  // 7 bok
                new Position(2, 2, Status.FreeToCapture)  // 8 skos
            };

            var board = new BoardBuilder()  // BoardBuilder ma za zadanie tworzyæ pust¹ planszê, ustawiæ swoje pola musisz manualnie
                .WithPositions()
                .Build();

            for (int c = 0; c < 9; c++)
            {
                board.Positions[c] = Positions[c];
            }

            Rating rating = new Rating();
            Console.WriteLine(rating.RateBoard(board));

            Assert.True((rating.RateBoard(board) == -6));
        }

        [Fact]
        public void Test08()
        {
            List<Position> Positions = new List<Position> {
                new Position(0, 0, Status.FreeToCapture),  // 0 skos
                new Position(0, 1, Status.PlayerTwo),  // 1 bok
                new Position(0, 2, Status.PlayerTwo),  // 2 skos
                new Position(1, 0, Status.FreeToCapture),  // 3 bok
                new Position(1, 1, Status.PlayerOne),  // 4 œrodek
                new Position(1, 2, Status.FreeToCapture),  // 5 bok
                new Position(2, 0, Status.PlayerOne),  // 6 skos
                new Position(2, 1, Status.FreeToCapture),  // 7 bok
                new Position(2, 2, Status.FreeToCapture)  // 8 skos
            };

            var board = new BoardBuilder()  // BoardBuilder ma za zadanie tworzyæ pust¹ planszê, ustawiæ swoje pola musisz manualnie
                .WithPositions()
                .Build();

            for (int c = 0; c < 9; c++)
            {
                board.Positions[c] = Positions[c];
            }

            Rating rating = new Rating();
            Console.WriteLine(rating.RateBoard(board));

            Assert.True((rating.RateBoard(board) == -3));
        }

        [Fact]
        public void Test09()
        {
            List<Position> Positions = new List<Position> {
                new Position(0, 0, Status.PlayerOne),  // 0 skos
                new Position(0, 1, Status.PlayerTwo),  // 1 bok
                new Position(0, 2, Status.PlayerTwo),  // 2 skos
                new Position(1, 0, Status.FreeToCapture),  // 3 bok
                new Position(1, 1, Status.PlayerOne),  // 4 œrodek
                new Position(1, 2, Status.FreeToCapture),  // 5 bok
                new Position(2, 0, Status.PlayerOne),  // 6 skos
                new Position(2, 1, Status.FreeToCapture),  // 7 bok
                new Position(2, 2, Status.FreeToCapture)  // 8 skos
            };

            var board = new BoardBuilder()  // BoardBuilder ma za zadanie tworzyæ pust¹ planszê, ustawiæ swoje pola musisz manualnie
                .WithPositions()
                .Build();

            for (int c = 0; c < 9; c++)
            {
                board.Positions[c] = Positions[c];
            }

            Rating rating = new Rating();
            Console.WriteLine(rating.RateBoard(board));

            Assert.True((rating.RateBoard(board) == -6));
        }

        [Fact]
        public void Test10()
        {
            List<Position> Positions = new List<Position> {
                new Position(0, 0, Status.PlayerOne),  // 0 skos
                new Position(0, 1, Status.PlayerTwo),  // 1 bok
                new Position(0, 2, Status.PlayerTwo),  // 2 skos
                new Position(1, 0, Status.PlayerTwo),  // 3 bok
                new Position(1, 1, Status.PlayerOne),  // 4 œrodek
                new Position(1, 2, Status.FreeToCapture),  // 5 bok
                new Position(2, 0, Status.PlayerOne),  // 6 skos
                new Position(2, 1, Status.FreeToCapture),  // 7 bok
                new Position(2, 2, Status.FreeToCapture)  // 8 skos
            };

            var board = new BoardBuilder()  // BoardBuilder ma za zadanie tworzyæ pust¹ planszê, ustawiæ swoje pola musisz manualnie
                .WithPositions()
                .Build();

            for (int c = 0; c < 9; c++)
            {
                board.Positions[c] = Positions[c];
            }

            Rating rating = new Rating();
            Console.WriteLine(rating.RateBoard(board));

            Assert.True((rating.RateBoard(board) == -1));
        }

        [Fact]
        public void Test11()
        {
            List<Position> Positions = new List<Position> {
                new Position(0, 0, Status.PlayerOne),  // 0 skos
                new Position(0, 1, Status.PlayerTwo),  // 1 bok
                new Position(0, 2, Status.PlayerTwo),  // 2 skos
                new Position(1, 0, Status.PlayerTwo),  // 3 bok
                new Position(1, 1, Status.PlayerOne),  // 4 œrodek
                new Position(1, 2, Status.FreeToCapture),  // 5 bok
                new Position(2, 0, Status.FreeToCapture),  // 6 skos
                new Position(2, 1, Status.PlayerOne),  // 7 bok
                new Position(2, 2, Status.FreeToCapture)  // 8 skos
            };

            var board = new BoardBuilder()  // BoardBuilder ma za zadanie tworzyæ pust¹ planszê, ustawiæ swoje pola musisz manualnie
                .WithPositions()
                .Build();

            for (int c = 0; c < 9; c++)
            {
                board.Positions[c] = Positions[c];
            }

            Rating rating = new Rating();
            Console.WriteLine(rating.RateBoard(board));

            Assert.True((rating.RateBoard(board) == -3));
        }

        [Fact]
        public void Test12()
        {
            List<Position> Positions = new List<Position> {
                new Position(0, 0, Status.PlayerOne),  // 0 skos
                new Position(0, 1, Status.PlayerTwo),  // 1 bok
                new Position(0, 2, Status.FreeToCapture),  // 2 skos
                new Position(1, 0, Status.PlayerTwo),  // 3 bok
                new Position(1, 1, Status.PlayerOne),  // 4 œrodek
                new Position(1, 2, Status.PlayerTwo),  // 5 bok
                new Position(2, 0, Status.FreeToCapture),  // 6 skos
                new Position(2, 1, Status.PlayerOne),  // 7 bok
                new Position(2, 2, Status.FreeToCapture)  // 8 skos
            };

            var board = new BoardBuilder()  // BoardBuilder ma za zadanie tworzyæ pust¹ planszê, ustawiæ swoje pola musisz manualnie
                .WithPositions()
                .Build();

            for (int c = 0; c < 9; c++)
            {
                board.Positions[c] = Positions[c];
            }

            Rating rating = new Rating();
            Console.WriteLine(rating.RateBoard(board));

            Assert.True((rating.RateBoard(board) == -1));
        }

        [Fact]
        public void Test13()
        {
            List<Position> Positions = new List<Position> {
                new Position(0, 0, Status.PlayerOne),  // 0 skos
                new Position(0, 1, Status.PlayerTwo),  // 1 bok
                new Position(0, 2, Status.FreeToCapture),  // 2 skos
                new Position(1, 0, Status.PlayerTwo),  // 3 bok
                new Position(1, 1, Status.PlayerOne),  // 4 œrodek
                new Position(1, 2, Status.PlayerTwo),  // 5 bok
                new Position(2, 0, Status.FreeToCapture),  // 6 skos
                new Position(2, 1, Status.FreeToCapture),  // 7 bok
                new Position(2, 2, Status.PlayerOne)  // 8 skos
            };

            var board = new BoardBuilder()  // BoardBuilder ma za zadanie tworzyæ pust¹ planszê, ustawiæ swoje pola musisz manualnie
                .WithPositions()
                .Build();

            for (int c = 0; c < 9; c++)
            {
                board.Positions[c] = Positions[c];
            }

            Rating rating = new Rating();
            Console.WriteLine(rating.RateBoard(board));

            Assert.True((rating.RateBoard(board) == -50));
        }
    }
}
