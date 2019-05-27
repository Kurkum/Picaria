using MediatR;
using PicariaWebApp.Models;
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
            if(request.Board.Count(x=>x.Status == Status.PlayerTwo) == 3)
            {
                MovingPawns(request.Board);
            }
            else
            {
                SettingUpPawns(request.Board);
            }

            return request.Board;
        }

        private void SettingUpPawns(List<Position> board)
        {
            Player.GameTree gameTree = new Player.GameTree(Board.GetBasicBoard(), Status.PlayerOne, 3, 0);
            gameTree.Expand();
            Player.Rating rating = new Player.Rating();
            rating.AlfaBeta(gameTree);

            Player.GameTree bestChildren = gameTree.Children[0];

            for (int c = 0; c < gameTree.Children.Count(); c++) {
                if (gameTree.Children[c].Rate > bestChildren.Rate) {
                    bestChildren = gameTree.Children[c];
                }
            }
            board = bestChildren.BoardState.Positions;



            //board.FirstOrDefault(x => x.Status == Status.FreeToCapture).Status = Status.PlayerTwo;
        }

        private void MovingPawns(List<Position> board)
        {
            /*
             Utwórz drzewo Przemka od "board" podanego w argumentach tej tu funkcji
             Oceń je
             Zadeklaruj zmienną board i ustaw ją jako pierwszy element drugiego (od góry) piętra drzewa
             przeszukaj to piętro drzewa i każdy fragment o wyższym wyniku wstaw do zmiennej newBoard
             board z tej tu funkcji = uzyskany board
             
             */
            
            
            Player.GameTree gameTree = new Player.GameTree(Board.GetBasicBoard(), Status.PlayerOne, 3, 0);
            gameTree.Expand();
            Player.Rating rating = new Player.Rating();
            rating.AlfaBeta(gameTree);

            Player.GameTree bestChildren = gameTree.Children[0];
            
            for(int c = 0; c < gameTree.Children.Count(); c++) {
                if (gameTree.Children[c].Rate > bestChildren.Rate) {
                    bestChildren = gameTree.Children[c];
                }
            }
            board = bestChildren.BoardState.Positions;
            
            /*
            var newPosition = board.FirstOrDefault(x => x.Status == Status.FreeToCapture); // bierze pierwsze pole wolne
            board.FirstOrDefault(x => x.Status == Status.PlayerTwo).Status = Status.FreeToCapture; // pierwsze pole gracza drugiego ustawia na wolne
            newPosition.Status = Status.PlayerTwo; // ustawia wzięte wolne pole na stan gracza drugiego*/
        }

    }
}
