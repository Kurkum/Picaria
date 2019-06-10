import { Position, PositionStatus } from './position';
import axios from 'axios';

const colors = {
	PlayerOne: 0x00ff00,
	PlayerTwo: 0xff0000,
	FreeToCapture: 0x0000ff
};

const gameStates = {
	PawnSetup: 'pawnsetup',
	PawnMovement: 'pawnmovement'
};

const players = {
	PlayerOne: 'playerone',
	PlayerTwo: 'playertwo'
};

const moveStates = {
	PickingPawn: 'pickingpawn',
	MovingPawn: 'movingpawn'
};

const XToLogicPosition = {
	50: 0,
	400: 1,
	750: 2
};

const XFromLogicPosition = {
	0: 50,
	1: 400,
	2: 750
};

const YToLogicPosition = {
	50: 0,
	300: 1,
	550: 2
};

const YFromLogicPosition = {
	0: 50,
	300: 1,
	550: 2
};

export class PicariaEngine {
	constructor() {
		this.GameState = gameStates.PawnSetup;
		this.CurrentMove = players.PlayerOne;
		this.MoveStatus = moveStates.PickingPawn;
	}

	getPossibleMovesOfPaw = (board, pawnPosition) => {
		var ret = [];
		board.forEach(e => {
			if (e.status == PositionStatus.FreeToCapture && (e.x == pawnPosition.x || e.x == pawnPosition.x - 1 || e.x == pawnPosition.x + 1) &&
				(e.y == pawnPosition.y || e.y == pawnPosition.y - 1 || e.y == pawnPosition.y + 1)) {
				ret.push(e);
			}
		});

		return ret;
	}


	Positions = [
		new Position(50, 50, PositionStatus.FreeToCapture),
		new Position(400, 50, PositionStatus.FreeToCapture),
		new Position(750, 50, PositionStatus.FreeToCapture),
		new Position(50, 300, PositionStatus.FreeToCapture),
		new Position(400, 300, PositionStatus.FreeToCapture),
		new Position(750, 300, PositionStatus.FreeToCapture),
		new Position(50, 550, PositionStatus.FreeToCapture),
		new Position(400, 550, PositionStatus.FreeToCapture),
		new Position(750, 550, PositionStatus.FreeToCapture)
	];

	setDots(dots) {
		this.dots = dots;
	}

	PawnClick = (e) => {
		if (this.CurrentMove == players.PlayerTwo) {
			return;
		}

		var dot = e.currentTarget;
		var validMoveCompleted = false;
		if (this.GameState == gameStates.PawnSetup) {
			if (dot.pos.status == PositionStatus.FreeToCapture) {
				dot.pos.status = PositionStatus.PlayerOne;
				validMoveCompleted = true;
				this.drawCircle(dot, colors.PlayerOne);
			}
		} else {
			if (dot.pos.status == PositionStatus.PlayerOne) {
				this.selectedPawn = dot;
			}
			if (dot.pos.status == PositionStatus.FreeToCapture && this.selectedPawn != null) {
				if (this.isMoveValid(this.selectedPawn.pos, dot.pos)) {
					this.selectedPawn.pos.status = PositionStatus.FreeToCapture;
					dot.pos.status = PositionStatus.PlayerOne;
					validMoveCompleted = true;
				}
			}
		}

		if (validMoveCompleted) {
			this.CurrentMove = players.PlayerTwo;
			this.GetAiMove();
		}
	};

	isMoveValid(fromPosition, toPosition) {
		var mappedBoard = this.dots.map(function (el) {
			return {
				status: el.pos.status,
				x: XToLogicPosition[el.pos.x],
				y: YToLogicPosition[el.pos.y]
			}
		});

		var possibleMoves = this.getPossibleMovesOfPaw(mappedBoard, {
			status: fromPosition.status,
			x: XToLogicPosition[fromPosition.x],
			y: YToLogicPosition[fromPosition.y]
		})

		var isMoveValid = possibleMoves.filter(pos => pos.x === XToLogicPosition[toPosition.x] && pos.y === YToLogicPosition[toPosition.y])

		return isMoveValid.length === 1;
	}


	drawCircle = (dot, color) => {
		dot.clear();
		dot.beginFill(color, 1);
		dot.drawCircle(dot.pos.x, dot.pos.y, 8);
		dot.endFill();
	};

	GetAiMove = () => {
		var headers = {
			'Content-Type': 'application/json'
		};
		axios
			.post('https://localhost:5001/api/Picaria/Move', JSON.stringify(this.Positions), { headers: headers })
			.then((response) => {
				response.data.forEach((position) => {
					var dot = this.dots.find((dot) => dot.pos.x == position.x && dot.pos.y == position.y);
					dot.pos.status =
						position.status == 0
							? PositionStatus.PlayerOne
							: position.status == 1 ? PositionStatus.PlayerTwo : PositionStatus.FreeToCapture;

					if (position.status != 2) {
						var color =
							position.status == 0
								? colors.PlayerOne
								: position.status == 1 ? colors.PlayerTwo : colors.FreeToCapture;

						this.drawCircle(dot, color);
					} else {
						dot.clear();
					}
				});

				if (this.Positions.filter((x) => x.status == PositionStatus.PlayerOne).length === 3) {
					this.GameState = gameStates.PawnMovement;
				}
				this.CurrentMove = players.PlayerOne;
			});
	};
}
