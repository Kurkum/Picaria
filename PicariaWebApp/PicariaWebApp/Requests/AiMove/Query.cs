using MediatR;
using PicariaWebApp.Models;
using PicariaWebApp.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PicariaWebApp.Requests.AiMove
{
    public class Query : IRequest<List<Position>>
    {
        public List<Position> Board { get; set; }
    }
    public class Handler : IRequestHandler<Query, List<Position>>
    {
        public async Task<List<Position>> Handle(Query request, CancellationToken cancellationToken)
        {

//Jak zadziała, to weź to tutaj w jakiegoś if-a, np. zliczającego puste pola w request i jeśli >3 to sprawdź to niżej dla początku rozgrywki
            List<Position> firstMove = Board.GetEmptyBoard();
            for (int i = 0; i < 9; i++)
            {
                firstMove[i].Status = Status.FreeToCapture;
            }
            firstMove[4].Status = Status.PlayerOne;


            var areEqual = true;
            for(var i = 0; i < request.Board.Count; i++)
            {
                if (firstMove[i].Status != request.Board[i].Status)
                {
                    areEqual = false;
                }
            }

            if (areEqual)
            {
                //Console.WriteLine("\n\n\n\n\n\n\n\nWESZŁO\n\n\n\n\n\n\n\n");
                firstMove[0].Status = Status.PlayerTwo;
                return firstMove;
            }/**/


            firstMove[0].Status = Status.PlayerTwo;
            firstMove[8].Status = Status.PlayerOne;

            areEqual = true;


            for (var i = 0; i < request.Board.Count; i++)
            {
                if (firstMove[i].Status != request.Board[i].Status)
                {
                    areEqual = false;
                }
            }

            if (areEqual)
            {
                //Console.WriteLine("\n\n\n\n\n\n\n\nWESZŁO\n\n\n\n\n\n\n\n");
                firstMove[5].Status = Status.PlayerTwo;
                return firstMove;
            }/**/





            Player.GameTree gameTree = new Player.GameTree(new Board(request.Board), Status.PlayerTwo, 4, 0);
            gameTree.Expand();

            Player.Rating rating = new Player.Rating();
            rating.AlfaBeta(gameTree);

            Player.GameTree bestChildren = gameTree.Children[0];


            for (int c = 0; c < gameTree.Children.Count(); c++)
            {
                if (gameTree.Children[c].Rate > bestChildren.Rate)
                {
                    bestChildren = gameTree.Children[c];
                }
                else if(gameTree.Children[c].Rate == bestChildren.Rate && rating.RateBoard(gameTree.Children[c].BoardState) > rating.RateBoard(bestChildren.BoardState))
                {
                    bestChildren = gameTree.Children[c];
                }
            }

            return bestChildren.BoardState.Positions;





            /*

            SimpleArtificialIntelligence intelligence = new SimpleArtificialIntelligence(Status.PlayerTwo);
            Board gotBoard = new Board(request.Board);
            Board board = intelligence.GetBoardWithDecisonExecuted(gotBoard);
            Console.WriteLine(board);
            foreach (Position position in board.Positions)
            {
                position.TranslateToInnerSystem();
            }
            Console.WriteLine(board);
            request.Board = board.Positions;

            return request.Board;
            /**/
        }

        private void SettingUpPawns(List<Position> board)
        {
            board.FirstOrDefault(x => x.Status == Status.FreeToCapture).Status = Status.PlayerTwo;
        }

        private void MovingPawns(List<Position> board)
        {
            var newPosition = board.FirstOrDefault(x => x.Status == Status.FreeToCapture);
            board.FirstOrDefault(x => x.Status == Status.PlayerTwo).Status = Status.FreeToCapture;
            newPosition.Status = Status.PlayerTwo;
        }

    }
}
